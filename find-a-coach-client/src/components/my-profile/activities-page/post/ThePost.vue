<template> 
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="post">
    <ul class="post-header">
      <li class="post-header_user-info">
        <router-link :to="`/user-profile/${post.userId}`" class="post-header_user-info-link">
          <img class="post-header_user-info-profile-image" :src="post.userImagePath" alt="User profile image">
          <span class="post-header_user-info-user-name">{{ post.userFirstName }} {{ post.userLastName }}</span>
        </router-link>  
      </li>
      <li class="post-header_publication-time">
        <span class="post-header_publication-time-element">{{ formatDate(post.createdAt) }}</span>
      </li>
    </ul>

    <h1 class="post-title">{{ post.title }}</h1>
    <span class="post-subjects">{{ post.subjects?.join(', ') }}</span>

    <img v-if="post.imagePath" class="post-image" :src="post.imagePath" alt="Image of the post">

    <p class="post-description">{{ post.description }}</p>

    <h2 class="post-comments-header">Comments</h2>

    <ul class="post-create-comment-bar">
      <li class="post-create-comment-bar_left-side">
        <input class="post-create-comment-bar_left-side-input" type="text" placeholder="Leave your comment..." v-model="inputFieldAddCommentContent">
        <svg @click="createComment"
          class="post-create-comment-bar_left-side-icon"
          xmlns="http://www.w3.org/2000/svg"
          height="30px"
          viewBox="0 -960 960 960"
          width="30px">
          <path d="M120-160v-640l760 320-760 320Zm80-120 474-200-474-200v140l240 60-240 60v140Z" fill="#B1B1B1" />          
        </svg>
      </li>

      <li class="post-create-comment-bar_right-side">
        <div class="post-create-comment-bar_right-side-like">
          <img @click="toggleLike" v-if="!post.isLiked" class="post-create-comment-bar_right-side-like-icon" src="../../../../assets/images/icons/like-icon.svg" alt="Like icon">
          <svg @click="toggleLike" v-if="post.isLiked" class="post-create-comment-bar_right-side-like-icon"
            xmlns="http://www.w3.org/2000/svg"
            height="30px"
            width="30px"
            viewBox="0 -960 960 960">
            <path d="m480-120-58-52q-101-91-167-157T150-447.5Q111-500 95.5-544T80-634q0-94 63-157t157-63q52 0 99 22t81 62q34-40 81-62t99-22q94 0 157 63t63 157q0 46-15.5 90T810-447.5Q771-395 705-329T538-172l-58 52" fill="red" />
          </svg>
          <span class="post-create-comment-bar_right-side-like-amount">{{ post.numberOfLikes }}</span>
        </div>

        <img @click="toggleSave" v-if="!post.isSaved" class="post-create-comment-bar_right-side-save-icon" src="../../../../assets/images/icons/save-icon.svg" alt="Save icon">
        <svg @click="toggleSave" v-else class="post-create-comment-bar_right-side-save-icon"
          xmlns="http://www.w3.org/2000/svg"
          height="30px"
          viewBox="0 -960 960 960"
          width="30px">
          <path d="M200-120v-640q0-33 23.5-56.5T280-840h400q33 0 56.5 23.5T760-760v640L480-240 200-120Z" fill="black" />
        </svg>
      </li>
    </ul>

    <ul class="post-comments">
      <li v-for="(comment, index) in post.comments" :key="comment.commentId" class="post-comments_comment">
        <ul class="post-comments_comment-header">
          <li class="post-comments_comment-header_left-side">
            <router-link :to="`/user-profile/${comment.userId}`" class="post-comments_comment-header_left-side-user-name-link">
              <img class="post-comments_comment-header_left-side-user-profile-image" :src="comment.userImagePath" alt="User profile photo">
              <span class="post-comments_comment-header_left-side-user-name">{{ comment.userFirstName }} {{ comment.userLastName }}</span>
            </router-link>
          </li>
          <li class="post-comments_comment-header_right-side">
            <span class="post-comments_comment-header_right-side-time-of-publication">{{ formatDate(comment.dateOfCreation) }}</span>
          </li>
        </ul>
        <p class="post-comments_comment-content">{{ comment.content }}</p>
        <button v-if="activeUserEmail == comment.userEmail" @click="deleteComment(comment.commentId)" class="post-comments_comment-delete-button">Delete comment</button>
        <div v-if="index !== post.comments.length - 1" class="post-comments_comment-divider"></div>
      </li>
      <li v-if="!isLoading && post.comments.length == 0" class="post-comments_empty-state">
        <img class="post-comments_empty-state-icon" src="@/assets/images/icons/empty-state-icon.svg" alt="Empty state icon">
        <span class="post-comments_empty-state-inscription">No comments have been added. You can be first.</span>
      </li>
      <li class="post-comments_load-more-comments-button" v-if="isLoadMoreCommentsButtonVisible" @click="loadMoreComments">Load more comments..</li>
    </ul>

    <div class="post-delete" @click="deleteActivity(post.id)">
      <span class="post-delete-inscription">Delete event</span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted } from "vue"
