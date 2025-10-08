<template>
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="survey">
    <ul class="survey-header">
      <li class="survey-header_user-info">
        <router-link :to="`/user-profile/${survey.userId}`" class="survey-header_user-info-link">
          <img class="survey-header_user-info-profile-image" :src="survey.userImagePath" alt="User profile image">
          <span class="survey-header_user-info-user-name">{{ survey.userFirstName }} {{ survey.userLastName }}</span>
        </router-link>  
      </li>
      <li class="survey-header_publication-time">
        <span class="survey-header_publication-time-element">{{ formatDate(survey.createdAt) }}</span>
      </li>
    </ul>

    <h1 class="survey-title">{{ survey.title }}</h1>
    <span class="survey-subjects">{{ survey.subjects?.join(', ') }}</span>

    <img v-if="survey.imagePath" class="survey-image" :src="survey.imagePath" alt="Image of the survey">

    <p class="survey-description">{{ survey.description }}</p>

    <div class="survey-options" v-if="survey.options && survey.options.length > 0">
      <h2 class="survey-options-header">Select an option to view the survey results</h2>

      <div v-for="(option, index) in survey.options" :key="index" class="survey-options-survey-option">
        <div class="survey-options-survey-option-incription">
          <span class="survey-options-survey-option-incription-name">{{ option.inscription }}</span>
          <span v-if="selectedAnswer !== null" class="survey-options-survey-option-incription-percent-of-votes">
            {{ getPercentage(answers[index].votes).toFixed(1) }}%
          </span>
        </div>

        <v-progress-linear
          :model-value="selectedAnswer !== null ? getPercentage(answers[index].votes) : 0"
          :color="selectedAnswer !== null ? '#234CA2' : 'grey'"
          bg-color="grey"
          height="16"
          rounded
          @click="vote(index)"
          :class="selectedAnswer !== null ? 'survey-options-survey-option-progress-bar-selected' : 'survey-options-survey-option-progress-bar'"
        ></v-progress-linear>
      </div>
    </div>

    <h2 class="survey-comments-header">Comments</h2>

    <ul class="survey-create-comment-bar">
      <li class="survey-create-comment-bar_left-side">
        <input class="survey-create-comment-bar_left-side-input" type="text" placeholder="Leave your comment..." v-model="inputFieldAddCommentContent">
        <svg @click="createComment"
          class="survey-create-comment-bar_left-side-icon"
          xmlns="http://www.w3.org/2000/svg"
          height="30px"
          viewBox="0 -960 960 960"
          width="30px">
          <path d="M120-160v-640l760 320-760 320Zm80-120 474-200-474-200v140l240 60-240 60v140Z" fill="#B1B1B1" />          
        </svg>
      </li>

      <li class="survey-create-comment-bar_right-side">
        <div class="survey-create-comment-bar_right-side-like">
          <img @click="toggleLike" v-if="!survey.isLiked" class="survey-create-comment-bar_right-side-like-icon" src="../../../../assets/images/icons/like-icon.svg" alt="Like icon">
          <svg @click="toggleLike" v-if="survey.isLiked" class="survey-create-comment-bar_right-side-like-icon"
            xmlns="http://www.w3.org/2000/svg"
            height="30px"
            width="30px"
            viewBox="0 -960 960 960">
            <path d="m480-120-58-52q-101-91-167-157T150-447.5Q111-500 95.5-544T80-634q0-94 63-157t157-63q52 0 99 22t81 62q34-40 81-62t99-22q94 0 157 63t63 157q0 46-15.5 90T810-447.5Q771-395 705-329T538-172l-58 52" fill="red" />
          </svg>
          <span class="survey-create-comment-bar_right-side-like-amount">{{ survey.numberOfLikes }}</span>
        </div>

        <img @click="toggleSave" v-if="!survey.isSaved" class="survey-create-comment-bar_right-side-save-icon" src="../../../../assets/images/icons/save-icon.svg" alt="Save icon">
        <svg @click="toggleSave" v-else class="survey-create-comment-bar_right-side-save-icon"
          xmlns="http://www.w3.org/2000/svg"
          height="30px"
          viewBox="0 -960 960 960"
          width="30px">
          <path d="M200-120v-640q0-33 23.5-56.5T280-840h400q33 0 56.5 23.5T760-760v640L480-240 200-120Z" fill="black" />
        </svg>
      </li>
    </ul>

    <ul class="survey-comments">
      <li v-for="(comment, index) in survey.comments" :key="comment.commentId" class="survey-comments_comment">
        <ul class="survey-comments_comment-header">
          <li class="survey-comments_comment-header_left-side">
            <router-link :to="`/user-profile/${comment.userId}`" class="survey-comments_comment-header_left-side-user-name-link">
              <img class="survey-comments_comment-header_left-side-user-profile-image" :src="comment.userImagePath" alt="User profile photo">
              <span class="survey-comments_comment-header_left-side-user-name">{{ comment.userFirstName }} {{ comment.userLastName }}</span>
            </router-link>
          </li>
          <li class="survey-comments_comment-header_right-side">
            <span class="survey-comments_comment-header_right-side-time-of-publication">{{ formatDate(comment.dateOfCreation) }}</span>
          </li>
        </ul>
        <p class="survey-comments_comment-content">{{ comment.content }}</p>
        <button v-if="activeUserEmail == comment.userEmail" @click="deleteComment(comment.commentId)" class="survey-comments_comment-delete-button">Delete comment</button>
        <div v-if="index !== survey.comments.length - 1" class="survey-comments_comment-divider"></div>
      </li>
      <li class="survey-comments_load-more-comments-button" v-if="isLoadMoreCommentsButtonVisible" @click="loadMoreComments">Load more comments..</li>
    </ul>

    <div class="survey-delete" @click="deleteActivity(survey.id)">
      <span class="survey-delete-inscription">Delete event</span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, computed } from "vue"
