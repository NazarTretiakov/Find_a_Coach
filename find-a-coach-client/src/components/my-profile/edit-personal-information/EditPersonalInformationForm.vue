<template>
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="edit-personal-information">
    <ul class="edit-personal-information-items">
      <li class="edit-personal-information-items_header">
        <h1 class="edit-personal-information-items_header-element">Edit personal information</h1>
      </li>

      <li class="edit-personal-information-items_user-profile-image">
        <img @click="triggerFileInput" class="edit-personal-information-items_user-profile-image-small" :src="profileImagePreview" alt="Profile image small sized" />
        <img @click="triggerFileInput" class="edit-personal-information-items_user-profile-image-big" :src="profileImagePreview" alt="Profile image big sized" />
        <input type="file" ref="fileInput" accept="image/*" class="edit-personal-information-items_hidden-input-for-user-profile-image" @change="onFileChange" />
      </li>

      <li class="edit-personal-information-items_first-name-input">
        <input-field label="First name" name="first-name" type="text" v-model="formData.firstName" />
        <span v-if="getError('firstName')" class="error-message">{{ getError('firstName') }}</span>
      </li>

      <li class="edit-personal-information-items_last-name-input">
        <input-field label="Last name" name="last-name" type="text" v-model="formData.lastName" />
        <span v-if="getError('lastName')" class="error-message">{{ getError('lastName') }}</span>
      </li>

      <li class="edit-personal-information-items_primary-occupation-input">
        <input-field label="Primary occupation" name="primary-occupation" type="text" v-model="formData.primaryOccupation" />
        <span v-if="getError('primaryOccupation')" class="error-message">{{ getError('primaryOccupation') }}</span>
      </li>

      <li class="edit-personal-information-items_headline-input">
        <text-input-area label="Headline" name="headline" :max-number-of-signs="100" v-model="formData.headline" />
        <span v-if="getError('headline')" class="error-message">{{ getError('headline') }}</span>
      </li>

      <li class="edit-personal-information-items_location-input">
        <input-field label="Location" name="location" type="text" v-model="formData.location" />
        <span v-if="getError('location')" class="error-message">{{ getError('location') }}</span>
      </li>

      <li class="edit-personal-information-items_edit-contact-information-header">
        <h1 class="edit-personal-information-items_edit-contact-information-header-element">Edit contact information</h1>
      </li>

      <li class="edit-personal-information-items_phone-input">
        <input-field label="Phone number" name="phone-number" type="text" v-model="formData.phone" />
        <span v-if="getError('phone')" class="error-message">{{ getError('phone') }}</span>
      </li>

      <li class="edit-personal-information-items_websites-header">
        <h1 class="edit-personal-information-items_websites-header-element">Websites</h1>
      </li>

      <li class="edit-personal-information-items_websites">
        <ul class="edit-personal-information-items_websites-items">
          <li v-for="(website, index) in websites" :key="index" class="edit-personal-information-items_websites-items_website">
            <input-field
              class="edit-personal-information-items_websites-items_website-url-input"
              v-model="website.url"
              :label="'Website URL'"
              :name="'website-url-' + index"
              type="text"
            />

            <dropdown-menu
              class="edit-personal-information-items_websites-items_website-type-of-website-input"
              v-model="website.type"
              :label="'Choose type of website'"
              :name="'type-of-website-' + index"
              :options="[
                { value: 'company', label: 'Company' },
                { value: 'personal', label: 'Personal' },
                { value: 'portfolio', label: 'Portfolio' },
                { value: 'other', label: 'Other' }
              ]"
            />

            <span v-if="getError(`websites[${index}]`)" class="error-message">
              {{ getError(`websites[${index}]`) }}
            </span>

            <remove-button @click="removeWebsite(index)" class="edit-personal-information-items_websites-items_website-remove-button"></remove-button>
          </li>
        </ul>
      </li>

      <li v-if="isAddButtonVisible" class="edit-personal-information-items_add-button">
        <add-button @click="addWebsite" added-object-name="website"></add-button>
      </li>

      <li class="edit-personal-information-items_save-button">
        <save-button @click="onSave"></save-button>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'

import InputField from "../../input-fields/InputField.vue"
import TextInputArea from '../../input-fields/TextInputArea.vue'
import DropdownMenu from '../../input-fields/DropdownMenu.vue'
import RemoveButton from '../../input-fields/RemoveButton.vue'
import AddButton from '../../input-fields/AddButton.vue'
import SaveButton from '../../input-fields/SaveButton.vue'
import LoadingSquare from '../../LoadingSquare.vue'

import type { Form } from '@/types/my-profile/personal-information/edit-personal-information/Form'
import type { Website } from '@/types/my-profile/personal-information/edit-personal-information/Website'
import type { ValidationError } from '@/types/ValidationError'
import useValidationOfForm from '../../../composables/my-profile/personal-information/edit-personal-information/useValidationOfForm'
import useEditPersonalInformation from '../../../composables/my-profile/personal-information/edit-personal-information/useEditPersonalInformation'
import useGetPersonalAndContactInformation from "../../../composables/my-profile/personal-information/useGetPersonalAndContactInformation"
import { useRouter } from 'vue-router'
import { useAuthenticationStore } from "@/stores/authentication"

