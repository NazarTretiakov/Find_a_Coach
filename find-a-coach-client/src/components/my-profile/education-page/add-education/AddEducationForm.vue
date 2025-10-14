<template>
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="add-education">
    <ul class="add-education-items">
      <li class="add-education-items_header">
        <h1 class="add-education-items_header-element">Add education</h1>
      </li>

      <li class="add-education-items_school-input">
        <input-field
          label="School"
          name="school-name"
          type="text"
          v-model="formData.schoolName"
        />
        <span v-if="getError('schoolName')" class="error-message">{{ getError('schoolName') }}</span>
      </li>

      <li class="add-education-items_degree-dropdown">
        <dropdown-menu
          label="Degree"
          name="degree"
          v-model="formData.degree"
          :options="degreeOptions"
        />
        <span v-if="getError('degree')" class="error-message">{{ getError('degree') }}</span>
      </li>

      <li class="add-education-items_field-of-study-input">
        <input-field
          label="Field of study"
          name="field-of-study"
          type="text"
          v-model="formData.fieldOfStudy"
        />
        <span v-if="getError('fieldOfStudy')" class="error-message">{{ getError('fieldOfStudy') }}</span>
      </li>

      <li class="add-education-items_location-input">
        <input-field
          label="Location"
          name="location"
          type="text"
          v-model="formData.location"
        />
        <span v-if="getError('location')" class="error-message">{{ getError('location') }}</span>
      </li>

      <li class="add-education-items_start-date-input">
        <input-field
          label="Start date"
          name="start-date"
          type="date"
          v-model="formData.startDate"
        />
        <span v-if="getError('startDate')" class="error-message">{{ getError('startDate') }}</span>
      </li>

      <li class="add-education-items_end-date-input">
        <input-field
          label="End date"
          name="end-date"
          type="date"
          v-model="formData.endDate"
        />
        <span v-if="getError('endDate')" class="error-message">{{ getError('endDate') }}</span>
      </li>

      <li class="add-education-items_skills-header">
        <h1 class="add-education-items_skills-header-element">Add skills</h1>
      </li>

      <li class="add-education-items_skills">
        <ul class="add-education-items_skills-items">
          <li
            v-for="(skill, index) in formData.skills"
            :key="index"
            class="add-education-items_skills-items_skill"
          >
            <input-field
              v-model="formData.skills[index]"
              class="add-education-items_skills-items_skill-name"
              label="Skill name"
              :name="'skill-name-' + index"
              type="text"
            />
            <span v-if="getError(`skills[${index}]`)" class="error-message">{{ getError(`skills[${index}]`) }}</span>
            <remove-button
              @click="removeSkill(index)"
              class="add-education-items_skills-items_skill-remove-button"
            ></remove-button>
          </li>
        </ul>
      </li>

      <li v-if="isAddSkillButtonVisible" class="add-education-items_add-button">
        <add-button @click="addSkill" added-object-name="skill"></add-button>
      </li>

      <li class="add-education-items_save-button">
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
import RemoveButton from '../../../input-fields/RemoveButton.vue'
import AddButton from '../../../input-fields/AddButton.vue'
import SaveButton from '../../../input-fields/SaveButton.vue'
import LoadingSquare from '../../../LoadingSquare.vue'

import type { ValidationError } from '@/types/ValidationError'
import type { EducationForm } from '@/types/my-profile/education/EducationForm'

import useValidationOfAddEducationForm from '@/composables/my-profile/education/useValidationOfAddEducationForm'
import useAddSchool from '@/composables/my-profile/education/useAddSchool'

export default defineComponent({
  components: {
    InputField,
    DropdownMenu,
    RemoveButton,
    AddButton,
    SaveButton,
    LoadingSquare
  },
  setup() {
    const router = useRouter()
    const isLoading = ref<boolean>(false)

    const degreeOptions = [
      { value: 'Bachelor', label: "Bachelor's degree" },
      { value: 'Master', label: "Master's degree" },
      { value: 'Doctor', label: "Doctoral degree (PhD)" },
      { value: 'Habilitation', label: "Habilitated Doctor degree" },
      { value: 'Professor', label: "Professor title" }
    ]
    const formData = ref<EducationForm>({
      schoolName: '',
      degree: '',
      fieldOfStudy: '',
      location: '',
      startDate: null,
      endDate: null,
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
      formErrors.value = useValidationOfAddEducationForm(formData.value)

      if (formErrors.value.length === 0) {
        isLoading.value = true
        console.log(formData.value)
        const result = await useAddSchool(formData.value)
        isLoading.value = false

        if (result.isSuccessful) {
          router.push('/my-profile/education')
        }
      }
    }

    return {
      isLoading,
      formData,
      formErrors,
      degreeOptions,
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

.add-education {
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

    &_school-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_degree-dropdown {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_field-of-study-input {
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
          margin-top: 10px;

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