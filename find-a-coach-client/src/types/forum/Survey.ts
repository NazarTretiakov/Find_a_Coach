import type { Activity } from "./Activity"
import type { SurveyOption } from "./SurveyOption"

interface Survey extends Activity {
  surveyOptions: SurveyOption[];
}

export type { Survey }