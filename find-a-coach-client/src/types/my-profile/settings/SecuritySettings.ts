interface SecuritySettings {
  isLoginNotificationEnabled: boolean;
  oldPassword: string;
  newPassword: string;
  repeatNewPassword: string;
}

export type { SecuritySettings };