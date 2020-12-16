import React from 'react';
import Api from "../Api";
import Chart from "./Chart/Chart";
import {getFakeExerciseData} from "./Chart/utils";

export default class HomePage extends React.Component {
    state = {
        exercisesRawDataPool: [],
    }

    componentDidMount() {
        this.fetchData();
    }

    fetchData = async () => {
        Api.get("/exercise/mine")
            .then(({data}) => {
                if (data) {
                    console.log('fetched ', data);
                    data.forEach(async (exerciseId) => {
                        const rawData = await Api.get(`/history/exercise/${exerciseId}`);
                        const exercise = await Api.get(`exercise/${exerciseId}`);

                        const rawDataPool = this.state.exercisesRawDataPool;
                        rawDataPool[exerciseId] = ({
                            label: exercise.data.name,
                            raw: rawData.data
                        });


                        this.setState({
                            exercisesRawDataPool: rawDataPool,
                        })

                    })


                }
            })
    }

    renderExerciseChart = (exerciseRawData) => {
        const exerciseAttributesToShow = ['powtorzenia', 'serie', 'obciazenie'];

        // @info - this function creates fake exercise data for 15 days in row
        // const exerciseRawData = getFakeExerciseData(15);

        return <div>
            <div>
                <h3> Wykres Słupkowy</h3>
                <Chart type={"bar"} rawData={exerciseRawData}
                       attributesToShow={exerciseAttributesToShow}/>
            </div>
            <div>
                <h3> Wykres Liniowy</h3>
                <Chart type={"line"} rawData={exerciseRawData}
                       attributesToShow={exerciseAttributesToShow}/>
            </div>

        </div>
    }


    render() {
        return <div>
            {this.state.exercisesRawDataPool.map((exerciseRawData, key) => (
                <div key={key}>
                    <div>
                        <h1>Twoje Postępy dla ćwiczenia <i>{exerciseRawData.label}</i></h1>
                    </div>

                    <div key={key}>
                        {this.renderExerciseChart(exerciseRawData.raw)}
                    </div>
                </div>
            ))}
        </div>

    }
}