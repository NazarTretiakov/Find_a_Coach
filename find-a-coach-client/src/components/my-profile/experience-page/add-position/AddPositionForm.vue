<template> 
  <div class="add-position">
    <ul class="add-position-items">
      <li class="add-position-items_header">
        <h1 class="add-position-items_header-element">Add position</h1>
      </li>
      <li class="add-position-items_title-input">
        <input-field label="Title" name="title" type="text"></input-field>
      </li>
      <li class="add-position-items_employment-type-dropdown">
        <dropdown-menu
          label="Employment type"
          name="employment-type"
          :options="[
            { value: 'full-time', label: 'Full-time' },
            { value: 'part-time', label: 'Part-time' },
            { value: 'self-employed', label: 'Self-employed' },
            { value: 'internship', label: 'Internship' }
          ]"
        />
      </li>
      <li class="add-position-items_company-input">
        <input-field label="Company or organization" name="company" type="text"></input-field>
      </li>
      <li class="add-position-items_is-currently-working-checkbox">
        <checkbox-field label="I am currently working in this role" name="is-currently-working" v-model="isCurrentlyWorking"></checkbox-field>
      </li>
      <li class="add-position-items_start-date-input">
        <input-field label="Start date" name="start-date" type="date"></input-field>
      </li>
      <li class="add-position-items_end-date-input">
        <input-field label="End date" name="end-date" type="date" :disabled="isCurrentlyWorking"></input-field>
      </li>
      <li class="add-position-items_location-input">
        <input-field label="Location" name="location" type="text"></input-field>
      </li>
      <li class="add-position-items_description-input">
        <text-input-area label="Description" name="description" max-number-of-signs="200"></text-input-area>
      </li>
      <li class="add-position-items_skills-header">
        <h1 class="add-position-items_skills-header-element">Add skills</h1>
      </li>
      <li class="add-position-items_skills">
        <ul class="add-position-items_skills-items">
          <li v-for="(skill, index) in skills" :key="index" class="add-position-items_skills-items_skill">
            <input-field
              v-model="skills[index]"
              class="add-position-items_skills-items_skill-name"
              label="Skill name"
              :name="'skill-name-' + index"
              type="text"
            />
            <remove-button @click="removeSkill(index)" class="add-position-items_skills-items_skill-remove-button"></remove-button>
          </li>
        </ul>
      </li>

      <li v-if="isAddSkillButtonVisible" class="add-position-items_add-button"><add-button @click="addSkill" added-object-name="skill"></add-button></li>
      <li class="add-position-items_save-button"><save-button></save-button></li>
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

    return { isCurrentlyWorking, skills, addSkill, removeSkill, isAddSkillButtonVisible }
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