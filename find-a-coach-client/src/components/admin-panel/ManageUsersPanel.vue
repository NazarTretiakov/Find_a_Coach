<template> 
  <div class="manage-users-panel">
    <ul class="manage-users-panel-items">
      <li class="manage-users-panel-items_section-info">
        <h2 class="manage-users-panel-items_section-info-name-of-section">Users</h2>
        <p class="manage-users-panel-items_section-info-description">Manage users</p>
      </li>
      <li class="manage-users-panel-items_icon">
        <router-link to="/admin/manage-users">
          <img class="manage-users-panel-items_icon-arrow" src="@/assets/images/icons/arrow-forward-icon.svg" alt="Arrow forward icon" />
        </router-link>
      </li>
    </ul>
  </div>
</template>

<style lang="scss" scoped>
@use '@/assets/styles/config' as *;

.manage-users-panel {
  width: 100%;
  height: 70px;
  border-radius: 14px;
  border: 2px solid $grayBorderColor;

  @media (max-width: $breakpoint) {
    height: 50px;
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
      
      &-arrow {
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
    }
  }
}
</style>
