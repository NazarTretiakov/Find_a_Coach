export default function useFormatToReadableDate(date: string): string {
  const formattedDate = new Date(date).toLocaleDateString("en-US", {
    year: "numeric",
    month: "long"
  });
  
  return formattedDate;
}