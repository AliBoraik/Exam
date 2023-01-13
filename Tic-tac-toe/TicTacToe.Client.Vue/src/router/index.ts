import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Login from "@/views/auth/Login.vue";
import Registr from "@/views/auth/Registr.vue";
import {getCookie} from "@/utils/cookies";
import GameO from "@/components/Game/GameO.vue";
import GameX from "@/components/Game/GameX.vue";
import GameVisitor from "@/components/Game/GameVisitor.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: '/login',
    name: 'login',
    component: Login
  },
  {
    path: '/registration',
    name: 'registration',
    component: Registr
  },
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/game/o/:id',
    name: 'gameO',
    component: GameO
  },
  {
    path: '/game/x/:id',
    name: 'gameX',
    component: GameX
  },
  {
    path: '/game/V/:id',
    name: 'gameV',
    component: GameVisitor
  },

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

router.beforeEach( (to, from) => {
  const token = getCookie("access_token");
  const isAuthorized = token !== null && token !== undefined && token !== "";
  if(!isAuthorized && to.name !== "login" &&  to.name !== "registration") {
    console.log("try redirect to login");
    return { name: "login" }
  }

})

export default router
