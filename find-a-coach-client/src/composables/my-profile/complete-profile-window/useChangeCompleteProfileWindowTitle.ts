import { Result } from '@/types/Result';
import useEnsureValidToken from '../../authentication/useEnsureValidToken'


const API_URL = 'https://localhost:5058/api/completeProfileWindow/change-state';

export default async function changeCompleteProfileWindowTitle(): Promise<Result> {
  try {
    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Accept': 'application/json',
        'Authorization': `Bearer ${token}`
      },
      body: JSON.stringify({
        isVisible: true,
        title: 'Welcome back'
      })
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occured while changing "Complete profile" window title.',
      }
    }

    return {
      isSuccessful: true,
      errorMessage: null,
    }
  } catch (error) {
    return {
      isSuccessful: false,
      errorMessage: (error as Error).message,
    }
  }
}