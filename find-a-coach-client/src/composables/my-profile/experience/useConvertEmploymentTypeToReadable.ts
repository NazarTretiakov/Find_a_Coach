export default function useConvertEmploymentTypeToReadable(imploymentType: string): string {
  if (imploymentType === 'FullTime') {
    return 'Full Time'
  } else if (imploymentType === 'PartTime') {
    return 'Part Time'
  } else if (imploymentType === 'SelfEmployed') {
    return 'Self Employed'
  } else if (imploymentType === 'Internship') {
    return 'Internship'
  } else {
    return ''
  }
}