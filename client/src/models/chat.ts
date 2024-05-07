import { ChatChannel } from "./chatChannel";
import { ChatMessage } from "./chatMessage";
import { ChatType } from "./chatType";
import { ChatUser } from "./chatUser";

export interface Chat {
    id: number;
    name: string;
    type: ChatType;
    recipient?: ChatUser;
    channel?: ChatChannel;
    messages: ChatMessage[];
}