<template>
  <div class="projects">
    <ul class="projects-header">
      <li class="projects-header_inscription">
        <h1 class="projects-header_inscription-element">Projects</h1>
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

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true,
    },
  },
  components: { TheSkills },
  setup(props) {
    const projects = ref<Project[]>([])
    const router = useRouter()

    const loadProjects = async () => {
      const result = await useGetAllProjects(props.id)
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

    onMounted(loadProjects)

    return { projects, formatDate, getReadableRelatedTo }
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

    &_project {
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
    }
  }

  &-load-more-projects {
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