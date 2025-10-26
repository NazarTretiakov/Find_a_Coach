<template>
  <section class="search-panel">
    <ul class="search-panel-items">
      <li class="search-panel-items_input-area">
        <ul class="search-panel-items_input-area-items">
          <li class="search-panel-items_input-area-items_search-icon">
            <img
              class="search-panel-items_input-area-items_search-icon-element"
              src="../assets/images/icons/search-icon.svg"
              alt="Search icon"
            />
          </li>
          <li class="search-panel-items_input-area-items_input-control">
            <input
              v-model="searchText"
              @keyup.enter="emitSearch"
              class="search-panel-items_input-area-items_input-control-element"
              type="text"
              placeholder="Search..."
            />
          </li>
        </ul>
      </li>
      <li class="search-panel-items_search-button">
        <search-button
          class="search-panel-items_search-button-element"
          @click="emitSearch"
        />
      </li>
    </ul>
  </section>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import SearchButton from './SearchButton.vue'

export default defineComponent({
  components: { SearchButton },
  emits: ['search'],
  setup(_, { emit }) {
    const searchText = ref('')

    const emitSearch = () => {
      emit('search', searchText.value.trim())
    }

    return { searchText, emitSearch }
  },
})
</script>

<style lang="scss" scoped>
@use '../assets/styles/config' as *;

.search-panel {
  margin: 0;
  padding: 10px 100px;
  background-color: $searchPanelBackgroundColor;

  @media (max-width: $breakpoint) {
    padding: 10px;
  }

  &-items {
    margin: 0;
    padding: 0;
    list-style-type: none;
    display: flex;
    align-items: center;
    flex-wrap: wrap;

    &_input-area {
      flex: 1;

      &-items {
        margin: 0;
        padding: 0;
        list-style-type: none;
        display: flex;
        align-items: center;
        flex: 1;

        &_search-icon {

          &-element {
            width: 20px;
            margin: -10px 0 0 20px;
            position: absolute;
          }
        }

        &_input-control {
          flex: 1;

          &-element {
            height: 36px;
            width: 100%;
            max-width: 1084px;
            margin-right: 20px;
            padding: 0 20px 0 50px;
            border-radius: 20px;
            font-size: 14px;
            border: none;
            transition: border-color 0.3s ease, box-shadow 0.3s ease;
            background-color: white;

            @media (max-width: $breakpoint) {
              height: 34px;
              font-size: 12px;
            }

            &:focus {
              outline: 2px solid #a6a6a6;
              box-shadow: 0 0 5px rgba($mainColor, 0.5);
            }
          }
        }
      }
    }

    &_search-button {
      margin-left: 20px;
    }
  }
}
</style>
