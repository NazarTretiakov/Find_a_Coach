<template>
  <div class="people-section">
    <div class="people-section_header-wrapper">
      <h1 class="people-section_header">People you may know</h1>
    </div>

    <ul class="people-section_people-list">
      <li class="people-section_people-person" v-for="n in 10" :key="n">
        <ul class="people-section_people-person-items">
          <li class="people-section_people-person-items_image">
            <img class="people-section_people-person-items_image-element" src="../../assets/images/icons/user-icon.jpg" alt="User profile image" />
          </li>
          <li class="people-section_people-person-items_info">
            <h2 class="people-section_people-person-items_info-name">Grzegorz Kwiatkowski</h2>
            <p class="people-section_people-person-items_info-incription">Menedżer do spraw sprzedaży w firmie...</p>
            <connect-button class="people-section_people-person-items_info-connect-button" />
          </li>
        </ul>
        <div v-if="n != 10" class="people-section_people-person-divider" />
        <div v-else class="people-section_people-person-divider-last" />
      </li>
    </ul>

    <div class="people-section_footer-wrapper">
      <router-link to="/path-to-recommended-people-show-all" class="people-section_people-show-all-link">
        <div class="people-section_people-show-all">
          <span class="people-section_people-show-all-element">Show all</span>
        </div>
      </router-link>
    </div>
  </div>
</template>


<script lang="ts">
import { defineComponent } from 'vue'
import ConnectButton from './ConnectButton.vue'

export default defineComponent({
  components: {
    ConnectButton
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.people-section {
  list-style: none;
  margin: 50px 100px 0 30px;
  border: $grayBorderColor 2px solid;
  border-radius: 20px;
  height: 600px;
  display: flex;
  flex-direction: column;
  overflow: hidden;

  padding: 0;

  &_header-wrapper {
    padding: 30px 50px 0 50px;
    flex-shrink: 0;
  }

  &_header {
    font-size: 20px;
    margin-bottom: 20px;
  }

  &_people-list {
    list-style: none;
    padding: 0 50px;
    margin: 0;
    overflow-y: auto;
    flex-grow: 1;

    scrollbar-width: none;
    -ms-overflow-style: none;
    &::-webkit-scrollbar {
      display: none;
    }
  }

  &_people-person {

    &-items {
      list-style: none;
      display: flex;

      &_image {
        &-element {
          width: 40px;
          margin: 14px 10px 0 0;
        }
      }

      &_info {
        &-name {
          font-size: 14px;
          margin-bottom: 4px;
        }

        &-incription {
          font-size: 14px;
          margin-bottom: 8px;
        }
      }
    }

    &-divider {
      border-bottom: 1px solid $grayBorderColor;
      width: 100%;
      height: 20px;
      margin-bottom: 20px;

      &-last {
        width: 100%;
        height: 30px;
      }
    }
  }

  &_footer-wrapper {
    flex-shrink: 0;
  }

  &_people-show-all-link {
    display: block;
    width: 100%;
    text-decoration: none;
    color: #000000;
  }

  &_people-show-all {
    width: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    border-top: 2px solid $grayBorderColor;
    padding: 10px 0;
    transition: background-color 0.3s ease;
    border-bottom-right-radius: 20px;
    border-bottom-left-radius: 20px;

    &:hover {
      cursor: pointer;
      background-color: #ececec;
    }

    &-element {
      font-size: 14px;
    }
  }
}
</style>