interface ConnectionRequest {
  connectionId: string;
  userId: string;
  userFirstName: string;
  userLastName: string;
  userImagePath: string;
  message: string;
  dateOfCreation: string;
  status: string;
}

export type { ConnectionRequest }