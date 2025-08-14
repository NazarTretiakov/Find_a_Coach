<template> 
  <div class="create-activity">
    <ul class="create-activity-items">
      <li class="create-activity-items_header">
        <h1 class="create-activity-items_header-element">Create activity</h1>
      </li>
      <li class="create-activity-items_title-input">
        <input-field label="Title" name="title" type="text"></input-field>
      </li>
      <li class="create-activity-items_subject-input">
        <input-field label="Subject (You can add more than one, separate subjects using comma sign. Example: Subject1, Subject2, Subject3)" name="subject" type="text"></input-field>
      </li>
        <li class="create-activity-items_date-input">
        <input-field label="Date of beginning" name="date" type="date"></input-field>
      </li>
      <li class="create-activity-items_type-input">
        <dropdown-menu
          class="create-activity-items_activity-dropdown"
          label="Activity type"
          name="activity-type"
          v-model="selectedActivityType"
          :options="[
            { value: 'event', label: 'Event' },
            { value: 'survey', label: 'Survey' },
            { value: 'questionAndAnswer', label: 'Question&Answer' }
          ]"
        />
      </li>
      <li class="create-activity-items_image-input">
        <file-input-field label="Image" name="image"></file-input-field>
      </li>
      <li class="create-activity-items_description-input">
        <text-input-area label="Description" name="description" max-number-of-signs="200"></text-input-area>
      </li>

      <li v-if="selectedActivityType == 'event'" class="create-activity-items_panels-header">
        <h1 class="create-activity-items_panels-header-element">Add panels for search people</h1>
      </li>
      <li v-if="selectedActivityType == 'event'" class="create-activity-items_panels">
        <ul class="create-activity-items_panels-items">
          <li v-for="(panelForSearchPeople, index) in panelsForSearchPeople" :key="index" class="create-activity-items_panels-items_panel">
            <input-field
              v-model="panelForSearchPeople.nameOfPosition"
              class="create-activity-items_panels-items_panel-name"
              label="Name of position"
              :name="'name-of-position-' + index"
              type="text"
            />
            <input-field
              v-model="panelForSearchPeople.prefferedSkills"
              class="create-activity-items_panels-items_panel-preffered-skills"
              label="Preffered skills"
              :name="'preffered-skills-' + index"
              type="text"
            />
            <input-field
              v-model="panelForSearchPeople.payment"
              class="create-activity-items_panels-items_panel-payment"
              label="Payment"
              :name="'payment-' + index"
              type="text"
            />
            <text-input-area 
              v-model="panelForSearchPeople.description"
              class="create-activity-items_panels-items_panel-description"
              label="Description" 
              :name="'description-' + index" 
              max-number-of-signs="200">
            </text-input-area>
            <remove-button @click="removePanel(index)" class="create-activity-items_panels-items_panel-remove-button"></remove-button>
          </li>
        </ul>
      </li>

      <li v-if="selectedActivityType == 'survey'" class="create-activity-items_panels-header">
        <h1 class="create-activity-items_panels-header-element">Add survey options</h1>
      </li>
      <li v-if="selectedActivityType == 'survey'" class="create-activity-items_panels">
        <ul class="create-activity-items_panels-items">
          <li v-for="(surveyOption, index) in surveyOptions" :key="index" class="create-activity-items_panels-items_panel">
            <input-field
              v-model="surveyOptions[index]"
              class="create-activity-items_panels-items_panel-survey-option-name"
              label="Survey option inscription"
              :name="'option-inscription-' + index"
              type="text"
            />
            <remove-button @click="removeSurveyOption(index)" class="create-activity-items_panels-items_panel-remove-button"></remove-button>
          </li>
        </ul>
      </li>

      <li v-if="isAddPanelButtonVisible && selectedActivityType == 'event'" class="create-activity-items_add-button"><add-button @click="addPanel" added-object-name="panel"></add-button></li>
      <li v-if="isAddOptionButtonVisible && selectedActivityType == 'survey'" class="create-activity-items_add-button"><add-button @click="addSurveyOption" added-object-name="survey"></add-button></li>
      <li class="create-activity-items_save-button"><save-button></save-button></li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import InputField from "../input-fields/InputField.vue";
import TextInputArea from '../input-fields/TextInputArea.vue';
import DropdownMenu from '../input-fields/DropdownMenu.vue';
import FileInputField from '../input-fields/FileInputField.vue';
import RemoveButton from '../input-fields/RemoveButton.vue';
import AddButton from '../input-fields/AddButton.vue';
import SaveButton from '../input-fields/SaveButton.vue';
import type { Panel } from '../../types/add-activity/Panel'

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
    const selectedActivityType = ref<string>('event')
    const panelsForSearchPeople = ref<Panel[]>([])
    const isAddPanelButtonVisible = ref<boolean>(true)
    const surveyOptions = ref<string[]>([])
    const isAddOptionButtonVisible = ref<boolean>(true)

    const addPanel = () => {
      if (panelsForSearchPeople.value.length >= 4) {
        isAddPanelButtonVisible.value = false
      }

      panelsForSearchPeople.value.push({ nameOfPosition: '', prefferedSkills: '', payment: '', description: '' })
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

    return { selectedActivityType, panelsForSearchPeople, isAddPanelButtonVisible, addPanel, removePanel, surveyOptions, addSurveyOption, removeSurveyOption, isAddOptionButtonVisible }
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

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

    &_subject-input {
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
      padding-bottom: 54px;
      border-bottom: 1px solid $grayBorderColor;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
        padding-bottom: 34px;
      }
    }

    &_panels-header {
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

    &_panels {
      .create-activity-items_panels-items {
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

          &-preffered-skills {
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
</style>