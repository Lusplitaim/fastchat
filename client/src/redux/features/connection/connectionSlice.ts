import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { RootState } from "../../store";

interface ConnectionState {
    connected: boolean,
}

const initState: ConnectionState = {
    connected: false
};

export const connectionSlice = createSlice({
    name: "connection",
    initialState: initState,
    reducers: {
        setConnectionState: (state, action: PayloadAction<boolean>) => {
            state.connected = action.payload;
        },
    },
});

export const { setConnectionState } = connectionSlice.actions;

export const selectConnectionState = (state: RootState): ConnectionState => state.connectionState;

export default connectionSlice.reducer;