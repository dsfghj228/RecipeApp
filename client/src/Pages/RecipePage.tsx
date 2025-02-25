import React, { useState } from 'react'
import Navbar from '../Components/Navbar'
import RecipesGrid from '../Components/RecipesGrid'

type Props = {}

const RecipePage = (props: Props) => {
  const [ search, setSearch ] = useState<string>("");
  return (
    <div>
        <Navbar search={search} setSearch={setSearch}/>
        <RecipesGrid search={search} setSearch={setSearch}/>
    </div>
  )
}

export default RecipePage