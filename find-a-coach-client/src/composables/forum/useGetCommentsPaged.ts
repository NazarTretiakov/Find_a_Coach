import { Result } from "@/types/Result"
import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { CommentsInfo } from "@/types/forum/CommentsInfo"

const API_URL = config.apiBaseUrl + '/Activity'

export default async function useGetCommentsPaged(activityId: string, page: number, pageSize: number): Promise<Result | CommentsInfo> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-comments?activityId=${activityId}&page=${page}&pageSize=${pageSize}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting comments.',
      }
    }

    const data: CommentsInfo = await response.json()

    return data
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}