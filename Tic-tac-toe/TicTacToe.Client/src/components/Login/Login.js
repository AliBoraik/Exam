import React, {useState} from 'react';
import './Login.css';
import PropTypes from 'prop-types';

async function loginUser(credentials) {
    return fetch('http://localhost:5035/Auth/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(credentials.user)
    })
        .then(data => (data))
}

export default function Login({setToken}) {
    const [username, setUserName] = useState();
    const [password, setPassword] = useState();
    
    const handleSubmit = async e => {
        e.preventDefault();
        const user = {
            Name: username
            // Password: password;
        };
        const response = await loginUser({
            user
        });
        //TODO check user data from api ... (response.status === 200)
        if (true){
            setToken(username);
            return;
        }
        alert("The User name already exists!!")
    }
    
    return(
        <div className="login-wrapper">
            <h1>Please Log In</h1>
            <form onSubmit={handleSubmit}>
                <label>
                    <p>Username</p>
                    <input type="text" onChange={e => setUserName(e.target.value)}/>
                </label>
                {/*<label>*/}
                {/*    <p>Password</p>*/}
                {/*    <input type="password" onChange={e => setPassword(e.target.value)}/>*/}
                {/*</label>*/}
                <div>
                    <button type="submit">Submit</button>
                </div>
            </form>
        </div>
    )
}

Login.propTypes = {
    setToken: PropTypes.func.isRequired
};
