<template>
  <div class="login-page">
    <div class="login-form">
      <CustomInput :width="180" label="Login" :value="login" :height="40" @update:modelValue="setLogin"/>
      <CustomInput :width="180" label="Password" :value="password" :height="40" @update:modelValue="setPassword"/>
      <CustomInput :width="180" label="Password" :value="repeatPassword" :height="40" @update:modelValue="setRepeatPassword"/>
      <Button :width="180" label="SIGN UP" :height="40" @click="submit"/>
    </div>
    <router-link class="redirect-link" to="/registration">
      LOG IN
    </router-link>
  </div>
</template>

<script setup lang="ts">
import CustomInput from "@/components/UI/CustomInput.vue";
import Button from "@/components/UI/Button.vue";
import {ref} from "vue";
import $api from "@/utils/api";
import {useRouter} from "vue-router";
import {setCookie} from "@/utils/cookies";

const router = useRouter();

const login = ref<string>("");

const password = ref<string>("");

const repeatPassword = ref<string>("");

const setLogin = (value: string) => {
  login.value = value;
}

const setPassword = (value: string) => {
  password.value = value;
}

const setRepeatPassword = (value: string) => {
  repeatPassword.value = value;
}
const submit = async () => {
  if(login.value === "" || password.value === "")
    return;
  if(password.value !== repeatPassword.value)
    return;
  const {data, status} = await $api.post("/Auth/register", {username: login.value, password: password.value, email: login.value});
  console.log(data);
  setCookie("access_token", "123", 1);
  await router.push({path: "/"});
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
