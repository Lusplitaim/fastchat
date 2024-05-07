import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { AppUser } from "../../../models/appUser";
import { RootState } from "../../store";

interface CurrentUserState {
    user?: AppUser,
}

const initState: CurrentUserState = {};

export const currentUserSlice = createSlice({
    name: "currentUser",
    initialState: initState,
    reducers: {
        setCurrentUser: (state, action: PayloadAction<AppUser>) => {
            state.user = action.payload;
        },
        removeCurrentUser: (state) => {
            state.user = undefined;
        },
    },
});

export const { setCurrentUser, removeCurrentUser } = currentUserSlice.actions;

export const selectCurrentUser = (state: RootState): AppUser => state.currentUser.user!;

export default currentUserSlice.reducer;