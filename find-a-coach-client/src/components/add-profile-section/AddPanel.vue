<template> 
  <div class="add-panel" :class="isProfileSectionAdded ? 'add-panel-added' : 'add-panel-unadded'">
    <ul class="add-panel-items">
      <li class="add-panel-items_section-info">
        <h2 class="add-panel-items_section-info-name-of-section">{{ nameOfSection }}</h2>
        <p class="add-panel-items_section-info-description">{{ description }}</p>
      </li>
      <li class="add-panel-items_icon">
        <router-link :to="linkToAddProfileSection" v-if="!isProfileSectionAdded">
          <svg class="add-panel-items_icon-add" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
            <path d="M12 5v14m-7-7h14" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
          </svg>
        </router-link>
        <router-link :to="linkToChangeProfileSection" v-else>
          <img src="../../assets/images/icons/tick-icon.svg" class="add-panel-items_icon-tick" alt="Tick icon">
        </router-link>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  name: 'AddPanel',
  props: {
    nameOfSection: {
      type: String,
      required: true
    },
    description: {
      type: String,
      required: true
    },
    isProfileSectionAdded: {
      type: Boolean,
      required: true
    },
    linkToAddProfileSection: {
      type: String,
      required: true
    },
    linkToChangeProfileSection: {
      type: String,
      required: true
    }
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.add-panel {
  width: 100%;
  height: 70px;
  border-radius: 14px;

  @media (max-width: $breakpoint) {
    height: 50px;
  }

  &-added {
    background-color: #a5ffb1;
  }
  &-unadded {
    border: 2px solid $grayBorderColor;
  }

  &-items {
    list-style: none;
    display: flex;
    justify-content: space-between;
    align-items: center;
    height: 100%;
    margin: 0 30px;

    @media (max-width: $breakpoint) {
      margin: 0 20px;
    }

    &_section-info {
      display: flex;
      align-items: center;
      
      &-name-of-section {
        font-size: 20px;
        width: 300px;

        @media (max-width: $breakpoint) {
          font-size: 16px;
          width: 150px;
        }
      }

      &-description {
        font-size: 14px;

        @media (max-width: $breakpoint) {
          font-size: 12px;
        }
      }
    }

    &_icon {
      margin: 4px 0 0 10px;
      
      &-add {
        width: 30px;
        height: 30px;
        color: $grayBorderColor;
        transition: transform 0.3s ease;

        &:hover {
          transform: scale(1.20);
        }

        @media (max-width: $breakpoint) {
          width: 24px;
          height: 24px;
        }
      }

      &-tick {
        width: 30px;
        transition: transform 0.3s ease;

        &:hover {
          transform: scale(1.20);
        }
        
        @media (max-width: $breakpoint) {
          width: 24px;
        }
      }
    }
  }
}
</style>
