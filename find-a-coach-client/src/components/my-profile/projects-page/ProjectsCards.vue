<template>
  <div class="projects">
    <ul class="projects-header">
      <li class="projects-header_inscription">
        <h1 class="projects-header_inscription-element">Projects</h1>
      </li>
      <li class="projects-header_add-button">
        <router-link to="/my-profile/add-project" class="projects-header_add-button-link">
          <button class="projects-header_add-button-element">
            <svg class="projects-header_add-button-element-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
              <path d="M12 5v14m-7-7h14" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
            <span class="projects-header_add-button-element-inscription">Add</span>
          </button>
        </router-link>
      </li>
    </ul>

    <ul class="projects-items">
      <li v-for="project in projects" :key="project.projectId" class="projects-items_project" :id="project.projectId">
        <ul class="projects-items_project-items">
          <li class="projects-items_project-items_title">
            <ul class="projects-items_project-items_title-items">
              <li class="projects-items_project-items_title-items_name">
                <h2 class="projects-items_project-items_title-items_name-element">{{ project.title }}</h2>
              </li>
              <li class="projects-items_project-items_title-items_edit">
                <router-link :to="`/my-profile/edit-project/${project.projectId}`">
                  <img class="projects-items_project-items_title-items_edit-element" src="@/assets/images/icons/edit-icon.svg" alt="Edit icon"/>
                </router-link>
              </li>
            </ul>
          </li>

          <li class="projects-items_project-items_participated-with">
            <span class="projects-items_project-items_participated-with-element">
              Participated with: {{ project.participants.join(', ') }}
            </span>
          </li>

          <li class="projects-items_project-items_type">
            <span class="projects-items_project-items_type-element">{{ getReadableRelatedTo(project.relatedTo) }}</span>
          </li>

          <li class="projects-items_project-items_location">
            <span class="projects-items_project-items_location-element">{{ project.location }}</span>
          </li>

          <li class="projects-items_project-items_time">
            <span class="projects-items_project-items_time-element">{{ formatDate(project.startDate) }} - {{ formatDate(project.endDate) }}</span>
          </li>

          <li v-if="project.description" class="projects-items_project-items_description">
            <p class="projects-items_project-items_description-element" v-html="project.description"></p>
          </li>

          <li v-if="project.skillTitles && project.skillTitles.length > 0" class="projects-items_project-items_skills">
            <the-skills :skills="project.skillTitles"></the-skills>
          </li>
        </ul>

        <div class="projects-items_project-delete" @click="deleteProject(project.projectId)">
          <span class="projects-items_project-delete-inscription">Delete project</span>
        </div>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import TheSkills from '../TheSkills.vue'
import { Project } from '@/types/my-profile/projects/Project'
import useGetAllProjects from '@/composables/my-profile/projects/useGetAllProjects'
import { useAuthenticationStore } from '@/stores/authentication'
import useDeleteProject from '@/composables/my-profile/projects/useDeleteProject'

export default defineComponent({
  components: { TheSkills },
  setup() {
    const projects = ref<Project[]>([])
    const authenticationStore = useAuthenticationStore()
    const router = useRouter()

    const loadProjects = async () => {
      const result = await useGetAllProjects(authenticationStore.userId)
      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        projects.value = result as Project[]
      }
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

    const formatDate = (date: Date | string | null) => {
      if (!date) return ''
      return new Date(date).toLocaleDateString('en-US', { year: 'numeric', month: 'short' })
    }

    const deleteProject = async (projectId: string) => {
      const result = await useDeleteProject(projectId)
    
      if (!result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      projects.value = projects.value.filter(project => project.projectId !== projectId)
    }

    onMounted(loadProjects)

    return { projects, formatDate, getReadableRelatedTo, deleteProject }
  }
})
</script>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.projects {
  margin: 50px 0 100px 100px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
  }

  &-header {
    list-style: none;
    display: flex;
    justify-content: space-between;
    align-content: center;
    margin-bottom: 30px;

    @media (max-width: $breakpoint) {
      margin-bottom: 20px;
    }

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

    &_project {
      margin-bottom: 30px;
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

        &_participated-with {

          &-element {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
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

          &-element {
            font-size: 14px;
            color: $grayBorderColor;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
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