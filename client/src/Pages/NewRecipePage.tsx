import React, { useState } from 'react'
import { postNewRecipe, uploadPhoto } from '../Api/api';
import { useNavigate } from 'react-router-dom';

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
        return <li>{e.name} <button onClick={() => deleteIngredient(e)}>🗑</button></li>
    })

    const addIngredient = () => {
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

    const selectUnit = units.map(e => {
        return <option value={e}>{e}</option>
    })

    const steps = instruction.map(e => {
        return <div>
            <h2>Шаг №{instruction.findIndex(i => i.step === e.step) + 1}</h2>
            <span>{e.step}</span>
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
    <div>
        <h2>Добавление нового рецепта:</h2>
        <div>
            <div>
                <p>Название блюда:</p>
                <input type='text'
                       placeholder='Введите название вашего блюда'
                       value={name}
                       onChange={(e) => setName(e.target.value)} />
                <p>Описание блюда:</p>
                <input type='text'
                       placeholder='Введите краткое описание'
                       value={description}
                       onChange={(e) => setDescription(e.target.value)} />
            </div>
            
            <h3>Параметры блюда:</h3>
            <div>
                <p>Порции:</p>
                <div>
                    <button disabled={servings === 0} onClick={() => setServings(servings - 1)}>-</button>
                    <p>{servings}</p>
                    <button onClick={() => setServings(servings + 1)}>+</button>
                </div>
                <p>Будет готово через:</p>
                <div>
                    <input type="number"
                        max={12}
                        min={0}
                        value={hours}
                        onChange={(e) => setHours(Number(e.target.value))}/>
                    <p>Часов</p>
                    <input type="number"
                        max={59}
                        min={0}
                        value={minutes}
                        onChange={(e) => setMinutes(Number(e.target.value))}/>
                    <p>Минут</p>
                </div>
            </div>

            <h3>Ингредиенты:</h3>
            <div>
                {ingredients.length !== 0 && <ul>{ingredientsName}</ul>}
                <div>
                    <input type='text'
                           placeholder='Введите название ингредиета'
                           value={ingredientName}
                           onChange={(e) => setIngredientName(e.target.value)}/>
                    <div>
                        <input type='number' 
                               placeholder='Кол-во'
                               min={0}
                               value={quantity}
                               onChange={(e) => setQuanitiy(e.target.value)}/>
                        <select onChange={(e) => setUnit(e.target.value)} value={unit}>
                            <option value="">Ед. Измерения</option>
                            {selectUnit}
                        </select>
                    </div>
                </div>
            </div>
            <button onClick={() => addIngredient()}>Добавить ингредиент</button>

            <h3>Пошаговая инструкция:</h3>
            <div>
                {steps}
                <div>
                    <input type='text'
                           placeholder='Введите описание шага'
                           value={step}
                           onChange={(e) => setStep(e.target.value)}/>
                    <button onClick={() => addStep()}>Добавить шаг</button>
                </div>
            </div>

            <h3>Добавьте фото:</h3>
            <div>
                <input type="file" onChange={handleFileChange} />
            </div>

            <button disabled={disabledPostBtn()} onClick={() => postRecipe()}>Опубликовать рецепт</button>
        </div>
    </div>
  )
}

export default NewRecipePage