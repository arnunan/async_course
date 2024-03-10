import Button from "react-bootstrap/Button";
import {useEffect, useState} from "react";
import {Table} from "react-bootstrap";

function Account() {
    const [username, setUsername] = useState("");

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
                <td>1</td>
                <td>Mark</td>
                <td>Otto</td>
                <td>@mdo</td>
            </tr>
            </tbody>
        </Table>
    </header>);
}

export default Account;