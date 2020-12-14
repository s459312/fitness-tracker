import "./App.css";
import { Route, BrowserRouter as Router, Switch } from "react-router-dom";
import ResponsiveDrawer from "./components/ResponsiveDrawer";
import LandingPage from "./components/LandingPage";
import Registration from "./components/Registration";
import Login from "./components/Login";

const App = () => {
  return (
    <Router>
      <Switch>
        <Route exact path="/" component={LandingPage} />
        <Route path="/register" component={Registration} />
        <Route path="/login" component={Login} />
        <Route path="/app" component={ResponsiveDrawer} />
      </Switch>
    </Router>
  );
};

export default App;
