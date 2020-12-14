import { makeStyles } from "@material-ui/core/styles";
import { Link as RouterLink } from "react-router-dom";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import landing from "../assets/landing.jpg";
import Button from "@material-ui/core/Button";
import Typography from "@material-ui/core/Typography";

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

const LandingPage = () => {
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
        <h1 style={{ textAlign: "center", margin: 0, fontSize: "3.77rem" }}>
          FITNESS TRACKER
        </h1>
        <h2 style={{ margin: 0, textAlign: "center", fontSize: "1.5rem" }}>
          MONITOROWANIE POSTĘPÓW W TRENINGACH
        </h2>
      </div>
    </div>
  );
};

export default LandingPage;
