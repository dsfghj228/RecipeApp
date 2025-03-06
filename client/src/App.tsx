import { Route, Routes } from 'react-router-dom';
import './App.css';
import RecipePage from './Pages/RecipePage';
import RecipeInfoPage from './Pages/RecipeInfoPage';
import { UserProvider } from './Context/userAuth';
import LoginPage from './Pages/LoginPage';
import RegisterPage from './Pages/RegisterPage';
import ProfilePage from './Pages/ProfilePage';
import NewRecipePage from './Pages/NewRecipePage';
import ProtectedRoute from './Routes/ProtectedRoute';

function App() {
  return (
    <div className="App">
      <UserProvider>
        <Routes>
          <Route path="/" element={<RecipePage />} />
          <Route path="/recipe/:id" element={<RecipeInfoPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/profile" element={<ProfilePage />} />
          <Route path="/newRecipe" element={<ProtectedRoute><NewRecipePage /></ProtectedRoute>} />
        </Routes>
      </UserProvider>
    </div>
  );
}

export default App;
