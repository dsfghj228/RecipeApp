import React from 'react'
import { useAuth } from '../Context/userAuth'
import { Link } from 'react-router-dom';
import s from "../Styles/Pages/ProfilePage.module.css";

type Props = {}

const ProfilePage = (props: Props) => {
    const { user, logout } = useAuth();

  return (
    <div className={s.profile_info_box}>
        <div className={s.back_link_box}>
            <Link to="/"><p className={s.back_link}>Обратно к списку рецептов</p></Link>
        </div>
        <h1>Profile info:</h1>
        <div className={s.main_info_wrap}> 
            <div className={s.info}>
                <p>Username: </p>{user?.userName}
            </div>
            <div className={s.info}>
                <p>Email: </p>{user?.email}
            </div>
        </div>
        <button onClick={() => logout()}>Logout</button>
    </div>
  )
}

export default ProfilePage