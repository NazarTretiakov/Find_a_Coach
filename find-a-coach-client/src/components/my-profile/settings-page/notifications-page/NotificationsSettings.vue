<template>
  <div class="notifications-settings">
    <h1 class="notifications-settings_header">Notifications</h1>
    <checkbox-field class="notifications-settings_whole-notifications-checkbox" label="I want to receive notifications (via email)" name="is-notifications-enabled" v-model="isNotificationsEnabled"></checkbox-field>
    <checkbox-field class="notifications-settings_request-to-connect-notifications-checkbox" label="I want to receive notifications when other users send a request to connect with me" name="is-request-to-connect-notifications-enabled" :disabled="!isNotificationsEnabled"></checkbox-field>
    <checkbox-field class="notifications-settings_review-leaving-notifications-checkbox" label="I want to receive notifications when other users leave review (recommendation) about me " name="is-review-leaving-notifications-enabled" :disabled="!isNotificationsEnabled"></checkbox-field>
    <checkbox-field class="notifications-settings_activity-creation-notifications-checkbox" label="I want to receive notifications when other users from my network create an activity" name="is-activity-creation-notifications-enabled" :disabled="!isNotificationsEnabled"></checkbox-field>
    <save-button></save-button>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import CheckboxField from '../../../input-fields/CheckboxField.vue'
import SaveButton from '../../../input-fields/SaveButton.vue' 

export default defineComponent({
  components: {
    CheckboxField,
    SaveButton
  },
  setup() {
    const isNotificationsEnabled = ref<boolean>(false)

    return { isNotificationsEnabled }
  }
})
</script>

<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.notifications-settings {
  margin: 50px 0 100px 0;

  @media (max-width: $breakpoint) {
    margin: 30px 10px 100px 10px;
  }

  &_header {
    font-size: 28px;
    margin-bottom: 50px;

    @media (max-width: $breakpoint) {
      font-size: 20px;
    }
  }

  &_whole-notifications-checkbox {
    padding-bottom: 30px;
    border-bottom: 1px solid $grayBorderColor;
    margin-bottom: 30px;
  }

  &_request-to-connect-notifications-checkbox {
    margin-bottom: 20px;
  }

  &_review-leaving-notifications-checkbox {
    margin-bottom: 20px;
  }

  &_activity-creation-notifications-checkbox {
    margin-bottom: 70px;
  }
}

</style>