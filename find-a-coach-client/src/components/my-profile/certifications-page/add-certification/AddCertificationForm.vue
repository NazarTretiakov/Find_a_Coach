<template>
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="add-certification">
    <ul class="add-certification-items">
      <li class="add-certification-items_header">
        <h1 class="add-certification-items_header-element">Add certification</h1>
      </li>

      <li class="add-certification-items_title-input">
        <input-field
          label="Title"
          name="title"
          type="text"
          v-model="formData.title"
        />
        <span v-if="getError('title')" class="error-message">{{ getError('title') }}</span>
      </li>

      <li class="add-certification-items_organization-input">
        <input-field
          label="Issuing organization"
          name="organization"
          type="text"
          v-model="formData.issuingOrganization"
        />
        <span v-if="getError('organization')" class="error-message">{{ getError('organization') }}</span>
      </li>

      <li class="add-certification-items_issue-date-input">
        <input-field
          label="Issue date"
          name="issue-date"
          type="date"
          v-model="formData.issueDate"
        />
        <span v-if="getError('issueDate')" class="error-message">{{ getError('issueDate') }}</span>
      </li>

      <li class="add-certification-items_image-input">
        <file-input-field
          label="Image"
          name="image"
          v-model="formData.image"
        />
        <span v-if="getError('image')" class="error-message">{{ getError('image') }}</span>
      </li>

      <li class="add-certification-items_credential-id-input">
        <input-field
          label="Credential ID"
          name="credential-id"
          type="text"
          v-model="formData.credentialId"
        />
        <span v-if="getError('credentialId')" class="error-message">{{ getError('credentialId') }}</span>
      </li>

      <li class="add-certification-items_credential-url-input">
        <input-field
          label="Credential URL"
          name="credential-url"
          type="text"
          v-model="formData.credentialUrl"
        />
        <span v-if="getError('credentialUrl')" class="error-message">{{ getError('credentialUrl') }}</span>
      </li>

      <li class="add-certification-items_skills-header">
        <h1 class="add-certification-items_skills-header-element">Add skills</h1>
      </li>

      <li class="add-certification-items_skills">
        <ul class="add-certification-items_skills-items">
          <li
            v-for="(skill, index) in formData.skills"
            :key="index"
            class="add-certification-items_skills-items_skill"
          >
            <input-field
              v-model="formData.skills[index]"
              class="add-certification-items_skills-items_skill-name"
              label="Skill name"
              :name="'skill-name-' + index"
              type="text"
            />
            <span v-if="getError(`skills[${index}]`)" class="error-message">{{ getError(`skills[${index}]`) }}</span>
            <remove-button
              @click="removeSkill(index)"
              class="add-certification-items_skills-items_skill-remove-button"
            />
          </li>
        </ul>
      </li>

      <li v-if="isAddSkillButtonVisible" class="add-certification-items_add-button">
        <add-button @click="addSkill" added-object-name="skill"></add-button>
      </li>

      <li class="add-certification-items_save-button">
        <save-button @click="onSave"></save-button>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import { useRouter } from 'vue-router'

import InputField from "../../../input-fields/InputField.vue"
import FileInputField from "../../../input-fields/FileInputField.vue"
import RemoveButton from "../../../input-fields/RemoveButton.vue"
import AddButton from "../../../input-fields/AddButton.vue"
import SaveButton from "../../../input-fields/SaveButton.vue"
import LoadingSquare from "../../../LoadingSquare.vue"

import type { ValidationError } from '@/types/ValidationError'
import type { CertificationForm } from '@/types/my-profile/certifications/CertificationForm'

import useValidationOfAddCertificationForm from '@/composables/my-profile/certifications/useValidationOfAddCertificationForm'
import useAddCertification from '@/composables/my-profile/certifications/useAddCertification'

export default defineComponent({
  components: {
    InputField,
    FileInputField,
    RemoveButton,
    AddButton,
    SaveButton,
    LoadingSquare
  },
  setup() {
    const router = useRouter()
    const isLoading = ref<boolean>(false)

    const formData = ref<CertificationForm>({
      title: '',
      issuingOrganization: '',
      issueDate: null,
      image: null,
      credentialId: '',
      credentialUrl: '',
      skills: []
    })

    const formErrors = ref<ValidationError[]>([])
    const isAddSkillButtonVisible = ref<boolean>(true)

    const addSkill = () => {
      if (formData.value.skills.length >= 4) {
        isAddSkillButtonVisible.value = false
      }
      formData.value.skills.push('')
    }

    const removeSkill = (index: number) => {
      if (formData.value.skills.length === 5) {
        isAddSkillButtonVisible.value = true
      }
      formData.value.skills.splice(index, 1)
    }

    const getError = (fieldName: string) => {
      return formErrors.value.find(e => e.fieldName === fieldName)?.errorMessage || ''
    }

    const onSave = async () => {
      formErrors.value = useValidationOfAddCertificationForm(formData.value)

      if (formErrors.value.length === 0) {
        isLoading.value = true
        const result = await useAddCertification(formData.value)
        isLoading.value = false

        if (result.isSuccessful) {
          router.push('/my-profile/certifications')
        }
      }
    }

    return {
      isLoading,
      formData,
      formErrors,
      addSkill,
      removeSkill,
      isAddSkillButtonVisible,
      onSave,
      getError
    }
  }
})
</script>

<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.add-certification {
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

    &_title-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_organization-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_issue-date-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_image-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_credential-id-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_credential-url-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_skills-header {
      margin-top: 60px;

      @media (max-width: $breakpoint) {
        margin-top: 40px;
      }

      &-element {
        font-size: 24px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }

    &_skills {

      &-items {
        list-style: none;
        display: flex;
        flex-direction: column;
        width: 600px;

        @media (max-width: $breakpoint) {
          width: 100%;
        }

        &_skill {
          margin-top: 30px;

          &-name {
            margin-top: 20px;
            margin-bottom: 16px;

            @media (max-width: $breakpoint) {
              margin-top: 14px;
              margin-bottom: 14px;
            }
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