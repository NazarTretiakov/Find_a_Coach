<template>
  <div class="certifications-section">
    <ul class="certifications-section-items">
      <li class="certifications-section-items_header">
        <ul class="certifications-section-items_header-items">
          <li class="certifications-section-items_header-items_incription">
            <h1 class="certifications-section-items_header-items_incription-element">Certifications</h1>
          </li>
        </ul>
      </li>

      <li v-for="(certification, index) in certifications" :key="certification.certificationId" class="certifications-section-items_certification">
        <ul class="certifications-section-items_certification-items">
          <li class="certifications-section-items_certification-items_title">
            <ul class="certifications-section-items_certification-items_title-items">
              <li class="certifications-section-items_certification-items_title-items_name">
                <router-link :to="`/user-profile/${id}/certifications/#${certification.certificationId}`" class="certifications-section-items_certification-items_title-items_name-link">
                  <h2 class="certifications-section-items_certification-items_title-items_name-element">{{ certification.title }}</h2>
                </router-link>
              </li>
              <li class="certifications-section-items_certification-items_title-items_issued">
                <span class="certifications-section-items_certification-items_title-items_issued-element">Issued {{ formatDate(certification.issueDate) }}</span>
              </li>
            </ul>
          </li>

          <li class="certifications-section-items_certification-items_organization-that-issued">
            <span class="certifications-section-items_certification-items_organization-that-issued-element">{{ certification.issuingOrganization }}</span>
          </li>

          <li class="certifications-section-items_certification-items_source">
            <img v-if="certification.imagePath" class="certifications-section-items_certification-items_source-image" :src="certification.imagePath" alt="Certification image">
            <span class="certifications-section-items_certification-items_source-id">Id: <a :href="certification.credentialUrl" target="_blank" class="certifications-section-items_certification-items_source-id-link">{{ certification.credentialId }}</a></span>
          </li>

          <li class="certifications-section-items_certification-items_skills">
            <router-link :to="`/user-profile/${id}/certifications/#${certification.certificationId}`" class="certifications-section-items_certification-items_skills-link">
              <the-skills :skills="certification.skillTitles"></the-skills>
            </router-link>
          </li>
        </ul>

        <div v-if="index !== certifications.length - 1" class="certifications-section-items_certification-divider"></div>
      </li>
    </ul>

    <router-link class="certifications-section-items_show-all-certifications-link" :to="`/user-profile/${id}/certifications`">
      <div class="certifications-section-items_show-all-certifications">
        <span class="certifications-section-items_show-all-certifications-element">Show all certifications</span>
        <img class="certifications-section-items_show-all-certifications-icon-arrow-forward" src="../../assets/images/icons/arrow-forward-icon.svg" alt="Arrow forward icon">
      </div>
    </router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import TheSkills from './TheSkills.vue'
import { Certification } from '@/types/my-profile/certifications/Certification'
import useGetLastTwoCertifications from '@/composables/my-profile/certifications/useGetLastTwoCertifications'

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
      const result = await useGetLastTwoCertifications(props.id)

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        certifications.value = result as Certification[]
      }
    }

    function formatDate(date: string | null): string {
      if (!date) return ''
      const formattedDate = new Date(date).toLocaleDateString('en-US', {
        year: 'numeric',
        month: 'short'
      })
      return formattedDate
    }

    onMounted(() => loadCertifications())

    return { certifications, formatDate }
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.certifications-section {
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
      }
    }

    &_certification {
      
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

            &_issued {
              
              @media (max-width: $breakpoint) {
                display: none;
              }

              &-element {
                font-size: 14px;
                color: $grayBorderColor;
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
          display: none;
          
          @media (max-width: $breakpoint) {
            display: block;
          }

          &-element {
            font-size: 12px;
            color: $grayBorderColor;
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

    &_show-all-certifications {
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