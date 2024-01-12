import React, { useState, useEffect } from 'react';
import './game.css';
import axios from 'axios';
import Modal from "../components/modal";

const initialBoard = Array(9).fill(null);
console.log(initialBoard);

const calculateWinner = (squares) => {
    const lines = [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6],
    ];

    for (let i = 0; i < lines.length; i++) {
        const [a, b, c] = lines[i];
        if (squares[a] && squares[a] === squares[b] && squares[a] === squares[c]) {
            return squares[a];
        }
    }
    return null;
};

const Square = ({ value, onClick }) => (
    <button className="square" onClick={onClick}>
        {value}
    </button>
);

const Board = ({ squares, onClick }) => (
    <div className="boardd">
        {squares.map((value, index) => (
            <Square key={index} value={value} onClick={() => onClick(index)} />
        ))}
    </div>
);

const Game = () => {
    const [history, setHistory] = useState([initialBoard]);
    const [step, setStep] = useState(0);
    const [modalActive, setModalActive] = useState(false);
    const id = sessionStorage.getItem('id');

    const [kostyaLoh, setKostyaLoh] = useState([
        [" ", " ", " "],
        [" ", " ", " "],
        [" ", " ", " "]
    ]);

    const hash_result = {
        0: false,
        1: "X win",
        2: "O win",
        3: "You are both loser"
    }


    const currentSquares = history[step];
    const [infoApp, setInfoApp] = useState([]);

    const winner = calculateWinner(currentSquares);
    const status = `Next player: ${step % 2 === 0 ? "X" : "O"}`;

    const handleClick = (index) => {
        if (winner || currentSquares[index]) {
            return;
        }

        const newSquares = currentSquares.slice();
        newSquares[index] = step % 2 === 0 ? "X" : "O";

        const rowIndex = Math.floor(index / 3);
        const colIndex = index % 3;

        const newKostyaLoh = kostyaLoh.map((row, i) =>
            i === rowIndex ? row.map((col, j) => (j === colIndex ? newSquares[index] : col)) : row
        );

        setHistory(history.slice(0, step + 1).concat([newSquares]));
        setStep(step + 1);
        setKostyaLoh(newKostyaLoh);

        console.log(index, newSquares[index]);
        console.log(newKostyaLoh);

        axios.post(`https://localhost:7294/api/Game/${id}`, newKostyaLoh)
            .then((response) => {
                console.log("Peremoga");
                setInfoApp(response.data);
            })
            .catch((error) => {
                console.error('Ошибка при отправке данных:', error);
            });
    };

    const jumpTo = () => {
        setStep(0);
        setKostyaLoh([
            [" ", " ", " "],
            [" ", " ", " "],
            [" ", " ", " "]
        ]);
    };

    console.log(infoApp, hash_result[infoApp])

    useEffect(() => {
        if (infoApp > 0) {
            console.log("Zrada");
            setModalActive(true);

        }
    }, [infoApp]);

    return (
        <div className="game">
            <div className="game-boardd">
                <Board squares={currentSquares} onClick={handleClick} />

            </div>
            <div className="game-info">
                <h1 className="status">{status}</h1>
                <button className='start' onClick={() => jumpTo()}>Start new game</button>
            </div>

            {modalActive && (
                <Modal
                    active={modalActive}
                    setActive={() => {
                        setModalActive(false);
                        jumpTo()
                    }}
                >
                    <h3>{hash_result[infoApp]}</h3>
                </Modal>

            )}
        </div>
    );
};

export default Game;


