<template>
  <div class="security-settings">
    <h1 class="security-settings_header">Security</h1>
    <checkbox-field class="security-settings_login-notifications" label="I want to receive an email notification whenever there is a login to my account" name="login-notification"></checkbox-field>
    <h2 class="security-settings_change-password-header">Change password</h2>
    <input-field class="security-settings_old-password" label="Old password" name="old-password" type="password"></input-field>
    <input-field class="security-settings_new-password" label="New password" name="new-password" type="password"></input-field>
    <input-field class="security-settings_repeat-new-password" label="Repeat new password" name="repeat-new-password" type="password"></input-field>
    <save-button></save-button>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import CheckboxField from '../../../input-fields/CheckboxField.vue'
import InputField from '../../../input-fields/InputField.vue'
import SaveButton from '../../../input-fields/SaveButton.vue'

export default defineComponent({
  components: {
    CheckboxField,
    InputField,
    SaveButton
  }
})
</script>

<style lang="scss" scoped>
@use '../../../../assets/styles/config' as *;

.security-settings {
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

  &_login-notifications {
    margin-bottom: 50px;
  }

  &_change-password-header {
    font-size: 24px;
    margin-bottom: 20px;

    @media (max-width: $breakpoint) {
      font-size: 16px;
      margin-bottom: 14px;
    }
  }

  &_old-password {
    margin-bottom: 20px;

    @media (max-width: $breakpoint) {
      margin-bottom: 14px;
    }
  }

  &_new-password {
    margin-bottom: 20px;

    @media (max-width: $breakpoint) {
      margin-bottom: 14px;
    }
  }

  &_repeat-new-password {
    margin-bottom: 70px;
  }
}


</style>