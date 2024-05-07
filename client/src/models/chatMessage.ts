export interface ChatMessage {
    chatId: number;
    id: string;
    content: string;
    createdAt: Date;
    authorId: number;
}