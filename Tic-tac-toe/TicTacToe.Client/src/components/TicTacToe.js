import React, {useEffect, useState} from "react";
import Square from "./EndGame/Square";
import EndGame from "./EndGame/EndGame";
import {HubConnectionBuilder} from "@microsoft/signalr";

const INITIAL = "";

function TicTacToe({token}) {
  const [grid, setGrid] = useState(Array(9).fill(INITIAL));
  const [player, setPlayer] = useState(false);
  const [gameFinished, setGameFinished] = useState(false);
  const [draw, setDraw] = useState(false);
  const [winCount, setWinCount] = useState({ A: 0, B: 0 });
  const [connection, setConnection] = useState(null);
  const [groupName, setGroupName] = useState(null);


  useEffect ( () => {
    const newConnection = new HubConnectionBuilder()
        .withUrl('http://localhost:5035/gameHub')
        .withAutomaticReconnect()
        .build();
    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      connection.start()
          .then(() => {
            //getHistory();
            connection.on("GroupName",groupName =>{
                setGroupName(groupName);
              alert("you already to play...")
            })
            connection.invoke("FindGame",token)

            connection.on("UpdateBoard",updateBoard =>{
                setGrid(updateBoard)
            })
              
            connection.on("GameOver",result => {
                setGameFinished(true);
                if (result === "Game DRAW"){
                    setDraw(true)
                }else {
                    setPlayer(result)
                }
            })
          })
          .catch(e => console.log('Connection failed: ', e));
    }
  }, [connection]);
  

  function restartGame() {
    setGrid(Array(9).fill(INITIAL));
    setGameFinished(false);
    setDraw(false);
    connection.invoke("RestartGame",groupName);
  }

  function clearHistory() {
      setWinCount({ A: 0, B: 0 });
      restartGame();
  }

  function handleClick(id) {
      if (groupName) 
          connection.invoke("PlacePiece",groupName,id);
  }

  return (
    <div>
        
      <span className="win-history">
        A's WINS: {winCount.A}
        <br />
        B's WINS: {winCount.B}
      </span>
      {gameFinished && (
        <EndGame
          winCount={winCount}
          restartGame={restartGame}
          player={player}
          draw={draw}
          clearHistory={clearHistory}
        />
      )}
      <Square clickedArray={grid} handleClick={handleClick} />
    </div>
  );
}

export default TicTacToe;
