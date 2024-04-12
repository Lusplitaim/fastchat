import { Link } from "react-router-dom";
import { Chat } from "../../models/searchChat";

export default function ChatCard({ chat }: { chat: Chat }) {
  return (
    <Link className="flex p-4 hover:bg-slate-50" to={`chats/${chat.linkName}`} state={chat}>
      <img
        className="object-scale-down rounded-full pr-4"
        src="https://gravatar.com/avatar/205e460b479e2e5b48aec07710c08d50?s=50"
        alt="avatar"
      />
      <div className="flex flex-col items-start">
        <p className="font-medium truncate">{chat.name}</p>
        <p className="truncate">Ipsum no lora bas epicrum</p>
      </div>
    </Link>
  );
}
