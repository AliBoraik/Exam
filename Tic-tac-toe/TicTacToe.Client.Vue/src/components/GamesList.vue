<template>
  <div v-before-mount>
    <div v-for="game in games" class="game">
      <div class="players">
        <div class="player1">
          {{game.player1.playerSign}}: {{game.player1.name}}
        </div>
        <div class="player2">
          {{game.player2?.playerSign}}: {{game.player2?.name}}
        </div>
      </div>
      <div class="status">
        {{getStatus(game.status)}}
      </div>
      <div class="join">
        <Button width="100" label="JOIN" height="20" @click="join(game.id, game.status)"/>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import {ref} from "vue";
import Button from "@/components/UI/Button.vue";
import {useRouter} from "vue-router";

const router = useRouter();

interface Game {
  id: string,
  player1: {
    playerId: number,
    rating: number,
    name: string,
    connectionId: string,
    playerSign: string
  },
  player2: {
    playerId: number,
    rating: number,
    name: string,
    connectionId: string,
    playerSign: string
  } | null,
  status: number
}

const games = ref<Array<Game>>([
  {
    id: "1",
    player1: {
      playerId: 1,
      rating: 10,
      name: "123",
      connectionId: "123",
      playerSign: "X"
    },
    player2: {
      playerId: 2,
      rating: 12,
      name: "321",
      connectionId: "123",
      playerSign: "O"
    },
    status: 1
  },
  {
    id: "1",
    player1: {
      playerId: 1,
      rating: 10,
      name: "123",
      connectionId: "123",
      playerSign: "X"
    },
    player2: null,
    status: 0
  },
  {
    id: "1",
    player1: {
      playerId: 1,
      rating: 10,
      name: "123",
      connectionId: "123",
      playerSign: "X"
    },
    player2: {
      playerId: 2,
      rating: 12,
      name: "321",
      connectionId: "123",
      playerSign: "O"
    },
    status: 2
  }
]);

const getStatus = (value: number) => {
  if(value === 0)
    return "PENDING";
  if(value === 1)
    return "IN PROCESS";
  if(value === 2)
    return "GAME OVER"
}

const join = (id: string, status: number) => {
  if(status === 0)
    router.push({path: `/game/o/${id}`})
  if(status === 1)
    router.push({path: `/game/v/${id}`})
}

const vBeforeMount = {
  beforeMount: async () => {
    //todo: fetch games
  }
}
</script>

<style scoped>
.game {
  display: flex;
  flex-direction: column;
  gap: 15px;
  align-items: center;
  padding: 10px;
  border-bottom: 1px solid #2c3e50;
}

.players {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  width: 100%;
  font-weight: bold;
}
</style>
