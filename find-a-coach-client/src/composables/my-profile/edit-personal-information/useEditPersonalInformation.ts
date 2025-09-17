import { useAuthenticationStore } from "../../../stores/authentication";
import { Form } from "../../../types/edit-personal-information/Form"
import { config } from '@/config'

const API_URL = config.apiBaseUrl + '/MyProfile'

export default async function useEditPersonalInformation(formData: Form): Promise<{ isSuccessful: boolean, errorMessage: string | null }> {
  const authenticationStore = useAuthenticationStore()

  try {
    const now = new Date()

    if (authenticationStore.tokenExpiration && now > authenticationStore.tokenExpiration) {
      if (authenticationStore.refreshTokenExpiration && now < authenticationStore.refreshTokenExpiration) {
        await authenticationStore.refreshJWTToken()
      } else {
        authenticationStore.clearAllFieldsInStore()
        authenticationStore.clearAuthenticationStateFromLocalStore()
        window.location.href = '/login'
      }
    }

    console.log(authenticationStore.token)

    const formDataToSend = new FormData();

    if (formData.profileImage) {
      formDataToSend.append('profileImage', formData.profileImage as File);
    }

    formDataToSend.append('firstName', formData.firstName);
    formDataToSend.append('lastName', formData.lastName);
    formDataToSend.append('primaryOccupation', formData.primaryOccupation);
    formDataToSend.append('headline', formData.headline);
    formDataToSend.append('location', formData.location);
    formDataToSend.append('phone', formData.phone);

    formData.websites.forEach((website, index) => {
      formDataToSend.append(`websites[${index}].url`, website.url);
      formDataToSend.append(`websites[${index}].type`, website.type);
    });

    const response = await fetch(`${API_URL}/edit-personal-information`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${authenticationStore.token}`
      },
      body: formDataToSend,
    })

    if (!response.ok) {
      const responseData = await response.json();
      return {
        isSuccessful: false,
        errorMessage: responseData.errorMessage || 'Unexpected error occured while editing personal information.',
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