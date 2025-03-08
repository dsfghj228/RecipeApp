import React, { createContext, useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { loginApi, registerApi,} from "../Services/AuthService";
import axios from "axios";

type UserProfile = {
    userName: string;
    email: string;
}

type UserContextType = {
    user: UserProfile | null;
    token: string | null;
    register: (email: string, username: string, password: string) => void;
    login: (username: string, password: string) => void;
    logout: () => void;
    isLoggedIn: () => boolean;
}

const UserContext = createContext<UserContextType>({} as UserContextType);

export const UserProvider: React.FC<{children: React.ReactNode}> = ({ children }) => {
    const navigate = useNavigate();
    const [ token, setToken ] = useState<string | null>(null);
    const [ user, setUser ] = useState<UserProfile | null>(null);
    const [ isReady, setIsReady ] = useState(false);

    useEffect(() => {
        const user = localStorage.getItem("user");
        const token = localStorage.getItem("token");
        if(user && token)
        {
            setUser(JSON.parse(user));
            setToken(token);
            axios.defaults.headers.common["Authorization"] = "Bearer " + token;
        }
        setIsReady(true);
    }, [])

    const register = async (username: string, email: string, password: string) => {
        try {
            const res = await registerApi( email, username,password);
            if (res) {
                localStorage.setItem("token", res?.data.token);
                localStorage.setItem("user", JSON.stringify({
                    userName: res?.data.userName,
                    email: res?.data.email,
                }));
                setToken(res?.data.token);
                setUser({ userName: res?.data.userName, email: res?.data.email });
                navigate("/");
            }
        } catch (e) {
            console.error('Registration failed:', e);
        }
    };

    const login = async (username: string, password: string) => {
        await loginApi(username, password)
                .then((res) => {
                    if(res)
                    {
                        localStorage.setItem("token", res?.data.token);
                        const userObj = {
                            userName: res?.data.userName,
                            email: res?.data.email
                        }
                        localStorage.setItem("user", JSON.stringify(userObj));
                        setToken(res?.data.token!);
                        setUser(userObj!);
                        navigate("/");
                    }
            }).catch(e => console.log(e));
    };

    const isLoggedIn = () => {
        return !!user; // преобразование в булевый тип
    };

    const logout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        setUser(null);
        setToken("");
        navigate("/login");
    };

    return (
        <UserContext.Provider value={{ login, user, token, logout, isLoggedIn, register }}>
            {isReady ? children : null}
        </UserContext.Provider>
    )
};

export const useAuth = () => React.useContext(UserContext);