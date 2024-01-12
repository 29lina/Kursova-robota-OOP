import React from 'react';
import { BrowserRouter as Router, Routes, Route, useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import './App.css';
import Game from "./pages/game";
import Registr from "./pages/registr";
import Login from "./pages/login";
import Myprofile from "./pages/myprofile";
import NavBar from "./components/navbar";


const App = () => {
    return (
        <Router>
            <div className="App">
                <nav>
                    <NavBar />
                </nav>
                <main>
                    <Routes>
                        <Route path="/game" element={<Game />} />
                        <Route path="/registr" element={<Registr />} />
                        <Route path="/login" element={<Login />} />
                        <Route path="/myprofile" element={<Myprofile />} />
                    </Routes>

                </main>
            </div>
        </Router>
    );
};

export default App;
