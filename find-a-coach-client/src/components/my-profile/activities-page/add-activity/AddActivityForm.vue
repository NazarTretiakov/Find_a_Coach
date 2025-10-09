<template> 
  <div class="create-activity">
    <ul class="create-activity-items">
      <li class="create-activity-items_header">
        <h1 class="create-activity-items_header-element">Create activity</h1>
      </li>

      <li class="create-activity-items_title-input">
        <input-field v-model="formData.title" label="Title" name="title" type="text" />
        <span v-if="getError('title')" class="error-message">{{ getError('title') }}</span>
      </li>

      <li v-if="formData.activityType == 'event'" class="create-activity-items_date-input">
        <input-field v-model="formData.dateOfBeginning" label="Date of beginning" name="dateOfBeginning" type="date" />
        <span v-if="getError('dateOfBeginning')" class="error-message">{{ getError('dateOfBeginning') }}</span>
      </li>

      <li class="create-activity-items_type-input">
        <dropdown-menu
          v-model="formData.activityType"
          label="Activity type"
          name="activity-type"
          :options="[
            { value: 'event', label: 'Event' },
            { value: 'survey', label: 'Survey' },
            { value: 'qa', label: 'Question&Answer' },
            { value: 'post', label: 'Post'}
          ]"
        />
      </li>

      <li class="create-activity-items_image-input">
        <file-input-field v-model="formData.image" label="Image" name="image" />
      </li>

      <li :class="formData.activityType !== 'event' && formData.activityType !== 'survey' ? 'create-activity-items_description-input-without-border-bottom' : 'create-activity-items_description-input'">
        <text-input-area v-model="formData.description" label="Description" name="description" :max-number-of-signs="200" />
        <span v-if="getError('description')" class="error-message">{{ getError('description') }}</span>
      </li>

      <li class="create-activity-items_add-subjects-header">
        <h1 class="create-activity-items_add-subjects-header-element">Add subjects</h1>
      </li>
      <li class="create-activity-items_subjects">
        <ul class="create-activity-items_subjects-items">
          <li v-for="(subject, index) in formData.subjects" :key="index" class="create-activity-items_subjects-items_subject">
            <input-field v-model="formData.subjects[index]" class="create-activity-items_subjects-items_subject-name" label="Subject title" :name="'skill-title-' + index" type="text" />
            <span v-if="getError(`subjects[${index}]`)" class="error-message">
              {{ getError(`subjects[${index}]`) }}
            </span>

            <remove-button @click="removeSubject(index)" class="create-activity-items_subjects-items_subject-remove-button"></remove-button>
          </li>
        </ul>
      </li>
      <li v-if="isAddSubjectButtonVisible" class="create-activity-items_add-subject-button">
        <add-button @click="addSubject" added-object-name="subject"></add-button>
      </li>

      <li v-if="formData.activityType == 'event'" class="create-activity-items_panels-header">
        <h1 class="create-activity-items_panels-header-element">Add panels for search people</h1>
      </li>
      <li v-if="formData.activityType == 'event'" class="create-activity-items_panels">
        <ul class="create-activity-items_panels-items">
          <li v-for="(panel, index) in formData.searchPeoplePanels" :key="index" class="create-activity-items_panels-items_panel">
            <input-field v-model="panel.nameOfPosition" class="create-activity-items_panels-items_panel-name" label="Name of position" :name="'name-of-position-' + index" type="text" />
            <span v-if="getError(`panelsForSearchPeople[${index}].nameOfPosition`)" class="error-message">
              {{ getError(`panelsForSearchPeople[${index}].nameOfPosition`) }}
            </span>

            <input-field v-model="panel.preferredSkills" class="create-activity-items_panels-items_panel-preferred-skills" label="Preferred skills" :name="'preferred-skills-' + index" type="text" />
            <span v-if="getError(`panelsForSearchPeople[${index}].preferredSkills`)" class="error-message">
              {{ getError(`panelsForSearchPeople[${index}].preferredSkills`) }}
            </span>

            <input-field v-model="panel.payment" class="create-activity-items_panels-items_panel-payment" label="Payment" :name="'payment-' + index" type="text" />
            <span v-if="getError(`panelsForSearchPeople[${index}].payment`)" class="error-message">
              {{ getError(`panelsForSearchPeople[${index}].payment`) }}
            </span>

            <text-input-area v-model="panel.description" class="create-activity-items_panels-items_panel-description" label="Description" :name="'description-' + index" :max-number-of-signs="200" />
            <span v-if="getError(`panelsForSearchPeople[${index}].description`)" class="error-message">
              {{ getError(`panelsForSearchPeople[${index}].description`) }}
            </span>

            <remove-button @click="removePanel(index)" class="create-activity-items_panels-items_panel-remove-button"></remove-button>
          </li>
        </ul>
      </li>

      <li v-if="formData.activityType == 'survey'" class="create-activity-items_panels-header">
        <h1 class="create-activity-items_panels-header-element">Add survey options</h1>
      </li>
      <li v-if="formData.activityType == 'survey'" class="create-activity-items_panels">
        <ul class="create-activity-items_panels-items">
          <li v-for="(option, index) in formData.surveyOptions" :key="index" class="create-activity-items_panels-items_panel">
            <input-field v-model="formData.surveyOptions[index]" label="Survey option inscription" :name="'option-inscription-' + index" type="text" />
            <span v-if="getError(`surveyOptions[${index}]`)" class="error-message">
              {{ getError(`surveyOptions[${index}]`) }}
            </span>

            <remove-button @click="removeSurveyOption(index)" class="create-activity-items_panels-items_panel-remove-button"></remove-button>
          </li>
        </ul>
      </li>

      <li v-if="isAddPanelButtonVisible && formData.activityType == 'event'" class="create-activity-items_add-button">
        <add-button @click="addPanel" added-object-name="panel"></add-button>
      </li>
      <li v-if="isAddOptionButtonVisible && formData.activityType == 'survey'" class="create-activity-items_add-button">
        <add-button @click="addSurveyOption" added-object-name="survey"></add-button>
      </li>
      <li class="create-activity-items_save-button">
        <save-button @click="onSave"></save-button>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import InputField from "../../../input-fields/InputField.vue";
