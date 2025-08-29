<template> 
  <div class="invitation">
    <ul class="invitation-header">
      <li class="invitation-header_user-info">
        <router-link to="/user-profile" class="invitation-header_user-info-link">
          <img class="invitation-header_user-info-profile-image" src="../../../../assets/images/icons/user-icon.jpg" alt="User profile image">
          <span class="invitation-header_user-info-user-name">Janusz Kowalski</span>
        </router-link>  
      </li>
      <li class="invitation-header_publication-time">
        <span class="invitation-header_publication-time-element">2 days ago</span>
      </li>
    </ul>
    <p class="invitation-description">Jestem osobą proaktywną, z ciągłą chęcią do rozwoju. Lubię pracować w zespole – uważam, że współpraca w zespole odgrywa kluczową rolę w osiąganiu wspólnych celów. Rozumiem ważność samokształcenia oraz rozwijania umiejętności miękkich.Zawodowo zajmuję się programowaniem backendu na platformie .NET. Mam doświadczenie w tworzeniu aplikacji webowych w technologiach: ASP.NET Core MVC oraz ASP.NET Core Web API. Także posiadam umiejętności programowania frontendu w Vue.js.</p>
    <div class="invitation-buttons">
      <button class="invitation-buttons-decline">Decline</button>
      <button class="invitation-buttons-accept">Accept</button>
    </div>
  </div> 
</template>

<style lang="scss" scoped>
@use "../../../../assets/styles/config" as *;

.invitation {
  margin: 50px 0 100px 100px;
  padding: 0 50px 0 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;
  max-width: 921px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 100px 10px;
    padding: 0 30px 0 30px;
  }

  &-header {
    list-style: none;
    display: flex;
    justify-content: space-between;
    align-items: center;
    border-bottom: 2px solid $grayBorderColor;
    margin: 0 -50px;
    padding: 16px 50px;

    @media (max-width: $breakpoint) {
      margin: 0 -30px;
      padding: 14px 30px;
    }

    &_user-info {
      display: flex;
      align-items: center;

      &-link {
        display: flex;
        align-items: center;
        color: black;
        text-decoration: none;

        &:hover {
          text-decoration: underline;
        }
      }

      &-profile-image {
        width: 36px;
        margin-right: 10px;

        @media (max-width: $breakpoint) {
          width: 30px;
          margin-right: 8px;
        }
      }
      &-user-name {
        font-size: 14px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }

    &_publication-time {

      &-element {
        font-size: 14px;
        color: $grayBorderColor;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }
  }

  &-description {
    font-size: 14px;
    margin: 30px 0;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }

  &-buttons {
    display: flex;
    justify-content: space-between;
    margin-bottom: 50px;

    &-decline {
      border: 2px solid $mainColor;
      color: $mainColor;
      background-color: $mainBackgroundColor;
      width: 160px;
      height: 40px;
      border-radius: 12px;
      transition: background-color 0.3s ease;
      font-size: 14px;

      &:hover {
        background-color: $mainBackgroundColorHoverColor;
      }

      @media (max-width: $breakpoint) {
        width: 130px;
        height: 36px;
        font-size: 12px;
      }
    }

    &-accept {
      color: $mainBackgroundColor;
      background-color: $mainColor;
      width: 160px;
      height: 40px;
      border-radius: 12px;
      transition: background-color 0.3s ease;
      font-size: 14px;

      &:hover {
        background-color: $mainColorHoverColor;
      }

      @media (max-width: $breakpoint) {
        width: 130px;
        height: 36px;
        font-size: 12px;
      }
    }
  }
}
</style>