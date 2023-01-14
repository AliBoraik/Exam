<template>
  <div class="grid">
    <div v-for="(n, i) in 9">
        <Section :value="grid[i] ?? null" @click="fillSection(i)"/>
    </div>
  </div>
</template>

<script setup lang="ts">
import Section from "@/components/TicTacToe/Section.vue";
import {ref} from "vue";
import {Value} from "@/types/Value";
import {GameStatus} from "@/types/GameStatus";
import {GameResult} from "@/types/GameResult";
import {Game} from "@/types/Game";

interface Props {
  game: Game
  grid: Array<string | null>
  value?: Value
}

const emit = defineEmits(['gameOver', 'fillSection']);
const props = defineProps<Props>();

const fillSection = (i: number) => {
  if(props.game.GameStatus !== GameStatus.InProcess){
    return;
  }
  if(props.grid[i] !== null){
    return;
  }
  emit('fillSection', i, props.value);

  let winner = check3InARow();
  let isDraw = checkDraw();

  if(winner !== null){
    emit('gameOver', winner);
  }
  else if(isDraw){
    emit('gameOver', "Draw");
  }
}

const check3InARow = () => {

}

const checkDraw = () => {
  return false;
}

</script>

<style scoped>
.grid {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-auto-rows: 1fr 1fr 1fr ;
  border: 1px solid #2c3e50;
}
</style>
