<template> 
  <section class="search-panel">
    <ul class="search-panel-items">
      <li class="search-panel-items_input-area">
        <ul class="search-panel-items_input-area-items">
          <li class="search-panel-items_input-area-items_search-icon"><img class="search-panel-items_input-area-items_search-icon-element" src="../assets/images/icons/search-icon.svg" alt="Search icon"></li>
          <li class="search-panel-items_input-area-items_input-control"><input class="search-panel-items_input-area-items_input-control-element" type="text" placeholder="Search coaches or posts..."></li>
        </ul>
      </li>
      <li class="search-panel-items_search-button">
        <search-button class="search-panel-items_search-button-element"></search-button>
      </li>
    </ul>
  </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

import SearchButton from './SearchButton.vue'

export default defineComponent({
  components: {
    SearchButton
  }
})
</script>

<style lang="scss" scoped>
@use '../assets/styles/config' as *;

.search-panel {
  margin: 0;
  padding: 10px 100px;
  background-color: $searchPanelBackgroundColor;

  &-items {
    margin: 0;
    padding: 0;
    list-style-type: none;
    display: flex;
    align-items: center;

    &_input-area-items {
      margin: 0;
      padding: 0;
      list-style-type: none;
      display: flex;
      align-items: center;

      &_search-icon {

        &-element {
          width: 20px;
          margin: -10px 0 0 20px;
          position: absolute;
        }
      }

      &_input-control {
        

        &-element {
          height: 36px;
          width: 1084px;
          margin-right: 68px;
          padding: 0;
          border-radius: 20px;
          font-size: 14px;
          border: none;
          transition: border-color 0.3s ease, box-shadow 0.3s ease;
          padding: 0 20px 0 50px;
          background-color: white;

          &:focus {
            outline: 2px solid #a6a6a6;
            box-shadow: 0 0 5px rgba($mainColor, 0.5);
          }
        }
      }
    }
  }
}
</style>