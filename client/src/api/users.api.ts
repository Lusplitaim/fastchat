import axios from "axios";
import { User } from "../models/user";
import BASE_API from "./baseApi";

interface UsersApi {
    get(userName: string): Promise<User>,
}

const usersApi: UsersApi = {
    get: async function (userName: string): Promise<User> {
        return (await axios.get<User>(`${BASE_API}/users/${userName}`)).data;
    },
}

export default usersApi;