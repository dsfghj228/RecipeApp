import axios from 'axios';


const api = "http://localhost:5275/api";

//RECIPES

export const getAllRecipesFromDb = async (pageNumber: number = 1) => {
    const data = await axios.get(`${api}/allRecipes?PageNumber=${pageNumber}`)
                        .then(r => r.data);

    return data;
}

//PHOTOS

export const getPhoto = async (fileName: string) => {
    const data = await axios.get(`${api}/photos/${fileName}`, { responseType: 'blob' })
                        .then(r => r.data);

    console.log(data)
    return data;
}