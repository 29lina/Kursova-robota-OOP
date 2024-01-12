import React, { useState, useEffect } from 'react';
import axios from 'axios';
import "./myprofile.css";
import { useNavigate } from 'react-router-dom';

function MyProfile() {
    const navigate = useNavigate();
    const [infoProf, setInfoProf] = useState([]);
    const id = sessionStorage.getItem('id');
    const hash_result = {
        1: "X win",
        2: "O win",
        3: "Both loser"
    };

    useEffect(() => {
        axios.get(`https://localhost:7294/api/User/GameResult/${id}`)
            .then(response => {
                setInfoProf(response.data);
            })
            .catch(error => {
                console.error("Помилка при отриманні даних:", error);
            });
    }, [id]);

    return (
        <div className="body1">
            <h3 className="prof">My profile:</h3>
            <div className="all_info">
            <p className="info" ><span className="grey">Rating X:</span> {infoProf.ratingX}</p>
            <p className="info"><span className="grey">Rating O:</span> {infoProf.rating0}</p>
            <p className="info"><span className="grey">User Name:</span> {infoProf.userName}</p>
        </div>
            {infoProf.gameResults && infoProf.gameResults.map((gameResult, index) => (
                <div key={index} className="div1">
                    <h2 className="h21">Game #{gameResult.counter} - {hash_result[gameResult.winner]}</h2>
                </div>
            ))}

        </div>
    );
}

export default MyProfile;
