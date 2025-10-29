<template>
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="event">
    <ul class="event-header">
      <li class="event-header_user-info">
        <router-link :to="`/user-profile/${event.userId}`" class="event-header_user-info-link">
          <img class="event-header_user-info-profile-image" :src="event.userImagePath" alt="User profile image">
          <span class="event-header_user-info-user-name">{{ event.userFirstName }} {{ event.userLastName }}</span>
        </router-link>  
      </li>
      <li class="event-header_publication-time">
        <span class="event-header_publication-time-element">{{ formatRelativeDate(event.createdAt) }}</span>
      </li>
    </ul>

    <h1 class="event-title">{{ event.title }}</h1>
    <span class="event-subjects">{{ event.subjects.join(', ') }}</span>

    <img v-if="event.imagePath" class="event-image" :src="event.imagePath" alt="Image of the event">

    <h3 v-if="event.searchPersonPanels.length == 1" class="event-searching-for">
      Searching for {{ event.searchPersonPanels.length }} person:
    </h3>
    <h3 v-if="event.searchPersonPanels.length > 1" class="event-searching-for">
      Searching for {{ event.searchPersonPanels.length }} people:
    </h3>

    <div v-for="(panel, index) in event.searchPersonPanels" :key="panel.id" :class="!isPositionPanelOpened[index] ? 'event-position' : 'event-position-opened'">
      <ul :class="!isPositionPanelOpened[index] ? 'event-position-header' : 'event-position-opened-header'">
        <li :class="!isPositionPanelOpened[index] ? 'event-position-header_left-side' : 'event-position-opened-header_left-side'">
          <img :class="!isPositionPanelOpened[index] ? 'event-position-header_left-side-icon' : 'event-position-opened-header_left-side-icon'" src="@/assets/images/icons/position-icon.svg" alt="Position icon">
          <span :class="!isPositionPanelOpened[index] ? 'event-position-header_left-side-name' : 'event-position-opened-header_left-side-name'">{{ panel.positionName }}</span>
        </li>
        <li :class="!isPositionPanelOpened[index] ? 'event-position-header_right-side' : 'event-position-opened-header_right-side'">
          <img @click="triggerPositionPanelOpening(index)" :class="!isPositionPanelOpened[index] ? 'event-position-header_right-side-arrow-icon' : 'event-position-opened-header_right-side-arrow-icon'" src="@/assets/images/icons/arrow-down-icon.svg" alt="Arrow down icon">
        </li>
      </ul>

      <the-skills v-if="panel.preferredSkills && panel.preferredSkills.length > 0" :skills="panel.preferredSkills" :class="!isPositionPanelOpened[index] ? 'event-position-skills' : 'event-position-opened-skills'" />

      <span :class="!isPositionPanelOpened[index] ? 'event-position-payment' : 'event-position-opened-payment'">Payment: {{ panel.payment || 'Unpaid' }}</span>

      <p :class="!isPositionPanelOpened[index] ? 'event-position-description' : 'event-position-opened-description'">{{ panel.description }}</p>

      <show-applications-button @click="showApplications(panel.id)" :class="!isPositionPanelOpened[index] ? 'event-position-button' : 'event-position-opened-button'" />
    </div>

    <span class="event-date-of-beginning">Date of beginning: {{ formatToReadableDate(event.beginningDate) }}</span>
    <p class="event-description">{{ event.description }}</p>

    <h2 class="event-comments-header">Comments</h2>
    <ul class="event-create-comment-bar">
      <li class="event-create-comment-bar_left-side">
        <input class="event-create-comment-bar_left-side-input" type="text" placeholder="Leave your comment..." v-model="inputFieldAddCommentContent">
        <svg @click="createComment" 
          class="event-create-comment-bar_left-side-icon"
          xmlns="http://www.w3.org/2000/svg"
          height="30px"
          viewBox="0 -960 960 960"
          width="30px">
          <path d="M120-160v-640l760 320-760 320Zm80-120 474-200-474-200v140l240 60-240 60v140Z" fill="#B1B1B1" />          
        </svg>
      </li>
      <li class="event-create-comment-bar_right-side">
        <div class="event-create-comment-bar_right-side-like">
          <img @click="toggleLike" v-if="!event.isLiked" class="event-create-comment-bar_right-side-like-icon" src="@/assets/images/icons/like-icon.svg" alt="Like icon">
          <svg @click="toggleLike" v-if="event.isLiked"  class="event-create-comment-bar_right-side-like-icon"
            xmlns="http://www.w3.org/2000/svg"
            height="30px"
            width="30px"
            viewBox="0 -960 960 960">
            <path d="m480-120-58-52q-101-91-167-157T150-447.5Q111-500 95.5-544T80-634q0-94 63-157t157-63q52 0 99 22t81 62q34-40 81-62t99-22q94 0 157 63t63 157q0 46-15.5 90T810-447.5Q771-395 705-329T538-172l-58 52" fill="red" />
          </svg>
          <span class="event-create-comment-bar_right-side-like-amount">{{ event.numberOfLikes }}</span>
        </div>
        <img @click="toggleSave" v-if="!event.isSaved" class="event-create-comment-bar_right-side-save-icon" src="@/assets/images/icons/save-icon.svg" alt="Save icon">
        <svg @click="toggleSave" v-else class="event-create-comment-bar_right-side-save-icon"
          xmlns="http://www.w3.org/2000/svg"
          height="30px"
          viewBox="0 -960 960 960"
          width="30px">
          <path d="M200-120v-640q0-33 23.5-56.5T280-840h400q33 0 56.5 23.5T760-760v640L480-240 200-120Z" fill="black" />
        </svg>
      </li>
    </ul>
    <ul class="event-comments">
      <li v-for="(comment, index) in event.comments" :key="comment.commentId" class="event-comments_comment">
        <ul class="event-comments_comment-header">
          <li class="event-comments_comment-header_left-side">
            <router-link :to="`/user-profile/${comment.userId}`" class="event-comments_comment-header_left-side-user-name-link">
              <img
                class="event-comments_comment-header_left-side-user-profile-image"
                :src="comment.userImagePath || '@/assets/images/icons/user-icon.jpg'"
                alt="User profile photo"
              >
              <span class="event-comments_comment-header_left-side-user-name">
                {{ comment.userFirstName }} {{ comment.userLastName }}
              </span>
            </router-link>
          </li>
          <li class="event-comments_comment-header_right-side">
            <span class="event-comments_comment-header_right-side-time-of-publication">
              {{ formatRelativeDate(comment.dateOfCreation) }}
            </span>
          </li>
        </ul>
        <p class="event-comments_comment-content">{{ comment.content }}</p>
        <button v-if="activeUserEmail == comment.userEmail" @click="deleteComment(comment.commentId)" class="event-comments_comment-delete-button">Delete comment</button>
        <div v-if="index !== event.comments.length - 1" class="event-comments_comment-divider"></div>
      </li>
      <li class="event-comments_load-more-comments-button" v-if="isLoadMoreCommentsButtonVisible" @click="loadMoreComments">Load more comments..</li>
    </ul>
    <div class="event-delete" @click="deleteActivity(event.id)">
      <span class="event-delete-inscription">Delete event</span>
    </div>
  </div> 
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from "vue";

