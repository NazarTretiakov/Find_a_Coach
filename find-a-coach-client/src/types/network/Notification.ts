interface Notification {
  notificationId: boolean;
  content: string;
  dateOfCreation: string;
  type: string;
  notifiedObjectId: string;
  relatedUserFirstName: string;
  relatedUserLastName: string;
  relatedUserImagePath: string;
}

export type { Notification }