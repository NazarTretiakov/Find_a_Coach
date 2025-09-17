import type { Website } from './Website'

interface Form {
  profileImage: File | null;
  firstName: string;
  lastName: string;
  primaryOccupation: string;
  headline: string;
  location: string;
  phone: string;
  websites: Website[];
}

export type { Form };