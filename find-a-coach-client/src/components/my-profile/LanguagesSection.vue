<template>
  <div class="languages-section">
    <ul class="languages-section-items">
      <li class="languages-section-items_header">
        <ul class="languages-section-items_header-items">
          <li class="languages-section-items_header-items_incription"><h1 class="languages-section-items_header-items_incription-element">Languages</h1></li>
          <li class="languages-section-items_header-items_buttons">
            <ul class="languages-section-items_header-items_buttons-items">
              <li class="languages-section-items_header-items_buttons-items_add"><router-link to="/my-profile/add-language" class="languages-section-items_header-items_buttons-items_add-link"><img src="../../assets/images/icons/add-icon.svg" alt="Add icon" class="languages-section-items_header-items_buttons-items_add-element" /></router-link></li>
              <li class="languages-section-items_header-items_buttons-items_edit"><router-link to="/my-profile/languages" class="languages-section-items_header-items_buttons-items_edit-link"><img src="../../assets/images/icons/edit-icon.svg" alt="Edit icon" class="languages-section-items_header-items_buttons-items_edit-element" /></router-link></li>
            </ul>
          </li>
        </ul>
      </li>

      <li v-for="(language, index) in languages" :key="language.languageId" class="languages-section-items_language">
        <ul class="languages-section-items_language-items">
          <li class="languages-section-items_language-items_name"><router-link :to="`/my-profile/languages/#${language.languageId}`" class="languages-section-items_language-items_name-link"><h2 class="languages-section-items_language-items_name-element">{{ language.title }}</h2></router-link></li>
          <li class="languages-section-items_language-items_level-of-knowledge"><span class="languages-section-items_language-items_level-of-knowledge-element">{{ mapProficiency(language.proficiency) }}</span></li>
        </ul>
        <div v-if="index !== languages.length - 1" class="languages-section-items_language-divider"></div>
      </li>
    </ul>

    <router-link class="languages-section-items_show-all-languages-link" to="/my-profile/languages">
      <div class="languages-section-items_show-all-languages">
        <span class="languages-section-items_show-all-languages-element">Show all languages</span>
        <img class="languages-section-items_show-all-languages-icon-arrow-forward" src="../../assets/images/icons/arrow-forward-icon.svg" alt="Arrow forward icon" />
      </div>
    </router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthenticationStore } from '@/stores/authentication'
import type { Language } from '@/types/my-profile/languages/Language'
import useGetTwoLanguages from '@/composables/my-profile/languages/useGetLastTwoLanguages'
import useConvertLanguageProficiency from '@/composables/my-profile/languages/useConvertLanguageProficiency'

export default defineComponent({
  setup() {
    const authenticationStore = useAuthenticationStore()
    const router = useRouter()
    const languages = ref<Language[]>([])

    async function loadLanguages() {
      const result = await useGetTwoLanguages(authenticationStore.userId)
      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        languages.value = result as Language[]
      }
    }

    function mapProficiency(proficiency: string): string {
      return useConvertLanguageProficiency(proficiency)
    }

    onMounted(() => loadLanguages())
    return { languages, mapProficiency }
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.languages-section {
  margin: 0 0 30px 100px;
  padding: 50px 50px 0 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 0 10px;
    padding: 0;
    border: none;
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;

    &_header {
      margin-bottom: 30px;

      @media (max-width: $breakpoint) {
        margin-bottom: 14px;
      }

      &-items {
        list-style: none;
        display: flex;
        justify-content: space-between;
        align-content: center;

        &_incription {

          &-element {
            font-size: 24px;

            @media (max-width: $breakpoint) {
              font-size: 20px;
            }
          }
        }

        &_buttons {
          width: 100px;

          @media (max-width: $breakpoint) {
            width: 80px;
          }

          &-items {
            list-style: none;
            display: flex;
            justify-content: space-between;
            align-content: center;

            &_add {
              &-element {
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

            &_edit {
              &-element {
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
        }
      }
    }

    &_language {
      
      &-items {
        list-style: none;
        display: flex;
        flex-direction: column;

        &_name {

          &-link {
            color: #000000;
            text-decoration: none;
          } 
          &-element {
            font-size: 20px;

            @media (max-width: $breakpoint) {
              font-size: 16px;
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
    }

    &_show-all-languages {
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
        border: none;
        margin: 0;
        padding: 0;
        display: flex;
        align-items: center;
        margin: 16px 0;
      }

      &-element {
        font-size: 14px;
      }

      &-icon-arrow-forward {
        display: none;
        margin-left: 8px;

        @media (max-width: $breakpoint) {
          display: inline;
        }
      }

      &:hover {
        cursor: pointer;
        background-color: #ececec;
      }

      &-link {
        color: #000000;
        text-decoration: none;
      }
    }
  }
}
</style>