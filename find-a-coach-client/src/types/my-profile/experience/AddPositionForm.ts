interface AddPositionForm {
  title: string;
  employmentType: string;
  companyName: string;
  isCurrentlyWorkingHere: boolean;
  startDate: Date | null;
  endDate: Date | null;
  location: string;
  description: string;
  skills: string[];
}

export type { AddPositionForm };