import { useRouter } from "vue-router"
import LoadingSquare from "@/components/LoadingSquare.vue"

import useGetPost from "@/composables/forum/useGetPost"
import type { Post } from "@/types/forum/Post"
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
    const post = ref<Post>({} as Post)
    const isLoading = ref(true)
    const inputFieldAddCommentContent = ref<string>('')
    const isLoadMoreCommentsButtonVisible = ref<boolean>(true)
    let page = 1
    const pageSize = 3

    onMounted(async () => {
      const startTime = performance.now()

      const result = await useGetPost(props.id)

      if ("isSuccessful" in result && !result.isSuccessful) {
        router.push("/error-page")
      } else {
        post.value = result as Post
        if (authenticationStore.userId != post.value.userId) {
          router.push(`/my-profile/activities/post/${post.value.id}`)
        }
        isLoadMoreCommentsButtonVisible.value = post.value.comments.length == pageSize
        console.log("Loaded post: ", post.value)
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
      const result = await useToggleLikeOfActivity(post.value.id, authenticationStore.userId)

      if ("isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      if ("numberOfLikes" in result) {
        post.value.numberOfLikes = result.numberOfLikes
        post.value.isLiked = result.isLiked
      }

    }

    const toggleSave = async () => {
      const result = await useToggleSaveOfActivity(post.value.id, authenticationStore.userId)

      if ("isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      if ("isSaved" in result) {
        post.value.isSaved = result.isSaved
      }
    }

    const createComment = async () => {
      const result = await useCreateComment(post.value.id, authenticationStore.userId, inputFieldAddCommentContent.value)
    
      if ("isSuccessful" in result && !result.isSuccessful) {
        console.error(result.errorMessage)
        return
      }

      if ("commentId" in result) {
        console.log("Created comment: ", result)
        inputFieldAddCommentContent.value = ''

        post.value.comments.unshift({
          commentId: result.commentId,
          activityId: post.value.id,
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

      post.value.comments = post.value.comments.filter(c => c.commentId !== commentId)
    }

    const loadMoreComments = async () => {
      const result = await useGetCommentsPaged(post.value.id, page, pageSize)

      if (typeof result === 'object' && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
          return
        }
      } else {
        if (result.length < pageSize) {
          isLoadMoreCommentsButtonVisible.value = false
        }
        post.value.comments.push(...result)
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

    return { post, isLoading, toggleLike, toggleSave , inputFieldAddCommentContent, activeUserEmail, createComment, formatDate, deleteComment, loadMoreComments, isLoadMoreCommentsButtonVisible, deleteActivity }
  }
});
</script>

<style lang="scss" scoped>
@use "../../../../assets/styles/config" as *;

.post {
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
    &_empty-state {
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;

      &-icon {
        width: 50px;
        margin-bottom: 14px;

        @media (max-width: $breakpoint) {
          width: 40px;
          margin-bottom: 10px;
        }
      }
      &-inscription {
        font-size: 14px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
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