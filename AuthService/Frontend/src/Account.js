import Button from "react-bootstrap/Button";
import {useEffect, useState} from "react";
import {Table} from "react-bootstrap";

async function GetUser(username, password, navigate) {

    let response = await fetch('http://localhost.dev.course:4000/api/users/user', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json;charset=utf-8',
            'Access-Control-Allow-Credentials': true,
            'Access-Control-Allow-Headers': 'Origin, X-Requested-With, Content-Type, Accept, authorization',
            'Access-Control-Allow-Origin': 'http://localhost.dev.course:3000',
        },
        credentials: 'include'
    });

    let result = await response.json();
    if (result != null)
        navigate("/account");
}

function Account() {
    let [username, setUsername] = useState("");
    username = GetUser().userName;
    return (<header className="App-header">
        <Table striped bordered hover>
            <thead>
            <tr>
                <th>Id</th>
                <th>Username</th>
                <th>First Name</th>
                <th>Last Name</th>
                <th>Role</th>
            </tr>
            </thead>
            <tbody>
            <tr>
                <td>username</td>
                <td>Mark</td>
                <td>Otto</td>
                <td>@mdo</td>
            </tr>
            </tbody>
        </Table>
    </header>);
}

export default Account;