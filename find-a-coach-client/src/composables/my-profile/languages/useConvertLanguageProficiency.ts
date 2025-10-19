export default function useConvertLanguageProficiency(proficiency: string): string {
  if (proficiency === 'A1') {
    return 'Beginner - A1'
  } else if (proficiency === 'A2') {
    return 'Pre-Intermediate - A2'
  } else if (proficiency === 'B1') {
    return 'Intermediate - B1'
  } else if (proficiency === 'B2') {
    return 'Upper-Intermediate - B2'
  } else if (proficiency === 'C1') {
    return 'Advanced - C1'
  } else if (proficiency === 'C2') {
    return 'Mastery - C2'
  } else {
    return "Unknown"
  }
}