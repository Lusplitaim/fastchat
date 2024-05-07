import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { Chat } from "../../../models/chat";
import { RootState } from "../../store";
import { ChatMessage } from "../../../models/chatMessage";

interface UserChatsState {
    chats: Chat[],
    filteredChats: Chat[],
}

const initState: UserChatsState = { chats: [], filteredChats: [] };

export const userChatsSlice = createSlice({
    name: "userChats",
    initialState: initState,
    reducers: {
        setChats: (state, action: PayloadAction<Chat[]>) => {
            action.payload.forEach(c => {
                if (!c.messages) {
                    c.messages = [];
                }
            });
            state.chats = action.payload;
        },
        addChat: (state, action: PayloadAction<Chat>) => {
            state.chats.push(action.payload);
        },
        removeChats: (state) => {
            state.chats = [];
        },
        updateMessages: (state, action: PayloadAction<{ chatId: number, messages: ChatMessage[] }>) => {
            const chat = state.chats.find(c => c.id === action.payload.chatId);
            if (chat === undefined) {
                return;
            }
            action.payload.messages.forEach(nmsg => {
                let msg = chat.messages.find(msg => msg.id === nmsg.id);
                if (msg != undefined) {
                    msg = nmsg;
                } else {
                    chat.messages.push(nmsg);
                }
            });
        },
        addMessage: (state, action: PayloadAction<ChatMessage>) => {
            const msg = action.payload;
            const chat = state.chats.find(c => c.id === msg.chatId);
            if (chat != undefined) {
                chat.messages.push(msg);
            }
        },
    },
});

export const { setChats, removeChats, updateMessages, addMessage, addChat } = userChatsSlice.actions;

export const selectChats = (state: RootState): Chat[] => state.userChats.chats;
export const selectChatByUserId = (state: RootState, userId: number): Chat | undefined => state.userChats.chats.find(c => c.recipient?.id === userId);
export const selectChatByChannelId = (state: RootState, channelId: number): Chat | undefined => state.userChats.chats.find(c => c.channel?.id === channelId);

export default userChatsSlice.reducer;