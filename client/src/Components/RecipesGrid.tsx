import s from '../Styles/Components/RecipesGrid.module.css';
import RecipeCart from './RecipeCart';

type Props = {}

const RecipesGrid = (props: Props) => {
  return (
    <div className={s.grid_wrapper}>
        <div className={s.container}>
            <RecipeCart />
        </div>
    </div>
  )
}

export default RecipesGrid