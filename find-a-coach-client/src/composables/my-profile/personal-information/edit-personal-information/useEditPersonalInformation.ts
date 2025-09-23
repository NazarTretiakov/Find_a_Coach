import { Result } from "@/types/Result";
import { Form } from "@/types/my-profile/personal-information/edit-personal-information/Form"
import useEnsureValidToken from '../../../authentication/useEnsureValidToken'
import { config } from '@/config'

const API_URL = config.apiBaseUrl + '/MyProfile'

export default async function useEditPersonalInformation(formData: Form): Promise<Result> {
  try {
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

    const token = await useEnsureValidToken()

    const response = await fetch(`${API_URL}/edit-personal-information`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`
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