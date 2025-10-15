interface CertificationForm {
  title: string;
  issuingOrganization: string;
  issueDate: string | null;
  image: File | null;
  credentialId: string;
  credentialUrl: string;
  skills: string[];
}

export type { CertificationForm };