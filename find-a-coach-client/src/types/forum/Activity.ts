import { Comment } from "@/types/forum/Comment"

interface Activity {
  id: string;
  userId: string;
  userImagePath: string | null;
  userFirstName: string;
  userLastName: string;
  title: string;
  imagePath: string | null;
  description: string;
  createdAt: string;
  subjects: string[];
  isLiked: boolean;
  numberOfLikes: number;
  isSaved: boolean;
  comments: Comment[]
}

export type { Activity }