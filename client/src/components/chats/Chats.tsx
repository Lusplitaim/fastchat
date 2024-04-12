import { useState, useRef, useEffect } from "react";
import ChatCard from "../chat-card/ChatCard";
import Spinner from "../spinner/Spinner";
import chatsApi from "../../api/chats.api";
import debounce from "../../utils/debounce";
import { Chat } from "../../models/searchChat";

export default function Chats() {
  const [loaded, setLoaded] = useState<boolean>(false);
  const [chats, setChats] = useState<Chat[]>([]);
  const inputRef = useRef<HTMLInputElement>(null);

  useEffect(() => {
    const response = chatsApi.get(7);
    response.then((data) => {
      setChats(data);
      setLoaded(true);
    });
  }, []);

  function findChats() {
    setLoaded(true);
    chatsApi.find(inputRef.current!.value).then((data) => {
      const chats: Chat[] = [];
      for (const chat of data) {
        chats.push(chat);
      }
      setChats(chats);
    });
  }

  const chatItems = chats.map((c) => <ChatCard chat={c} key={c.linkName} />);

  if (loaded) {
    return (
      <div className="flex flex-col">
        <div className="flex justify-center w-full my-5">
          <input
            type="text"
            className="transition w-full mx-3 rounded-xl border-2 border-slate-100 bg-slate-100 focus:bg-white focus:outline-none duration-300 p-1 pl-3"
            placeholder="Search"
            onChange={debounce(findChats, 1000)}
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
