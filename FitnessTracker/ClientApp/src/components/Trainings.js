import { useEffect, useState } from "react";
import Api from "../Api";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import IconButton from "@material-ui/core/IconButton";
import ListItemSecondaryAction from "@material-ui/core/ListItemSecondaryAction";
import ListItemText from "@material-ui/core/ListItemText";
import Grid from "@material-ui/core/Grid";
import Typography from "@material-ui/core/Typography";
import { makeStyles } from "@material-ui/core/styles";
import Button from "@material-ui/core/Button";

import StarBorderIcon from "@material-ui/icons/StarBorder";
import StarIcon from "@material-ui/icons/Star";

const useStyles = makeStyles((theme) => ({
  root: {},
  demo: {
    backgroundColor: theme.palette.background.paper,
  },
  title: {
    margin: theme.spacing(4, 0, 2),
  },
}));

const Trainings = () => {
  const classes = useStyles();
  const [userTrainings, setUserTrainings] = useState([]);
  const [publicTrainings, setPublicTrainings] = useState([]);
  const [selectedTraining, setSelectedTraining] = useState();

  useEffect(() => {
    const getUserTrainings = async () => {
      try {
        const { data } = await Api.get("/training");
        setUserTrainings(data);
      } catch (err) {
        console.error(err);
      }
    };
    const getPublicTrainings = async () => {
      try {
        const { data } = await Api.get("/training/availablePublic");
        setPublicTrainings(data);
      } catch (err) {
        console.error(err);
      }
    };
    getUserTrainings();
    getPublicTrainings();
  }, []);

  const getTrainingInfo = async (id) => {
    try {
      const { data } = await Api.get(`/training/${id}`);
      setSelectedTraining(data);
    } catch (err) {
      console.error(err);
    }
  };

  const TrainingsList = ({ trainings }) => (
    <div className={classes.demo}>
      <List dense={false}>
        {trainings.length > 0 ? (
          trainings.map((training) => (
            <ListItem
              button
              key={training.id}
              selected={training.id === selectedTraining?.id}
              onClick={() => getTrainingInfo(training.id)}
            >
              <ListItemText primary={training.name} />
              <ListItemSecondaryAction>
                <IconButton edge="end" aria-label="favorite">
                  <StarBorderIcon />
                </IconButton>
              </ListItemSecondaryAction>
            </ListItem>
          ))
        ) : (
          <Typography>Brak</Typography>
        )}
      </List>
    </div>
  );

  return (
    <Grid container spacing={4}>
      <Grid item xs={12} md={4} lg={3}>
        <Typography align="center" gutterBottom variant="h5">
          Moje plany treningowe
        </Typography>
        <Button
          variant="contained"
          style={{ width: "100%" }}
          color="primary"
          size="large"
        >
          Dodaj nowy plan
        </Button>
        <TrainingsList trainings={userTrainings} />
        <Typography align="center" gutterBottom variant="h5">
          Gotowe plany treningowe
        </Typography>
        <TrainingsList trainings={publicTrainings} />
      </Grid>
      <Grid item xs={12} md={8} lg={9}>
        {selectedTraining ? (
          selectedTraining?.trainingExercises.map((exercise) => (
            <p>{exercise.name}</p>
          ))
        ) : (
          <Typography variant="h5">Wybierz plan</Typography>
        )}
      </Grid>
    </Grid>
  );
};

export default Trainings;
