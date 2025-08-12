<template>
  <div class="about-me">
    <ul class="about-me-header">
      <li class="about-me-header-title"><h1 class="about-me-header-title-element">About me</h1></li>
      <li class="about-me-header-edit"><router-link to="/profile/edit-about-me" class="about-me-header-edit-link"><img src="../../assets/images/icons/edit-icon.svg" alt="Edit icon" class="about-me-header-edit-icon"></router-link></li>
    </ul>
    <p class="about-me-paragraph">Jestem osobą proaktywną, z ciągłą chęcią do rozwoju. Lubię pracować w zespole – uważam, że współpraca w zespole odgrywa kluczową rolę w osiąganiu wspólnych celów. Rozumiem ważność samokształcenia oraz rozwijania umiejętności miękkich.Zawodowo zajmuję się programowaniem backendu na platformie .NET. Mam doświadczenie w tworzeniu aplikacji webowych w technologiach: ASP.NET Core MVC oraz ASP.NET Core Web API. Także posiadam umiejętności programowania frontendu w Vue.js. Interesuję się również tworzeniem aplikacji mobilnych oraz desktopowych przy użyciu technologii .NET MAUI oraz WPF.</p>
  </div>
</template>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.about-me {
  margin: 0 0 30px 100px;
  padding: 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 0 10px;
    padding: 0;
    border: none;
  }

  &-header {
    list-style: none;
    display: flex;
    justify-content: space-between;
    align-content: center;

    &-title {
      margin-bottom: 30px;

      @media (max-width: $breakpoint) {
        margin-bottom: 14px;
      }
      
      &-element {
        font-size: 24px;

        @media (max-width: $breakpoint) {
          font-size: 20px;
        }
      }
    }
    &-edit {
      &-icon {
        width: 30px;
        transition: transform 0.3s ease;

        &:hover {
          transform: scale(1.15);
        }

        @media (max-width: $breakpoint) {
          width: 24px;
        }
      }
    }
  }

  &-paragraph {
    font-size: 14px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }
}
</style>