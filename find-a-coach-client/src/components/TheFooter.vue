<template>
  <section class="footer">
    <ul class="footer-items">
      <li class="footer-items_logo">
        <router-link to="/home">
          <img class="footer-items_logo-element" src="../assets/images/logo-transparent.png" alt="Logo of the webpage.">
        </router-link>
      </li>
      <li class="footer-items_info">
        <ul class="footer-items_info-items">
          <li class="footer-items_info-items_about-us">
            <router-link class="footer-items_info-items_about-us-link" to="/home/about-us">About us</router-link>
          </li>
          <li class="footer-items_info-items_find-us-on">
            <span class="footer-items_info-items_find-us-on-element">Find us on:</span>
          </li>
          <li class="footer-items_info-items_social-medias">
            <ul class="footer-items_info-items_social-medias-items">
              <li class="footer-items_info-items_social-medias-items_element"><a href="https://instagram.com" target="_blank" rel="noopener"><img class="footer-items_info-items_social-medias-items_element-icon" src="../assets/images/icons/instagram-icon.png" alt="Logo of the instagram."></a></li>
              <li class="footer-items_info-items_social-medias-items_element"><a href="https://facebook.com" target="_blank" rel="noopener"><img class="footer-items_info-items_social-medias-items_element-icon" src="../assets/images/icons/facebook-icon.png" alt="Logo of the facebook."></a></li>
              <li class="footer-items_info-items_social-medias-items_element"><a href="https://youtube.com" target="_blank" rel="noopener"><img class="footer-items_info-items_social-medias-items_element-icon" src="../assets/images/icons/youtube-icon.png" alt="Logo of the youtube."></a></li>
              <li class="footer-items_info-items_social-medias-items_element"><a href="https://x.com" target="_blank" rel="noopener"><img class="footer-items_info-items_social-medias-items_element-icon" src="../assets/images/icons/x-icon.png" alt="Logo of the x."></a></li>
            </ul>
          </li>
        </ul>
      </li>
    </ul>
  </section>
</template>

<style lang="scss" scoped>
@use '../assets/styles/config' as *;

.footer {
  background-color: $footerBackgroundColor;
  padding: 75px 100px;

  @media (max-width: $breakpoint) {
    padding: 36px;
  }

  &-items {
    list-style: none;
    margin: 0;
    padding: 0;
    display: flex;
    justify-content: space-between;

    &_logo {
      
      &-element {
        width: 80px;

        @media (max-width: $breakpoint) {
          width: 60px;
        }
      }
    }

    &_info {

      &-items {
        list-style: none;
        margin: 0;
        padding: 0;

        &_about-us {
          margin-bottom: 20px;

          &-link {
            font-size: 14px;
            color: $linkColor;
            text-decoration: none;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }

            &:hover {
              text-decoration: underline;
            }
          }
        }

        &_find-us-on {
          margin-bottom: 10px;

          &-element {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }

        &_social-medias {

          &-items {
            list-style: none;
            margin: 0;
            padding: 0;
            display: flex;

            &_element {
              margin-right: 2px;

              &-icon {
                width: 40px;

                @media (max-width: $breakpoint) {
                  width: 30px;
                }
              }
            }
          }
        }
      }
    }
  }
}
</style>