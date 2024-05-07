import { useEffect, useRef, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../redux/hooks";
import { initDialog, sendMessage } from "../../redux/features/chatConnection/chatConnectionActions";
import { selectChatByChannelId, selectChatByUserId } from "../../redux/features/userChats/userChatsSlice";
import Spinner from "../spinner/Spinner";
import { Chat } from "../../models/chat";
import usersApi from "../../api/users.api";
import channelsApi from "../../api/channels.api";

function ChatSpace() {
  const textareaRef = useRef<HTMLTextAreaElement>(null);

  const { userOrChannelId } = useParams();
  const dispatch = useAppDispatch();
  const chatFromStore = useAppSelector((state) => {
    return isChannel()
      ? selectChatByChannelId(state, -getUserOrChannelId())
      : selectChatByUserId(state, getUserOrChannelId());
  });
  const [chat, setChat] = useState<Chat | undefined>(undefined);
  const navigate = useNavigate();

  function getUserOrChannelId(): number {
    return Number.parseInt(userOrChannelId!);
  }
  function isChannel(): boolean {
    return getUserOrChannelId() < 0;
  }

  useEffect(() => {
    const getChat = async () => {
      let chatData: Chat | undefined = chatFromStore;
      try {
        if (!chatFromStore) {
          chatData = isChannel()
            ? await channelsApi.getChat(-getUserOrChannelId())
            : await usersApi.getChat(getUserOrChannelId());
        }
      } catch {
        navigate("/");
      }

      setChat(chatData);
    };

    getChat();
  }, [chatFromStore, userOrChannelId]);

  async function SendMessageHandler(event: React.KeyboardEvent<HTMLTextAreaElement>) {
    if (!textareaRef.current?.value.trim() || !chat) {
      return;
    }

    if (
      event.code === "Enter" &&
      !event.shiftKey &&
      textareaRef.current?.value
    ) {
      event.preventDefault();

      const messageContent = textareaRef.current!.value;

      !chat.id
        ? dispatch(initDialog(chat.recipient!.id, messageContent))
        : dispatch(sendMessage(chat.id, messageContent));

      textareaRef.current!.value = "";
    } else {
      return;
    }
  }

  if (!chat) {
    return (
      <div className="flex flex-1 flex-col">
        <Spinner />
      </div>
    );
  }

  const messageElements = chat.messages?.map((msg) => (
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

export default ChatSpace;