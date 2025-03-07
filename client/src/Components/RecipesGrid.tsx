import { useEffect, useState } from 'react';
import s from '../Styles/Components/RecipesGrid.module.css';
import RecipeCart from './RecipeCart';
import { getAllRecipesFromDb, getRecipesCount } from '../Api/api';

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
  const [ recipesInDBCount, setRecipesInDBCount ] = useState<number>(0);
  const [ currentPage, setCurrentPage ] = useState<number>(1);

    useEffect(() => {
      const getRecipes = async () => {
        const data = await getAllRecipesFromDb(search, currentPage);
        const recipesCount = await getRecipesCount();

        setRecipesInDBCount(recipesCount)
        setRecipes(data);
      }

      getRecipes();
    }, [search, currentPage])
    
    const recipeCarts = recipes.map((r) => {
      return <RecipeCart id={r.id} name={r.name} description={r.description} photoName={r.photoName} />
    })

    const returnPagesBtn = (pagesCount: number) => {
      let buttons = [];
  
      if (pagesCount >= 5) {
          if (currentPage + 4 <= pagesCount) {
              for (let i = currentPage; i < currentPage + 5; i++) {
                  if (i <= pagesCount) {
                      buttons.push(
                          <button className={i == currentPage? s.paginator_btn_current : s.paginator_btn} onClick={() => setCurrentPage(i)} key={i}>
                              {i}
                          </button>
                      );
                  }
              }
          } else {
              for (let i = pagesCount - 4; i <= pagesCount; i++) {
                  buttons.push(
                      <button className={i == currentPage? s.paginator_btn_current : s.paginator_btn} onClick={() => setCurrentPage(i)} key={i}>
                          {i}
                      </button>
                  );
              }
          }
      } else {
          for (let i = 1; i <= pagesCount; i++) {
              buttons.push(
                  <button className={i == currentPage? s.paginator_btn_current : s.paginator_btn} onClick={() => setCurrentPage(i)} key={i}>
                      {i}
                  </button>
              );
          }
      }
  
      return buttons;
  };
  


    const paginator = () => {
      let pagesCount = Math.ceil(recipesInDBCount / 12);
      return (
        <div className={s.paginator_btns_box}>
            <button className={s.prev_btn} disabled={currentPage === 1} onClick={() => setCurrentPage(currentPage-1)}>{"<"}</button>
            {returnPagesBtn(pagesCount)}
            <button className={s.next_btn} disabled={currentPage === pagesCount} onClick={() => setCurrentPage(currentPage+1)}>{">"}</button>
        </div>
      )
    }

  return (

    <div className={s.grid_wrapper}>
        <div className={s.container}>
          {recipeCarts}
        </div>
        {paginator()}
    </div>
  )
}

export default RecipesGrid