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

  if (diffDays < 7) {
    if (diffDays === 0) return "Today"
    return `${diffDays} day${diffDays > 1 ? "s" : ""} ago`
  }

  const diffWeeks = Math.floor(diffDays / 7)
  if (diffWeeks < 4) return `${diffWeeks} week${diffWeeks > 1 ? "s" : ""} ago`

  const diffMonths = Math.floor(diffDays / 30)
  return `${diffMonths} month${diffMonths > 1 ? "s" : ""} ago`
}