<template>
  <div class="field">
    <div class="label">{{label}}</div>
    <input class="input"
           :style="{'width': width + 'px', 'height': height + 'px', 'line-height': height + 'px'}"
           :value="value"
           @input="input"/>
  </div>
</template>

<script setup lang="ts">

interface Props {
  placeHolder?: string,
  label?: string,
  height: number,
  width: number,
  value: string
}

const props = defineProps<Props>();
const emit = defineEmits(['update:modelValue']);


const input = (event: any) => { //todo: replace any to event type
  emit("update:modelValue", event.target.value);
}
</script>

<style scoped>
.field {
  display: flex;
  flex-direction: column;
  gap: 5px;
}

.label {
  text-align: start;
}

.input {
  border-radius: 5px;
  border: 1px solid #2c3e50;
  background: #e1e1e1;
  font-size: 16px;
}
</style>

