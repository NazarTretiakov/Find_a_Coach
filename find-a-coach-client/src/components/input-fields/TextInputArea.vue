<template> 
  <div class="text-area">
    <label class="text-area-label" :for="name">{{ label }}</label>
    <textarea 
      v-model="internalValue" 
      class="text-area-element" 
      :name="name" 
      :id="name"
      @input="$emit('update:modelValue', $event.target.value)"
    ></textarea>
    <span class="text-area-number-of-signs">
      <span :class="isLimitExceeded ? 'text-area-number-of-signs-entered-exceeded' : 'text-area-number-of-signs-entered'">{{ numberOfSignsEntered }}</span>/<span class="text-area-number-of-signs-max">{{ maxNumberOfSigns }}</span>
    </span>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed, watch } from 'vue'

export default defineComponent({
  props: {
    label: { 
      type: String, 
      required: true
    },
    name: { 
      type: String, 
      required: true 
    },
    maxNumberOfSigns: { 
      type: Number, 
      required: true
    },
    modelValue: { 
      type: String, 
      required: false, 
      default: '' 
    }
  },
  setup(props) {
    const internalValue = ref<string>(props.modelValue)

    watch(() => props.modelValue, (newValue) => {
      internalValue.value = newValue
    })

    const numberOfSignsEntered = computed(() => internalValue.value.length)
    const isLimitExceeded = computed(() => numberOfSignsEntered.value > props.maxNumberOfSigns)

    return { internalValue, numberOfSignsEntered, isLimitExceeded }
  }
})
</script>

<style lang="scss" scoped>
@use '../../assets/styles/config' as *;

.text-area {
  width: 600px;

  @media (max-width: $breakpoint) {
    width: 100%;
  }

  &-label {
    color: $grayBorderColor;
    font-size: 14px;
    display: block;
    margin: 0 0 2px 2px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
    }
  }
  &-element {
    border: 1px #000000 solid;
    width: 600px;
    height: 70px;
    border-radius: 10px;
    transition: border 0.2s ease;
    padding: 0 8px;
    font-size: 14px;

    @media (max-width: $breakpoint) {
      font-size: 12px;
      width: 100%;
      height: 60px;
    }

    &:hover {
      border: 2px #000000 solid;
    }
  }
  &-number-of-signs {
    color: $grayBorderColor;
    font-size: 14px;
    display: block;
    justify-self: flex-end;
    margin: -6px 2px 0 0;

    &-entered {

      &-exceeded {
        color: $errorColor;
      }
    }
  }
}
</style>