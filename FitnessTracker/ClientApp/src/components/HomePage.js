import React from 'react';
import Api from "../Api";
import Chart from "./Chart/Chart";
import {getFakeExerciseData} from "./Chart/utils";

export default class HomePage extends React.Component {
    state = {
        exercisesPool: []
    }

    componentDidMount() {
        this.fetchData();
    }

    fetchData = () => {
        Api.get("/exercise/mine")
            .then(({data}) => {
                if (data) {
                    console.log('fetched ', data);
                    // this.setState({
                    //     exercisesPool: [...data]
                    // })
                }
            })
    }

    renderExerciseChart = async (exerciseId) => {
        const exerciseAttributesToShow = ['powtorzenia', 'serie', 'obciazenie'];

        //@info - this function creates fake exercise data for 15 days in row
        // const exerciseRawData = getFakeExerciseData(15);

        const exerciseRawData = await Api.get(`/history/exercise/${exerciseId}`);
        console.log('raaaw', exerciseRawData);
        return <div>
            <div>
                <h1>Twoje Postępy dla ćwiczenia o numerze {exerciseId} </h1>
            </div>

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
            {this.state.exercisesPool.map((exerciseId) => (
                <div key={exerciseId}>
                    {this.renderExerciseChart(exerciseId)}
                </div>
            ))}
        </div>
        </div>
    }
}