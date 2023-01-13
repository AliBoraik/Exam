import React from "react";
import "./App.css";
import Login from './components/Login/Login';
import useToken from './components/Login/useToken';
import TicTacToe from "./components/TicTacToe";
import ListGame from "./components/ListGames"


function App() {
    const { token, setToken } = useToken();
    
    if(!token) {
        return <Login setToken={setToken} />
    }
  return (
    <div style={{ margin: '4% 30%' }}>
        <TicTacToe token={token}/>
        {/*<ListGame token={token}/>*/}
    </div>
  );
}

export default App;
