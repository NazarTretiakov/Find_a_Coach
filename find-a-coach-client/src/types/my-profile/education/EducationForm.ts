interface EducationForm {
  schoolName: string;
  degree: string;
  fieldOfStudy: string;
  location: string;
  startDate: string | null;
  endDate: string | null;
  skills: string[];
}

export type { EducationForm };