<template>
  <div class="languages">
    <ul class="languages-header">
      <li class="languages-header_inscription">
        <h1 class="languages-header_inscription-element">Languages</h1>
      </li>
    </ul>

    <ul class="languages-items">
      <li v-for="language in languages" :key="language.languageId" class="languages-items_language" :id="language.languageId">
        <ul class="languages-items_language-items">
          <li class="languages-items_language-items_header">
            <h2 class="languages-items_language-items_header-name-of-language">{{ language.title }}</h2>
          </li>
          <li class="languages-items_language-items_level-of-knowledge">
            <span class="languages-items_language-items_level-of-knowledge-element">{{ mapProficiency(language.proficiency) }}</span>
          </li>
        </ul>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import type { Language } from '@/types/my-profile/languages/Language'
import useGetAllLanguages from '@/composables/my-profile/languages/useGetAllLanguages'

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true,
    },
  },
  setup(props) {
    const router = useRouter()
    const languages = ref<Language[]>([])

    async function loadLanguages() {
      const result = await useGetAllLanguages(props.id)

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

    onMounted(() => loadLanguages())

    return { languages, mapProficiency }
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
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;

    &_language {
      margin-top: 30px;
      padding: 50px;
      border: 2px $grayBorderColor solid;
      border-radius: 20px;

      @media (max-width: $breakpoint) {
        padding: 30px;
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
  }

  &-load-more-languages  {
    display: flex;
    justify-content: center;
    align-items: center;
    border: 2px $grayBorderColor solid;
    border-radius: 20px;
    height: 50px;
    transition: background-color 0.3s ease;

    &:hover {
      cursor: pointer;
      background-color: #ececec;
    }

    &-link {
      color: #000000;
      text-decoration: none;
    }

    &-inscription {
      font-size: 14px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }
  }
}
</style>