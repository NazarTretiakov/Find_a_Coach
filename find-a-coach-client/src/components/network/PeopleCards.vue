<template>
  <div class="network">
    <div class="network-people-cards">
      <h1 class="network-people-cards-header">People you may know based on your activity</h1>
      <ul class="network-people-cards-items">
        <router-link v-for="n in 20" :key="n" to="/user-profile" class="network-people-cards-items_person-link">
          <li class="network-people-cards-items_person">
            <img class="network-people-cards-items_person-image" src="../../assets/images/icons/user-icon.jpg" alt="User profile image">
            <div class="network-people-cards-items_person-info">
              <h3 class="network-people-cards-items_person-info-name">Grzegorz Kwiatkowski</h3>
              <p class="network-people-cards-items_person-info-paragraph">Pracownik działu technicznego firmy The Best Storage w Gdańsku</p>
            </div>
          </li>
        </router-link>
      </ul>
    </div>
    <div class="network-load-more">
      <span class="network-load-more-inscription">Load more</span>
    </div>
  </div>
</template>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.network-people-cards {
  margin: 50px 0 30px 100px;
  padding: 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 30px 10px 30px 10px;
    padding: 30px;
  }

  &-header {
    font-size: 24px;
    margin: 0 0 30px 6px;

    @media (max-width: $breakpoint) {
      font-size: 16px;
    }
  }

  &-items {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 20px;
    list-style: none;
    padding: 0;

    @media (max-width: $breakpoint) {
      grid-template-columns: 1fr;
    }

    &_person {
      height: 150px;
      width: 100%;
      max-width: 400px;
      padding: 20px;
      border: 2px $grayBorderColor solid;
      border-radius: 20px;
      display: flex;
      align-items: center;
      background: #fff;

      @media (max-width: $breakpoint) {
        height: 100px;
      }

      &-image {
        width: 50px;
        margin: -6px 10px 0 0;

        @media (max-width: $breakpoint) {
          width: 40px;
        }
      }

      &-info {
        display: flex;
        flex-direction: column;

        &-name {
          font-size: 16px;
          margin: 0 0 6px 0;

          @media (max-width: $breakpoint) {
            font-size: 14px;
          }
        }

        &-paragraph {
          font-size: 14px;
          margin: 0;

          @media (max-width: $breakpoint) {
            font-size: 12px;
          }
        }
      }
      &-link {
        color: #000000;
        text-decoration: none;
      }
    }
  }
}
.network-load-more {
  margin: 0 0 100px 100px; 
  display: flex;
  justify-content: center;
  align-items: center;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;
  height: 50px;
  transition: background-color 0.3s ease;

  @media (max-width: $breakpoint) {
    margin: 0 10px 100px 10px;
  }

  &:hover {
    cursor: pointer;
    background-color: #ececec;
  }

  &-link {
    color: #000000;
    text-decoration: none;
  }

  &-inscription {
    font-size: 14px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }
}
</style>