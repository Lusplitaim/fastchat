import { useState, useRef, useEffect } from "react";
import ChatCard from "../chat-card/ChatCard";
import Spinner from "../spinner/Spinner";
import chatsApi from "../../api/chats.api";
import debounce from "../../utils/debounce";
import { Chat } from "../../models/chat";
import { useAppDispatch, useAppSelector } from "../../redux/hooks";
import {
  selectChats,
  setChats,
} from "../../redux/features/userChats/userChatsSlice";
import { selectCurrentUser } from "../../redux/features/currentUser/currentUserSlice";
import { LocalStorageKeys } from "../../storage/localStorageKeys";
import { ChatType } from "../../models/chatType";

export default function Chats() {
  const [loaded, setLoaded] = useState<boolean>(false);
  const inputRef = useRef<HTMLInputElement>(null);
  const [filteredChats, setFilteredChats] = useState<Chat[]>([]);
  const currentUser =
    useAppSelector(selectCurrentUser) ??
    JSON.parse(localStorage.getItem(LocalStorageKeys.authUser) ?? "");
  const userChats = useAppSelector(selectChats);
  const dispatch = useAppDispatch();

  useEffect(() => {
    const getChats = async () => {
      const chats = await chatsApi.get(currentUser.id);

      dispatch(setChats(chats));
      setLoaded(true);
    };

    getChats();
  }, []);

  function findChats() {
    if (!inputRef.current!.value) {
      setFilteredChats([]);
      return;
    }

    setLoaded(true);
    chatsApi.find(inputRef.current!.value).then((data) => {
      setFilteredChats(data);
    });
  }

  const chatItems = (filteredChats.length ? filteredChats : userChats).map(
    (c) => {
      const userOrChannelId = c.type === ChatType.Channel || c.type === ChatType.Group
        ? -c.channel!.id
        : c.recipient!.id;
      return <ChatCard chat={c} key={userOrChannelId} />;
    }
  );

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
        <div className="flex flex-col overflow-y-auto">{chatItems}</div>
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
