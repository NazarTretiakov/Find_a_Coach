<template> 
  <div class="leave-review">
    <ul class="leave-review-items">
      <li class="leave-review-items_header">
        <h1 class="leave-review-items_header-element">Send connection request to {{ name }}</h1>
      </li>
      <li class="leave-review-items_content-input">
        <text-input-area label="Message" name="message" :max-number-of-signs="1000" v-model="formData.message"></text-input-area>
        <span v-if="getError('content')" class="error-message">{{ getError('content') }}</span>
      </li>
      <li class="leave-review-items_save-button"><save-button @click="onSave"></save-button></li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

import TextInputArea from '../../input-fields/TextInputArea.vue'
import SaveButton from '../../input-fields/SaveButton.vue'

import { useRouter } from 'vue-router'
import { ValidationError } from '@/types/ValidationError'
import { useAuthenticationStore } from '@/stores/authentication'
import useValidationOfSendConnectionRequestForm from '@/composables/my-profile/connections/useValidationOfSendConnectionRequestForm'
import useSendConnectionRequest from '@/composables/my-profile/connections/useSendConnectionRequest'
import { ConnectionForm } from '@/types/my-profile/connections/ConnectionForm'

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true,
    },
    name: {
      type: String,
      required: true
    }
  },
  components: {
    TextInputArea,
    SaveButton
  },
  setup(props) {
    const router = useRouter()
    const authenticationStore = useAuthenticationStore()
    const isLoading = ref<boolean>(false)

    const formData = ref<ConnectionForm>({
      userId: authenticationStore.userId,
      connectedUserId: props.id,
      message: ''
    })

    const formErrors = ref<ValidationError[]>([])

    const getError = (fieldName: string) => {
      return formErrors.value.find(e => e.fieldName === fieldName)?.errorMessage || ''
    }

    const onSave = async () => {
      formErrors.value = useValidationOfSendConnectionRequestForm(formData.value)

      if (formErrors.value.length === 0) {
        isLoading.value = true
        const result = await useSendConnectionRequest(formData.value)
        isLoading.value = false

        if (result.isSuccessful) {
          router.push(`/user-profile/${props.id}`)
        }
      }
    }

    return {
      isLoading,
      onSave,
      getError,
      formData,
      formErrors
    }
  }
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.leave-review {
  margin: 50px 0 0 150px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 0 10px;
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
        font-size: 24px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }

    &_content-input {
      margin-top: 14px;
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