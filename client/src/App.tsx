import { Route, Routes } from 'react-router-dom';
import './App.css';
import RecipePage from './Pages/RecipePage';
import RecipeInfoPage from './Pages/RecipeInfoPage';

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={<RecipePage />} />
        <Route path="/recipe/:id" element={<RecipeInfoPage />} />
      </Routes>
    </div>
  );
}

export default App;
