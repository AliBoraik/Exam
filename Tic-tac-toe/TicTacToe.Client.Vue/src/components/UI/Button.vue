<template>
  <div class="button" :style="{'width': width + 'px', 'height': height + 'px', 'line-height': height + 'px'}">
    {{label}}
  </div>
</template>

<script setup lang="ts">
interface Props{
  label: string,
  width: number,
  height: number,
}

const props = defineProps<Props>();

</script>

<style scoped>
.button{
  border: none;
  border-radius: 5px;
  background-color: #cb0000;
  font-weight: 600;
  letter-spacing: 2px;
  color: #FFFFFF;
  cursor: pointer;
}
</style>
