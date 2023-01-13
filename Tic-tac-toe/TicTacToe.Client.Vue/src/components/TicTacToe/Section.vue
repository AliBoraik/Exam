<template>
    <div class="section" >
      {{props.value}}
    </div>
</template>

<script setup lang="ts">
import { Value } from "@/types/Value";

interface Props {
  value: Value | null
}

const props = defineProps<Props>()
</script>

<style scoped>
.section {
  border: 1px solid #2c3e50;
  font-size: xx-large;
  line-height: 100px;
  width: 100px;
  height: 100px;
}
</style>
