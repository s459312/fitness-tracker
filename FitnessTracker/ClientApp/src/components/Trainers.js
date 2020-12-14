import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import Accordion from "@material-ui/core/Accordion";
import AccordionSummary from "@material-ui/core/AccordionSummary";
import AccordionDetails from "@material-ui/core/AccordionDetails";
import Typography from "@material-ui/core/Typography";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import AccountCircle from "@material-ui/icons/AccountCircle";

const useStyles = makeStyles((theme) => ({
  root: {
    width: "100%",
  },
  headingWrapper: {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center'
  },
  heading: {
    fontSize: theme.typography.pxToRem(15),
    fontWeight: theme.typography.fontWeightRegular,
    marginLeft: '10px',
  },
}));

export default function Trainers() {
  const classes = useStyles();
  const list = [
    { name: "John", surname: "Test" },
    { name: "Andrew", surname: "Test" },
  ];

  return (
    <div className={classes.root}>
      {list.map((item) => (
        <Accordion>
          <AccordionSummary expandIcon={<ExpandMoreIcon />}>
            <div className={classes.headingWrapper}>
              <AccountCircle />
              <Typography className={classes.heading}>
                {item.name} {item.surname}
              </Typography>
            </div>
          </AccordionSummary>
          <AccordionDetails>
            <Typography>Opis trenera</Typography>
          </AccordionDetails>
        </Accordion>
      ))}
    </div>
  );
}
