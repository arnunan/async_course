import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";

async function SignIn(username, password, navigate) {
    let signInRequest = {
        username: username,
        password: password
    };

    let response = await fetch('http://localhost.dev.course:4000/api/auth/sign-in', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8',
            'Access-Control-Allow-Credentials': true,
            'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept, authorization',
            'Access-Control-Allow-Origin': 'http://localhost.dev.course:3000',
        },
        body: JSON.stringify(signInRequest),
        credentials: 'include'
    });

    let result = await response.json();
    if (result != null)
        navigate("/account");
}

async function SignUp(username, password) {
    let signUpRequest = {
        username: username,
        password: password
    };

    let response = await fetch('http://localhost:4000/api/auth/sign-up', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(signUpRequest)
    });

    await response.json();
    alert("Аккаунт зарегистрирован");
}

async function ForgotPassword(username) {

    let response = await fetch('http://localhost:4000/api/auth/forgot-password', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(username)
    });

    let result = await response.json();
    alert(result.password);
}

function Auth() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();
    useEffect(() => {
        console.log(username);
    }, [username])
    useEffect(() => {
        console.log(password);
    }, [password])

    return (<header className="App-header">
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
        <Button onClick={() => SignIn(username, password, navigate)} variant="outline-light">Sign in</Button>
        <Button onClick={() => SignUp(username, password)} variant="outline-light" style={{margin: 20}}>Sign up</Button>
        <Button onClick={() => ForgotPassword(username)} variant="outline-light">Forgot password</Button>
    </header>);
}

export default Auth;