import { API_BASE_URL } from "@/utils/consts";
import axios from "axios";

const $api = axios.create({
    baseURL: API_BASE_URL,
});

export default $api;
