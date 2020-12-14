import "./App.css";
import { useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import { Link as RouterLink } from "react-router-dom";
import { Route, BrowserRouter as Router, Switch } from "react-router-dom";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import Select from "@material-ui/core/Select";
import MenuItem from "@material-ui/core/MenuItem";
import InputLabel from "@material-ui/core/InputLabel";
import FormControl from "@material-ui/core/FormControl";
import Button from "@material-ui/core/Button";
import landing from "./assets/landing.jpg";
import Api from "./Api";
import ResponsiveDrawer from "./components/ResponsiveDrawer";

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
  },
}));

const Home = () => {
  const classes = useStyles();
  return (
    <div className={classes.root}>
      <AppBar style={{ background: "gray" }} position="static">
        <Toolbar>
          <Typography variant="h6" className={classes.title}>
            Fitness Tracker
          </Typography>
          <Button color="inherit" component={RouterLink} to="/login">
            Zaloguj się
          </Button>
          <Button color="inherit" component={RouterLink} to="/register">
            Rejestracja
          </Button>
        </Toolbar>
      </AppBar>
      <div
        style={{
          display: "flex",
          color: "white",
          flexDirection: "column",
          width: "100%",
          height: "93.5vh",
          alignItems: "center",
          justifyContent: "center",
          backgroundImage: `url(${landing})`,
          backgroundRepeat: "no-repeat",
          backgroundSize: "cover",
        }}
      >
        <h1 style={{ margin: 0, fontSize: "3rem" }}>FITNESS TRACKER</h1>
        <h2 style={{ margin: 0, textAlign: "center", fontSize: "1.5rem" }}>
          TEST TEST TEST
        </h2>
      </div>
    </div>
  );
};

const Registration = () => {
  const useStyles = makeStyles((theme) => ({
    formControl: {
      margin: theme.spacing(1),
      minWidth: 120,
    },
    selectEmpty: {
      marginTop: theme.spacing(2),
    },
  }));
  const classes = useStyles();

  const [state, setState] = useState({});

  const register = async () => {
    try {
      const { data } = await Api.post("/auth/register", state);
      localStorage.setItem("token", data.token);
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <Typography variant="h3" gutterBottom>
        REJESTRACJA
      </Typography>
      <form noValidate autoComplete="off">
        <Grid container direction="column">
          <TextField
            id="name"
            label="Imię"
            variant="outlined"
            value={state.name}
            style={{ margin: "0.5rem" }}
            onChange={({ target: { value } }) =>
              setState({ ...state, name: value })
            }
          />
          <TextField
            id="surname"
            label="Nazwisko"
            variant="outlined"
            value={state.surname}
            style={{ margin: "0.5rem" }}
            onChange={({ target: { value } }) =>
              setState({ ...state, surname: value })
            }
          />
          <TextField
            id="email"
            label="Email"
            variant="outlined"
            value={state.email}
            style={{ margin: "0.5rem" }}
            onChange={({ target: { value } }) =>
              setState({ ...state, email: value })
            }
          />
          <TextField
            id="password"
            label="Hasło"
            variant="outlined"
            value={state.password}
            onChange={({ target: { value } }) =>
              setState({ ...state, password: value })
            }
            style={{ margin: "0.5rem" }}
          />
          <TextField
            id="repeat-password"
            label="Powtórz hasło"
            variant="outlined"
            value={state.confirmPassword}
            onChange={({ target: { value } }) =>
              setState({ ...state, confirmPassword: value })
            }
            style={{ margin: "0.5rem" }}
          />
          <FormControl variant="outlined" className={classes.formControl}>
            <InputLabel id="demo-simple-select-outlined-label">Cel</InputLabel>
            <Select
              labelId="demo-simple-select-outlined-label"
              id="demo-simple-select-outlined"
              value={12}
              // onChange={handleChange}
              label="Cel"
            >
              <MenuItem value={10}>Ten</MenuItem>
              <MenuItem value={20}>Twenty</MenuItem>
              <MenuItem value={30}>Thirty</MenuItem>
            </Select>
          </FormControl>
          <Button
            variant="contained"
            color="primary"
            style={{ margin: "0.5rem" }}
            size="large"
            onClick={register}
          >
            ZAŁÓŻ KONTO
          </Button>
        </Grid>
      </form>
    </div>
  );
};

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const login = async () => {
    try {
      const { data } = await Api.post("/auth/login", { email, password });
      localStorage.setItem("token", data.token);
    } catch (err) {
      console.error(err);
    }
  };

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <Typography variant="h3" gutterBottom>
        LOGOWANIE
      </Typography>{" "}
      <form noValidate autoComplete="off">
        <Grid container direction="column">
          <TextField
            id="email"
            label="Email"
            variant="outlined"
            style={{ margin: "0.5rem" }}
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <TextField
            id="password"
            label="Hasło"
            variant="outlined"
            style={{ margin: "0.5rem" }}
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <Button
            variant="contained"
            color="primary"
            style={{ margin: "0.5rem" }}
            size="large"
            onClick={login}
          >
            Zaloguj się
          </Button>
        </Grid>
      </form>
    </div>
  );
};

const App = () => {
  return (
    <Router>
      <Switch>
        <Route exact path="/" component={Home} />
        <Route path="/register" component={Registration} />
        <Route path="/login" component={Login} />
        <Route path="/dashboard" component={ResponsiveDrawer} />
      </Switch>
    </Router>
  );
};

export default App;
