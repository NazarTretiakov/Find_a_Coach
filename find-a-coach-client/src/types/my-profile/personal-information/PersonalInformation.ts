interface PersonalInformation {
  profileImageUrl: string;
  firstName: string;
  lastName: string;
  primaryOccupation: string;
  headline: string;
  location: string;
  connectionsAmount: number;
  isConnected: boolean;
  connectionStatus: string | null;
}

export type { PersonalInformation };