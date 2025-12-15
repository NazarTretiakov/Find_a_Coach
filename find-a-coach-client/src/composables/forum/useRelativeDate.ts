export default function useRelativeDate(dateStr: string | Date): string {
  const now = new Date()
  let pubDate: Date

  if (typeof dateStr === "string") {
    const fixedDateStr = dateStr.replace(/(\.\d{3})\d+/, "$1")
    pubDate = new Date(fixedDateStr)
  } else {
    pubDate = dateStr
  }

  if (isNaN(pubDate.getTime())) return "Invalid date"

  const diffMs = now.getTime() - pubDate.getTime()
  const diffDays = Math.floor(diffMs / (1000 * 60 * 60 * 24))

  if (diffDays < 1) {
    return "Today"
  }

  if (diffDays < 7) {
    return `${diffDays} day${diffDays > 1 ? "s" : ""} ago`
  }

  if (diffDays < 30) {
    const weeks = Math.floor(diffDays / 7)
    return `${weeks} week${weeks > 1 ? "s" : ""} ago`
  }

  const months = Math.floor(diffDays / 30)
  return `${months} month${months > 1 ? "s" : ""} ago`
}