<template>
  <div class="languages">
    <ul class="languages-header">
      <li class="languages-header_inscription">
        <h1 class="languages-header_inscription-element">Languages</h1>
      </li>
      <li class="languages-header_add-button">
        <router-link to="/my-profile/add-language" class="languages-header_add-button-link">
          <button class="languages-header_add-button-element">
            <svg class="languages-header_add-button-element-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
              <path d="M12 5v14m-7-7h14" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
            </svg>
            <span class="languages-header_add-button-element-inscription">Add</span>
          </button>
        </router-link>
      </li>
    </ul>

    <ul class="languages-items">
      <li v-for="language in languages" :key="language.languageId" class="languages-items_language" :id="language.languageId">
        <ul class="languages-items_language-items">
          <li class="languages-items_language-items_header">
            <h2 class="languages-items_language-items_header-name-of-language">{{ language.title }}</h2>
            <router-link :to="`/my-profile/edit-language/${language.languageId}`">
              <img class="languages-items_language-items_header-edit-icon" src="../../../assets/images/icons/edit-icon.svg" alt="Edit icon" />
            </router-link>
          </li>
          <li class="languages-items_language-items_level-of-knowledge">
            <span class="languages-items_language-items_level-of-knowledge-element">{{ mapProficiency(language.proficiency) }}</span>
          </li>
        </ul>
        <div class="languages-items_language-delete" @click="deleteLanguage(language.languageId)">
          <span class="languages-items_language-delete-inscription">Delete language</span>
        </div>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthenticationStore } from '@/stores/authentication'
import type { Language } from '@/types/my-profile/languages/Language'
import useGetAllLanguages from '@/composables/my-profile/languages/useGetAllLanguages'
import useDeleteLanguage from '@/composables/my-profile/languages/useDeleteLanguage'

export default defineComponent({
  setup() {
    const authenticationStore = useAuthenticationStore()
    const router = useRouter()
    const languages = ref<Language[]>([])

    async function loadLanguages() {
      const result = await useGetAllLanguages(authenticationStore.userId)

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        languages.value = result as Language[]
      }
    }

    function mapProficiency(level: string): string {
      switch (level) {
        case 'A1': return 'Beginner - A1'
        case 'A2': return 'Pre-Intermediate - A2'
        case 'B1': return 'Intermediate - B1'
        case 'B2': return 'Upper-Intermediate - B2'
        case 'C1': return 'Advanced - C1'
        case 'C2': return 'Mastery - C2'
        default: return level
      }
    }

    const deleteLanguage = async (languageId: string) => {
      const result = await useDeleteLanguage(languageId)

      if (!result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      languages.value = languages.value.filter(lang => lang.languageId !== languageId)
    }

    onMounted(() => loadLanguages())

    return { languages, mapProficiency, deleteLanguage }
  }
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.languages {
  margin: 50px 0 100px 100px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
  }

  &-header {
    list-style: none;
    display: flex;
    justify-content: space-between;
    align-content: center;

    &_inscription {
      &-element {
        font-size: 28px;
        margin-left: 6px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }

    &_add-button {
      display: flex;
      align-items: flex-end;

      &-element {
        display: flex;
        justify-content: center;
        align-items: center;
        border: 2px solid $grayBorderColor;
        border-radius: 10px;
        width: 100px;
        height: 40px;
        transition: transform 0.3s ease;

        @media (max-width: $breakpoint) {
          height: 36px;
        }

        &:hover {
          transform: scale(1.10);
        }

        &-icon {
          width: 30px;
          margin-right: 8px;
          color: $grayBorderColor;

          @media (max-width: $breakpoint) {
            width: 24px;
            margin-right: 6px;
          }
        }
        &-inscription {
          font-size: 20px;
          color: $grayBorderColor;

          @media (max-width: $breakpoint) {
            font-size: 16px;
          }
        }
      }

      &-link {
        text-decoration: none;
      }
    }
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;

    &_language {
      margin-top: 30px;
      padding: 50px 50px 0 50px;
      border: 2px $grayBorderColor solid;
      border-radius: 20px;

      @media (max-width: $breakpoint) {
        padding: 30px 30px 0 30px;
      }

      &-items {
        list-style: none;
        display: flex;
        flex-direction: column;

        &_header {
          display: flex;
          justify-content: space-between;
          align-items: center;

          &-name-of-language {
            font-size: 20px;

            @media (max-width: $breakpoint) {
              font-size: 16px;
            }
          }
          &-edit-icon {
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

        &_level-of-knowledge {

          &-element {
            font-size: 14px;
            color: $grayBorderColor;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_skills {
          margin-top: 20px;

           &-link {
            color: #000000;
            text-decoration: none;
          } 
        }
      }

      &-divider {
        border-bottom: 1px solid $grayBorderColor;
        width: 100%;
        height: 30px;
        margin-bottom: 30px;

        @media (max-width: $breakpoint) {
          height: 20px;
          margin-bottom: 20px;
        }
      }

      &-delete {
        display: flex;
        justify-content: center;
        align-content: center;
        border-top: 2px solid $grayBorderColor;
        margin: 40px -50px 0 -50px;
        padding: 14px 0;
        transition: background-color 0.3s ease;
        border-bottom-right-radius: 20px;
        border-bottom-left-radius: 20px;

        @media (max-width: $breakpoint) {
          margin: 40px -30px 0 -30px;
        }

        &-inscription {
          font-size: 14px;
          color: $errorColor;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }
        }

        &:hover {
          cursor: pointer;
          background-color: #ececec;
        }
      }
    }
  }
}
</style>