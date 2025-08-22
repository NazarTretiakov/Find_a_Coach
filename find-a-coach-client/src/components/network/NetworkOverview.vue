<template>
  <div class="network-overview">
    <h2 class="network-overview-header">Network overview</h2>
    <span class="network-overview-invites-sent">6 invites sent</span>
    <router-link to="/network/connections" class="network-overview-connections"> 34 connections</router-link>
  </div>
</template>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.network-overview {
  list-style: none;
  margin: 50px 100px 0 30px;
  border: $grayBorderColor 2px solid;
  border-radius: 20px;
  padding: 50px;

  @media (max-width: $breakpoint) {
    margin: 30px 10px 30px 10px;
    padding: 30px;
  }
  
  &-header {
    font-size: 20px;
    margin-bottom: 20px;

    @media (max-width: $breakpoint) {
      font-size: 16px;
      margin-bottom: 10px;
    }
  }
  
  &-invites-sent,
  &-connections {
    display: block;
    font-size: 14px;
    margin-bottom: 10px;
    color: #000000;
    text-decoration: none;

    @media (max-width: $breakpoint) {
      font-size: 12px;
      display: inline;
    }
  }

  &-connections {

    @media (max-width: $breakpoint) {
      margin-left: 40px;
    }

    &:hover {
      text-decoration: underline;
      cursor: pointer;
    }
  }
}
</style>