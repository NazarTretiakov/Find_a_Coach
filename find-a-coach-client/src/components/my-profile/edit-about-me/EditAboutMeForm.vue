<template> 
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="edit-about-me">
    <ul class="edit-about-me-items">
      <li class="edit-about-me-items_header">
        <h1 class="edit-about-me-items_header-element">Edit about me</h1>
      </li>

      <li class="edit-about-me-items_content-input">
        <text-input-area 
          label="Write about yourself" 
          name="content" 
          :max-number-of-signs="2000" 
          v-model="aboutMe" 
        />
        <span v-if="error" class="error-message">{{ error }}</span>
      </li>

      <li class="edit-about-me-items_save-button">
        <save-button @click="onSave"></save-button>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'

import TextInputArea from '../../input-fields/TextInputArea.vue';
import SaveButton from '../../input-fields/SaveButton.vue';
import LoadingSquare from '../../LoadingSquare.vue'

import useGetAboutMe from '../../../composables/my-profile/about-me/useGetAboutMe'
import useEditAboutMe from '../../../composables/my-profile/about-me/useEditAboutMe'
import { useRouter } from 'vue-router'
import { useAuthenticationStore } from "@/stores/authentication"

export default defineComponent({
  components: {
    TextInputArea,
    SaveButton,
    LoadingSquare
  }, 
  setup() {
    const router = useRouter()
    const authenticationStore = useAuthenticationStore()
    const isLoading = ref<boolean>(true)
    const aboutMe = ref<string>('')
    const error = ref<string>('')

    onMounted(async () => {
      const startTime = performance.now()
      
      const result = await useGetAboutMe(authenticationStore.userId)
      if (typeof result === 'object' && result !== null && 'isSuccessful' in result) {
        if (!result.isSuccessful) router.push('/error-page')
      } else {
        aboutMe.value = result as string
      }

      const elapsed = performance.now() - startTime
      const remaining = 500 - elapsed

      if (remaining > 0) {
        setTimeout(() => {
          isLoading.value = false
        }, remaining)
      } else {
        isLoading.value = false
      }
    })

    const onSave = async () => {
      if (aboutMe.value.length > 2000) {
        error.value = 'The content is too long - max number of signs is 2000.'
        return
      } 

      const response = await useEditAboutMe(aboutMe.value)
      if (response.isSuccessful) {
        router.push('/my-profile')
      } else {
        error.value = 'Unexpected error'
      }
    }

    return { isLoading, aboutMe, error, onSave }
  }
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.edit-about-me {
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
        font-size: 28px;

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
  font-size: 14px;
  margin-top: 4px;
  display: block;

  @media (max-width: $breakpoint) {
    font-size: 12px;
  }
}
</style>