import axios from 'axios';
import BASE_API from './baseApi';
import { SearchChat } from '../models/searchChat';

interface ChatsApi {
    find(userName: string): Promise<SearchChat[]>,
    get(userId: number): Promise<SearchChat[]>,
}

const chatsApi: ChatsApi = {
    find: async function (userName: string): Promise<SearchChat[]> {
        return (await axios.get<SearchChat[]>(`${BASE_API}/chats/search/${userName}`)).data;
    },

    get: async function (userId: number): Promise<SearchChat[]> {
        return (await axios.get<SearchChat[]>(`${BASE_API}/users/${userId}/chats`)).data;
    },
}

export default chatsApi;