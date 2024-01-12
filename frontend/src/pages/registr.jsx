import React, { useState } from 'react';
import './registr.css';
import axios from "axios";
import { useNavigate } from 'react-router-dom';

function Registr() {
    const [name, setName] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();  // Добавлено

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post('https://localhost:7294/api/User', {
                name: name,
                password: password,
            });

            const id = response.data;

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
                <p className="form-title">Sign up new account</p>
                <div className="input-container">
                    <input
                        placeholder="Ім'я"
                        type="text"
                        name="username"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    /><br/>
                </div>

                <div className="input-container">
                    <input
                        placeholder="Ваш пароль"
                        type="password"
                        name="user_password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    /><br/>
                </div>

                <button type="submit" className="submit">
                    Sign up
                </button>
            </form>
        </div>
    );
}

export default Registr;
