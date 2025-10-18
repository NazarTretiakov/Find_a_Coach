<template>
  <div class="projects-section">
    <ul class="projects-section-items">
      <li class="projects-section-items_header">
        <ul class="projects-section-items_header-items">
          <li class="projects-section-items_header-items_incription">
            <h1 class="projects-section-items_header-items_incription-element">Projects</h1>
          </li>
          <li class="projects-section-items_header-items_buttons">
            <ul class="projects-section-items_header-items_buttons-items">
              <li class="projects-section-items_header-items_buttons-items_add">
                <router-link to="/my-profile/add-project" class="projects-section-items_header-items_buttons-items_add-link">
                  <img src="../../assets/images/icons/add-icon.svg" alt="Add icon" class="projects-section-items_header-items_buttons-items_add-element" />
                </router-link>
              </li>
              <li class="projects-section-items_header-items_buttons-items_edit">
                <router-link to="/my-profile/projects" class="projects-section-items_header-items_buttons-items_edit-link">
                  <img src="../../assets/images/icons/edit-icon.svg" alt="Edit icon" class="projects-section-items_header-items_buttons-items_edit-element" />
                </router-link>
              </li>
            </ul>
          </li>
        </ul>
      </li>

      <li v-for="(project, index) in projects" :key="project.projectId" class="projects-section-items_project">
        <ul class="projects-section-items_project-items">
          <li class="projects-section-items_project-items_title">
            <ul class="projects-section-items_project-items_title-items">
              <li class="projects-section-items_project-items_title-items_name">
                <router-link :to="`/my-profile/projects/#${project.projectId}`" class="projects-section-items_project-items_title-items_name-link">
                  <h2 class="projects-section-items_project-items_title-items_name-element">{{ project.title }}</h2>
                </router-link>
              </li>
              <li class="projects-section-items_project-items_title-items_time">
                <span class="projects-section-items_project-items_title-items_time-element">{{ formatDate(project.startDate) }} - {{ formatDate(project.endDate) }}</span>
              </li>
            </ul>
          </li>

          <li v-if="project.participants.length > 0" class="projects-section-items_project-items_participated-with">
            <span class="projects-section-items_project-items_participated-with-element">Participated with: {{ project.participants.join(', ') }}</span>
          </li>

          <li class="projects-section-items_project-items_type">
            <span class="projects-section-items_project-items_type-element">{{ getReadableRelatedTo(project.relatedTo) }}</span>
          </li>

          <li class="projects-section-items_project-items_location">
            <span class="projects-section-items_project-items_location-element">{{ project.location }}</span>
          </li>

          <li v-if="project.description" class="projects-section-items_project-items_description">
            <p class="projects-section-items_project-items_description-element">{{ project.description }}</p>
          </li>

          <li v-if="project.skillTitles && project.skillTitles.length > 0" class="projects-section-items_project-items_skills">
            <router-link :to="`/my-profile/projects/#${project.projectId}`" class="projects-section-items_project-items_skills-link">
              <the-skills :skills="project.skillTitles" />
            </router-link>
          </li>
        </ul>

        <div v-if="index !== projects.length - 1" class="projects-section-items_project-divider"></div>
      </li>
    </ul>

    <router-link class="projects-section-items_show-all-projects-link" to="/my-profile/projects">
      <div class="projects-section-items_show-all-projects">
        <span class="projects-section-items_show-all-projects-element">Show all projects</span>
        <img class="projects-section-items_show-all-projects-icon-arrow-forward" src="../../assets/images/icons/arrow-forward-icon.svg" alt="Arrow forward icon" />
      </div>
    </router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import TheSkills from './TheSkills.vue'
import { useAuthenticationStore } from '@/stores/authentication'
import type { Project } from '@/types/my-profile/projects/Project'
import useGetLastTwoProjects from '@/composables/my-profile/projects/useGetLastTwoProjects'

export default defineComponent({
  components: { TheSkills },
  setup() {
    const authenticationStore = useAuthenticationStore()
    const router = useRouter()
    const projects = ref<Project[]>([])

    async function loadProjects() {
      const result = await useGetLastTwoProjects(authenticationStore.userId)

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        projects.value = result as Project[]
        console.log('Loaded projects:', projects.value)
      }
    }

    function formatDate(date: Date | string | null): string {
      if (!date) return ''
      return new Date(date).toLocaleDateString('en-US', { year: 'numeric', month: 'short' })
    }

    function getReadableRelatedTo(value: string): string {
      switch (value) {
        case 'Job': return 'Job-related project'
        case 'Education': return 'Education-related project'
        case 'Event': return 'Event project'
        case 'Other': return 'Other project'
        default: return value
      }
    }

    onMounted(() => loadProjects())

    return { projects, formatDate, getReadableRelatedTo }
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.projects-section {
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

    &_project {
      
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

            &_time {
              
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

        &_participated-with {

          &-element {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }

            &-person-link {
              color: $linkColor;
              text-decoration: none;

              &:hover {
                text-decoration: underline;
              }
            }
          }
        }

        &_type {

          &-element {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_location {

          &-element {
            font-size: 14px;
            color: $grayBorderColor;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_time {
          display: none;
          
          @media (max-width: $breakpoint) {
            display: block;
          }

          &-element {
            font-size: 12px;
            color: $grayBorderColor;
          }
        }

        &_description {
          margin-top: 20px;

          @media (max-width: $breakpoint) {
            margin-top: 10px;
          }

          &-element {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_skills {
          margin-top: 20px;

          @media (max-width: $breakpoint) {
            margin-top: 10px;
          }

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

    &_show-all-projects {
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