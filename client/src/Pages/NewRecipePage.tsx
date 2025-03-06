import React, { useState } from 'react'
import { postNewRecipe, uploadPhoto } from '../Api/api';
import { Link, useNavigate } from 'react-router-dom';
import s from "../Styles/Pages/NewRecipePage.module.css";

type Ingredient = {
    name: string;
    quantity: string;
    unit: string;
}

type Instruction = {
    step: string;
}

type Recipe = {
    name: string;
    description: string;
    cookTime: string;
    servings: number;
    ingredients: Ingredient[];
    instruction: Instruction[];
    photoName: string;
}

const NewRecipePage = () => {
    const [ name, setName ] = useState<string>("");
    const [ description, setDescription ] = useState<string>("");
    const [ servings, setServings ] = useState<number>(0);
    const [ hours, setHours ] = useState<number>(0);
    const [ minutes, setMinutes ] = useState<number>(0);
    const [ ingredientName, setIngredientName ] = useState<string>("");
    const [ quantity, setQuanitiy ] = useState<string>("");
    const [ unit, setUnit ] = useState<string>("");
    const [ ingredients, setIngredients] = useState<Ingredient[]>([])
    const [ step, setStep ] = useState<string>("");
    const [ instruction, setInstruction ] = useState<Instruction[]>([]);
    const [selectedFile, setSelectedFile] = useState<File | null>(null);
    const navigate = useNavigate();

    const units: string[] = [
        "г", "стакан", "ст.л.", "ч.л.", "шт", 
        "зубчик", "мл", "л", "кг", "лист", "пучок", 
        "перо", "головка", "щепотка", "веточка", 
        "кочан", "ломтик", "палочка", "долька", "по вкусу",
        "по желанию", "банка/ 70г", "банка/ 190г", "банка/ 120г", 
        "банка/ 150г", "банка/ 180г", "банка/ 200г", "банка/ 240г",
        "банка/ 250г", "банка/ 300г", "банка/ 350г", "банка/ 370г",
        "банка/ 380г", "банка/ 400г", "банка/ 450г", "банка/ 500г",
        "метр", "капля"
    ]

    const deleteIngredient = (ingr: Ingredient) => {
        const index = ingredients.findIndex(i => i.name === ingr.name);

        if(index !== -1)
        {
            const updatedIngredients = [...ingredients];
            updatedIngredients.splice(index, 1);
            setIngredients(updatedIngredients);
        }
    }
    

    const ingredientsName = ingredients.map((e) => {
        return <li className={s.ingredient}>
                <span className={s.ingredient_name}>{e.name}</span>
                <div className={s.ingredient_wrapp}>
                    <span>{e.quantity} {e.unit}</span>
                    <button className={s.ingredient_del_btn} onClick={() => deleteIngredient(e)}><svg width="30px" height="30px" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
<path d="M20.5001 6H3.5" stroke="#1C274C" stroke-width="1.5" stroke-linecap="round"/>
<path d="M18.8332 8.5L18.3732 15.3991C18.1962 18.054 18.1077 19.3815 17.2427 20.1907C16.3777 21 15.0473 21 12.3865 21H11.6132C8.95235 21 7.62195 21 6.75694 20.1907C5.89194 19.3815 5.80344 18.054 5.62644 15.3991L5.1665 8.5" stroke="#1C274C" stroke-width="1.5" stroke-linecap="round"/>
<path d="M9.5 11L10 16" stroke="#1C274C" stroke-width="1.5" stroke-linecap="round"/>
<path d="M14.5 11L14 16" stroke="#1C274C" stroke-width="1.5" stroke-linecap="round"/>
<path d="M6.5 6C6.55588 6 6.58382 6 6.60915 5.99936C7.43259 5.97849 8.15902 5.45491 8.43922 4.68032C8.44784 4.65649 8.45667 4.62999 8.47434 4.57697L8.57143 4.28571C8.65431 4.03708 8.69575 3.91276 8.75071 3.8072C8.97001 3.38607 9.37574 3.09364 9.84461 3.01877C9.96213 3 10.0932 3 10.3553 3H13.6447C13.9068 3 14.0379 3 14.1554 3.01877C14.6243 3.09364 15.03 3.38607 15.2493 3.8072C15.3043 3.91276 15.3457 4.03708 15.4286 4.28571L15.5257 4.57697C15.5433 4.62992 15.5522 4.65651 15.5608 4.68032C15.841 5.45491 16.5674 5.97849 17.3909 5.99936C17.4162 6 17.4441 6 17.5 6" stroke="#1C274C" stroke-width="1.5"/></svg></button>
                </div>
            </li>
    })

    const addIngredient = () => {
        if(ingredientName !== "" && quantity !== "" && unit !== "") {
            const ingredient: Ingredient = {
                name: ingredientName,
                quantity: quantity,
                unit: unit
            }
    
            ingredients.push(ingredient);
            setIngredientName("");
            setQuanitiy("");
            setUnit("");

        }

    }

    const selectUnit = units.map(e => {
        return <option value={e}>{e}</option>
    })

    const steps = instruction.map(e => {
        return <div className={s.step_box}>
            <h2 className={s.step_number}>Шаг №{instruction.findIndex(i => i.step === e.step) + 1}</h2>
            <span className={s.step_content}>{e.step}</span>
        </div>
    })

    const addStep = () => {
        const instructionStep: Instruction = {
            step: step
        }

        instruction.push(instructionStep);
        setStep('');
    }

    
  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files) {
      setSelectedFile(event.target.files[0]);
    }
  };

  const disabledPostBtn = () => {
    if(name === "" || description === "" 
        || servings === 0 || ingredients.length === 0
        || instruction.length === 0 
        || selectedFile === null 
        || (minutes === 0 && hours === 0))
    {
        return true;
    }

    return false;
  }

  const postRecipe = async () => {
    var cookTime: string = '';

    if (hours === 0) {
        cookTime = `${minutes} мин.`;
    } else if (minutes === 0) {
        cookTime = `${hours} ч.`;
    } else {
        cookTime = `${hours} ч. ${minutes} мин.`;
    }

    const photoName = await uploadPhoto(selectedFile).then(r => r?.fileName);

    if(photoName){
        const recipeForPost: Recipe = {
            name: name,
            description: description,
            cookTime: cookTime,
            servings: servings,
            ingredients: ingredients,
            instruction: instruction,
            photoName: photoName
        }

        postNewRecipe(recipeForPost);

        navigate("/")
    
    } else {
        console.log("ошибка");
    }
  }

  return (
    <div className={s.recipe_container}>
        <div className={s.back_link_box}>
            <Link to="/"><p className={s.back_link}>Обратно к списку рецептов</p></Link>
        </div>
        <h2 className={s.recipe_title}>Добавление нового рецепта:</h2>
        <div className={s.recipe_form}>
            <div className={s.input_group}>
                <p className={s.input_label}>Название блюда:</p>
                <input 
                    className={s.input_field} 
                    type="text"
                    placeholder="Введите название вашего блюда"
                    value={name}
                    onChange={(e) => setName(e.target.value)} 
                />
                <p className={s.input_label}>Описание блюда:</p>
                <textarea 
                    className={s.textarea_field}
                    placeholder="Введите краткое описание"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)} 
                />
            </div>
        
            <h3 className={s.section_title}>Параметры блюда:</h3>
            <div className={s.input_group}>
                <p className={s.input_label}>Порции:</p>
                <div className={s.servings_controls}>
                    <button 
                        className={s.servings_button} 
                        disabled={servings === 0} 
                        onClick={() => setServings(servings - 1)}
                    >
                        -
                    </button>
                    <p className={s.servings_count}>{servings}</p>
                    <button 
                        className={s.servings_button}
                        onClick={() => setServings(servings + 1)}
                    >
                        +
                    </button>
                </div>
                <p className={s.input_label}>Будет готово через:</p>
                <div className={s.time_inputs}>
                    <input 
                        className={s.time_input}
                        placeholder='0'
                        type="number"
                        max={12}
                        min={0}
                        value={hours === 0 ? "" : (hours > 12 ? 12 : hours)}
                        onChange={(e) => setHours(Number(e.target.value))}
                    />
                    <p className={s.time_hours_label}>Часов</p>
                    <input 
                        className={s.time_input}
                        placeholder='0'
                        type="number"
                        max={59}
                        min={0}
                        value={minutes === 0 ? "" : (minutes > 59 ? 59 : minutes)}
                        onChange={(e) => setMinutes(Number(e.target.value))}
                    />
                    <p className={s.time_label}>Минут</p>
                </div>
            </div>

            <h3 className={s.section_title}>Ингредиенты:</h3>
            <div className={s.ingredients_section}>
                {ingredients.length !== 0 && <ul className={s.ingredients_list}>{ingredientsName}</ul>}
                <div className={s.ingredient_input_group}>
                    <input 
                        className={s.ingredient_name_input}
                        type="text"
                        placeholder="Введите название ингредиета"
                        value={ingredientName}
                        onChange={(e) => setIngredientName(e.target.value)}
                    />
                    <div className={s.ingredient_quantity_group}>
                        <input 
                            className={s.ingredient_quantity_input}
                            type="number" 
                            placeholder="Кол-во"
                            min={0}
                            value={quantity}
                            onChange={(e) => setQuanitiy(e.target.value)}
                        />
                        <select 
                            className={s.ingredient_unit_select} 
                            onChange={(e) => setUnit(e.target.value)} 
                            value={unit}
                        >
                            <option value="">Ед. Изм</option>
                            {selectUnit}
                        </select>
                    </div>
                    <button className={s.add_ingredient_button} onClick={() => addIngredient()}>Добавить ингредиент</button>
            </div>
            </div>

            <h3 className={s.section_title}>Пошаговая инструкция:</h3>
            <div className={s.steps_section}>
                {steps}
                <div className={s.step_input_group}>
                    <input 
                        className={s.step_description_input}
                        type="text"
                        placeholder="Введите описание шага"
                        value={step}
                        onChange={(e) => setStep(e.target.value)}
                    />
                    <button className={s.add_step_button} onClick={() => addStep()}>Добавить шаг</button>
                </div>
            </div>

            <h3 className={s.section_title}>Добавьте фото:</h3>
            <div className={s.file_upload}>
                <span className={s.format_warning}>Выложите фотографию в формате .jpg</span>
                <input 
                    className={s.file_input} 
                    type="file" 
                    onChange={handleFileChange} 
                />
            </div>

            <button 
            className={s.post_recipe_button} 
            disabled={disabledPostBtn()} 
            onClick={() => postRecipe()}
        >
            Опубликовать рецепт
            </button>
    </div>
</div>

  )
}

export default NewRecipePage