import TheSkills from "../../TheSkills.vue";
import ShowApplicationsButton from "./ShowApplicationsButton.vue";
import LoadingSquare from "../../../LoadingSquare.vue";

import { useAuthenticationStore } from '@/stores/authentication'
import useGetEvent from '@/composables/forum/useGetEvent'
import { useRouter } from "vue-router";
import { Event } from "@/types/forum/Event";
import useRelativeDate from "@/composables/forum/useRelativeDate"
import useformatToReadableDate from "@/composables/useFormatToReadableDate";
import useToggleLikeOfActivity from "@/composables/forum/useToggleLikeOfActivity";
import useToggleSaveOfActivity from "@/composables/forum/useToggleSaveOfActivity";
import useCreateComment from "@/composables/forum/useCreateComment";
import useDeleteComment from "@/composables/forum/useDeleteComment";
import useGetCommentsPaged from "@/composables/forum/useGetCommentsPaged";
import useDeleteActivity from "@/composables/my-profile/activities/useDeleteActivity";

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true
    }
  },
  components: {
    TheSkills,
    LoadingSquare,
    ShowApplicationsButton
  },
  setup(props) {
    const authenticationStore = useAuthenticationStore()
    const activeUserEmail = authenticationStore.email
    const router = useRouter()
    const event = ref<Event>({} as Event)
    const isLoading = ref(true)
    const isPositionPanelOpened = ref<boolean[]>([false, false])
    const inputFieldAddCommentContent = ref<string>('')
    const isLoadMoreCommentsButtonVisible = ref<boolean>(true)
    let page = 1
    const pageSize = 3

    onMounted(async () => {
      const startTime = performance.now()

      const result = await useGetEvent(props.id)

      if (typeof result === 'object' && result !== null && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
        }
      } else {
        event.value = result as Event
        if (authenticationStore.userId != event.value.userId) {
          router.push(`/forum/event/${event.value.id}`)
        }
        isLoadMoreCommentsButtonVisible.value = event.value.comments.length == pageSize
        console.log(event.value)
      }

      const elapsed = performance.now() - startTime
      const remaining = 500 - elapsed
      if (remaining > 0) {
        setTimeout(() => { isLoading.value = false }, remaining)
      } else {
        isLoading.value = false
      }
    })

    const formatRelativeDate = (date: string) => {
      console.log(`relative date input: ${date}`)
      return useRelativeDate(date)
    }

    const formatToReadableDate = (date: string) => {
      return useformatToReadableDate(date)
    }

    const triggerPositionPanelOpening = (positionId: number) => {
      isPositionPanelOpened.value[positionId] = !isPositionPanelOpened.value[positionId]
    }

    const showApplications = (panelId: string) => {
      router.push({
        path: `/my-profile/activities/event/${props.id}/applications/${panelId}`
      })
    }

    const toggleLike = async () => {
      const result = await useToggleLikeOfActivity(event.value.id, authenticationStore.userId)

      if ("isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      if ("numberOfLikes" in result) {
        event.value.numberOfLikes = result.numberOfLikes
        event.value.isLiked = result.isLiked
      }
    }

    const toggleSave = async () => {
      const result = await useToggleSaveOfActivity(event.value.id, authenticationStore.userId)

      if ("isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      if ("isSaved" in result) {
        event.value.isSaved = result.isSaved
      }
    }

    const createComment = async () => {
      const result = await useCreateComment(event.value.id, authenticationStore.userId, inputFieldAddCommentContent.value)
    
      if ("isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      if ("commentId" in result) {
        console.log("Created comment: ", result)
        inputFieldAddCommentContent.value = ''

        event.value.comments.unshift({
          commentId: result.commentId,
          activityId: event.value.id,
          userId: result.userId,
          userEmail: result.userEmail,
          userFirstName: result.userFirstName,
          userLastName: result.userLastName,
          userImagePath: result.userImagePath,
          dateOfCreation: result.dateOfCreation,
          content: result.content
        })
      }
    }

    const deleteComment = async (commentId: string) => {
      const result = await useDeleteComment(commentId, authenticationStore.userId)
    
      if (!result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      event.value.comments = event.value.comments.filter(c => c.commentId !== commentId)
    }

    const loadMoreComments = async () => {
      const result = await useGetCommentsPaged(event.value.id, page, pageSize)

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        if (result.length < pageSize) {
          isLoadMoreCommentsButtonVisible.value = false
        }
        event.value.comments.push(...result)
        page++
      }
    }

    const deleteActivity = async (activityId: string) => {
      const result = await useDeleteActivity(activityId, authenticationStore.userId)
    
      if (!result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      router.push('/my-profile/activities')
    }

    return { event, formatRelativeDate, formatToReadableDate, isPositionPanelOpened, triggerPositionPanelOpening, showApplications, toggleLike, toggleSave, inputFieldAddCommentContent, createComment, activeUserEmail, deleteComment, isLoadMoreCommentsButtonVisible, loadMoreComments, deleteActivity, isLoading }
  }
});
</script>

