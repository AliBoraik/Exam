<template>
  <div class="home">
    <div class="blocks">
      <div class="block">
        <div class="title">
          <div class="title-part">
            WELCOM TO
          </div>
          <div class="title-part">
            <div class="grid">
              <div class="section red">
                T
              </div>
              <div class="section">
                I
              </div>
              <div class="section">
                C
              </div>
              <div class="section red">
                T
              </div>
              <div class="section">
                A
              </div>
              <div class="section">
                C
              </div>
              <div class="section red">
                T
              </div>
              <div class="section">
                O
              </div>
              <div class="section">
                E
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="block">
        <div class="menu">
          <div class="nav-link" @click="createGame">Create Game</div>
          <GamesList/>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import GamesList from "@/components/GamesList.vue";
import {useRouter} from "vue-router";
import $api from "@/utils/api";

const router = useRouter();

const createGame = () => {
 /* $api.post("").then((res) => {
    router.push({path: `/game/x/${res.data}`})
  });
  */ //todo: request create game -> get created game id -> connect with this id
  router.push({path: `/game/x/`});
}

</script>

<style scoped>
  .home {
    display: flex;
    flex-direction: column;
    gap: 25px;
    align-items: center;
  }

  .blocks {
    display: flex;
    flex-direction: column;
    gap: 25px;

  }

  .block {
    width: 100%;
    padding: 15px;
    border-radius: 10px;
    background: #e1e1e1;
  }

  .title {
    display: flex;
    font-size: xx-large;
    letter-spacing: 5px;
    align-items: center;
    justify-content: center;
    gap: 25px;
  }

  .grid {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    grid-auto-rows: 1fr 1fr 1fr ;
  }

  .section {
    font-size: xx-large;
    line-height: 40px;
  }

  .red {
    color: #cb0000;
  }

  .menu {
    display: flex;
    flex-direction: column;
    gap: 25px;
  }

  .nav-link {
    text-decoration: none;
    font-size: x-large;
    color: #2c3e50;
  }
</style>

