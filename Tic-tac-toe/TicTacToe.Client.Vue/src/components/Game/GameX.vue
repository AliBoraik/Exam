<template>
  <div class="game">
    <div class="game-field">
      <div class="grid-wrapper">
        <Grid :game="game" :grid="grid" :value="Value.X" @fillSection="fillSection"/>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import Grid from "@/components/TicTacToe/Grid.vue";
import {GameStatus} from "@/types/GameStatus";
import {ref} from "vue";
import {Game} from "@/types/Game";
import {Value} from "@/types/Value";

const game = ref<Game>({
  PlayerX: "",
  PlayerO: "",
  GameStatus: GameStatus.InProcess,
  GameResult: undefined});

const grid = ref<Array<Value | null>>([
  null, null, null,
  null, null, null,
  null, null, null
])

const fillSection = (index: number) => {
  grid.value[index] = Value.X
}
</script>

<style scoped>
.game {
  display: flex;
  gap: 25px;
  justify-content: center;
}

.grid-wrapper {
  padding: 15px;
  border-radius: 10px;
  background: #e1e1e1;
}

.result {
  padding: 15px;
  border-radius: 10px;
  background: #e1e1e1;
  margin-top: 25px;
  font-size: xx-large;
}

.players-data {
  display: flex;
  flex-direction: column;
  gap: 15px;
}

</style>
