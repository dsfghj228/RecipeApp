import axios from 'axios';


const api = "http://localhost:5275/api";


export const getAllRecipesFromDb = async (pageNumber: number = 1) => {
    const data = await axios.get(`${api}/allRecipes?PageNumber=${pageNumber}`)
                        .then(r => r.data);

    return data;
}

export const getRecipe = async (id: string) => {
    const data = await axios.get(`${api}/recipes/${id}`)
                            .then(r => r.data);

    return data;
}