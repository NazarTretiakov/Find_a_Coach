<template>
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="privacy-settings">
    <h1 class="privacy-settings_header">Privacy</h1>
    <dropdown-menu 
      class="privacy-settings_contact-information"
      label="Who can see your contact information" 
      name="contact-information-privacy" 
      :options="contactInformationVisibilityOptions"
      v-model="contactInformationVisibilityType"
    ></dropdown-menu>
    <save-button @click="onSave"></save-button>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'
import DropdownMenu from '../../../input-fields/DropdownMenu.vue'
import SaveButton from '../../../input-fields/SaveButton.vue'
import LoadingSquare from '../../../LoadingSquare.vue'

import useGetContactInfomationVisibility from '@/composables/my-profile/settings/useGetContactInformationVisibility'
import useEditContactInformationVisibility from '@/composables/my-profile/settings/useEditContactInformationVisibility'
import { useRouter } from 'vue-router'

export default defineComponent({
  components: {
    DropdownMenu,
    SaveButton,
    LoadingSquare
  },
  setup() {
    const contactInformationVisibilityType = ref<string>('')
    const router = useRouter()
    const isLoading = ref<boolean>(true)

    const contactInformationVisibilityOptions = [
      { value: 'Everyone', label: 'Everyone' },
      { value: 'Network', label: 'Only your network' },
      { value: 'NoOne', label: 'No one' }
    ]

    onMounted(async () => {
      const startTime = performance.now()

      const result = await useGetContactInfomationVisibility()

      if (typeof result === 'object' && result !== null && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
        }
      } else {
        contactInformationVisibilityType.value = result as string
        const elapsed = performance.now() - startTime
        const remaining = 500 - elapsed

        if (remaining > 0) {
          setTimeout(() => {
            isLoading.value = false
          }, remaining)
        } else {
          isLoading.value = false
        }
      }
    })

    const onSave = async () => {
      isLoading.value = true
      const startTime = performance.now()

      const result = await useEditContactInformationVisibility(contactInformationVisibilityType.value)

      if (typeof result === 'object' && result !== null && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
        }
      } else {
        contactInformationVisibilityType.value = result as string
        const elapsed = performance.now() - startTime
        const remaining = 500 - elapsed

        if (remaining > 0) {
          setTimeout(() => {
            isLoading.value = false
          }, remaining)
        } else {
          isLoading.value = false
        }
      }
    }

    return { contactInformationVisibilityType, contactInformationVisibilityOptions, isLoading, onSave }
  },
})
</script>

<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.privacy-settings {
  margin: 50px 0 100px 0;

  @media (max-width: $breakpoint) {
    margin: 30px 10px 100px 10px;
  }

  &_header {
    font-size: 28px;
    margin-bottom: 50px;

    @media (max-width: $breakpoint) {
      font-size: 20px;
    }
  }

  &_contact-information {
    margin-bottom: 70px;
  }
}

</style>