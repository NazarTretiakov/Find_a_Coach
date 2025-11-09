<template>
  <div class="network-overview">
    <h2 class="network-overview-header">Network overview</h2>
    <span v-if="networkOverview.numberOfConnectionRequestsSent == 1" class="network-overview-invites-sent">{{ networkOverview.numberOfConnectionRequestsSent }} connection request sent</span>
    <span v-else class="network-overview-invites-sent">{{ networkOverview.numberOfConnectionRequestsSent }} connection requests sent</span>
    <router-link v-if="networkOverview.numberOfConnections == 1" :to="`/network/connections/${userId}`" class="network-overview-connections"> {{ networkOverview.numberOfConnections }} connection</router-link>
    <router-link v-else :to="`/network/connections/${userId}`" class="network-overview-connections"> {{ networkOverview.numberOfConnections }} connections</router-link>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from "vue"
import { useRouter } from "vue-router"
import { useAuthenticationStore } from "@/stores/authentication"
import useGetNetworkOverview from "@/composables/network/useGetNetworkOverview"
import { NetworkOverview } from "@/types/network/NetworkOverview"

export default defineComponent({
  setup() {
    const router = useRouter()
    const authenticationStore = useAuthenticationStore()

    const networkOverview = ref<NetworkOverview>({} as NetworkOverview)
    const userId = authenticationStore.userId;

    async function loadNetworkOverview() {
      let result = await useGetNetworkOverview(userId)

      if (typeof result === "object" && "isSuccessful" in result && !result.isSuccessful) {
        router.push("/error-page")
        return
      }

      networkOverview.value = result as NetworkOverview
    }

    onMounted(() => {
      loadNetworkOverview()
    })

    return { networkOverview, userId }
  },
})
</script>

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