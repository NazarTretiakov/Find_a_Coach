<template> 
  <div class="add-certification">
    <ul class="add-certification-items">
      <li class="add-certification-items_header">
        <h1 class="add-certification-items_header-element">Edit certification</h1>
      </li>
      <li class="add-certification-items_name-input">
        <input-field label="Name" name="name" type="text"></input-field>
      </li>
      <li class="add-certification-items_issuing-organization-input">
        <input-field label="Issuing organization" name="issuing-organization" type="text"></input-field>
      </li>
      <li class="add-certification-items_issue-date-input">
        <input-field label="Issue date" name="issue-date" type="date"></input-field>
      </li>
      <li class="add-certification-items_image-input">
        <file-input-field label="Image" name="image"></file-input-field>
      </li>
      <li class="add-certification-items_credential-id-input">
        <input-field label="Credential ID" name="credential-id" type="text"></input-field>
      </li>
      <li class="add-certification-items_credential-url-input">
        <input-field label="Credential URL" name="credential-url" type="text"></input-field>
      </li>
      <li class="add-certification-items_skills-header">
        <h1 class="add-certification-items_skills-header-element">Add skills</h1>
      </li>
      <li class="add-certification-items_skills">
        <ul class="add-certification-items_skills-items">
          <li v-for="(skill, index) in skills" :key="index" class="add-certification-items_skills-items_skill">
            <input-field
              v-model="skills[index]"
              class="add-certification-items_skills-items_skill-name"
              label="Skill name"
              :name="'skill-name-' + index"
              type="text"
            />
            <remove-button @click="removeSkill(index)" class="add-certification-items_skills-items_skill-remove-button"></remove-button>
          </li>
        </ul>
      </li>

      <li v-if="isAddSkillButtonVisible" class="add-certification-items_add-button"><add-button @click="addSkill" added-object-name="skill"></add-button></li>
      <li class="add-certification-items_save-button"><save-button></save-button></li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import InputField from "../../../input-fields/InputField.vue"
import FileInputField from '../../../input-fields/FileInputField.vue'
import RemoveButton from '../../../input-fields/RemoveButton.vue'
import AddButton from '../../../input-fields/AddButton.vue'
import SaveButton from '../../../input-fields/SaveButton.vue'

export default defineComponent({
  components: {
    InputField,
    FileInputField,
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

    &_name-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_issuing-organization-input {
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
</style>