import TextInputArea from '../../../input-fields/TextInputArea.vue';
import DropdownMenu from '../../../input-fields/DropdownMenu.vue';
import FileInputField from '../../../input-fields/FileInputField.vue';
import RemoveButton from '../../../input-fields/RemoveButton.vue';
import AddButton from '../../../input-fields/AddButton.vue';
import SaveButton from '../../../input-fields/SaveButton.vue';

import type { Form } from '@/types/my-profile/activities/add-activity/Form'
import type { Panel } from '@/types/my-profile/activities/add-activity/Panel'
import useFormValidation from '@/composables/my-profile/activities/add-activity/useFormValidation'
import { useRouter } from 'vue-router'
import useAddActivity from '@/composables/my-profile/activities/add-activity/useAddActivity';

export default defineComponent({
  components: {
    InputField,
    TextInputArea,
    DropdownMenu,
    FileInputField,
    RemoveButton,
    AddButton,
    SaveButton
  },
  setup() {
    const router = useRouter()

    const panelsForSearchPeople = ref<Panel[]>([])
    const isAddPanelButtonVisible = ref<boolean>(true)
    const surveyOptions = ref<string[]>([])
    const isAddOptionButtonVisible = ref<boolean>(true)
    const subjects = ref<string[]>([])
    const isAddSubjectButtonVisible = ref<boolean>(true)
    const formErrors = ref<{ fieldName: string; errorMessage: string }[]>([])
    const formData = ref<Form>({
      title: '',
      dateOfBeginning: null,
      activityType: 'event',
      image: null,
      description: '',
      subjects: subjects.value,
      searchPeoplePanels: panelsForSearchPeople.value,
      surveyOptions: surveyOptions.value
    })

    const addSubject = () => {
      if (subjects.value.length >= 4) {
        isAddSubjectButtonVisible.value = false
      }

      subjects.value.push('')
    }
    const removeSubject = (index: number) => {
      if (subjects.value.length == 5) {
        isAddSubjectButtonVisible.value = true
      }
      subjects.value.splice(index, 1)
    }

    const addPanel = () => {
      if (panelsForSearchPeople.value.length >= 4) {
        isAddPanelButtonVisible.value = false
      }

      panelsForSearchPeople.value.push({ nameOfPosition: '', preferredSkills: '', payment: '', description: '' })
    }
    const removePanel = (index: number) => {
      if (panelsForSearchPeople.value.length == 5) {
       isAddPanelButtonVisible.value = true
      }
      panelsForSearchPeople.value.splice(index, 1)
    }

    const addSurveyOption = () => {
      if (surveyOptions.value.length >= 4) {
        isAddOptionButtonVisible.value = false
      }

      surveyOptions.value.push('')
    }
    const removeSurveyOption = (index: number) => {
      if (surveyOptions.value.length == 5) {
        isAddOptionButtonVisible.value = true
      }
      surveyOptions.value.splice(index, 1)
    }

    const onSave = async () => {
      formErrors.value = useFormValidation(formData.value)
      console.log(formErrors.value)
      console.log(formData.value)

      if (formErrors.value.length === 0) {
        let response = await useAddActivity(formData.value)

        if (response.isSuccessful) {
          router.push('/my-profile')
        }
      }
    }

    const getError = (field: string) => {
      return formErrors.value.find(e => e.fieldName === field)?.errorMessage || ''
    }

    return { 
      formData,
      panelsForSearchPeople,
      isAddPanelButtonVisible,
      subjects,
      isAddSubjectButtonVisible,
      addSubject,
      removeSubject,
      addPanel,
      removePanel,
      surveyOptions,
      addSurveyOption,
      removeSurveyOption,
      isAddOptionButtonVisible,
      getError,
      onSave
    }
  }
})
</script>

