import axios from 'axios';
import BASE_API from './baseApi';
import { Chat } from '../models/chat';

interface ChatsApi {
    find(userName: string): Promise<Chat[]>,
    get(userId: number): Promise<Chat[]>,
    getById(chatId: number): Promise<Chat>,
}

const chatsApi: ChatsApi = {
    find: async function (userName: string): Promise<Chat[]> {
        return (await axios.get<Chat[]>(`${BASE_API}/chats/search/${userName}`)).data;
    },

    get: async function (userId: number): Promise<Chat[]> {
        return (await axios.get<Chat[]>(`${BASE_API}/users/${userId}/chats`)).data;
    },

    getById: async function (chatId: number): Promise<Chat> {
        return (await axios.get<Chat>(`${BASE_API}/chats/${chatId}`)).data;
    },
}

export default chatsApi;