interface Position {
  positionId: string;
  title: string;
  employmentType: string;
  companyName: string;
  isCurrentlyWorkingHere: boolean;
  startDate: string | null;
  endDate: string | null;
  location: string;
  description: string;
  skillTitles: string[];
}

export type { Position };