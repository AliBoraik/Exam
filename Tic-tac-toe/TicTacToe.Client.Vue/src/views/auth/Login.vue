<template>
  <div class="login-page">
    <div class="login-form">
      <CustomInput :width="180" label="Login" :value="login" :height="40" @update:modelValue="setLogin"/>
      <CustomInput :width="180" label="Password" :value="password" :height="40" @update:modelValue="setPassword"/>
      <Button :width="180" label="LOG IN" :height="40" @click="submit"/>
    </div>
      <router-link class="redirect-link" to="/registration">
        SIGN UP
      </router-link>
  </div>
</template>

<script setup lang="ts">
import CustomInput from "@/components/UI/CustomInput.vue";
import Button from "@/components/UI/Button.vue";
import {reactive, ref} from "vue";
import $api from "@/utils/api";
import {setCookie} from "@/utils/cookies";
import {useRouter} from "vue-router";

const router = useRouter();

const login = ref<string>("");

const password = ref<string>("");

const setLogin = (value: string) => {
  login.value = value;
}

const setPassword = (value: string) => {
  password.value = value;
}
const submit = async () => {
  if(login.value === "" || password.value === "")
    return;
  const {data, status} = await $api.post("/Auth/login", {username: login.value, password: password.value});
  console.log(data)
  setCookie("access_token", data.token, 1);

  const userId = await getUseId(data.token);
  window.localStorage.setItem("userId", userId);
  await router.push({path: "/"});
}

const getUseId = async (token: string) => {
  const {data, status} = await $api.post(`/Auth/token?key=${token}`)
  return data;
}
</script>

<style scoped>
.login-page {
  display: flex;
  flex-direction: column;
  gap: 15px;
  align-items: center;
}

.login-form {
  background-color: #e1e1e1;
  border-radius: 10px;
  padding: 10px;
  display: flex;
  flex-direction: column;
  gap: 15px;
  width: fit-content;
}

.redirect-link {
  color: #2c3e50;
  font-weight: bold;
}
</style>
