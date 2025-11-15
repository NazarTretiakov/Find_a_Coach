<template>
  <loading-square v-if="isLoading"></loading-square>

  <div v-else class="security-settings">
    <h1 class="security-settings_header">Security</h1>
    <checkbox-field v-model="securitySettings.isLoginNotificationEnabled" class="security-settings_login-notifications" label="I want to receive an email notification whenever there is a login to my account" name="login-notification"></checkbox-field>
    <h2 class="security-settings_change-password-header">Change password</h2>
    <input-field v-model="securitySettings.oldPassword" class="security-settings_old-password" label="Old password" name="old-password" type="password"></input-field>
    <span v-if="getError('oldPassword')" class="error-message">{{ getError('oldPassword') }}</span>
    <input-field v-model="securitySettings.newPassword" class="security-settings_new-password" label="New password" name="new-password" type="password"></input-field>
    <span v-if="getError('newPassword')" class="error-message">{{ getError('newPassword') }}</span>
    <input-field v-model="securitySettings.repeatNewPassword" class="security-settings_repeat-new-password" label="Repeat new password" name="repeat-new-password" type="password"></input-field>
    <span v-if="getError('repeatNewPassword')" class="error-message">{{ getError('repeatNewPassword') }}</span>
    <save-button @click="onSave" class="security-settings_save-button"></save-button>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from 'vue'

import CheckboxField from '../../../input-fields/CheckboxField.vue'
import InputField from '../../../input-fields/InputField.vue'
import SaveButton from '../../../input-fields/SaveButton.vue'
import LoadingSquare from '@/components/LoadingSquare.vue'

import { useRouter } from 'vue-router'
import useIsUserHasLoginNotificationEnabled from '@/composables/my-profile/settings/useIsUserHasLoginNotificationEnabled'
import { SecuritySettings } from '@/types/my-profile/settings/SecuritySettings'
import { ValidationError } from '@/types/ValidationError'
import useValidationOfEditSecurityForm from '@/composables/my-profile/settings/useValidationOfEditSecuritySettingsForm'
import useEditSecuritySettings from '@/composables/my-profile/settings/useEditSecuritySettings'
import useEditIsUserHasLoginNotificationEnabled from '@/composables/my-profile/settings/useEditIsUserHasLoginNotificationEnabled'

export default defineComponent({
  components: {
    CheckboxField,
    InputField,
    SaveButton,
    LoadingSquare
  },
  setup() {
    const securitySettings = ref<SecuritySettings>({} as SecuritySettings)
    const formErrors = ref<ValidationError[]>([])
    const router = useRouter()
    const isLoading = ref<boolean>(true)

    onMounted(async () => {
      const startTime = performance.now()

      const result = await useIsUserHasLoginNotificationEnabled()

      if (typeof result === 'object' && result !== null && 'isSuccessful' in result) {
        if (!result.isSuccessful) {
          router.push('/error-page')
        }
      } else {
        securitySettings.value.isLoginNotificationEnabled = result as boolean
        const elapsed = performance.now() - startTime
        const remaining = 500 - elapsed

        if (remaining > 0) {
          setTimeout(() => {
            isLoading.value = false
          }, remaining)
        } else {
          isLoading.value = false
        }
      }
    })

    const onSave = async () => {
      isLoading.value = true
      const startTime = performance.now()

      if (securitySettings.value.oldPassword && securitySettings.value.oldPassword.trim() || securitySettings.value.newPassword && securitySettings.value.newPassword.trim() || securitySettings.value.repeatNewPassword && securitySettings.value.repeatNewPassword.trim()) {
        formErrors.value = useValidationOfEditSecurityForm(securitySettings.value)

        if (formErrors.value.length != 0) {
          isLoading.value = false
          return
        }

        const result = await useEditSecuritySettings(securitySettings.value)

        if (typeof result === 'object' && result !== null && 'isSuccessful' in result) {
          if (!result.isSuccessful) {
            formErrors.value.push({
              fieldName: 'oldPassword',
              errorMessage: 'Old password is incorrect.'
            })
            isLoading.value = false
            return
          }
        } else {
          securitySettings.value.isLoginNotificationEnabled = result as boolean
          const elapsed = performance.now() - startTime
          const remaining = 500 - elapsed

          securitySettings.value.oldPassword = ''
          securitySettings.value.newPassword = ''
          securitySettings.value.repeatNewPassword = ''

          if (remaining > 0) {
            setTimeout(() => {
              isLoading.value = false
            }, remaining)
          } else {
            isLoading.value = false
          }
        }
      } else {
        const result = await useEditIsUserHasLoginNotificationEnabled(securitySettings.value.isLoginNotificationEnabled)

        if (typeof result === 'object' && result !== null && 'isSuccessful' in result) {
          if (!result.isSuccessful) {
            router.push('/error-page')
          }
        } else {
          console.log(result)
          securitySettings.value.isLoginNotificationEnabled = result as boolean
          const elapsed = performance.now() - startTime
          const remaining = 500 - elapsed

          if (remaining > 0) {
            setTimeout(() => {
              isLoading.value = false
            }, remaining)
          } else {
            isLoading.value = false
          }
        }
      }
    }

    const getError = (fieldName: string) => {
      return formErrors.value.find(e => e.fieldName === fieldName)?.errorMessage || ''
    }

    return { securitySettings, getError, isLoading, onSave }
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
    margin-top: 20px;
    margin-bottom: 6px;

    @media (max-width: $breakpoint) {
      margin-top: 14px;
      margin-bottom: 4px;
    }
  }

  &_new-password {
    margin-top: 20px;
    margin-bottom: 6px;

    @media (max-width: $breakpoint) {
      margin-top: 14px;
      margin-bottom: 4px;
    }
  }

  &_repeat-new-password {
    margin-top: 20px;
    margin-bottom: 6px;

    @media (max-width: $breakpoint) {
      margin-top: 14px;
      margin-bottom: 4px;
    }
  }

  &_save-button {
    margin-top: 70px;
  }
}

.error-message {
  color: red;
  font-size: 14px;
  margin-top: 4px;
  display: block;

  @media (max-width: $breakpoint) {
    font-size: 12px;
  }
}
</style>