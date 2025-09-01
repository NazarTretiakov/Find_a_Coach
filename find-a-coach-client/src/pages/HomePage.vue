<template> 
  <complete-profile-window v-if="isCompleteProfileWindowVisible" :title="completeProfileWindowTitle" @complete-profile="onCompleteProfile" @maybe-later="onMaybeLater"></complete-profile-window>
  <receive-reminder-window v-if="isReceiveReminderWindowVisible" @want-to-receive-reminder="onWantToReceiveReminder" @do-not-want-to-receive-reminder="onDoNotWantToReceiveReminder"></receive-reminder-window>
  <basic-sticky-header></basic-sticky-header>
  <coaches-section></coaches-section>
  <forum-section></forum-section>
  <the-footer></the-footer>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import { useRoute } from 'vue-router'
import { useRouter } from 'vue-router'

import CompleteProfileWindow from '../components/homepage/CompleteProfileWindow.vue'
import ReceiveReminderWindow from '../components/homepage/ReceiveReminderWindow.vue'
import BasicStickyHeader from '../components/BasicStickyHeader.vue'
import TheFooter from '../components/TheFooter.vue'
import CoachesSection from '../components/homepage/CoachesSection.vue'
import ForumSection from '../components/homepage/ForumSection.vue'

export default defineComponent({
  components: {
    CompleteProfileWindow,
    ReceiveReminderWindow,
    BasicStickyHeader,
    CoachesSection,
    ForumSection,
    TheFooter
  },
  setup() {
    const route = useRoute()
    const router = useRouter()

    const completeProfileWindowTitle = ref(route.query.completeProfileWindowTitle)
    const isCompleteProfileWindowVisible = ref<boolean>(route.query.isCompleteProfileWindowVisible === 'true' && completeProfileWindowTitle.value != null)
    const isReceiveReminderWindowVisible = ref<boolean>(false)
    
    const onCompleteProfile = () => {
      //TODO: write field on server "showCompleteProfileWindowTitle" = "Welcome back"
      router.push('/my-profile/add-profile-section')
    }

    const onMaybeLater = () => {
      //TODO: write field on server "showCompleteProfileWindowTitle" = "Welcome back"
      isCompleteProfileWindowVisible.value = false
      isReceiveReminderWindowVisible.value = true
    }

    const onWantToReceiveReminder = () => {
      //TODO: write field on server: "showCompleteProfileWindow" = true
      isReceiveReminderWindowVisible.value = false
    }

    const onDoNotWantToReceiveReminder = () => {
      //TODO: write field on server: "showCompleteProfileWindow" = false
      isReceiveReminderWindowVisible.value = false
    }

    return { isCompleteProfileWindowVisible, completeProfileWindowTitle, onCompleteProfile, onMaybeLater, isReceiveReminderWindowVisible, onWantToReceiveReminder, onDoNotWantToReceiveReminder }
  }
})
</script>