import { useEffect, useState } from "react";
import Api from "../Api";

const Trainings = () => {
  const [userTrainings, setUserTrainings] = useState([]);
  const [publicTrainings, setPublicTrainings] = useState([]);
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

  return (
    <div>
      <h2>Moje plany treningowe</h2>
      {userTrainings.length > 0 ? (
        userTrainings.map((training) => <p>{training.name}</p>)
      ) : (
        <p>Brak</p>
      )}
      <h2>Gotowe plany treningowe</h2>
      {publicTrainings.length > 0 ? (
        publicTrainings.map((training) => <p>{training.name}</p>)
      ) : (
        <p>Brak</p>
      )}
    </div>
  );
};

export default Trainings;
