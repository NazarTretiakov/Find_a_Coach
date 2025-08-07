<template>
  <div class="skills-section">
    <ul class="skills-section-items">
      <li class="skills-section-items_header">
        <ul class="skills-section-items_header-items">
          <li class="skills-section-items_header-items_inscription"><h1 class="skills-section-items_header-items_inscription-element">Skills</h1></li>
          <li class="skills-section-items_header-items_buttons">
            <ul class="skills-section-items_header-items_buttons-items">
              <li class="skills-section-items_header-items_buttons-items_add"><router-link to="/link-to-add-skill" class="skills-section-items_header-items_buttons-items_add-link"><img src="../../assets/images/icons/add-icon.svg" alt="Add icon" class="skills-section-items_header-items_buttons-items_add-element"></router-link></li>
              <li class="skills-section-items_header-items_buttons-items_edit"><router-link to="/link-to-edit-skills" class="skills-section-items_header-items_buttons-items_edit-link"><img src="../../assets/images/icons/edit-icon.svg" alt="Edit icon" class="skills-section-items_header-items_buttons-items_edit-element"></router-link></li>
            </ul>
          </li>
        </ul>
      </li>
      <li class="skills-section-items_skill">
        <ul class="skills-section-items_skill-items">
          <li class="skills-section-items_skill-items_name"><router-link to="/path-to-all-skills-of-user-with-scrollable-thing" class="skills-section-items_skill-items_name-link"><h2 class="skills-section-items_skill-items_name-element">Store management</h2></router-link></li>
          <li class="skills-section-items_skill-items_source">
            <img class="skills-section-items_skill-items_source-icon" src="../../assets/images/icons/project-icon.svg" alt="Project icon">
            <span class="skills-section-items_skill-items_source-inscription">Store management for Amazon company</span>
          </li>
          <li class="skills-section-items_skill-items_source">
            <img class="skills-section-items_skill-items_source-icon" src="../../assets/images/icons/certification-icon.svg" alt="Certification icon">
            <span class="skills-section-items_skill-items_source-inscription">Complete store management guide</span>
          </li>
        </ul>
        <div class="skills-section-items_skill-divider"></div>
      </li>
      <li class="skills-section-items_skill">
        <ul class="skills-section-items_skill-items">
          <li class="skills-section-items_skill-items_name"><router-link to="/path-to-all-skills-of-user-with-scrollable-thing" class="skills-section-items_skill-items_name-link"><h2 class="skills-section-items_skill-items_name-element">JavaScript</h2></router-link></li>
          <li class="skills-section-items_skill-items_source">
            <img class="skills-section-items_skill-items_source-icon" src="../../assets/images/icons/education-icon.svg" alt="Education icon">
            <span class="skills-section-items_skill-items_source-inscription">Pomeranian University in Słupsk</span>
          </li>
          <li class="skills-section-items_skill-items_source">
            <img class="skills-section-items_skill-items_source-icon" src="../../assets/images/icons/job-icon.svg" alt="Job icon">
            <span class="skills-section-items_skill-items_source-inscription">The Best Company</span>
          </li>
          <li class="skills-section-items_skill-items_source">
            <img class="skills-section-items_skill-items_source-icon" src="../../assets/images/icons/certification-icon.svg" alt="Certification icon">
            <span class="skills-section-items_skill-items_source-inscription">JavaScript + TypeScript Course</span>
          </li>
        </ul>
      </li>
    </ul>
    <router-link class="skills-section-items_show-all-skills-link" to="/path-to-list-of-skills-of-user">
      <div class="skills-section-items_show-all-skills">
        <span class="skills-section-items_show-all-skills-element">Show all skills</span>
        <img class="skills-section-items_show-all-skills-icon-arrow-forward" src="../../assets/images/icons/arrow-forward-icon.svg" alt="Arrow forward icon">
        </div>
    </router-link>
  </div>
</template>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.skills-section {
  margin: 0 0 30px 100px;
  padding: 50px 50px 0 50px;
  border: 2px $grayBorderColor solid;
  border-radius: 20px;

  @media (max-width: $breakpoint) {
    margin: 50px 10px 0 10px;
    padding: 0;
    border: none;
  }

  &-items {
    list-style: none;
    display: flex;
    flex-direction: column;

    &_header {
      margin-bottom: 30px;

      @media (max-width: $breakpoint) {
        margin-bottom: 14px;
      }

      &-items {
        list-style: none;
        display: flex;
        justify-content: space-between;
        align-content: center;

        &_inscription {

          &-element {
            font-size: 24px;

            @media (max-width: $breakpoint) {
              font-size: 20px;
            }
          }
        }

        &_buttons {
          width: 100px;

          @media (max-width: $breakpoint) {
            width: 80px;
          }

          &-items {
            list-style: none;
            display: flex;
            justify-content: space-between;
            align-content: center;

            &_add {
              &-element {
                width: 30px;

                @media (max-width: $breakpoint) {
                  width: 24px;
                }
              }
            }

            &_edit {
              &-element {
                width: 30px;

                @media (max-width: $breakpoint) {
                  width: 24px;
                }
              }
            }
          }
        }
      }
    }

    &_skill {
      
      &-items {
        list-style: none;
        display: flex;
        flex-direction: column;

        &_name {
          margin-bottom: 10px;

          &-link {
            color: #000000;
            text-decoration: none;
          } 
          &-element {
            font-size: 20px;

            @media (max-width: $breakpoint) {
              font-size: 16px;
            }
          }
        }

        &_source {
          display: flex;
          align-items: center;
          margin-bottom: 8px;

          &-icon {
            width: 28px;
            margin-right: 10px;

            @media (max-width: $breakpoint) {
              width: 24px;
              margin-right: 8px;
            }
          }
          &-inscription {
            font-size: 14px;

            @media (max-width: $breakpoint) {
              font-size: 12px;
            }
          }
        }
      }

            &-divider {
        border-bottom: 1px solid $grayBorderColor;
        width: 100%;
        height: 30px;
        margin-bottom: 30px;

        @media (max-width: $breakpoint) {
          height: 20px;
          margin-bottom: 20px;
        }
      }
    }

    &_show-all-skills {
      display: flex;
      justify-content: center;
      align-content: center;
      border-top: 2px solid $grayBorderColor;
      margin: 40px -50px 0 -50px;
      padding: 14px 0;
      transition: background-color 0.3s ease;
      border-bottom-right-radius: 20px;
      border-bottom-left-radius: 20px;

      @media (max-width: $breakpoint) {
        border: none;
        margin: 0;
        padding: 0;
        display: flex;
        align-items: center;
        margin: 16px 0;
      }

      &-element {
        font-size: 14px;
      }

      &-icon-arrow-forward {
        display: none;
        margin-left: 8px;

        @media (max-width: $breakpoint) {
          display: inline;
        }
      }

      &:hover {
        cursor: pointer;
        background-color: #ececec;
      }

      &-link {
        color: #000000;
        text-decoration: none;
      }
    }
  }
}
</style>