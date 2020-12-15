import React, { useEffect, useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import Accordion from "@material-ui/core/Accordion";
import AccordionSummary from "@material-ui/core/AccordionSummary";
import AccordionDetails from "@material-ui/core/AccordionDetails";
import Typography from "@material-ui/core/Typography";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import AccountCircle from "@material-ui/icons/AccountCircle";
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
    marginLeft: "10px",
  },
  details: { display: "flex", flexDirection: "column" },
}));

const Coaches = () => {
  const classes = useStyles();
  const [coaches, setCoaches] = useState([]);

  useEffect(() => {
    const getCoaches = async () => {
      try {
        const { data } = await Api.get("/coach");
        setCoaches(data);
      } catch (err) {
        console.error(err);
      }
    };
    getCoaches();
  }, []);

  return (
    <div className={classes.root}>
      {coaches?.map((item) => (
        <Accordion style={{ margin: 10 }} key={item.id}>
          <AccordionSummary expandIcon={<ExpandMoreIcon />}>
            <div className={classes.headingWrapper}>
              <AccountCircle />
              <Typography className={classes.heading}>
                {item.name} {item.surname}
              </Typography>
            </div>
          </AccordionSummary>
          <AccordionDetails className={classes.details}>
            <Typography paragraph>Email: {item.email}</Typography>
            <Typography paragraph>Nr telefonu: {item.phone}</Typography>
          </AccordionDetails>
        </Accordion>
      ))}
    </div>
  );
}
export default Coaches;