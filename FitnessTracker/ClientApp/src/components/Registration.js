import Select from "@material-ui/core/Select";
import MenuItem from "@material-ui/core/MenuItem";
import InputLabel from "@material-ui/core/InputLabel";
import FormControl from "@material-ui/core/FormControl";
import {makeStyles} from "@material-ui/core/styles";
import {useState} from "react";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import Api from "../Api";

const useStyles = makeStyles((theme) => ({
    formControl: {
        margin: theme.spacing(1),
        minWidth: 120,
    },
    selectEmpty: {
        marginTop: theme.spacing(2),
    },
}));

const Registration = () => {
    const classes = useStyles();

    const [registerErrorMsg, setErrorMsg] = useState("");
    const [state, setState] = useState({
        name: '',
        surname: '',
        email: '',
        password: '',
        confirmPassword: '',
        goalId: 1,
    });

    const register = async () => {
        try {

            const {data} = (await Api.post("/auth/register", state)).response;

            if (!data.errors) {
                localStorage.setItem("token", data.token);
                // eslint-disable-next-line no-restricted-globals
                location.replace("/app");
            } else {
                setErrorMsg(data.errors[0].errors[0])
                console.log(data.errors);
            }

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
                marginTop: "15vh",
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
                        style={{margin: "0.5rem"}}
                        onChange={({target: {value}}) =>
                            setState({...state, name: value})
                        }
                    />
                    <TextField
                        id="surname"
                        label="Nazwisko"
                        variant="outlined"
                        value={state.surname}
                        style={{margin: "0.5rem"}}
                        onChange={({target: {value}}) =>
                            setState({...state, surname: value})
                        }
                    />
                    <TextField
                        id="email"
                        label="Email"
                        variant="outlined"
                        value={state.email}
                        style={{margin: "0.5rem"}}
                        onChange={({target: {value}}) =>
                            setState({...state, email: value})
                        }
                    />
                    <TextField
                        id="password"
                        type="password"
                        label="Hasło"
                        variant="outlined"
                        value={state.password}
                        onChange={({target: {value}}) =>
                            setState({...state, password: value})
                        }
                        style={{margin: "0.5rem"}}
                    />
                    <TextField
                        id="repeat-password"
                        type="password"
                        label="Powtórz hasło"
                        variant="outlined"
                        value={state.confirmPassword}
                        onChange={({target: {value}}) =>
                            setState({...state, confirmPassword: value})
                        }
                        style={{margin: "0.5rem"}}
                    />
                    <FormControl variant="outlined" className={classes.formControl}>
                        <InputLabel id="demo-simple-select-outlined-label">Cel</InputLabel>
                        <Select
                            labelId="demo-simple-select-outlined-label"
                            id="demo-simple-select-outlined"
                            value={state.goalId ?? 1}
                            onChange={({target: {value}}) =>
                                setState((prevState) => ({...prevState, goalId: value}))
                            }
                            label="Cel"
                        >
                            <MenuItem value={1}>Redukcja tkanki tłuszczowej</MenuItem>
                            <MenuItem value={2}>Przybranie masy mięśniowej</MenuItem>
                            <MenuItem value={3}>Rekompozycja sylwetki</MenuItem>
                        </Select>
                    </FormControl>

                    <div style={{
                        color: 'red',
                        textAlign:'center'
                    }}>
                        {registerErrorMsg}
                    </div>
                    <Button
                        variant="contained"
                        color="primary"
                        style={{margin: "0.5rem"}}
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

export default Registration;
