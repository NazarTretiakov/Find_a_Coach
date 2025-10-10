interface PositionForm {
  title: string;
  employmentType: string;
  companyName: string;
  isCurrentlyWorkingHere: boolean;
  startDate: Date | string | null;
  endDate: Date| string | null;
  location: string;
  description: string;
  skills: string[];
}

export type { PositionForm };