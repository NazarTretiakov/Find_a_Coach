<template> 
  <div class="add-education">
    <ul class="add-education-items">
      <li class="add-education-items_header">
        <h1 class="add-education-items_header-element">Edit education</h1>
      </li>
      <li class="add-education-items_school-input">
        <input-field label="School" name="school" type="text"></input-field>
      </li>
      <li class="add-education-items_degree-dropdown">
        <dropdown-menu
          label="Degree"
          name="degree"
          :options="[
            { value: 'associate', label: 'Associate\'s degree' },
            { value: 'bachelor', label: 'Bachelor\'s degree' },
            { value: 'master', label: 'Master\'s degree' },
            { value: 'doctor', label: 'Doctoral degree' }
          ]"
        />
      </li>
      <li class="add-education-items_specialization-input">
        <input-field label="Specialization" name="specialization" type="text"></input-field>
      </li>
      <li class="add-education-items_location-input">
        <input-field label="Location" name="location" type="text"></input-field>
      </li>
      <li class="add-education-items_start-date-input">
        <input-field label="Start date" name="start-date" type="date"></input-field>
      </li>
      <li class="add-education-items_end-date-input">
        <input-field label="End date" name="end-date" type="date" :disabled="isCurrentlyWorking"></input-field>
      </li>
      <li class="add-education-items_skills-header">
        <h1 class="add-education-items_skills-header-element">Add skills</h1>
      </li>
      <li class="add-education-items_skills">
        <ul class="add-education-items_skills-items">
          <li v-for="(skill, index) in skills" :key="index" class="add-education-items_skills-items_skill">
            <input-field
              v-model="skills[index]"
              class="add-education-items_skills-items_skill-name"
              label="Skill name"
              :name="'skill-name-' + index"
              type="text"
            />
            <remove-button @click="removeSkill(index)" class="add-education-items_skills-items_skill-remove-button"></remove-button>
          </li>
        </ul>
      </li>

      <li v-if="isAddSkillButtonVisible" class="add-education-items_add-button"><add-button @click="addSkill" added-object-name="skill"></add-button></li>
      <li class="add-education-items_save-button"><save-button></save-button></li>
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

    &_specialization-input {
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