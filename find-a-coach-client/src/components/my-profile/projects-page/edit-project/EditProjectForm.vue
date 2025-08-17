<template> 
  <div class="add-project">
    <ul class="add-project-items">
      <li class="add-project-items_header">
        <h1 class="add-project-items_header-element">Edit project</h1>
      </li>
      <li class="add-project-items_name-input">
        <input-field label="Project name" name="name" type="text"></input-field>
      </li>
      <li class="add-project-items_related-to-dropdown">
        <dropdown-menu
          label="Related to"
          name="related-to"
          :options="[
            { value: 'job', label: 'Job-related project' },
            { value: 'education', label: 'Education-related project' },
            { value: 'other', label: 'Other' }
          ]"
        />
      </li>
      <li class="add-project-items_location-input">
        <input-field label="Location" name="location" type="text"></input-field>
      </li>
      <li class="add-project-items_start-date-input">
        <input-field label="Start date" name="start-date" type="date"></input-field>
      </li>
      <li class="add-project-items_end-date-input">
        <input-field label="End date" name="end-date" type="date" :disabled="isCurrentlyWorking"></input-field>
      </li>
      <li class="add-project-items_description-input">
        <text-input-area label="Description" name="description" max-number-of-signs="200"></text-input-area>
      </li>
      <li class="add-project-items_skills-header">
        <h1 class="add-project-items_skills-header-element">Add skills</h1>
      </li>
      <li class="add-project-items_skills">
        <ul class="add-project-items_skills-items">
          <li v-for="(skill, index) in skills" :key="index" class="add-project-items_skills-items_skill">
            <input-field
              v-model="skills[index]"
              class="add-project-items_skills-items_skill-name"
              label="Skill name"
              :name="'skill-name-' + index"
              type="text"
            />
            <remove-button @click="removeSkill(index)" class="add-project-items_skills-items_skill-remove-button"></remove-button>
          </li>
        </ul>
      </li>
      <li v-if="isAddSkillButtonVisible" class="add-project-items_add-button"><add-button @click="addSkill" added-object-name="skill"></add-button></li>
      <div class="add-project-items_skills-and-people-divider"></div>
      <li class="add-project-items_people-header">
        <h1 class="add-project-items_people-header-element">People that participated in the project</h1>
        <span class="add-project-items_people-header-inscription">(Except you)</span>
      </li>
      <li class="add-project-items_people">
        <ul class="add-project-items_people-items">
          <li v-for="(person, index) in people" :key="index" class="add-project-items_people-items_person">
            <input-field
              class="add-project-items_people-items_person-name"
              v-model="people[index]"
              label="Person name"
              :name="'person-name-' + index"
              type="text"
            />
            <remove-button @click="removePerson(index)" class="add-project-items_skills-items_skill-remove-button"></remove-button>
          </li>
        </ul>
      </li>
      <li v-if="isAddPersonButtonVisible" class="add-project-items_add-button"><add-button @click="addPerson" added-object-name="person"></add-button></li>
      <li class="add-project-items_save-button"><save-button></save-button></li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import InputField from "../../../input-fields/InputField.vue";
import DropdownMenu from '../../../input-fields/DropdownMenu.vue';
import TextInputArea from '../../../input-fields/TextInputArea.vue';
import CheckboxField from '../../../input-fields/CheckboxField.vue';
import RemoveButton from '../../../input-fields/RemoveButton.vue';
import AddButton from '../../../input-fields/AddButton.vue';
import SaveButton from '../../../input-fields/SaveButton.vue';

export default defineComponent({
  components: {
    InputField,
    DropdownMenu,
    TextInputArea,
    CheckboxField,
    RemoveButton,
    AddButton,
    SaveButton
  },
  setup() {
    const isCurrentlyWorking = ref<boolean>(false)
    const skills = ref<string[]>([])
    const isAddSkillButtonVisible = ref<boolean>(true)
    const people = ref<string[]>([])
    const isAddPersonButtonVisible = ref<boolean>(true)

    const addSkill = () => {
      if (skills.value.length >= 4) {
        isAddSkillButtonVisible.value = false
      }
      skills.value.push('')
    }
    const removeSkill = (index: number) => {
      if (skills.value.length == 5) {
        isAddSkillButtonVisible.value = true
      }
      skills.value.splice(index, 1)
    }

    const addPerson = () => {
      if (people.value.length >= 4) {
        isAddPersonButtonVisible.value = false
      }
      people.value.push('')
    }
    const removePerson = (index: number) => {
      if (people.value.length == 5) {
        isAddPersonButtonVisible.value = true
      }
      people.value.splice(index, 1)
    }

    return { isCurrentlyWorking, skills, addSkill, removeSkill, isAddSkillButtonVisible, people, addPerson, removePerson, isAddPersonButtonVisible }
  }
})
</script>

<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.add-project {
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

    &_skills-and-people-divider {
      border-bottom: 1px solid $grayBorderColor;
      margin-bottom: 50px;
      height: 50px;
    }

    &_people-header {

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
</style>