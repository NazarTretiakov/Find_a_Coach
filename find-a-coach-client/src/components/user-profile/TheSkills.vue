<template>
  <div class="skills">
    <ul class="skills-items">
      <li class="skills-items_icon"><img src="../../assets/images/icons/skills-icon.svg" alt="Skills icon"></li>
      <li class="skills-items_skills"><span class="skills-items_skills-element">Team Work, Finance, Microsoft Office and + 5 more skills</span></li>
    </ul>
  </div>
</template>

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