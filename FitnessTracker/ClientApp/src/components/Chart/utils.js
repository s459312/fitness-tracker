import moment from "moment";

export const getRandomNumber = (min, max) => {
    return Math.floor(Math.random() * (max - min + 1)) + min;
};

export const getFakeExerciseData = (days) => {
    const exerciseData = [];
    const today = moment();
    for (let i = 0; i < days; i++) {
        const date = today.add(1, 'days');
        exerciseData.push({
            date: date.format().slice(0, 10),
            powtorzenia: Math.floor(Math.random() * 10) + 3,
            serie: Math.floor(Math.random() * 5) + 3,
            obciazenie: Math.floor(Math.random() * 35) + 10,
            czas: 0,
            dystans: 0
        });
    }

    return exerciseData;
}