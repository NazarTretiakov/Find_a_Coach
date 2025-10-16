<template>
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="edit-project">
    <ul class="edit-project-items">
      <li class="edit-project-items_header">
        <h1 class="edit-project-items_header-element">Edit project</h1>
      </li>

      <li class="edit-project-items_name-input">
        <input-field label="Project name" name="name" type="text" v-model="formData.title" />
        <span v-if="getError('title')" class="error-message">{{ getError('title') }}</span>
      </li>

      <li class="edit-project-items_related-to-dropdown">
        <dropdown-menu
          label="Related to"
          name="related-to"
          v-model="formData.relatedTo"
          :options="relatedToOptions"
        />
        <span v-if="getError('relatedTo')" class="error-message">{{ getError('relatedTo') }}</span>
      </li>

      <li class="edit-project-items_location-input">
        <input-field label="Location" name="location" type="text" v-model="formData.location" />
        <span v-if="getError('location')" class="error-message">{{ getError('location') }}</span>
      </li>

      <li class="edit-project-items_start-date-input">
        <input-field label="Start date" name="start-date" type="date" v-model="formData.startDate" />
        <span v-if="getError('startDate')" class="error-message">{{ getError('startDate') }}</span>
      </li>

      <li class="edit-project-items_end-date-input">
        <input-field
          label="End date"
          name="end-date"
          type="date"
          v-model="formData.endDate"
          :disabled="isCurrentlyWorking"
        />
        <span v-if="getError('endDate')" class="error-message">{{ getError('endDate') }}</span>
      </li>

      <li class="edit-project-items_description-input">
        <text-input-area
          label="Description"
          name="description"
          :max-number-of-signs="500"
          v-model="formData.description"
        />
        <span v-if="getError('description')" class="error-message">{{ getError('description') }}</span>
      </li>

      <li class="edit-project-items_skills-header">
        <h1 class="edit-project-items_skills-header-element">Skills</h1>
      </li>

      <li class="edit-project-items_skills">
        <ul class="edit-project-items_skills-items">
          <li v-for="(skill, index) in formData.skillTitles" :key="index" class="edit-project-items_skills-items_skill">
            <input-field
              v-model="formData.skillTitles[index]"
              class="edit-project-items_skills-items_skill-name"
              label="Skill name"
              :name="'skill-name-' + index"
              type="text"
            />
            <remove-button @click="removeSkill(index)" class="edit-project-items_skills-items_skill-remove-button"></remove-button>
          </li>
        </ul>
      </li>

      <li v-if="isAddSkillButtonVisible" class="edit-project-items_add-button">
        <add-button @click="addSkill" added-object-name="skill"></add-button>
      </li>

      <li class="edit-project-items_people-header">
        <h1 class="edit-project-items_people-header-element">People that participated</h1>
        <span class="edit-project-items_people-header-inscription">(Except you)</span>
      </li>

      <li class="edit-project-items_people">
        <ul class="edit-project-items_people-items">
          <li v-for="(person, index) in formData.participants" :key="index" class="edit-project-items_people-items_person">
            <input-field
              v-model="formData.participants[index]"
              class="edit-project-items_people-items_person-name"
              label="Person name"
              :name="'person-name-' + index"
              type="text"
            />
            <remove-button @click="removePerson(index)" class="edit-project-items_people-items_person-remove-button"></remove-button>
          </li>
        </ul>
      </li>

      <li v-if="isAddPersonButtonVisible" class="edit-project-items_add-button">
        <add-button @click="addPerson" added-object-name="person"></add-button>
      </li>

      <li class="edit-project-items_save-button">
        <save-button @click="onSave"></save-button>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'

import InputField from '../../../input-fields/InputField.vue'
import DropdownMenu from '../../../input-fields/DropdownMenu.vue'
import TextInputArea from '../../../input-fields/TextInputArea.vue'
import RemoveButton from '../../../input-fields/RemoveButton.vue'
import AddButton from '../../../input-fields/AddButton.vue'
import SaveButton from '../../../input-fields/SaveButton.vue'
import LoadingSquare from '../../../LoadingSquare.vue'

import type { ValidationError } from '@/types/ValidationError'
import type { ProjectForm } from '@/types/my-profile/projects/ProjectForm'

