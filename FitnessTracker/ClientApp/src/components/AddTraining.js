import { useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import Modal from "@material-ui/core/Modal";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import Api from "../Api";

const useStyles = makeStyles((theme) => ({
  paper: {
    position: "absolute",
    width: "auto",
    backgroundColor: theme.palette.background.paper,
    border: "2px solid #000",
    boxShadow: theme.shadows[5],
    padding: theme.spacing(2, 4, 3),
  },
}));

function AddTraining({callback}) {
  const classes = useStyles();
  const [open, setOpen] = useState(false);

  const handleOpen = () => {
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  const addTraining = async () => {
    try {
      await Api.post("/training", { name, description });
      setOpen(false);
      callback();
    } catch (err) {
      console.log(err);
    }
  };

  const [name, setName] = useState("");
  const [description, setDescription] = useState("");

  const body = (
    <div
      style={{
        top: "40%",
        left: "40%",
        transform: "translate(-10%, -40%)",
      }}
      className={classes.paper}
    >
      <Grid container direction="column">
        <Typography variant="h4">Plan treningowy</Typography>
        <TextField
          id="name"
          label="Nazwa"
          variant="outlined"
          value={name}
          onChange={(e) => setName(e.target.value)}
          style={{ margin: "0.5rem" }}
        />
        <TextField
          id="description"
          label="Opis"
          variant="outlined"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          style={{ margin: "0.5rem" }}
        />
        <Button
          variant="contained"
          color="primary"
          size="large"
          onClick={addTraining}
        >
          Dodaj
        </Button>
      </Grid>
    </div>
  );

  return (
    <div>
        <Button
          variant="contained"
          style={{ width: "100%" }}
          color="primary"
          size="large"
          onClick={handleOpen}
        >
          Dodaj nowy plan
        </Button>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="simple-modal-title"
        aria-describedby="simple-modal-description"
      >
        {body}
      </Modal>
    </div>
  );
}

export default AddTraining;
