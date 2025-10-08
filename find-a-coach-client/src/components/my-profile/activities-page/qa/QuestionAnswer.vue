<template> 
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="qa">
    <ul class="qa-header">
      <li class="qa-header_user-info">
        <router-link :to="`/user-profile/${qa.userId}`" class="qa-header_user-info-link">
          <img class="qa-header_user-info-profile-image" :src="qa.userImagePath" alt="User profile image">
          <span class="qa-header_user-info-user-name">{{ qa.userFirstName }} {{ qa.userLastName }}</span>
        </router-link>  
      </li>
      <li class="qa-header_publication-time">
        <span class="qa-header_publication-time-element">{{ formatDate(qa.createdAt) }}</span>
      </li>
    </ul>

    <h1 class="qa-title">{{ qa.title }}</h1>
    <span class="qa-subjects">{{ qa.subjects?.join(', ') }}</span>

    <img v-if="qa.imagePath" class="qa-image" :src="qa.imagePath" alt="Image of the qa">

    <p class="qa-description">{{ qa.description }}</p>

    <div class="qa-answer-text-area">
      <label class="qa-answer-text-area-label" for="answer">Leave your answer</label>
      <textarea 
        v-model="valueOfTextArea"
        class="qa-answer-text-area-element" 
        name="answer" 
        id="answer"
      ></textarea>
      <span class="qa-answer-text-area-number-of-signs">
        <span :class="isLimitExceeded ? 'qa-answer-text-area-number-of-signs-entered-exceeded' : 'qa-answer-text-area-number-of-signs-entered'">{{ numberOfSignsEntered }}</span>/<span class="qa-answer-text-area-number-of-signs-max">{{ maxNumberOfSigns }}</span>
      </span>
    </div>
    
    <h2 class="qa-comments-header">Comments</h2>

    <ul class="qa-create-comment-bar">
      <li class="qa-create-comment-bar_left-side">
        <input class="qa-create-comment-bar_left-side-input" type="text" placeholder="Leave your comment..." v-model="inputFieldAddCommentContent">
        <svg @click="createComment"
          class="qa-create-comment-bar_left-side-icon"
          xmlns="http://www.w3.org/2000/svg"
          height="30px"
          viewBox="0 -960 960 960"
          width="30px">
          <path d="M120-160v-640l760 320-760 320Zm80-120 474-200-474-200v140l240 60-240 60v140Z" fill="#B1B1B1" />          
        </svg>
      </li>

      <li class="qa-create-comment-bar_right-side">
        <div class="qa-create-comment-bar_right-side-like">
          <img @click="toggleLike" v-if="!qa.isLiked" class="qa-create-comment-bar_right-side-like-icon" src="../../../../assets/images/icons/like-icon.svg" alt="Like icon">
          <svg @click="toggleLike" v-if="qa.isLiked" class="qa-create-comment-bar_right-side-like-icon"
            xmlns="http://www.w3.org/2000/svg"
            height="30px"
            width="30px"
            viewBox="0 -960 960 960">
            <path d="m480-120-58-52q-101-91-167-157T150-447.5Q111-500 95.5-544T80-634q0-94 63-157t157-63q52 0 99 22t81 62q34-40 81-62t99-22q94 0 157 63t63 157q0 46-15.5 90T810-447.5Q771-395 705-329T538-172l-58 52" fill="red" />
          </svg>
          <span class="qa-create-comment-bar_right-side-like-amount">{{ qa.numberOfLikes }}</span>
        </div>

        <img @click="toggleSave" v-if="!qa.isSaved" class="qa-create-comment-bar_right-side-save-icon" src="../../../../assets/images/icons/save-icon.svg" alt="Save icon">
        <svg @click="toggleSave" v-else class="qa-create-comment-bar_right-side-save-icon"
          xmlns="http://www.w3.org/2000/svg"
          height="30px"
          viewBox="0 -960 960 960"
          width="30px">
          <path d="M200-120v-640q0-33 23.5-56.5T280-840h400q33 0 56.5 23.5T760-760v640L480-240 200-120Z" fill="black" />
        </svg>
      </li>
    </ul>

    <ul class="qa-comments">
      <li v-for="(comment, index) in qa.comments" :key="comment.commentId" class="qa-comments_comment">
        <ul class="qa-comments_comment-header">
          <li class="qa-comments_comment-header_left-side">
            <router-link :to="`/user-profile/${comment.userId}`" class="qa-comments_comment-header_left-side-user-name-link">
              <img class="qa-comments_comment-header_left-side-user-profile-image" :src="comment.userImagePath" alt="User profile photo">
              <span class="qa-comments_comment-header_left-side-user-name">{{ comment.userFirstName }} {{ comment.userLastName }}</span>
            </router-link>
          </li>
          <li class="qa-comments_comment-header_right-side">
            <span class="qa-comments_comment-header_right-side-time-of-publication">{{ formatDate(comment.dateOfCreation) }}</span>
          </li>
        </ul>
        <p class="qa-comments_comment-content">{{ comment.content }}</p>
        <button v-if="activeUserEmail == comment.userEmail" @click="deleteComment(comment.commentId)" class="qa-comments_comment-delete-button">Delete comment</button>
        <div v-if="index !== qa.comments.length - 1" class="qa-comments_comment-divider"></div>
      </li>
      <li class="qa-comments_load-more-comments-button" v-if="isLoadMoreCommentsButtonVisible" @click="loadMoreComments">Load more comments..</li>
    </ul>

    <div class="qa-delete" @click="deleteActivity(qa.id)">
      <span class="qa-delete-inscription">Delete event</span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted } from "vue"
