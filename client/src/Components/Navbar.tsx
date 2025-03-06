import { Link } from 'react-router-dom';
import { useAuth } from '../Context/userAuth';
import logo from '../Images/icons8-еда-100.png';
import s from '../Styles/Components/Navbar.module.css';
import { useState } from 'react';

type Props = {
  search: string;
  setSearch: React.Dispatch<React.SetStateAction<string>>;
}

const Navbar = ({ search, setSearch}: Props) => {
  const { user } = useAuth();

  return (
    <div className={s.navbar_box}>
        <div className={s.navbar_wrapp}>
            <img className={s.logo} src={logo} alt='logo' />
            <div className={s.search_box}>
              <input placeholder='Введите название блюда'
                     className={s.search_input}
                     type='search'
                     value={search}
                     onChange={(e) => setSearch(e.target.value)}/>
            </div>
            {user ?
            <>
            <div className={s.new_recipe_box}>
              <Link to="/newRecipe">
                Опубликовать рецепт
              </Link>
            </div>
              <Link to="/profile">
                  <p className={s.username}>{user?.userName}</p>
                </Link>
            </> 
                : 
                <div className={s.login_btn}>
                  <Link to="/login">Войти в аккаунт</Link>
                </div>
            }
        </div>
    </div>
  )
}

export default Navbar