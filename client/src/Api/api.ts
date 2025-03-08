import axios from 'axios';


const api = "http://localhost:5275/api";

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

//Recipes


export const getAllRecipesFromDb = async ( name: string, pageNumber: number = 1) => {
    const data = await axios.get(`${api}/allRecipes?PageNumber=${pageNumber}&Name=${name}`)
                        .then(r => r.data);

    return data;
}

export const getRecipe = async (id: string) => {
    const data = await axios.get(`${api}/recipes/${id}`)
                            .then(r => r.data);

    return data;
}

export const getRecipesCount = async (name: string) => {
  const data = await axios.get(`${api}/recipes/Count?Name=${name}`)
                          .then(r => r.data);
  
  return data;
}

export const postNewRecipe = async (recipe: Recipe) => {
    const data = await axios.post(`${api}/recipes`, recipe)
                            .then(r => r.data);

    return data;
}

//Photos

export const uploadPhoto = async (file: File | null): Promise<any> => {
    if(file !== null) {
        const formData = new FormData();
        formData.append('file', file);
  
    try {
      const response = await axios.post(`${api}/photos`, formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
      });
      return response.data;
    } catch (error) {
      console.error('Ошибка при загрузке фото:', error);
      throw error;
    }
    } else {
        return null;
    }
  };

  export const deletePhoto = async (fileName: string | undefined): Promise<void> => {
    try {
      await axios.delete(`${api}/photos/${fileName}`);
    } catch (error) {
      console.error('Ошибка при удалении фото:', error);
      throw error;
    }
  };

// User

export const updatePhoto = async (photoName: string) => {
  try {
    const data = axios.post(`${api}/account/uploadAvatar`,JSON.stringify(photoName), // Отправляем имя файла как строку в JSON
    {
        headers: {
            'Content-Type': 'application/json',  // Указываем, что данные в формате JSON
        }
    })
                      .then(r => r.data);
    return data;
  } catch (err)
  {
    console.log(err);
  }
};