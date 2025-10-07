interface Comment {
  commentId: string;
  activityId: string;
  userId: string;
  userEmail: string;
  userFirstName: string;
  userLastName: string;
  userImagePath: string;
  dateOfCreation: string;
  content: string;
}

export type { Comment }