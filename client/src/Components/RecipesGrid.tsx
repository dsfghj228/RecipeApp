import { useEffect, useState } from 'react';
import s from '../Styles/Components/RecipesGrid.module.css';
import RecipeCart from './RecipeCart';
import { getAllRecipesFromDb } from '../Api/api';

type Recipe = {
  id: string;
  name: string;
  description: string;
  photoName: string;
}

type Props = {
  search: string;
  setSearch: React.Dispatch<React.SetStateAction<string>>;
}

const RecipesGrid = ({ search, setSearch}: Props) => {

  const [ recipes, setRecipes ] = useState<Recipe[]>([]);

    useEffect(() => {
      const getRecipes = async () => {
        const data = await getAllRecipesFromDb(search);
    
        setRecipes(data);
      }

      getRecipes();
    }, [search])
    
    const recipeCarts = recipes.map((r) => {
      return <RecipeCart id={r.id} name={r.name} description={r.description} photoName={r.photoName} />
    })

  return (

    <div className={s.grid_wrapper}>
        <div className={s.container}>
          {recipeCarts}
        </div>
    </div>
  )
}

export default RecipesGrid