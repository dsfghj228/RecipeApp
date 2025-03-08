import React, { useState } from 'react'
import { useAuth } from '../Context/userAuth'
import { Link } from 'react-router-dom';
import s from "../Styles/Pages/ProfilePage.module.css";
import logo from "../Images/avatar.jpg";
import { deletePhoto, updatePhoto, uploadPhoto } from '../Api/api';

type Props = {}

const ProfilePage = (props: Props) => {
    const { user, logout, changeUser } = useAuth();
    const [ isEditing, setIsEditing ] = useState<boolean>(false);
    const [selectedFile, setSelectedFile] = useState<File | null>(null);

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.files) {
          setSelectedFile(event.target.files[0]);
        }
      };

    const changeAvatar = async () => {
        if(selectedFile !== null)
        {
            try {
                const newPhotoName = await uploadPhoto(selectedFile).then(r => r?.fileName);
                setSelectedFile(null);
                if(user?.photoName !== "" ) {
                    await deletePhoto(user?.photoName);
                }
                await updatePhoto(newPhotoName);
                changeUser(newPhotoName);
                setIsEditing(!isEditing);
            }catch(err)
            {
                console.log(err);
            }
        } else {
            setIsEditing(!isEditing)
        }
    }

  return (
    <div className={s.profile_info_box}>
        <div className={s.back_link_box}>
            <Link to="/"><p className={s.back_link}>Обратно к списку рецептов</p></Link>
        </div>
        <h1>Profile info:</h1>
        {isEditing ? (
            <div>
                <div className={s.file_upload}>
                    <span className={s.format_warning}>Выложите фотографию в формате .jpg</span>
                    <input 
                        className={s.file_input} 
                        type="file" 
                        onChange={handleFileChange} 
                    />
                </div>
                <div>
                    <button onClick={() => changeAvatar()}>Изменить аватарку</button>
                    <button onClick={() => setIsEditing(!isEditing)}>Отмена</button>
                </div>
            </div>
        ) : (
        <div className={s.main_info_wrap}>
            <img src={user?.photoName !== "" ? `http://localhost:5275/api/photos/${user?.photoName}` : logo} alt='logo' />
            <div className={s.info}>
                <p>Username: </p>{user?.userName}
            </div>
            <div className={s.info}>
                <p>Email: </p>{user?.email}
            </div>
            <button onClick={() => setIsEditing(!isEditing)}>Изменить аватарку</button>
        </div>
    )} 
        <button onClick={() => logout()}>Logout</button>
    </div>
  )
}

export default ProfilePage