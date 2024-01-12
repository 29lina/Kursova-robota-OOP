import React from 'react';
import { useNavigate } from 'react-router-dom';
import './navbar.css';

const NavBar = () => {
    const navigate = useNavigate();

    return (
        <div className="main">
            <div className="logo">
                <strong className="loggo">Khrestiki Noliki</strong>
            </div>
            <ul className="choose">
                <li>
                    <button className="btn" onClick={() => navigate("/game")}>Game</button>
                </li>

                <li>
                    <button className="btn" onClick={() => navigate("/registr")}>Sign Up</button>
                </li>

                <li>
                    <button className="btn" onClick={() => navigate("/login")}>Log In</button>
                </li>

                <li>
                    <button className="btn" onClick={() => navigate("/myprofile")}>My profile</button>
                </li>

            </ul>
        </div>
    );
};

export default NavBar;