import LoadingSquare from "../../../LoadingSquare.vue"
import { useRouter } from "vue-router"

import useGetSurvey from "@/composables/forum/useGetSurvey"
import useToggleLikeOfActivity from "@/composables/forum/useToggleLikeOfActivity"
import useToggleSaveOfActivity from "@/composables/forum/useToggleSaveOfActivity"
import { useAuthenticationStore } from "@/stores/authentication"
import useRelativeDate from "@/composables/forum/useRelativeDate"
import useCreateComment from "@/composables/forum/useCreateComment"
import useDeleteComment from "@/composables/forum/useDeleteComment"
import useGetCommentsPaged from "@/composables/forum/useGetCommentsPaged"
import useDeleteActivity from "@/composables/my-profile/activities/useDeleteActivity"
import type { Answer } from "@/types/survey/Answer"
import type { Survey } from "@/types/forum/Survey"

export default defineComponent({
  props: {
    id: {
      type: String,
      required: true,
    },
  },
  components: { 
    LoadingSquare
  },
  setup(props) {
    const authenticationStore = useAuthenticationStore()
    const activeUserEmail = authenticationStore.email
    const router = useRouter()
    const isLoading = ref(true)
    const survey = ref<Survey>({} as Survey)
    const selectedAnswer = ref<string | null>(null)
    const answers = ref<Answer[]>([
      { text: "Answer 1", votes: 43 },
      { text: "Some great second answer", votes: 15 },
      { text: "Third answer that also exists", votes: 20 },
    ])
    const inputFieldAddCommentContent = ref<string>('')
    const isLoadMoreCommentsButtonVisible = ref<boolean>(true)
    let page = 1
    const pageSize = 3

    onMounted(async () => {
      const startTime = performance.now()

      const result = await useGetSurvey(props.id)

      if ("isSuccessful" in result && !result.isSuccessful) {
        router.push("/error-page")
      } else {
        survey.value = result as Survey
        if (authenticationStore.userId != survey.value.userId) {
          router.push(`/forum/survey/${survey.value.id}`)
        }
        console.log("Loaded survey: ", survey.value)
      }

      const elapsed = performance.now() - startTime
      const remaining = 500 - elapsed
      if (remaining > 0) {
        setTimeout(() => { isLoading.value = false }, remaining)
      } else {
        isLoading.value = false
      }
    })

    const formatDate = (date: string) => {
      return useRelativeDate(date)
    }

    const toggleLike = async () => {
      const result = await useToggleLikeOfActivity(survey.value.id, authenticationStore.userId)

      if ("isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      if ("numberOfLikes" in result) {
        survey.value.numberOfLikes = result.numberOfLikes
        survey.value.isLiked = result.isLiked
      }

    }

    const toggleSave = async () => {
      const result = await useToggleSaveOfActivity(survey.value.id, authenticationStore.userId)

      if ("isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      if ("isSaved" in result) {
        survey.value.isSaved = result.isSaved
      }
    }

    const createComment = async () => {
      const result = await useCreateComment(survey.value.id, authenticationStore.userId, inputFieldAddCommentContent.value)
    
      if ("isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      if ("commentId" in result) {
        console.log("Created comment: ", result)
        inputFieldAddCommentContent.value = ''

        survey.value.comments.unshift({
          commentId: result.commentId,
          activityId: survey.value.id,
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

      survey.value.comments = survey.value.comments.filter(c => c.commentId !== commentId)
    }

    const loadMoreComments = async () => {
      const result = await useGetCommentsPaged(survey.value.id, page, pageSize)

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        if (result.length < pageSize) {
          isLoadMoreCommentsButtonVisible.value = false
        }
        survey.value.comments.push(...result)
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

    const vote = (index: number) => {
      if (selectedAnswer.value === null) {
        selectedAnswer.value = index.toString();
        answers.value[index].votes++;
      }
    };

    const totalVotes = computed(() =>
      answers.value.reduce((sum, ans) => sum + ans.votes, 0)
    );

    const getPercentage = (votes: number) => {
      return totalVotes.value > 0 ? (votes / totalVotes.value) * 100 : 0;
    };
    
    return { survey, isLoading, toggleLike, toggleSave , vote, answers, selectedAnswer, getPercentage, inputFieldAddCommentContent, activeUserEmail, createComment, formatDate, deleteComment, loadMoreComments, isLoadMoreCommentsButtonVisible, deleteActivity }
  },
})
</script>

<style lang="scss" scoped>
@use "../../../../assets/styles/config" as *;

.survey {
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
        border-radius: 50%;
        border: 1px solid #000000;

        @media (max-width: $breakpoint) {
          width: 30px;
          margin-right: 8px;
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

  &-description {
    font-size: 14px;
    margin-top: 20px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }

  &-options {
    margin: 40px 0;

    &-header {
      margin-bottom: 24px;
      font-size: 20px;

      @media (max-width: $breakpoint) {
        font-size: 16px;
      }
    }

    &-survey-option {
      margin: 20px 0;

      &-incription {
        font-size: 14px;
        display: flex;
        justify-content: space-between;
        margin: 0 6px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }

        &-percent-of-votes {
          color: $grayBorderColor;
        }
      }

      &-progress-bar {
        cursor: pointer;
        &-selected {
          cursor: auto;
        }
      }
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
    margin-top: 30px;

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
            border-radius: 50%;
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

        @media (max-width: $breakpoint) {
          font-size: 12px;
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