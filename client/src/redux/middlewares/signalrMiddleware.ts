import * as signalR from "@microsoft/signalr";
import { Middleware, PayloadAction } from "@reduxjs/toolkit";
import { RootState } from "../store";
import { setCurrentUser } from "../features/currentUser/currentUserSlice";
import { addChat, addMessage, setChats } from "../features/userChats/userChatsSlice";
import { Chat } from "../../models/chat";
import { ChatMessage } from "../../models/chatMessage";
import {
  InitDialogActionType,
  SendMessageActionType,
} from "../features/chatConnection/chatConnectionActions";
import { LocalStorageKeys } from "../../storage/localStorageKeys";

export const signalrMiddleware: Middleware<{}, RootState> = (storeApi) => {
  const initConnection = async () => {
    if (
      connection?.state === signalR.HubConnectionState.Disconnected ||
      !connection
    ) {
      connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7185/hub/chats", {
          accessTokenFactory: () => loginToken!,
        })
        .configureLogging(signalR.LogLevel.Information)
        .build();
      connection.onclose(() => (connection = undefined));
      connection.on("ReceiveMessage", (msg: ChatMessage) => {
        storeApi.dispatch(addMessage(msg));
      });
      connection.on("ReceiveNewChat", async (chat: Chat) => {
        storeApi.dispatch(addChat(chat));
        await connection?.invoke("SubscribeToChat", chat.id);
      });
      await connection.start();
      console.log("Connection established");
    }
  };

  let connection: signalR.HubConnection | undefined;
  const loginToken = localStorage.getItem(LocalStorageKeys.authToken);

  return (next) => async (action) => {
    const payloadAction = action as PayloadAction<any, string>;
    switch (payloadAction.type) {
      case setCurrentUser.type:
        await initConnection();
        break;

      case setChats.type:
        await initConnection();
        const chatIds = (payloadAction.payload as Chat[]).map((c) => c.id);
        await connection?.invoke("SubscribeToChats", chatIds);
        console.log("Subscribed to chats");
        break;

      case SendMessageActionType:
        const sendMessageData = payloadAction.payload as {
          chatId: number;
          content: string;
        };
        await connection?.invoke(
          "SendMessage",
          sendMessageData.chatId,
          sendMessageData.content
        );
        console.log(
          `Message sent for chat ${sendMessageData.chatId}, "${sendMessageData.content}"`
        );
        break;

      case InitDialogActionType:
        const initDialogData = payloadAction.payload as {
          recipientId: number;
          message: string;
        };
        await connection?.invoke(
          "InitDialog",
          initDialogData.recipientId,
          initDialogData.message
        );
        console.log(
          `Init dialog for ${initDialogData.recipientId}, "${initDialogData.message}"`
        );
        break;
    }

    return next(action);
  };
};
