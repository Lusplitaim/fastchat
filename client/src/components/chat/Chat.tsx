import { useEffect, useRef, useState } from "react";
import { ChatMessage } from "../../models/chatMessage";
import { useLocation } from "react-router-dom";
import * as signalR from "@microsoft/signalr";
import { SearchChat } from "../../models/searchChat";

function Chat() {
  const [messages, setMessages] = useState<ChatMessage[]>([]);
  const textareaRef = useRef<HTMLTextAreaElement>(null);
  const location = useLocation();
  const chat = location.state as SearchChat;
  const [chatHub, setChatHub] = useState<signalR.HubConnection | undefined>(undefined);

  useEffect(() => {
    if (!chat.id) {
      return;
    }
    const connection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7185/hub/chats")
      .configureLogging(signalR.LogLevel.Information)
      .build();

      connection.onclose(() => setChatHub(undefined));
      connection.onreconnecting(() => setChatHub(undefined));
      connection.onreconnected(() => setChatHub(connection));
      connection.on("ReceiveMessage", (message: ChatMessage) => {
        setMessages((items) => (message ? [...items, message] : items));
      });
      connection.start().then(() => {
        console.log("Connection established!");
        setChatHub(connection);
        connection.invoke("JoinChat", chat.id);
      });

      return () => {
        connection.stop();
        console.log("Connection closed");
      };
  }, [chat.id]);

  function SendMessageHandler(event: React.KeyboardEvent<HTMLTextAreaElement>) {
    if (!textareaRef.current?.value.trim()) {
      return;
    }

    if (
      event.code === "Enter" &&
      !event.shiftKey &&
      textareaRef.current?.value
    ) {
      event.preventDefault();

      const message = textareaRef.current!.value;

      chatHub?.invoke("SendMessage", chat.id, message);

      textareaRef.current!.value = "";
    } else {
      return;
    }

    //setMessages((items) => (message ? [...items, message] : items));
  }

  const messageElements = messages.map((msg) => (
    <div
      className="bg-white px-4 py-3 my-1 rounded-t-2xl rounded-br-2xl self-start max-w-md"
      key={msg.id}
    >
      <p>{msg.content}</p>
    </div>
  ));

  return (
    <div className="flex flex-1 flex-col">
      <div className="flex flex-col p-5 bg-white w-full">
        <h3 className="font-semibold">{chat.name}</h3>
      </div>

      <div className="flex flex-col flex-1 p-6 overflow-y-auto">
        {messageElements}
      </div>

      <textarea
        className="bg-white px-2 min-h-20 max-h-30 focus:outline-none"
        ref={textareaRef}
        placeholder="Write message"
        onKeyDown={SendMessageHandler}
      ></textarea>
    </div>
  );
}

export default Chat;
