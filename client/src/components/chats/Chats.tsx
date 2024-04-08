import { useState, useEffect, useRef } from "react";
import ChatCard from "../chat-card/ChatCard";
import Spinner from "../spinner/Spinner";
import chatsApi from "../../api/chats.api";
import debounce from "../../utils/debounce";
import { Chat } from "../../models/chat";

export default function Chats() {
  const [loaded, setLoaded] = useState<boolean>(true);
  const [chats, setChats] = useState<Chat[]>([]);
  const inputRef = useRef<HTMLInputElement>(null);

  /* useEffect(() => {
    const response = chatsApi.get();
    response.then((data) => {
      console.log(data);
      setLoaded(true);
    });
  }, []); */

  function findUser() {
    setLoaded(true);
    chatsApi.get(inputRef.current!.value).then((data) => {
      let chats: Chat[] = [];
      for (const userName of data) {
        chats.push({ name: userName });
      }
      setChats(chats);
    });
  }

  let chatItems = chats.map((c) => <ChatCard chat={c} />);

  if (loaded) {
    return (
      <div className="flex flex-col">
        <div className="flex justify-center w-full my-5">
          <input
            type="text"
            className="transition w-full mx-3 rounded-xl border-2 border-slate-100 bg-slate-100 focus:bg-white focus:outline-none duration-300 p-1 pl-3"
            placeholder="Search"
            onChange={debounce(findUser, 1000)}
            ref={inputRef}
          />
        </div>
        <div className="flex flex-col overflow-y-auto">
          {chatItems}
        </div>
      </div>
    );
  } else {
    return (
      <div className="h-full flex justify-center items-center">
        <Spinner />
      </div>
    );
  }
}
