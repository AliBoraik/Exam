import {useEffect,useState} from "react";
import TicTacToe2 from "./TicTacToe2";
import TicTacToe from "./TicTacToe";

export default function ListGames({token}) {
    const [games, setGames] = useState([])

    const fetchData = async () => {
        try {
            const response = await fetch("http://localhost:5035/Game")
            if (!response.ok) {
                throw new Error("Sorry something went wrong")
            }
            const data = await response.json()
            setGames(data)
        } catch (error) {
        }
    }

    useEffect(() => {
        fetchData()
    }, [])

    function handleClick(groupName,username) {
      return <TicTacToe2 groupName={groupName} token={username}/>
    }

    function createGame(username) {
        return <TicTacToe token={username}/>
    }
    
    return (
        <div className="container">
            <h3 className="p-3 text-center">Display a list of Games</h3>
            <button onClick={()=> { createGame(token)}} >Create game</button>
            <table className="table table-striped table-bordered">
                <thead>
                <tr>
                    <th>Player  </th>
                    <th>    Join</th>
                </tr>
                </thead>
                <tbody>
                {games.map(g =>
                    <tr key={g.player1.name}>
                        <td>{g.player1.name} </td>
                        <td><button  onClick={()=> { handleClick(g.id,token)}} >Join to game</button></td>
                    </tr>
                )}
                </tbody>
            </table>
        </div>
    );
}
