import { Result } from "@/types/Result"
import useEnsureValidToken from '../authentication/useEnsureValidToken'
import { config } from '@/config'
import { ConnectionRequest } from "@/types/network/ConnectionRequest"

const API_URL = config.apiBaseUrl + '/Network'

export default async function useGetConnectionRequest(connectionId: string): Promise<Result | ConnectionRequest> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/get-connection-request?connectionId=${connectionId}`, {
      method: 'GET',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    })

    if (!response.ok) {
      const responseData = await response.json()
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occurred while getting connection request.',
      }
    }

    const data: ConnectionRequest = await response.json()
    return data

  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}