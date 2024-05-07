import { Link } from "react-router-dom";
import { Chat } from "../../models/chat";
import { ChatType } from "../../models/chatType";

export default function ChatCard({ chat }: { chat: Chat }) {
  const chatMessages = [...chat.messages ?? []].sort((a, b) => Date.parse(a.createdAt.toString()) - Date.parse(b.createdAt.toString()));
  const lastMessage: string = chatMessages.length ? chatMessages[chatMessages.length-1].content : "";
  const userOrChannelId = chat.type === ChatType.Personal || chat.type === ChatType.Dialog
    ? chat.recipient!.id
    : -chat.channel!.id;
  return (
    <Link className="flex p-4 hover:bg-slate-50" to={`chats/${userOrChannelId}`} state={chat}>
      <img
        className="object-scale-down rounded-full pr-4"
        src="https://gravatar.com/avatar/205e460b479e2e5b48aec07710c08d50?s=50"
        alt="avatar"
      />
      <div className="flex flex-col items-start">
        <p className="font-medium truncate">{chat.name}</p>
        <p className="truncate">{lastMessage}</p>
      </div>
    </Link>
  );
}