<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.create-activity {
  margin: 50px 0 0 150px;

  @media (max-width: $breakpoint) {
    margin: 30px 10px 0 10px;
  }

  .create-activity-items {
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

    &_date-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_type-input {
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

    &_description-input {
      margin-top: 20px;

      &-without-border-bottom {
        border: none;
        padding-bottom: none;
        margin-top: 20px;
      }

      @media (max-width: $breakpoint) {
        margin-top: 14px;
        padding-bottom: 34px;
      }
    }

    &_add-subjects-header {
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

    &_subjects {

      &-items {
        list-style: none;
        display: flex;
        flex-direction: column;
        width: 600px;

        @media (max-width: $breakpoint) {
          width: 100%;
        }

        &_subject {
          margin-top: 10px;

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

    &_add-subject-button {
      margin-top: 30px;
    }

    &_panels-header {
      margin-top: 60px;
      padding-top: 54px;
      border-top: 1px solid $grayBorderColor;

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

    &_panels {

      &-items {
        list-style: none;
        display: flex;
        flex-direction: column;
        width: 600px;

        @media (max-width: $breakpoint) {
          width: 100%;
        }

        &_panel {
          margin-top: 30px;

          &-name {
            margin-top: 20px;

            @media (max-width: $breakpoint) {
              margin-top: 14px;
            }
          }

          &-preferred-skills {
            margin-top: 20px;

            @media (max-width: $breakpoint) {
              margin-top: 14px;
            }
          }

          &-payment {
            margin-top: 20px;

            @media (max-width: $breakpoint) {
              margin-top: 14px;
            }
          }

          &-description {
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

          &-survey-option-name {
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