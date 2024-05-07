import axios from "axios";
import { Chat } from "../models/chat";
import BASE_API from "./baseApi";

interface ChannelsApi {
    getChat(channelId: number): Promise<Chat>,
}

const channelsApi: ChannelsApi = {
    getChat: async function (channelId: number): Promise<Chat> {
        return (await axios.get<Chat>(`${BASE_API}/channels/${channelId}/chat`)).data;
    },
}

export default channelsApi;