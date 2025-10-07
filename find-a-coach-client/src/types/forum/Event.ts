import type { Activity } from "./Activity";
import type { SearchPersonPanel } from "./SearchPersonPanel";

interface Event extends Activity {
  beginningDate: string;
  searchPersonPanels: SearchPersonPanel[];
}

export type { Event }