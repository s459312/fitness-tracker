import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import Api from "../Api";
import {useState} from "react";

const Login = () => {
    const [loginErrorMsg, setErrorMsg] = useState("");

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const login = async () => {
        try {
            let serverAnswer = (await Api.post("/auth/login", {email, password}));
            if (serverAnswer.response) {
                serverAnswer = serverAnswer.response;
            }

            const {data} = serverAnswer;

            if (!data.errors && !data.message) {
                localStorage.setItem("token", data.token);
                // eslint-disable-next-line no-restricted-globals
                location.replace("/app");
            } else {
                if (data.errors) {
                    setErrorMsg(data.errors[0].errors[0])
                } else {
                    setErrorMsg(data.message)
                }
            }


        } catch (err) {
            console.log('dupa catch');
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
                marginTop: "30vh",
            }}
        >
            <Typography variant="h3" gutterBottom>
                LOGOWANIE
            </Typography>
            <form noValidate autoComplete="off">
                <Grid container direction="column">
                    <TextField
                        id="email"
                        label="Email"
                        variant="outlined"
                        style={{margin: "0.5rem"}}
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                    <TextField
                        id="password"
                        label="Hasło"
                        type="password"
                        variant="outlined"
                        style={{margin: "0.5rem"}}
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />

                    <div style={{
                        color: 'red',
                        textAlign: 'center'
                    }}>
                        {loginErrorMsg}
                    </div>
                    <Button
                        variant="contained"
                        color="primary"
                        style={{margin: "0.5rem"}}
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

export default Login;
