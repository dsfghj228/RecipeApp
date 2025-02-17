import axios from "axios";

type UserProfileToken = {
    userName: string;
    email: string;
    token: string;
}

const api = "http://localhost:5275/api";

export const loginApi = async (username: string, password: string) => {
    try{
        const data = await axios.post<UserProfileToken>(`${api}/account/login`, {
            username: username,
            password: password
        })
    
        return data;
    }catch(err)
    {
        console.log(err)
    }
}

export const registerApi = async (username: string, email: string, password: string) => {
    try{
        const data = await axios.post<UserProfileToken>(`${api}/account/login`, {
            username: username,
            email: email,
            password: password
        })
    
        return data;
    }catch(err)
    {
        console.log(err)
    }
}
