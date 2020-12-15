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
import StarBorderIcon from "@material-ui/icons/StarBorder";
import StarIcon from "@material-ui/icons/Star";
import AddTraining from "./AddTraining";
import Button from "@material-ui/core/Button";
import Accordion from "@material-ui/core/Accordion";
import AccordionSummary from "@material-ui/core/AccordionSummary";
import AccordionDetails from "@material-ui/core/AccordionDetails";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";

const useStyles = makeStyles((theme) => ({
  root: {},
  demo: {
    backgroundColor: theme.palette.background.paper,
  },
  title: {
    margin: theme.spacing(4, 0, 2),
  },
  headingWrapper: {
    display: "flex",
    justifyContent: "center",
    alignItems: "center",
  },
  heading: {
    fontSize: theme.typography.pxToRem(15),
    fontWeight: theme.typography.fontWeightRegular,
  },
  details: { display: "flex", flexDirection: "column" },
}));

const Trainings = () => {
  const classes = useStyles();
  const [userTrainings, setUserTrainings] = useState([]);
  const [publicTrainings, setPublicTrainings] = useState([]);
  const [selectedTraining, setSelectedTraining] = useState();

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
  useEffect(() => {
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

  const toggleFavourite = async (id, favourite) => {
    try {
      await Api.patch("/training/toggleFavourite", {
        trainingId: id,
        favourite: !favourite,
      });
      getUserTrainings();
    } catch (err) {
      console.error(err);
    }
  };
  
  const addToHistory = async (id, exercises) => {
    const exercisesList = exercises.map((exercise) => {
      const stats = {
        czas: exercise.czas ?? 0,
        serie: exercise.serie ?? 0,
        powtorzenia: exercise.powtorzenia ?? 0,
        obciazenie: exercise.obciazenie ?? 0,
        dystans: exercise.dystans ?? 0,
      };
      delete stats.goal;
      delete stats.id;
      delete stats.name;
      return { exerciseId: exercise.id, exerciseHistoryStat: stats };
    });
    try {
      await Api.patch("/training/addToHistory", {
        trainingId: id,
        exercises: exercisesList,
      });
    } catch (err) {
      console.error(err);
    }
  };

  const TrainingsList = ({ trainings }) => (
    <div className={classes.demo}>
      <List dense={false}>
        {trainings?.length > 0 ? (
          trainings.map((training) => (
            <ListItem
              button
              key={training.id}
              selected={training.id === selectedTraining?.id}
              onClick={() => getTrainingInfo(training.id)}
            >
              <ListItemText primary={training.name} />
              <ListItemSecondaryAction>
                {training.isPublic === false && (
                  <IconButton
                    onClick={() =>
                      toggleFavourite(training.id, training.favourite)
                    }
                    edge="end"
                    aria-label="favorite"
                  >
                    {training.favourite ? <StarIcon /> : <StarBorderIcon />}
                  </IconButton>
                )}
              </ListItemSecondaryAction>
            </ListItem>
          ))
        ) : (
          <Typography>Brak</Typography>
        )}
      </List>
    </div>
  );

  const excersiseParagraph = (title, field) =>
    field && field > 0 ? (
      <Typography paragraph>
        {title}: {field}
      </Typography>
    ) : null;

  return (
    <Grid container spacing={4}>
      <Grid item xs={12} md={4} lg={3}>
        <Typography align="center" gutterBottom variant="h5">
          Moje plany treningowe
        </Typography>
        <AddTraining callback={getUserTrainings} />
        <TrainingsList trainings={userTrainings} />
        <Typography align="center" gutterBottom variant="h5">
          Gotowe plany treningowe
        </Typography>
        <TrainingsList trainings={publicTrainings} />
      </Grid>
      <Grid item xs={12} md={8} lg={9}>
        {selectedTraining && (
          <>
            <Typography gutterBottom variant="h5">
              {selectedTraining?.name}
              <Button
                variant="contained"
                style={{ marginLeft: 20 }}
                color="primary"
                size="large"
                onClick={() =>
                  addToHistory(
                    selectedTraining.id,
                    selectedTraining.trainingExercises
                  )
                }
              >
                Dodaj do historii
              </Button>
            </Typography>
            {selectedTraining.description && (
              <Typography gutterBottom variant="h6">
                Opis
              </Typography>
            )}
            <p>{selectedTraining?.description}</p>
            {selectedTraining.trainingExercises.length > 0 && (
              <Typography gutterBottom variant="h6">
                Ćwiczenia
              </Typography>
            )}
          </>
        )}
        {selectedTraining ? (
          selectedTraining?.trainingExercises.map((exercise) => (
            <Accordion key={exercise.id}>
              <AccordionSummary expandIcon={<ExpandMoreIcon />}>
                <div>
                  <Typography className={classes.heading}>
                    {exercise.name}
                  </Typography>
                </div>
              </AccordionSummary>
              <AccordionDetails className={classes.details}>
                <Typography paragraph>Cel: {exercise.goal}</Typography>
                {excersiseParagraph("Opis", exercise.description)}
                {excersiseParagraph("Serie", exercise.serie)}
                {excersiseParagraph("Powtórzenia", exercise.powtorzenia)}
                {excersiseParagraph("Czas", exercise.czas)}
                {excersiseParagraph("Obciążenie", exercise.obciazenie)}
                {excersiseParagraph("Dystans", exercise.dystans)}
              </AccordionDetails>
            </Accordion>
          ))
        ) : (
          <Typography variant="h5">Wybierz plan</Typography>
        )}
      </Grid>
    </Grid>
  );
};

export default Trainings;
