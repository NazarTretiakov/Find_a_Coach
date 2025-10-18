interface Skill {
  skillId: string;
  title: string;
  usages: Usage[];
}

interface Usage {
  title: string;
  type: string;
}

export type { Skill };