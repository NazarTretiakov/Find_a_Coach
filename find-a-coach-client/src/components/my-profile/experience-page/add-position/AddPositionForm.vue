<template>
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="add-position">
    <ul class="add-position-items">
      <li class="add-position-items_header">
        <h1 class="add-position-items_header-element">Add position</h1>
      </li>

      <li class="add-position-items_title-input">
        <input-field
          label="Title"
          name="title"
          type="text"
          v-model="formData.title"
        />
        <span v-if="getError('title')" class="error-message">{{ getError('title') }}</span>
      </li>

      <li class="add-position-items_employment-type-dropdown">
        <dropdown-menu
          label="Employment type"
          name="employment-type"
          v-model="formData.employmentType"
          :options="employmentTypeOptions"
        />
        <span v-if="getError('employmentType')" class="error-message">{{ getError('employmentType') }}</span>
      </li>

      <li class="add-position-items_company-input">
        <input-field
          label="Company or organization"
          name="company"
          type="text"
          v-model="formData.companyName"
        />
        <span v-if="getError('companyName')" class="error-message">{{ getError('companyName') }}</span>
      </li>

      <li class="add-position-items_is-currently-working-checkbox">
        <checkbox-field
          label="I am currently working in this role"
          name="is-currently-working"
          v-model="formData.isCurrentlyWorkingHere"
        />
      </li>

      <li class="add-position-items_start-date-input">
        <input-field
          label="Start date"
          name="start-date"
          type="date"
          v-model="formData.startDate"
        />
        <span v-if="getError('startDate')" class="error-message">{{ getError('startDate') }}</span>
      </li>

      <li class="add-position-items_end-date-input">
        <input-field
          label="End date"
          name="end-date"
          type="date"
          v-model="formData.endDate"
          :disabled="formData.isCurrentlyWorkingHere"
        />
        <span v-if="getError('endDate')" class="error-message">{{ getError('endDate') }}</span>
      </li>

      <li class="add-position-items_location-input">
        <input-field
          label="Location"
          name="location"
          type="text"
          v-model="formData.location"
        />
        <span v-if="getError('location')" class="error-message">{{ getError('location') }}</span>
      </li>

      <li class="add-position-items_description-input">
        <text-input-area
          label="Description"
          name="description"
          :max-number-of-signs="200"
          v-model="formData.description"
        />
        <span v-if="getError('description')" class="error-message">{{ getError('description') }}</span>
      </li>

      <li class="add-position-items_skills-header">
        <h1 class="add-position-items_skills-header-element">Add skills</h1>
      </li>

      <li class="add-position-items_skills">
        <ul class="add-position-items_skills-items">
          <li
            v-for="(skill, index) in formData.skills"
            :key="index"
            class="add-position-items_skills-items_skill"
          >
            <input-field
              v-model="formData.skills[index]"
              class="add-position-items_skills-items_skill-name"
              label="Skill name"
              :name="'skill-name-' + index"
              type="text"
            />
            <span v-if="getError(`skills[${index}]`)" class="error-message">{{ getError(`skills[${index}]`) }}</span>

            <remove-button
              @click="removeSkill(index)"
              class="add-position-items_skills-items_skill-remove-button"
            ></remove-button>
          </li>
        </ul>
      </li>

      <li v-if="isAddSkillButtonVisible" class="add-position-items_add-button">
        <add-button @click="addSkill" added-object-name="skill"></add-button>
      </li>

      <li class="add-position-items_save-button">
        <save-button @click="onSave"></save-button>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import { useRouter } from 'vue-router'

import InputField from "../../../input-fields/InputField.vue"
import DropdownMenu from '../../../input-fields/DropdownMenu.vue'
import TextInputArea from '../../../input-fields/TextInputArea.vue'
import CheckboxField from '../../../input-fields/CheckboxField.vue'
import RemoveButton from '../../../input-fields/RemoveButton.vue'
import AddButton from '../../../input-fields/AddButton.vue'
import SaveButton from '../../../input-fields/SaveButton.vue'
import LoadingSquare from '../../../LoadingSquare.vue'

import type { PositionForm } from '@/types/my-profile/experience/PositionForm'
import type { ValidationError } from '@/types/ValidationError'

import useValidationOfAddPositionForm from '@/composables/my-profile/experience/useValidationOfAddPositionForm'
import useAddPosition from '@/composables/my-profile/experience/useAddPosition'

export default defineComponent({
  components: {
    InputField,
    DropdownMenu,
    TextInputArea,
    CheckboxField,
    RemoveButton,
    AddButton,
    SaveButton,
    LoadingSquare
  },
  setup() {
    const router = useRouter()
    const isLoading = ref<boolean>(false)

    const employmentTypeOptions = [
      { value: 'full-time', label: 'Full-time' },
      { value: 'part-time', label: 'Part-time' },
      { value: 'self-employed', label: 'Self-employed' },
      { value: 'internship', label: 'Internship' }
    ]

    const formData = ref<PositionForm>({
      title: '',
      employmentType: '',
      companyName: '',
      isCurrentlyWorkingHere: false,
      startDate: null,
      endDate: null,
      location: '',
      description: '',
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
      formErrors.value = useValidationOfAddPositionForm(formData.value)
      if (formErrors.value.length === 0) {
        isLoading.value = true
        console.log(formData.value)
        const result = await useAddPosition(formData.value)
        isLoading.value = false

        if (result.isSuccessful) {
          router.push('/my-profile/experience')
        }
      }
    }

    return {
      isLoading,
      formData,
      formErrors,
      employmentTypeOptions,
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

.add-position {
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

    &_employment-type-dropdown {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_company-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_is-currently-working-checkbox {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_start-date-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_end-date-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_location-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_description-input {
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

            @media (max-width: $breakpoint) {
              margin-top: 14px;
            }
          }

          &-remove-button {
            margin-top: 16px;

            @media (max-width: $breakpoint) {
              margin-top: 14px;
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