import LoadingSquare from "@/components/LoadingSquare.vue"
import { useRouter } from "vue-router"

import useGetqa from "@/composables/forum/useGetQA"
import type { QA } from "@/types/forum/QA"
import useToggleLikeOfActivity from "@/composables/forum/useToggleLikeOfActivity"
import useToggleSaveOfActivity from "@/composables/forum/useToggleSaveOfActivity"
import { useAuthenticationStore } from "@/stores/authentication"
import useRelativeDate from "@/composables/forum/useRelativeDate"
import useCreateComment from "@/composables/forum/useCreateComment"
import useDeleteComment from "@/composables/forum/useDeleteComment"
import useGetCommentsPaged from "@/composables/forum/useGetCommentsPaged"
import useDeleteActivity from "@/composables/my-profile/activities/useDeleteActivity"

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
    const qa = ref<QA>({} as QA)
    const isLoading = ref(true)
    const valueOfTextArea = ref<string>('')
    const maxNumberOfSigns = 400
    const inputFieldAddCommentContent = ref<string>('')
    const isLoadMoreCommentsButtonVisible = ref<boolean>(true)
    let page = 1
    const pageSize = 3

    const numberOfSignsEntered = computed(() => valueOfTextArea.value.length)
    const isLimitExceeded = computed(() => numberOfSignsEntered.value > maxNumberOfSigns)

    onMounted(async () => {
      const startTime = performance.now()

      const result = await useGetqa(props.id)

      if ("isSuccessful" in result && !result.isSuccessful) {
        router.push("/error-page")
      } else {
        qa.value = result as QA
        if (authenticationStore.userId != qa.value.userId) {
          router.push(`/forum/qa/${qa.value.id}`)
        }
        isLoadMoreCommentsButtonVisible.value = qa.value.comments.length == pageSize
        console.log("Loaded qa: ", qa.value)
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
      const result = await useToggleLikeOfActivity(qa.value.id, authenticationStore.userId)

      if ("isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      if ("numberOfLikes" in result) {
        qa.value.numberOfLikes = result.numberOfLikes
        qa.value.isLiked = result.isLiked
      }

    }

    const toggleSave = async () => {
      const result = await useToggleSaveOfActivity(qa.value.id, authenticationStore.userId)

      if ("isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      if ("isSaved" in result) {
        qa.value.isSaved = result.isSaved
      }
    }

    const createComment = async () => {
      const result = await useCreateComment(qa.value.id, authenticationStore.userId, inputFieldAddCommentContent.value)
    
      if ("isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      if ("commentId" in result) {
        console.log("Created comment: ", result)
        inputFieldAddCommentContent.value = ''

        qa.value.comments.unshift({
          commentId: result.commentId,
          activityId: qa.value.id,
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

      qa.value.comments = qa.value.comments.filter(c => c.commentId !== commentId)
    }

    const loadMoreComments = async () => {
      const result = await useGetCommentsPaged(qa.value.id, page, pageSize)

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        if (result.length < pageSize) {
          isLoadMoreCommentsButtonVisible.value = false
        }
        qa.value.comments.push(...result)
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

    return { qa, valueOfTextArea, numberOfSignsEntered, isLimitExceeded, maxNumberOfSigns, isLoading, toggleLike, toggleSave , inputFieldAddCommentContent, activeUserEmail, createComment, formatDate, deleteComment, loadMoreComments, isLoadMoreCommentsButtonVisible, deleteActivity }
  }
});
</script>

<style lang="scss" scoped>
@use "../../../../assets/styles/config" as *;

.qa {
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

  &-answer-text-area {
    width: 100%;
    margin-top: 40px;

    @media (max-width: $breakpoint) {
      width: 100%;
    }

    &-label {
      color: $grayBorderColor;
      font-size: 14px;
      display: block;
      margin: 0 0 2px 2px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }
    &-element {
      border: 1px #000000 solid;
      width: 100%;
      height: 100px;
      border-radius: 10px;
      transition: border 0.2s ease;
      padding: 0 8px;
      font-size: 14px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
        width: 100%;
        height: 60px;
      }

      &:hover {
        border: 2px #000000 solid;
      }
    }
    &-number-of-signs {
      color: $grayBorderColor;
      font-size: 14px;
      display: block;
      justify-self: flex-end;
      margin: -6px 2px 0 0;

      &-entered {

        &-exceeded {
          color: $errorColor;
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