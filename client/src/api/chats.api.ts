import axios from 'axios';
import BASE_API from './baseApi';

interface ChatsApi {
    get(userName: string): Promise<string[]>,
}

const chatsApi: ChatsApi = {
    get: async function (userName: string): Promise<string[]> {
        return (await axios.get<string[]>(`${BASE_API}/chats/${userName}`)).data;
    },
}

export default chatsApi;