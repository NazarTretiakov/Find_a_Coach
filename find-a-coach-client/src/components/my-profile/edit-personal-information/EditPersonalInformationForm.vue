<template> 
  <div class="edit-personal-information">
    <ul class="edit-personal-information-items">
      <li class="edit-personal-information-items_header">
        <h1 class="edit-personal-information-items_header-element">Edit personal information</h1>
      </li>
      <li class="edit-personal-information-items_user-profile-image">
        <img @click="triggerFileInput" class="edit-personal-information-items_user-profile-image-small" :src="profileImage" alt="Profile image small sized">
        <img @click="triggerFileInput" class="edit-personal-information-items_user-profile-image-big" :src="profileImage" alt="Profile image big sized">
        <input type="file" ref="fileInput" accept="image/*" class="edit-personal-information-items_hidden-input-for-user-profile-image" @change="onFileChange" />
      </li>
      <li class="edit-personal-information-items_first-name-input">
        <input-field label="First name" name="first-name" type="text"></input-field>
      </li>
      <li class="edit-personal-information-items_last-name-input">
        <input-field label="Last name" name="last-name" type="text"></input-field>
      </li>
      <li class="edit-personal-information-items_primary-occupation-input">
        <input-field label="Primary occupation" name="primary-occupation" type="text"></input-field>
      </li>
      <li class="edit-personal-information-items_headline-input">
        <text-input-area label="Headline" name="headline" max-number-of-signs="100"></text-input-area>
      </li>
      <li class="edit-personal-information-items_location-input">
        <input-field label="Location" name="location" type="text"></input-field>
      </li>
      <li class="edit-personal-information-items_edit-contact-information-header">
        <h1 class="edit-personal-information-items_edit-contact-information-header-element">Edit contact information</h1>
      </li>
      <li class="edit-personal-information-items_email-input">
        <input-field label="Email" name="email" type="text"></input-field>
      </li>
      <li class="edit-personal-information-items_phone-input">
        <input-field label="Phone number" name="phone-number" type="text"></input-field>
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

            <remove-button @click="removeWebsite(index)" class="edit-personal-information-items_websites-items_website-remove-button"></remove-button>
          </li>
        </ul>
      </li>
      <li v-if="isAddButtonVisible" class="edit-personal-information-items_add-button"><add-button @click="addWebsite" added-object-name="website"></add-button></li>
      <li class="edit-personal-information-items_save-button"><save-button></save-button></li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import InputField from "../../input-fields/InputField.vue";
import TextInputArea from '../../input-fields/TextInputArea.vue';
import DropdownMenu from '../../input-fields/DropdownMenu.vue';
import RemoveButton from '../../input-fields/RemoveButton.vue';
import AddButton from '../../input-fields/AddButton.vue';
import SaveButton from '../../input-fields/SaveButton.vue';
import type { Website } from '../../../types/edit-personal-information/Website'

export default defineComponent({
  components: {
    InputField,
    TextInputArea,
    DropdownMenu,
    RemoveButton,
    AddButton,
    SaveButton
  },
  setup() {
    const profileImage = ref(new URL('../../../assets/images/icons/user-icon.jpg', import.meta.url).href)
    const fileInput = ref<HTMLInputElement | null>(null)
    const websites = ref<Website[]>([])
    const isAddButtonVisible = ref<boolean>(true)

    const triggerFileInput = () => {
      fileInput.value?.click()
    }

    const onFileChange = (event: Event) => {
      const target = event.target as HTMLInputElement
      const file = target.files?.[0]
      if (file) {
        console.log('Choosed file:', file)
        const reader = new FileReader()
        reader.onload = (e) => {
          profileImage.value = e.target?.result as string
        }
        reader.readAsDataURL(file)
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

    return { profileImage, fileInput, triggerFileInput, onFileChange, websites, addWebsite, removeWebsite, isAddButtonVisible }
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
        margin-right: 40px;

        @media (max-width: $breakpoint) {
          width: 30px;
        }

        &:hover {
          cursor: pointer;
        }
      }
      &-big {
        width: 150px;

        @media (max-width: $breakpoint) {
          width: 120px;
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
      margin-top: -8px;
      padding-bottom: 70px;
      border-bottom: 1px solid $grayBorderColor;

      @media (max-width: $breakpoint) {
        padding-bottom: 50px;
      }
    }

    &_edit-contact-information-header {
      margin-top: 70px;

      @media (max-width: $breakpoint) {
        margin-top: 50px;
      }

      &-element {
        font-size: 28px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      } 
    }

    &_email-input {
      margin-top: 20px;
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
</style>