import React, { useEffect, useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import Accordion from "@material-ui/core/Accordion";
import AccordionSummary from "@material-ui/core/AccordionSummary";
import AccordionDetails from "@material-ui/core/AccordionDetails";
import Typography from "@material-ui/core/Typography";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import Api from "../Api";

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

const Exercises = ()  => {
  const classes = useStyles();
  const defaultList = [
    {
        "id": 0,
        "goal": {
          "id": 1,
          "name": "Rekompozycja sylwetki"
        },
        "name": "Ćwiczenie 1",
        "description": "Opis ćwiczenia",
        "serie": 3,
        "powtorzenia": 10,
        "czas": 0,
        "obciazenie": 50,
        "dystans": 0
    },
    {
        "id": 1,
        "goal": {
          "id": 1,
          "name": "Rekompozycja sylwetki"
        },
        "name": "Ćwiczenie 2",
        "description": "Opis ćwiczenia",
        "serie": 2,
        "powtorzenia": 5,
        "czas": 0,
        "obciazenie": 10,
        "dystans": 0
    },
  ];
  const [exercise, setExercise] = useState(defaultList);

  useEffect(() => {
    const getExercise = async () => {
      try {
        const { data } = await Api.get("/exercise");
        setExercise(data.data);
      } catch (err) {
        console.error(err);
      }
    };
    getExercise();
  }, []);

  const excersiseParagraph = (title, field) => field && field > 0 ? (<Typography paragraph>{title}: {field}</Typography>) : null


  return (
    <div className={classes.root}>
      {exercise.map((item) => (
        <Accordion style={{ margin: 10 }} key={item.id}>
          <AccordionSummary expandIcon={<ExpandMoreIcon />}>
            <div className={classes.headingWrapper}>
              <Typography className={classes.heading}>
                {item.name}
              </Typography>
            </div>
          </AccordionSummary>
          <AccordionDetails className={classes.details}>
            <Typography paragraph>Cel: {item.goal.name}</Typography>
            <Typography paragraph>Opis: {item.description}</Typography>
            {excersiseParagraph('Serie', item.serie)}
            {excersiseParagraph('Powtórzenia', item.powtorzenia)}
            {excersiseParagraph('Czas', item.czas)}
            {excersiseParagraph('Obciążenie', item.obciazenie)}
            {excersiseParagraph('Dystans', item.dystans)}
          </AccordionDetails>
        </Accordion>
      ))}
    </div>
  );
}
export default Exercises;