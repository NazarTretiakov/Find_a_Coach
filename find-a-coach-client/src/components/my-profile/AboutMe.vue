<template>
  <div class="about-me">
    <ul class="about-me-header">
      <li class="about-me-header-title">
        <h1 class="about-me-header-title-element">About me</h1>
      </li>
      <li class="about-me-header-edit">
        <router-link to="/my-profile/edit-about-me" class="about-me-header-edit-link">
          <img src="../../assets/images/icons/edit-icon.svg" alt="Edit icon" class="about-me-header-edit-icon">
        </router-link>
      </li>
    </ul>

    <p class="about-me-paragraph" style="white-space: pre-wrap;">
      {{ displayText }}
      <span v-if="isTruncated" class="about-me-paragraph-see-more" @click="showFullText">...see more</span>
    </p>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted } from "vue"
import useGetAboutMe from '../../composables/my-profile/about-me/useGetAboutMe'
import { useRouter } from "vue-router"

export default defineComponent({
  setup() {
    const aboutMe = ref<string>('')
    const router = useRouter()
    const isFullTextVisible = ref(false)
    const maxLength = 200

    const isTruncated = computed(() => !isFullTextVisible.value && (aboutMe.value.length > maxLength))
    const displayText = computed(() => {
      if (isFullTextVisible.value || aboutMe.value.length <= maxLength) {
        return aboutMe.value
      }
      return aboutMe.value.substring(0, maxLength)
    })

    const showFullText = () => {
      isFullTextVisible.value = true
    }

    onMounted(async () => {
      const result = await useGetAboutMe()

      if (typeof result === 'object' && result !== null && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
        }
      } else {
        aboutMe.value = result as string
      }
    })

    return { aboutMe, displayText, isTruncated, showFullText }
  },
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.about-me {
  margin: 0 0 30px 100px;
  padding: 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 0 10px;
    padding: 0;
    border: none;
  }

  &-header {
    list-style: none;
    display: flex;
    justify-content: space-between;
    align-content: center;

    &-title {
      margin-bottom: 30px;

      @media (max-width: $breakpoint) {
        margin-bottom: 14px;
      }
      
      &-element {
        font-size: 24px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }
    &-edit {
      &-icon {
        width: 30px;
        transition: transform 0.3s ease;

        &:hover {
          transform: scale(1.15);
        }

        @media (max-width: $breakpoint) {
          width: 24px;
        }
      }
    }
  }

  &-paragraph {
    font-size: 14px;
    white-space: pre-wrap;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }

    &-see-more {
      color: $grayBorderColor;
      cursor: pointer;

      &:hover {
        text-decoration: underline;
      }

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }
  }
}
</style>