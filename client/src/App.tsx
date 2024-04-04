import { Outlet } from "react-router-dom";
import "./App.css";
import Chats from "./components/chats/Chats";

function App() {
  return (
    <div className="flex h-full">
      <div className="basis-1/3 resize-x border-r-2 border-slate-200">
        <Chats />
      </div>
      <div className="flex flex-1 bg-sky-200">
        <Outlet />
      </div>
    </div>
  );
}

export default App;
