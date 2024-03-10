import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import {Routes, Route} from "react-router";
import Auth from "./Auth";
import Account from "./Account";
import React from "react";

function App() {
    return (<div className="App">
        <Routes>
            <Route
                path="/login"
                element={<Auth/>}/>
            <Route
                path="/account"
                element={<Account/>}/>
        </Routes>
    </div>);
}


export default App;
