<template>
  <loading-square v-if="isLoading"></loading-square>

  <div class="contact-information" v-else>
    <h1 class="contact-information-header">Contact information</h1>

    <div class="contact-information-email">
      <img class="contact-information-email-icon" src="@/assets/images/icons/email-icon.svg" alt="Email icon">
      <h3 class="contact-information-email-header">Email</h3>
    </div>
    <span class="contact-information-email-element">
      <a class="contact-information-email-element-link" :href="'mailto:' + contactInformation.email">{{ contactInformation.email }}</a>
    </span>

    <div class="contact-information-phone">
      <img class="contact-information-phone-icon" src="@/assets/images/icons/phone-icon.svg" alt="Phone icon">
      <h3 class="contact-information-phone-header">Phone</h3>
    </div>
    <span class="contact-information-phone-element">{{ contactInformation.phone }}</span>

    <div class="contact-information-websites">
      <img class="contact-information-websites-icon" src="@/assets/images/icons/website-icon.svg" alt="Website icon">
      <h3 class="contact-information-websites-header">Websites</h3>
    </div>

    <ul class="contact-information-websites-items">
      <li v-for="(website, index) in contactInformation.websites" :key="index" class="contact-information-websites-items_website">
        <a :href="website.url" target="_blank" rel="noopener noreferrer" class="contact-information-websites-items_website-url">{{ website.url }}</a>
        <span class="contact-information-websites-items_website-type">({{ website.type }})</span>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'

import LoadingSquare from '@/components/LoadingSquare.vue'

import { useRouter } from 'vue-router'
import { ContactInformation } from '@/types/my-profile/contact-information/ContactInformation'
import useGetContactInformation from '@/composables/my-profile/contact-information/useGetContactInformation'

export default defineComponent({
  components: {
    LoadingSquare
  },
  setup() {
    const router = useRouter()
    const contactInformation = ref<ContactInformation>({} as ContactInformation)
    const isLoading = ref<boolean>(true)

    async function loadContactInformation() {
      const startTime = performance.now()

      const result = await useGetContactInformation()

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        contactInformation.value = result as ContactInformation
      }

      const elapsed = performance.now() - startTime
      const remaining = 500 - elapsed
      if (remaining > 0) {
        setTimeout(() => (isLoading.value = false), remaining)
      } else {
        isLoading.value = false
      }
    }

    onMounted(() => loadContactInformation())

    return { contactInformation, isLoading }
  }
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.contact-information {
  margin: 50px 0 0 150px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 0 10px;
  }

  &-header {
    font-size: 28px;
    margin-bottom: 50px;

    @media (max-width: $breakpoint) {
      font-size: 20px;
    }
  }

  &-email {
    display: flex;
    align-items: center;

    &-icon {
      width: 30px;

      @media (max-width: $breakpoint) {
        width: 24px;
      }
    }

    &-header {
      margin-left: 10px;
      font-size: 20px;

      @media (max-width: $breakpoint) {
        font-size: 16px;
      }
    }

    &-element {
      display: block;
      margin: 10px 0 0 40px;
      font-size: 14px;
      margin-bottom: 30px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }

      &-link {
        color: $linkColor;
        text-decoration: none;

        &:hover {
          text-decoration: underline;
        }
      }
    }
  }

  &-phone {
    display: flex;
    align-items: center;

    &-icon {
      width: 30px;

      @media (max-width: $breakpoint) {
        width: 24px;
      }
    }

    &-header {
      margin-left: 10px;
      font-size: 20px;

      @media (max-width: $breakpoint) {
        font-size: 16px;
      }
    }

    &-element {
      font-size: 14px;
      display: block;
      margin: 10px 0 0 40px;
      margin-bottom: 30px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }
  }

  &-websites {
    display: flex;
    align-items: center;

    &-icon {
      width: 30px;

      @media (max-width: $breakpoint) {
        width: 24px;
      }
    }

    &-header {
      margin-left: 10px;
      font-size: 20px;

      @media (max-width: $breakpoint) {
        font-size: 16px;
      }
    }

    &-items {
      margin: 10px 0 100px 60px;

      &_website {
        margin-top: 10px;
        font-size: 14px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }

        &-url {
          color: $linkColor;
          text-decoration: none;

          &:hover {
            text-decoration: underline;
          }
        }

        &-type {
          color: $grayBorderColor;
          margin-left: 10px;
        }
      }
    }
  }
}
</style>