<style lang="scss" scoped>
@use "@/assets/styles/config" as *;

.event {
  margin: 50px 0 100px 100px;
  padding: 0 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
    padding: 0 30px;
  }

  &-header {
    list-style: none;
    display: flex;
    justify-content: space-between;
    align-items: center;
    border-bottom: 2px solid $grayBorderColor;
    margin: 0 -50px;
    padding: 16px 50px;

    @media (max-width: $breakpoint) {
      margin: 0 -30px;
      padding: 14px 30px;
    }

    &_user-info {
      display: flex;
      align-items: center;

      &-link {
        display: flex;
        align-items: center;
        color: black;
        text-decoration: none;

        &:hover {
          text-decoration: underline;
        }
      }

      &-profile-image {
        width: 36px;
        margin-right: 10px;
        border-radius: 20px;
        border: 1px solid #000000;

        @media (max-width: $breakpoint) {
          width: 30px;
          margin-right: 8px;
          border-radius: 15px;
        }
      }
      &-user-name {
        font-size: 14px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }

    &_publication-time {

      &-element {
        font-size: 14px;
        color: $grayBorderColor;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }
  }

  &-title {
    font-size: 24px;
    margin-top: 30px;

    @media (max-width: $breakpoint) {
      font-size: 20px;
      margin-top: 20px;
    }
  }

  &-subjects {
    font-size: 14px;
    display: block;
    color: $linkColor;
    margin-bottom: 20px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
      margin-bottom: 14px;
    }
  }

  &-image {
    width: 100%;
  }

  &-searching-for {
    font-size: 16px;
    font-weight: normal;
    margin-top: 20px;

    @media (max-width: $breakpoint) {
      font-size: 14px;
    }
  }

  &-position {
    border: 2px solid $grayBorderColor;
    padding: 10px 20px;
    border-radius: 12px;
    margin-top: 14px;
    transition: all 0.5s ease;

    &-header {
      list-style: none;
      display: flex;
      justify-content: space-between;
      align-items: center;

      &_left-side {
        display: flex;
        justify-content: space-between;
        align-items: center;

        &-icon {
          width: 30px;
          margin-right: 10px;

          @media (max-width: $breakpoint) {
            width: 24px;
          }
        }
        &-name {
          font-size: 14px;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }
        }
      }

      &_right-side {
        height: 30px;
        transition: transform 0.3s ease;

        @media (max-width: $breakpoint) {
          height: 24px;
        }

        &:hover {
          cursor: pointer;
          transform: scale(1.20);
        }

        &-arrow-icon {
          width: 30px;

          @media (max-width: $breakpoint) {
            width: 24px;
          }
        }
      }
    }

    &-skills {
      display: none;
    }

    &-payment {
      display: none;
    }

    &-description {
      display: none;
    }

    &-button {
      display: none;
    }
  }
  &-position-opened {
    border: 2px solid $grayBorderColor;
    padding: 10px 20px 20px 20px;
    border-radius: 12px;
    margin-top: 10px;
    transition: all 0.3s ease;

    &-header {
      list-style: none;
      display: flex;
      justify-content: space-between;
      align-items: center;

      &_left-side {
        display: flex;
        justify-content: space-between;
        align-items: center;

        &-icon {
          width: 30px;
          margin-right: 10px;

          @media (max-width: $breakpoint) {
            width: 24px;
          }
        }

        &-name {
          font-size: 14px;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }
        }
      }

      &_right-side {
        height: 30px;
        transition: transform 0.3s ease;

        @media (max-width: $breakpoint) {
          height: 24px;
        }

        &:hover {
          cursor: pointer;
          transform: scale(1.20);
        }

        &-arrow-icon {
          width: 30px;
          transform: rotate(180deg);

          @media (max-width: $breakpoint) {
            width: 24px;
          }
        }
      }
    }

    &-skills {
      display: block;
      margin-top: 20px;
    }
    &-payment {
      font-size: 14px;
      display: block;
      margin-top: 10px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }
    &-description {
      display: block;
      font-size: 14px;
      margin-top: 10px;
      white-space: pre-wrap;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }
    &-button {
      margin-top: 20px;
    }
  }

  &-date-of-beginning {
    font-size: 14px;
    display: block;
    margin-top: 30px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }

  &-description {
    font-size: 14px;
    margin-top: 20px;
    white-space: pre-wrap;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }

  &-comments-header {
    font-size: 20px;
    margin-top: 40px;

    @media (max-width: $breakpoint) {
      font-size: 16px;
    }
  }

  &-create-comment-bar {
    list-style: none;
    display: flex;
    justify-content: space-between;
    margin-top: 20px;

    &_left-side {

      &-input {
        border: 2px solid $grayBorderColor;
        border-radius: 12px;
        height: 34px;
        width: 46vw;
        padding: 10px 46px 10px 18px;
        font-size: 14px;

        &:active {
          border: 2px solid $grayBorderColor;
        }

        @media (max-width: $breakpoint) {
          font-size: 12px;
          width: 54vw;
          padding: 10px 46px 10px 14px;
        }
      }

      &-icon {
        margin: 0 0 -11px -30px;
        padding: 0 10px 4px 0;

        &:hover {
          cursor: pointer;
        }
      }
    }

    &_right-side {
      display: flex;
      align-items: center;

      &-like {
        display: flex;
        align-items: center;
        margin-right: 16px;

        &-icon {
          width: 30px;
          margin-right: 2px;
          transition: transform 0.3s ease;

          @media (max-width: $breakpoint) {
            width: 28px;
          }

          &:hover {
            cursor: pointer;
            transform: scale(1.20);
          }
        }

        &-amount {
          font-size: 14px;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }
        }
      }

      &-save-icon {
        width: 30px;
        transition: transform 0.3s ease;

        @media (max-width: $breakpoint) {
          width: 28px;
        }

        &:hover {
          cursor: pointer;
          transform: scale(1.20);
        }
      }
    }
  }

  &-comments {
    list-style: none;
    display: flex;
    flex-direction: column;
    margin-top: 40px;

    &_comment {

      &-header {
        list-style: none;
        display: flex;
        justify-content: space-between;
        align-items: center;

        &_left-side {
          display: flex;
          align-items: center;

          &-user-profile-image {
            width: 30px;
            margin-right: 10px;
            border-radius: 30px;
            border: 1px solid #000000;
          }

          &-user-name {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }

            &-link {
              display: flex;
              align-items: center;
              color: black;
              text-decoration: none;
              
              &:hover {
                text-decoration: underline;
              }
            }
          }
        }

        &_right-side {

          &-time-of-publication {
            font-size: 14px;
            color: $grayBorderColor;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }
      }

      &-content {
        font-size: 14px;
        margin-top: 14px;
        padding: 0 20px;
        white-space: pre-wrap;

        @media (max-width: $breakpoint) {
          font-size: 12px;
          padding: 0 10px;
        }
      }

      &-delete-button {
        border: 2px solid $errorColor;
        color: $errorColor;
        background-color: $mainBackgroundColor;
        width: 140px;
        height: 36px;
        border-radius: 12px;
        transition: background-color 0.3s ease;
        font-size: 14px;
        margin: 20px 20px 0 20px;

        &:hover {
          background-color: #ffe4e4;
        }

        @media (max-width: $breakpoint) {
          width: 130px;
          height: 34px;
          font-size: 12px;
          margin: 10px 10px 0 10px;
        }
      }

      &-divider {
        border-bottom: 1px solid $grayBorderColor;
        height: 24px;
        margin-bottom: 24px;
      }
    }

    &_load-more-comments-button {
      font-size: 14px;
      display: flex;
      justify-self: center;
      align-self: center;
      margin-top: 20px;

      &:hover {
        text-decoration: underline;
        cursor: pointer;
      }

      @media (max-width: $breakpoint) {
        font-size: 12px;
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
</style>