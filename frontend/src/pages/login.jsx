import React, { useState } from 'react';
import './registr.css';
import axios from "axios";
import {useNavigate} from "react-router-dom";

function Login() {
    const [nameLog, setNameLog] = useState('');
    const [passwordLog, setPasswordLog] = useState('');
    const navigate = useNavigate();  // Добавлено

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post('https://localhost:7294/api/User/Login', {
                name: nameLog,
                password: passwordLog,
            });

            const id = response.data;
            console.log(id);

            sessionStorage.setItem('id', id);

            navigate('/game');

            console.log('Успешный вход, JWT токен:', id);
        } catch (error) {
            console.error('Ошибка при входе:', error);
        }
    }

    return (
        <div className="master">
            <form action="#" method="POST" onSubmit={handleSubmit}>
                <p className="form-title">Log in to your account</p>
                <div className="input-container">
                    <input
                        placeholder="Ім'я"
                        type="text"
                        name="username"
                        value={nameLog}
                        onChange={(e) => setNameLog(e.target.value)}
                    /><br/>

                    <span>
          </span>
                </div>

                <div className="input-container">

                    <input
                        placeholder="Ваш пароль"
                        type="password"
                        name="user_password"
                        value={passwordLog}
                        onChange={(e) => setPasswordLog(e.target.value)}
                    /><br/>

                </div>
                <button type="submit" className="submit">
                    Log in
                </button>
            </form>

        </div>
    );
}

export default Login;