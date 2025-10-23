<template>
  <div class="certifications">
    <ul class="certifications-header">
      <li class="certifications-header_inscription">
        <h1 class="certifications-header_inscription-element">Certifications</h1>
      </li>
    </ul>

    <ul class="certifications-items">
      <li v-for="certification in certifications" :key="certification.certificationId" class="certifications-items_certification" :id="certification.certificationId">
        <ul class="certifications-items_certification-items">
          <li class="certifications-items_certification-items_title">
            <ul class="certifications-items_certification-items_title-items">
              <li class="certifications-items_certification-items_title-items_name">
                <h2 class="certifications-items_certification-items_title-items_name-element">{{ certification.title }}</h2>
              </li>
            </ul>
          </li>

          <li class="certifications-items_certification-items_organization-that-issued">
            <span class="certifications-items_certification-items_organization-that-issued-element">{{ certification.issuingOrganization }}</span>
          </li>

          <li class="certifications-items_certification-items_issued">
            <span class="certifications-items_certification-items_issued-element">Issued {{ formatDate(certification.issueDate) }}</span>
          </li>

          <li class="certifications-items_certification-items_source">
            <img v-if="certification.imagePath" class="certifications-items_certification-items_source-image" :src="certification.imagePath" alt="Certification image">
            <span class="certifications-items_certification-items_source-id">
              Id: 
              <a :href="certification.credentialUrl" target="_blank" rel="noopener noreferrer" class="certifications-items_certification-items_source-id-link">
                {{ certification.credentialId }}
              </a>
            </span>
          </li>

          <li class="certifications-items_certification-items_skills">
            <the-skills :skills="certification.skillTitles"></the-skills>
          </li>
        </ul>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'

import TheSkills from '../TheSkills.vue'
import { Certification } from '@/types/my-profile/certifications/Certification'
import useGetAllCertifications from '@/composables/my-profile/certifications/useGetAllCertifications'
import { useRouter } from 'vue-router'

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true,
    },
  },
  components: { TheSkills },
  setup(props) {   
    const router = useRouter()
    const certifications = ref<Certification[]>([])

    async function loadCertifications() {
      const result = await useGetAllCertifications(props.id)
      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
        }
      } else {
        certifications.value = result as Certification[]
      }
    }

    function formatDate(date: string | null): string {
      if (!date) return ''
      return new Date(date).toLocaleDateString('en-US', { month: 'short', year: 'numeric' })
    }

    onMounted(() => loadCertifications())

    return { certifications, formatDate }
  }
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.certifications {
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

    &_certification {
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

        &_title {
          &-items {
            list-style: none;
            display: flex;
            justify-content: space-between;
            align-items: center;

            &_name {

              &-element {
                font-size: 20px;

                @media (max-width: $breakpoint) {
                  font-size: 16px;
                }
              }
            }
          }
        }

        &_organization-that-issued {
          &-element {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_source {
          display: flex;
          align-items: center;
          margin: 4px 0;

          &-image {
            width: 140px;
            margin-right: 20px;

            @media (max-width: $breakpoint) {
              width: 100px;
              margin-right: 14px;
            }
          }

          &-id {
            font-size: 14px;

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

        &_issued {

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

  &-load-more-certifications {
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
