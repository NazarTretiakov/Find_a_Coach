<template>
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="edit-language">
    <ul class="edit-language-items">
      <li class="edit-language-items_header">
        <h1 class="edit-language-items_header-element">Edit language</h1>
      </li>

      <li class="edit-language-items_language-input">
        <input-field
          label="Language"
          name="language"
          type="text"
          v-model="formData.title"
          :disabled="true"
        />
        <span v-if="getError('title')" class="error-message">{{ getError('title') }}</span>
      </li>

      <li class="edit-language-items_proficiency-dropdown">
        <dropdown-menu
          label="Proficiency"
          name="proficiency"
          v-model="formData.proficiency"
          :options="proficiencyOptions"
        />
        <span v-if="getError('proficiency')" class="error-message">{{ getError('proficiency') }}</span>
      </li>

      <li class="edit-language-items_save-button">
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
import SaveButton from '../../../input-fields/SaveButton.vue'
import LoadingSquare from '../../../LoadingSquare.vue'

import type { ValidationError } from '@/types/ValidationError'
import type { LanguageForm } from '@/types/my-profile/languages/LanguageForm'

import useGetLanguage from '@/composables/my-profile/languages/useGetLanguage'
import useEditLanguage from '@/composables/my-profile/languages/useEditLanguage'
import useValidationOfLanguageForm from '@/composables/my-profile/languages/useValidationOfAddLanguageForm'

export default defineComponent({
  components: {
    InputField,
    DropdownMenu,
    SaveButton,
    LoadingSquare
  },
  setup() {
    const route = useRoute()
    const router = useRouter()
    const id = route.params.id as string

    const isLoading = ref<boolean>(true)
    const formData = ref<LanguageForm>({
      title: '',
      proficiency: ''
    })
    const formErrors = ref<ValidationError[]>([])

    const proficiencyOptions = [
      { value: 'A1', label: 'Beginner - A1' },
      { value: 'A2', label: 'Pre-Intermediate - A2' },
      { value: 'B1', label: 'Intermediate - B1' },
      { value: 'B2', label: 'Upper-Intermediate - B2' },
      { value: 'C1', label: 'Advanced - C1' },
      { value: 'C2', label: 'Mastery - C2' }
    ]

    onMounted(async () => {
      const startTime = performance.now()

      const result = await useGetLanguage(id)

      if ('isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        formData.value = {
          title: result.title || '',
          proficiency: result.proficiency || ''
        }
      }

      const elapsed = performance.now() - startTime
      const remaining = 500 - elapsed
      if (remaining > 0) {
        setTimeout(() => (isLoading.value = false), remaining)
      } else {
        isLoading.value = false
      }
    })

    const getError = (fieldName: string) => {
      return formErrors.value.find(e => e.fieldName === fieldName)?.errorMessage || ''
    }

    const onSave = async () => {
      formErrors.value = useValidationOfLanguageForm(formData.value)

      if (formErrors.value.length === 0) {
        isLoading.value = true
        const result = await useEditLanguage(id, formData.value)
        isLoading.value = false

        if (result.isSuccessful) {
          router.push('/my-profile/languages')
        } else {
          console.error(result.errorMessage)
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

.edit-language {
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
</style>