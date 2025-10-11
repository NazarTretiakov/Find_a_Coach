interface School {
  schoolId: string;
  schoolName: string;
  degree: string;
  fieldOfStudy: string;
  location: string;
  startDate: string | null;
  endDate: string | null;
  skillTitles: string[];
}

export type { School };