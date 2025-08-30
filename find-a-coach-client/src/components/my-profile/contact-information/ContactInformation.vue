<template> 
  <div class="contact-information">
    <h1 class="contact-information-header">Contact information</h1>
    <div class="contact-information-email">
      <img class="contact-information-email-icon" src="../../../assets/images/icons/email-icon.svg" alt="Email icon">
      <h3 class="contact-information-email-header">Email</h3>
    </div>
    <span class="contact-information-email-element">
      <a class="contact-information-email-element-link" href="mailto:grzegorz.kwiatkowski@best-company.eu">
        grzegorz.kwiatkowski@best-company.eu
      </a>
    </span>    
    <div class="contact-information-phone">
      <img class="contact-information-phone-icon" src="../../../assets/images/icons/phone-icon.svg" alt="Phone icon">
      <h3 class="contact-information-phone-header">Phone</h3>
    </div>
    <span class="contact-information-phone-element">123 654 897</span>
    <div class="contact-information-websites">
      <img class="contact-information-websites-icon" src="../../../assets/images/icons/website-icon.svg" alt="Website icon">
      <h3 class="contact-information-websites-header">Websites</h3>
    </div>
    <ul class="contact-information-websites-items">
      <li class="contact-information-websites-items_website">
        <router-link to="/link1" class="contact-information-websites-items_website-url">github.com/GrzegorzKwiatkowski</router-link>
        <span class="contact-information-websites-items_website-type">(Portfolio)</span>
      </li>
      <li class="contact-information-websites-items_website">
        <router-link to="/link2" class="contact-information-websites-items_website-url">best-company.com</router-link>
        <span class="contact-information-websites-items_website-type">(Company)</span>
      </li>
      <li class="contact-information-websites-items_website">
        <router-link to="/link3" class="contact-information-websites-items_website-url">facebook.com/GrzegorzKwiatkowski</router-link>
        <span class="contact-information-websites-items_website-type">(Personal)</span>
      </li>
    </ul>
  </div>
</template>

<style lang="scss" scoped>
@use '../../../assets/styles/config' as *;

.contact-information {
  margin: 50px 0 0 150px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 0 10px;
  }

  &-header {
    font-size: 28px;
    margin-bottom: 50px;

    @media (max-width: $breakpoint) {
      font-size: 20px;
    }
  }

  &-email {
    display: flex;
    align-items: center;

    &-icon {
      width: 30px;

      @media (max-width: $breakpoint) {
        width: 24px;
      }
    }

    &-header {
      margin-left: 10px;
      font-size: 20px;

      @media (max-width: $breakpoint) {
        font-size: 16px;
      }
    }

    &-element {
      display: block;
      margin: 10px 0 0 40px;
      font-size: 14px;
      margin-bottom: 30px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }

      &-link {
        color: $linkColor;
        text-decoration: none;

        &:hover {
          text-decoration: underline;
        }
      }
    }
  }

  &-phone {
    display: flex;
    align-items: center;

    &-icon {
      width: 30px;

      @media (max-width: $breakpoint) {
        width: 24px;
      }
    }

    &-header {
      margin-left: 10px;
      font-size: 20px;

      @media (max-width: $breakpoint) {
        font-size: 16px;
      }
    }

    &-element {
      font-size: 14px;
      display: block;
      margin: 10px 0 0 40px;
      margin-bottom: 30px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
      }
    }
  }

  &-websites {
    display: flex;
    align-items: center;

    &-icon {
      width: 30px;

      @media (max-width: $breakpoint) {
        width: 24px;
      }
    }

    &-header {
      margin-left: 10px;
      font-size: 20px;

      @media (max-width: $breakpoint) {
        font-size: 16px;
      }
    }

    &-items {
      margin: 10px 0 100px 60px;

      &_website {
        margin-top: 10px;
        font-size: 14px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }

        &-url {
          color: $linkColor;
          text-decoration: none;

          &:hover {
            text-decoration: underline;
          }
        }

        &-type {
          color: $grayBorderColor;
          margin-left: 10px;
        }
      }
    }
  }
}
</style>
