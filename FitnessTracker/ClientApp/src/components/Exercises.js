import React, { useEffect, useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import Accordion from "@material-ui/core/Accordion";
import AccordionSummary from "@material-ui/core/AccordionSummary";
import AccordionDetails from "@material-ui/core/AccordionDetails";
import Typography from "@material-ui/core/Typography";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import Api from "../Api";
import Checkbox from "@material-ui/core/Checkbox";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import MenuItem from "@material-ui/core/MenuItem";
import Select from "@material-ui/core/Select";
import Button from "@material-ui/core/Button";

const useStyles = makeStyles((theme) => ({
  root: {
    width: "100%",
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

const Exercises = () => {
  const classes = useStyles();
  const [exercise, setExercise] = useState([]);
  const [selectedExercises, setSelectedExercises] = useState([]);
  const [selectedTraining, setSelectedTraining] = useState(null);
  const handleChange = (event) => {
    setSelectedTraining(event.target.value);
  };
  const [trainings, setTrainings] = useState([]);
  useEffect(() => {
    const getData = async () => {
      try {
        const { data } = await Api.get("/exercise");
        setExercise(data.data);
        const { data: trainingsData } = await Api.get("/training");
        setTrainings(trainingsData);
      } catch (err) {
        console.error(err);
      }
    };
    getData();
  }, []);

  const excersiseParagraph = (title, field) =>
    field && field > 0 ? (
      <Typography paragraph>
        {title}: {field}
      </Typography>
    ) : null;

  const selectExercise = (event, id) => {
    event.stopPropagation();
    if (selectedExercises.includes(id)) {
      const filtered = selectedExercises.filter((_id) => _id !== id);
      setSelectedExercises(filtered);
    } else {
      setSelectedExercises((prevState) => [...prevState, id]);
    }
  };

  const addToTraining = async () => {
    try {
      await Api.patch(`/training/${selectedTraining}`, {
        exercises: selectedExercises,
      });
      setSelectedExercises([]);
      setSelectedTraining();
    } catch {
      alert("Nie udało się dodać treningów do listy");
    }
  };

  const SelectBar = () =>
    selectedExercises.length > 0 ? (
      <div style={{ fontSize: 16 }}>
        <span style={{ marginLeft: 10, marginRight: 20 }}>
          Wybrano: {selectedExercises.length}
        </span>
        <label for="demo-simpe-select">Trening: </label>
        <Select
          id="demo-simple-select"
          value={selectedTraining}
          onChange={handleChange}
        >
          {trainings.length > 0 ? (
            trainings.map((training) => (
              <MenuItem value={training.id}>{training.name}</MenuItem>
            ))
          ) : (
            <MenuItem value="Brak treningów">Brak treningów</MenuItem>
          )}
        </Select>
        <Button
          onClick={addToTraining}
          style={{ marginLeft: 20 }}
          variant="contained"
          color="secondary"
          disabled={selectedTraining === null || trainings.length === 0}
        >
          Dodaj do treningu
        </Button>
      </div>
    ) : null;

  return (
    <div className={classes.root}>
      <SelectBar />
      {exercise.map((item) => (
        <Accordion style={{ margin: 10 }} key={item.id}>
          <AccordionSummary expandIcon={<ExpandMoreIcon />}>
            <div className={classes.headingWrapper}>
              <FormControlLabel
                aria-label="Acknowledge"
                onClick={(event) => selectExercise(event, item.id)}
                onFocus={(event) => event.stopPropagation()}
                control={
                  <Checkbox checked={selectedExercises.includes(item.id)} />
                }
                label=""
              />
              <Typography className={classes.heading}>{item.name}</Typography>
            </div>
          </AccordionSummary>
          <AccordionDetails className={classes.details}>
            <Typography paragraph>Cel: {item.goal.name}</Typography>
            {excersiseParagraph("Opis", item.description)}
            {excersiseParagraph("Serie", item.serie)}
            {excersiseParagraph("Powtórzenia", item.powtorzenia)}
            {excersiseParagraph("Czas", item.czas)}
            {excersiseParagraph("Obciążenie", item.obciazenie)}
            {excersiseParagraph("Dystans", item.dystans)}
          </AccordionDetails>
        </Accordion>
      ))}
    </div>
  );
};
export default Exercises;
