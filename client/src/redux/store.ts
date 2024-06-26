import { Tuple, combineReducers, configureStore } from "@reduxjs/toolkit";
import currentUserReducer  from "./features/currentUser/currentUserSlice";
import userChatsReducer from "./features/userChats/userChatsSlice";
import { signalrMiddleware } from "./middlewares/signalrMiddleware";
import connectionSliceReducer from "./features/connection/connectionSlice";

const rootReducer = combineReducers({
    currentUser: currentUserReducer,
    userChats: userChatsReducer,
    connectionState: connectionSliceReducer,
});

export const store = configureStore({
    reducer: rootReducer,
    middleware: getDefaultMiddleware => new Tuple(signalrMiddleware),
});

export type RootState = ReturnType<typeof rootReducer>;
export type AppDispatch = typeof store.dispatch;