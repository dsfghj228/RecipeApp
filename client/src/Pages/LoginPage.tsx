import React, { FormEvent, useEffect, useState } from 'react'
import { useAuth } from '../Context/userAuth'
import { Link, useNavigate } from 'react-router-dom';
import s from "../Styles/Pages/LoginRegisterPage.module.css";

const LoginPage = () => {
    const { login } = useAuth();
    const navigate = useNavigate();
    const [ username, setUsername ] = useState<string>("");
    const [ password, setPassword ] = useState<string>("");

    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();
    };

    const onClick = () => {
        try {
            login(username, password);
            navigate("/");
        } catch (err)
        {
            console.log(err);
        }
    };


  return (
    <div className={s.register_wrapp}>
        <h2>Войти</h2>
        <form onSubmit={handleSubmit} className={s.input_form}>
            <input placeholder='Username'
                   type='username'
                   value={username}
                   onChange={(e) => setUsername(e.target.value)}/>
            <input placeholder='Password'
                   type='password'
                   value={password}
                   onChange={(e) => setPassword(e.target.value)}/>
        </form>
        <div className={s.link_box}>
          <Link to="/register"><p>Нет аккаунта?</p></Link>
        </div>
        <button onClick={() => onClick()}>Войти</button>
    </div>
  )
}

export default LoginPage