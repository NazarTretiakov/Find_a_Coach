interface ProjectForm {
  title: string;
  relatedTo: string;
  startDate: Date | string | null;
  endDate: Date| string | null;
  location: string;
  description: string;
  skillTitles: string[];
  participants: string[];
}

export type { ProjectForm };