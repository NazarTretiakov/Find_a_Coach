import type { Website } from '@/types/my-profile/personal-information/edit-personal-information/Website'

interface ContactInformation {
  email: string;
  phone: string;
  websites: Website[];
}

export type { ContactInformation };