import logo from '../Images/icons8-еда-100.png';
import s from '../Styles/Components/Navbar.module.css';

type Props = {}

const Navbar = (props: Props) => {
  return (
    <div className={s.navbar_box}>
        <div className={s.navbar_wrapp}>
            <img className={s.logo} src={logo} alt='logo' />
        </div>
    </div>
  )
}

export default Navbar