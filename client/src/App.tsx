import { Route, Routes } from 'react-router-dom';
import './App.css';
import RecipePage from './Pages/RecipePage';
import RecipeInfoPage from './Pages/RecipeInfoPage';
import { UserProvider } from './Context/userAuth';

function App() {
  return (
    <div className="App">
      <UserProvider>
        <Routes>
          <Route path="/" element={<RecipePage />} />
          <Route path="/recipe/:id" element={<RecipeInfoPage />} />
        </Routes>
      </UserProvider>
    </div>
  );
}

export default App;
