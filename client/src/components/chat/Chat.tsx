import { useRef, useState } from "react";

function Chat() {
  const [messages, setMessages] = useState<string[]>([]);
  const textareaRef = useRef<HTMLTextAreaElement>(null);

  function SendMessageHandler(event: React.KeyboardEvent<HTMLTextAreaElement>) {
    let message: string;
    if (event.code === "Enter" && !event.shiftKey && textareaRef.current?.value) {
      event.preventDefault();
      message = textareaRef.current!.value;
      textareaRef.current!.value = '';
    }

    setMessages(items => message ? [...items, message] : items);
  }

  const messageElements = messages.map((msg) => (
    <div className="bg-white px-4 py-3 my-1 rounded-t-3xl rounded-br-3xl self-start max-w-md">
      <p>{msg}</p>
    </div>
  ));

  return (
    <div className="flex flex-1 flex-col justify-between">
      <div className="flex flex-col p-6 overflow-y-auto">{messageElements}</div>

      <textarea
        className="bg-white px-2 min-h-20 max-h-30"
        ref={textareaRef}
        placeholder="Write message"
        onKeyDown={SendMessageHandler}
      ></textarea>
    </div>
  );
}

export default Chat;
