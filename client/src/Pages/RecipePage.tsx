import React from 'react'
import Navbar from '../Components/Navbar'
import RecipesGrid from '../Components/RecipesGrid'

type Props = {}

const RecipePage = (props: Props) => {
  return (
    <div>
        <Navbar />
        <RecipesGrid />
    </div>
  )
}

export default RecipePage