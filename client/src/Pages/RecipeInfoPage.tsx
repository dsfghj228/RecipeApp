import React, { useEffect, useState } from 'react'
import { getRecipe } from '../Api/api';
import Navbar from '../Components/Navbar';
import { Link, useParams } from 'react-router-dom';
import s from '../Styles/Pages/RecipeInfoPage.module.css';

type Recipe = {
    id: string;
    name: string;
    description: string;
    cookTime: string;
    servings: number;
    ingredients:
        {
          name: string;
          quantity: string;
          unit: string;
        }[];
    instruction:
        {
            step: string;
        }[];
    photoName: string;
    userName: string;
}

const RecipeInfoPage = () => {
    const [ recipe, setRecipe ] = useState<Recipe | void>();
    const { id } = useParams<string>();
    let stepsCount = 0;

    useEffect(() => {
        const getData = async (id: string) => {
            const data = await getRecipe(id);

            setRecipe(data);
        }

        getData(id || "");
    }, [])

    const ingredients = recipe?.ingredients.map((i) => {
        return (
            <li className={s.ingredient}>{i.name} {i.quantity}{i.unit}</li>
        )
    })

    const steps = recipe?.instruction.map((i) => {
        stepsCount += 1;
        return (
            <div className={s.step_box}>
                <div className={s.step_number}>{stepsCount} Шаг</div>
                <div className={s.step}>{i.step}</div>
            </div>
        )
    })


  return (
    <div>
        <div className={s.recipe_info_box}>
            <div className={s.back_link_box}>
            <Link to="/"><p className={s.back_link}>Обратно к списку рецептов</p></Link>
            </div>
            <img className={s.recipe_photo} src={`http://localhost:5275/api/photos/${recipe?.photoName}`} alt={recipe?.photoName}/>
            <p className={s.author}>Автор: {recipe?.userName}</p>
            <p className={s.name}>{recipe?.name}</p>
            <p className={s.description}>{recipe?.description}</p>
            <p className={s.cooktime}>Время готовки: {recipe?.cookTime}</p>
            <p className={s.servings}>Количество порций: {recipe?.servings}</p>
            <p className={s.ingredients}>Ингредиенты:</p>
            <ul className={s.ingredients_list}>
                {ingredients}
            </ul>
            <p className={s.recipe_steps}>Пошаговый рецепт:</p>
            {steps}
        </div>
    </div>
  )
}

export default RecipeInfoPage