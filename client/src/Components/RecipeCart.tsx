import { Link } from "react-router-dom";
import s from "../Styles/Components/RecipeCart.module.css";

type Recipe = {
  id: string;
  name: string;
  description: string
  photoName: string
}

const RecipeCart = ({ id, name, description, photoName}: Recipe) => {
  

  return (
    <Link to={`/recipe/${id}`}>
      <div className={s.cart_container}>
        <img className={s.recipe_photo} src={`http://localhost:5275/api/photos/${photoName}`} alt={photoName}/>
        <div className={s.recipe_name}>
          {name}
        </div>
        <div className={s.recipe_description}>
          {description}
        </div>
    </div>
    </Link>
  )
}

export default RecipeCart