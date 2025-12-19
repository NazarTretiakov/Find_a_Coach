import { ActivityOfActivitiesList } from "./ActivityOfActivitiesList";

interface ActivitiesPaged {
  activities: ActivityOfActivitiesList[];
  isMoreActivitiesLeft: boolean;
}

export type { ActivitiesPaged };