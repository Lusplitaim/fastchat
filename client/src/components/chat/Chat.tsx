import { useRef, useState } from "react";
import { ChatMessage } from "../../models/chatMessage";

function Chat() {
  const [messages, setMessages] = useState<ChatMessage[]>([]);
  const textareaRef = useRef<HTMLTextAreaElement>(null);

  function SendMessageHandler(event: React.KeyboardEvent<HTMLTextAreaElement>) {
    if (!textareaRef.current?.value.trimEnd()) {
      return;
    }
    
    let message: ChatMessage = {
      id: 0,
      content: ""
    };
    if (
      event.code === "Enter" &&
      !event.shiftKey &&
      textareaRef.current?.value
    ) {
      event.preventDefault();

      const messageId = Math.random() * 1000;
      message.id = messageId;
      message.content = textareaRef.current!.value;

      textareaRef.current!.value = "";
    } else {
      return;
    }

    setMessages((items) => (message ? [...items, message] : items));
  }

  const messageElements = messages.map((msg) => (
    <div className="bg-white px-4 py-3 my-1 rounded-t-2xl rounded-br-2xl self-start max-w-md" key={msg.id}>
      <p>{msg.content}</p>
    </div>
  ));

  return (
    <div className="flex flex-1 flex-col">
      <div className="flex flex-col p-5 bg-white w-full">
        <h3 className="font-semibold">Some name</h3>
      </div>

      <div className="flex flex-col flex-1 p-6 overflow-y-auto">{messageElements}</div>

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
