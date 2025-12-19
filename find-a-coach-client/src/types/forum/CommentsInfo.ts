import { Comment } from "@/types/forum/Comment"

interface CommentsInfo {
  comments: Comment[];
  isMoreCommentsLeft: boolean;
}

export type { CommentsInfo }