import useGetProject from '@/composables/my-profile/projects/useGetProject'
import useEditProject from '@/composables/my-profile/projects/useEditProject'
import useValidationOfAddProjectForm from '@/composables/my-profile/projects/useValidationOfAddProjectForm'

export default defineComponent({
  components: { InputField, DropdownMenu, TextInputArea, RemoveButton, AddButton, SaveButton, LoadingSquare },
  setup() {
    const route = useRoute()
    const router = useRouter()
    const id = route.params.id as string

    const isLoading = ref(true)
    const isCurrentlyWorking = ref(false)

    const formData = ref<ProjectForm>({
      title: '',
      relatedTo: '',
      location: '',
      startDate: null,
      endDate: null,
      description: '',
      skillTitles: [],
      participants: []
    })

    const formErrors = ref<ValidationError[]>([])
    const isAddSkillButtonVisible = ref(true)
    const isAddPersonButtonVisible = ref(true)

    const relatedToOptions = [
      { value: "Job", label: "Job-related project" },
      { value: "Education", label: "Education-related project" },
      { value: "Event", label: "Event-related project" },
      { value: "Other", label: "Other" }
    ];

    onMounted(async () => {
      const startTime = performance.now()
      const result = await useGetProject(id)

      if ('isSuccessful' in result && !result.isSuccessful) {
        router.push('/error-page')
        return
      }

      if (!('isSuccessful' in result)) {
        formData.value = {
          title: result.title || '',
          relatedTo: result.relatedTo || '',
          location: result.location || '',
          startDate: result.startDate ? result.startDate.toString().split('T')[0] : null,
          endDate: result.endDate ? result.endDate.toString().split('T')[0] : null,
          description: result.description || '',
          skillTitles: result.skillTitles || [],
          participants: result.participants || []
        } as ProjectForm

        if (formData.value.skillTitles.length == 5) {
          isAddSkillButtonVisible.value = false
        }
      }

      const elapsed = performance.now() - startTime
      const remaining = 500 - elapsed
      if (remaining > 0) setTimeout(() => (isLoading.value = false), remaining)
      else isLoading.value = false
    })

    const addSkill = () => {
      if (formData.value.skillTitles.length >= 4) isAddSkillButtonVisible.value = false
      formData.value.skillTitles.push('')
    }
    const removeSkill = (index: number) => {
      if (formData.value.skillTitles.length === 5) isAddSkillButtonVisible.value = true
      formData.value.skillTitles.splice(index, 1)
    }

    const addPerson = () => {
      if (formData.value.participants.length >= 4) isAddPersonButtonVisible.value = false
      formData.value.participants.push('')
    }
    const removePerson = (index: number) => {
      if (formData.value.participants.length === 5) isAddPersonButtonVisible.value = true
      formData.value.participants.splice(index, 1)
    }

    const getError = (fieldName: string) =>
      formErrors.value.find(e => e.fieldName === fieldName)?.errorMessage || ''

    const onSave = async () => {
      formErrors.value = useValidationOfAddProjectForm(formData.value)
      if (formErrors.value.length === 0) {
        isLoading.value = true
        const result = await useEditProject(id, formData.value)
        isLoading.value = false
        if (result.isSuccessful) router.push('/my-profile/projects')
      }
    }

    return {
      isLoading,
      formData,
      isCurrentlyWorking,
      formErrors,
      relatedToOptions,
      isAddSkillButtonVisible,
      isAddPersonButtonVisible,
      addSkill,
      removeSkill,
      addPerson,
      removePerson,
      getError,
      onSave
    }
  }
})
</script>


<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.edit-project {
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

    &_name-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_related-to-dropdown {
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

    &_skills-and-people-divider {
      border-bottom: 1px solid $grayBorderColor;
      margin-bottom: 50px;
      height: 50px;
    }

    &_people-header {
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
      &-inscription {
        color: $grayBorderColor;
        font-size: 14px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }

    &_people {

      &-items {
        list-style: none;
        display: flex;
        flex-direction: column;
        width: 600px;

        @media (max-width: $breakpoint) {
          width: 100%;
        }

        &_person {
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
</style>