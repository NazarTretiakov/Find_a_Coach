import type { Activity } from "./Activity"
import type { SurveyOption } from "./SurveyOption"

interface Survey extends Activity {
  options: SurveyOption[];
}

export type { Survey }