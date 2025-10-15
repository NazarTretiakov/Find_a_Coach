interface Certification {
  certificationId: string;
  title: string;
  issuingOrganization: string;
  issueDate: string | null;
  imagePath: string | null;
  credentialId: string;
  credentialUrl: string;
  skillTitles: string[];
}

export type { Certification };