export default function useFormatToReadableDate(date: string): string {
  let formattedDate  = new Date(date).toLocaleDateString("en-US", {
    year: "numeric",
    month: "long",
    day: "numeric",
  });
    
  return formattedDate
}