export default defineComponent({
  components: {
    InputField,
    TextInputArea,
    DropdownMenu,
    RemoveButton,
    AddButton,
    SaveButton,
    LoadingSquare
  },
  setup() {
    const router = useRouter()
    const authenticationStore = useAuthenticationStore()
    const isLoading = ref<boolean>(true)

    const profileImage = ref<File | null>(null)
    const profileImagePreview = ref<string>('')
    const fileInput = ref<HTMLInputElement | null>(null)
    const websites = ref<Website[]>([])
    const isAddButtonVisible = ref<boolean>(true)
    const formData = ref<Form>({
      profileImage: profileImage.value,
      firstName: '',
      lastName: '',
      primaryOccupation: '',
      headline: '',
      location: '',
      phone: '',
      websites: websites.value
    })
    const formErrors = ref<ValidationError[]>([])

    onMounted(async () => {
      const startTime = performance.now()

      const result = await useGetPersonalAndContactInformation(authenticationStore.userId)

      if ("isSuccessful" in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
        }
      } else {
        formData.value.firstName = result.firstName || ''
        formData.value.lastName = result.lastName || ''
        formData.value.primaryOccupation = result.primaryOccupation || ''
        formData.value.headline = result.headline || ''
        formData.value.location = result.location || ''
        formData.value.phone = result.phone || ''


        profileImagePreview.value = result.profileImageUrl

        websites.value = result.websites && Array.isArray(result.websites)
          ? result.websites
          : []
        formData.value.websites = websites.value
      }

      const elapsed = performance.now() - startTime
      const remaining = 500 - elapsed

      if (remaining > 0) {
        setTimeout(() => {
          isLoading.value = false
        }, remaining)
      } else {
        isLoading.value = false
      }
    })

    const triggerFileInput = () => {
      fileInput.value?.click()
    }

    const onFileChange = (event: Event) => {
      const target = event.target as HTMLInputElement
      const file = target.files?.[0]
      if (file) {
        profileImage.value = file
        formData.value.profileImage = file
        profileImagePreview.value = URL.createObjectURL(file)
      }
    }

    const addWebsite = () => {
      if (websites.value.length >= 4) {
        isAddButtonVisible.value = false
      }
      websites.value.push({ url: '', type: 'other' })
    }

    const removeWebsite = (index: number) => {
      if (websites.value.length == 5) {
        isAddButtonVisible.value = true
      }
      websites.value.splice(index, 1)
    }

    const onSave = async () => {
      formData.value.phone = formData.value.phone.replace(/\s+/g, '')
      formErrors.value = useValidationOfForm(formData.value)

      if (formErrors.value.length === 0) {
        let response = await useEditPersonalInformation(formData.value)

        if (response.isSuccessful) {
          router.push('/my-profile')
        }
      }
    }

    const getError = (fieldName: string) => {
      return formErrors.value.find(error => error.fieldName === fieldName)?.errorMessage || ''
    }

    return {
      formData,
      profileImage,
      profileImagePreview,
      fileInput,
      triggerFileInput,
      onFileChange,
      websites,
      addWebsite,
      removeWebsite,
      isAddButtonVisible,
      onSave,
      formErrors,
      getError,
      isLoading
    }
  }
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.edit-personal-information {
  margin: 50px 0 0 150px;

  @media (max-width: $breakpoint) {
    margin: 30px 10px 0 10px;
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;
    width: 600px;

    @media (max-width: $breakpoint) {
      width: auto;
    }

    &_header {
      &-element {
        font-size: 28px;
        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }

    &_user-profile-image {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }

      &-small {
        width: 50px;
        height: 50px;
        margin-right: 40px;
        border-radius: 50%;
        overflow: hidden;
        object-fit: cover;
        border: 1px solid #000000;

        @media (max-width: $breakpoint) {
          width: 30px;
          height: 30px;
        }

        &:hover { 
          cursor: pointer; 
        }
      }
      &-big {
        width: 150px;
        height: 150px;
        border-radius: 50%;
        overflow: hidden;
        object-fit: cover;
        border: 1px solid #000000;

        @media (max-width: $breakpoint) {
          width: 120px;
          height: 120px;
        }

        &:hover { 
          cursor: pointer; 
        }
      }
    }

    &_hidden-input-for-user-profile-image {
      display: none; 
    }

    &_first-name-input {
      margin-top: 20px; 
    }
    
    &_last-name-input { 
      margin-top: 14px; 
    
    }

    &_primary-occupation-input { 
      margin-top: 14px; 
    }

    &_headline-input {
      margin-top: 14px;
    }

    &_location-input {
      margin-top: 14px;
      padding-bottom: 70px;
      border-bottom: 1px solid $grayBorderColor;

      @media (max-width: $breakpoint) { 
        padding-bottom: 50px; 
      }
    }

    &_edit-contact-information-header { 
      margin: 56px 0 14px 0;

      @media (max-width: $breakpoint) { 
        margin: 14px 0 0 0; 
        font-size: 20px;
      }

      &-element {
        font-size: 24px;

        @media (max-width: $breakpoint) { 
          margin: 30px 0 14px 0; 
          font-size: 20px;
        }
      }
    }

    &_phone-input { 
      margin-top: 14px;
    }

    &_websites-header {
      margin-top: 40px;

      &-element {
        font-size: 24px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }
    &_websites {

      &-items {
        list-style: none;
        display: flex;
        flex-direction: column;
        width: 600px;

        @media (max-width: $breakpoint) {
          width: 100%;
        }

        &_website {
          &-url-input { 
            margin-top: 14px; 
          }
          &-type-of-website-input { 
            margin-top: 14px; 
          }
          &-remove-button { 
            margin: 24px 0 10px 0; 
          }
        }
      }
    }
    &_add-button { 
      margin-top: 30px; 
    }
    &_save-button { 
      margin: 70px 0 100px 0; 
    }
  }
}

.error-message {
  color: red;
  font-size: 14px;
  margin-top: 4px;
  display: block;

  @media (max-width: $breakpoint) {
    font-size: 12px;
  }
}
</style>