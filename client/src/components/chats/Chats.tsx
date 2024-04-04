import ChatCard from "../chat-card/ChatCard";

export default function Chats() {
    return (
        <div className="flex flex-col overflow-y-auto">
            <ChatCard />
            <ChatCard />
            <ChatCard />
            <ChatCard />
        </div>
    );
}