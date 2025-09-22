import type { Website } from '../../personal-information/edit-personal-information/Website'

interface PersonalAndContactInformation {
  profileImageUrl: string;
  firstName: string;
  lastName: string;
  primaryOccupation: string;
  headline: string;
  location: string;
  phone: string;
  websites: Website[];
}

export type { PersonalAndContactInformation };