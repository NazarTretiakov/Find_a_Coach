import { Notification } from "./Notification"

interface NotificationsPaged {
  notifications: Notification[];
  isMoreNotificationsLeft: boolean;
}

export type { NotificationsPaged }