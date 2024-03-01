import './App.css';
import {useEffect, useState} from "react";
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import 'bootstrap/dist/css/bootstrap.min.css';

async function clickMe(username, password) {
    //alert("You clicked me! login {0} password {1}", login, password);
    alert(username);
    let authenticateRequest = {
        username: username,
        password: password
    };

    let response = await fetch('http://localhost:4000/api/user/authenticate', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(authenticateRequest)
    });

    let result = await response.json();
    alert(result.message);
}

function App() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    useEffect(() => {
        console.log(username);
    }, [username])
    useEffect(() => {
        console.log(password);
    }, [password])


    return (
        <div className="App">

            <header className="App-header">
                <Form>
                    <Form.Group className="mb-3" controlId="Login">
                        <Form.Label>Login</Form.Label>
                        <Form.Control type="email" placeholder="name@example.com" value={username}
                                      onChange={(event) => setUsername(event.target.value)}/>
                    </Form.Group>
                    <Form.Group className="mb-3" controlId="Password">
                        <Form.Label>Password</Form.Label>
                        <Form.Control type="password" value={password}
                                      onChange={(event) => setPassword(event.target.value)}/>
                    </Form.Group>
                </Form>
                <Button onClick={() => clickMe(username, password)} variant="outline-light">Sign in</Button>
                <Button href="https://reactjs.org" variant="outline-light" style={{margin: 20}}>Sign up</Button>
                <Button href="https://reactjs.org" variant="outline-light">Forgot password</Button>
            </header>
        </div>
    );
}


export default App;
