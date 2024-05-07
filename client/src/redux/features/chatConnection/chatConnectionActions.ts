import { PayloadAction } from "@reduxjs/toolkit";

export const SendMessageActionType = "chatConnection/sendMessage";
export const InitDialogActionType = "chatConnection/initDialog";

export const sendMessage = (chatId: number, content: string): PayloadAction<{ chatId: number, content: string }> => {
    return {
        type: SendMessageActionType,
        payload: { chatId, content },
    };
};

export const initDialog = (recipientId: number, message: string): PayloadAction<{ recipientId: number, message: string }> => {
    return {
        type: InitDialogActionType,
        payload: { recipientId, message },
    };
};