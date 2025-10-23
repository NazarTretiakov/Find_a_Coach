<template>
  <div class="skills" v-if="skills && skills.length > 0">
    <ul class="skills-items">
      <li class="skills-items_icon">
        <img src="@/assets/images/icons/skills-icon.svg" alt="Skills icon" />
      </li>
      <li class="skills-items_skills">
        {{ skills.join(', ') }}
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  props: {
    skills: {
      type: Array as () => string[],
      required: true
    }
  }
});
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.skills {

  &-items {
    list-style: none;
    display: flex;
    flex-direction: row;
    align-content: center;

    @media (max-width: $breakpoint) {
      display: flex;
      align-items: center;
    }

    &_icon {
      width: 30px;
      margin-right: 6px;

      @media (max-width: $breakpoint) {
        width: 24px;
      }
    }
    &_skills {
      font-weight: bold;
      font-size: 16px;

      @media (max-width: $breakpoint) {
        font-size: 12px;
        margin-bottom: 8px;
      }
    }
  }
}
</style>