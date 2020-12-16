import "./App.css";
import {BrowserRouter as Router, Switch} from "react-router-dom";
import ResponsiveDrawer from "./components/ResponsiveDrawer";
import LandingPage from "./components/LandingPage";
import Registration from "./components/Registration";
import Login from "./components/Login";
import {PrivateRoute} from "./components/PrivateRoute";

import React from "react";
import {PublicRoute} from "./components/PublicRoute";

const App = () => {
    return (
        <Router>
            <Switch>
                <PublicRoute exact path="/" component={LandingPage}/>
                <PublicRoute path="/register" component={Registration}/>
                <PublicRoute path="/login" component={Login}/>
                <PrivateRoute path="/app" component={ResponsiveDrawer}/>
            </Switch>
        </Router>
    );
};

export default App;
