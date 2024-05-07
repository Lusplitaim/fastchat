import axios from "axios";
import { User } from "../models/user";
import BASE_API from "./baseApi";
import { Chat } from "../models/chat";

interface UsersApi {
    get(userId: number): Promise<User>,
    getChat(userId: number): Promise<Chat>,
}

const usersApi: UsersApi = {
    get: async function (userId: number): Promise<User> {
        return (await axios.get<User>(`${BASE_API}/users/${userId}`)).data;
    },

    getChat: async function (userId: number): Promise<Chat> {
        return (await axios.get<Chat>(`${BASE_API}/users/${userId}/chat`)).data;
    },
}

export default usersApi;