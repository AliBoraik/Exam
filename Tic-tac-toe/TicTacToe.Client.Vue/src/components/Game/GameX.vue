<template>
  <div v-before-mount class="game">
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
import $api from "@/utils/api";
import {HttpTransportType, HubConnectionBuilder} from "@microsoft/signalr";
import {API_BASE_URL} from "@/utils/consts";

const game = ref<Game>({
  PlayerX: "",
  PlayerO: "",
  GameStatus: GameStatus.InProcess,
  GameResult: undefined});

const grid = ref<Array<string | null>>([])


const connection = new HubConnectionBuilder().withUrl(
    API_BASE_URL + "/gameHub",
    {
      transport: HttpTransportType.LongPolling,
    }).build();


const restartGame = () => {
  grid.value.forEach(el => el = null)
  connection.invoke("RestartGame", "groupName");
}

const vBeforeMount = {
  beforeMount: async () => {
    await connection.start();
    await connection.invoke("JoinToGame", "groupId", window.localStorage.getItem("userId"));
    connection.on("StartGame", id => { alert(`Game #${id} started`) });
    connection.on("UpdateBoard", board => {console.log(board); grid.value = board; });
    connection.on("GameOver",result => {game.value.GameResult = result; alert(game.value.GameResult)});
  }
}

const fillSection = (index: number) => {
  grid.value[index] = Value.X.toString()
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
