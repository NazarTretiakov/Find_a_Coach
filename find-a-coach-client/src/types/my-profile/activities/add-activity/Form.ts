import { Panel } from "./Panel";

interface Form {
  title: string;
  dateOfBeginning: Date | null;
  activityType: string;
  image: File | null;
  description: string;
  subjects: string[];
  searchPeoplePanels: Panel[];
  surveyOptions: string[];
}

export type { Form };