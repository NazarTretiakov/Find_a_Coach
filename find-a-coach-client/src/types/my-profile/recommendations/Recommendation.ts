interface Recommendation {
  recommenderUserProfileImagePath: string;
  recommenderUserFirstName: string;
  recommenderUserLastName: string;
  recommenderUserHeadline: string;
  recommendationId: string;
  recommenderUserId: string;
  recommendedUserId: string;
  content: string;
  dateOfCreation: string | null;
}

export type { Recommendation };