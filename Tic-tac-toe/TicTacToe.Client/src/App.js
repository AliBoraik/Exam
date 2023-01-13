import React from "react";
import "./App.css";
import { Switch, Route } from 'react-router-dom';
import Login from './components/Login/Login';
import useToken from './components/Login/useToken';
import TicTacToe from "./components/TicTacToe";
import ListGame from "./components/ListGames"


function App() {
    const { token, setToken } = useToken();

   /* if(!token) {
        navigate("/login")
    }*/
  return (
      <Switch>
          <div style={{ margin: '4% 30%' }}>
              <Route path="/" component={ListGame}/>
              <Route path="/game" component={TicTacToe}/>
              <Route path="/login" component={Login}/>
          </div>
      </Switch>

  );
}

export default App;
