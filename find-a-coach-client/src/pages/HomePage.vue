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
import useChangeCompleteProfileWindowTitle from '../composables/my-profile/complete-profile-window/useChangeCompleteProfileWindowTitle'
import useChangeCompleteProfileWindowVisibility from '../composables/my-profile/complete-profile-window/useChangeCompleteProfileWindowVisibility'
import { useAuthenticationStore } from '@/stores/authentication'

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
    const authenticationStore = useAuthenticationStore()

    const completeProfileWindowTitle = ref(route.query.completeProfileWindowTitle)
    const isCompleteProfileWindowVisible = ref<boolean>(route.query.isCompleteProfileWindowVisible === 'true' && completeProfileWindowTitle.value != null)
    const isReceiveReminderWindowVisible = ref<boolean>(false)

    if (authenticationStore.role === 'Admin') {
      router.push('/admin')
    }
    
    const onCompleteProfile = async () => {
      await useChangeCompleteProfileWindowTitle()

      router.push('/my-profile/add-profile-section')
    }

    const onMaybeLater = async () => {
      isCompleteProfileWindowVisible.value = false
      isReceiveReminderWindowVisible.value = true
    }

    const onWantToReceiveReminder = async () => {
      await useChangeCompleteProfileWindowTitle()
      
      isReceiveReminderWindowVisible.value = false
    }

    const onDoNotWantToReceiveReminder = async () => {
      await useChangeCompleteProfileWindowTitle()
      await useChangeCompleteProfileWindowVisibility()

      isReceiveReminderWindowVisible.value = false
    }

    return { isCompleteProfileWindowVisible, completeProfileWindowTitle, onCompleteProfile, onMaybeLater, isReceiveReminderWindowVisible, onWantToReceiveReminder, onDoNotWantToReceiveReminder }
  }
})
</script>