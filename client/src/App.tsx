import { Route, Routes } from 'react-router-dom';
import './App.css';
import RecipePage from './Pages/RecipePage';
import RecipeInfoPage from './Pages/RecipeInfoPage';
import { UserProvider } from './Context/userAuth';
import LoginPage from './Pages/LoginPage';
import RegisterPage from './Pages/RegisterPage';

function App() {
  return (
    <div className="App">
      <UserProvider>
        <Routes>
          <Route path="/" element={<RecipePage />} />
          <Route path="/recipe/:id" element={<RecipeInfoPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
        </Routes>
      </UserProvider>
    </div>
  );
}

export default App;
