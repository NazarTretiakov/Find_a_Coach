<template>
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="add-language">
    <ul class="add-language-items">
      <li class="add-language-items_header">
        <h1 class="add-language-items_header-element">Add language</h1>
      </li>

      <li class="add-language-items_language-input">
        <input-field
          label="Language"
          name="title"
          type="text"
          v-model="formData.title"
        />
        <span v-if="getError('title')" class="error-message">{{ getError('title') }}</span>
      </li>

      <li class="add-language-items_proficiency-dropdown">
        <dropdown-menu
          label="Proficiency"
          name="proficiency"
          v-model="formData.proficiency"
          :options="proficiencyOptions"
        />
        <span v-if="getError('proficiency')" class="error-message">{{ getError('proficiency') }}</span>
      </li>

      <li class="add-language-items_save-button">
        <save-button @click="onSave"></save-button>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import { useRouter } from 'vue-router'

import InputField from '../../../input-fields/InputField.vue'
import DropdownMenu from '../../../input-fields/DropdownMenu.vue'
import SaveButton from '../../../input-fields/SaveButton.vue'
import LoadingSquare from '../../../LoadingSquare.vue'

import type { ValidationError } from '@/types/ValidationError'
import type { LanguageForm } from '@/types/my-profile/languages/LanguageForm'

import useValidationOfAddLanguageForm from '@/composables/my-profile/languages/useValidationOfAddLanguageForm'
import useAddLanguage from '@/composables/my-profile/languages/useAddLanguage'

export default defineComponent({
  components: {
    InputField,
    DropdownMenu,
    SaveButton,
    LoadingSquare
  },
  setup() {
    const router = useRouter()
    const isLoading = ref<boolean>(false)

    const proficiencyOptions = [
      { value: 'A1', label: 'Beginner - A1' },
      { value: 'A2', label: 'Pre-Intermediate - A2' },
      { value: 'B1', label: 'Intermediate - B1' },
      { value: 'B2', label: 'Upper-Intermediate - B2' },
      { value: 'C1', label: 'Advanced - C1' },
      { value: 'C2', label: 'Mastery - C2' }
    ]

    const formData = ref<LanguageForm>({
      title: '',
      proficiency: ''
    })

    const formErrors = ref<ValidationError[]>([])

    const getError = (fieldName: string) => {
      return formErrors.value.find(e => e.fieldName === fieldName)?.errorMessage || ''
    }

    const onSave = async () => {
      formErrors.value = useValidationOfAddLanguageForm(formData.value)

      if (formErrors.value.length === 0) {
        isLoading.value = true
        const result = await useAddLanguage(formData.value)
        isLoading.value = false

        if (result.isSuccessful) {
          router.push('/my-profile/languages')
        }
      }
    }

    return {
      isLoading,
      formData,
      formErrors,
      proficiencyOptions,
      onSave,
      getError
    }
  }
})
</script>

<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.add-language {
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

    &_language-input {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }

    &_proficiency-dropdown {
      margin-top: 20px;

      @media (max-width: $breakpoint) {
        margin-top: 14px;
      }
    }
    
    &_save-button {
      margin: 70px 0 100px 0;
    }
  }
}

.error-message {
  color: red;
  font-size: 0.9rem;
  margin-top: 4px;
}
</style>