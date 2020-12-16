import React from 'react';
import Chart from "./Chart/Chart";
import {getFakeExerciseData} from "./Chart/utils";

export default class HomePage extends React.Component {
    componentDidMount() {
        //fetch data from database here
    }

    render() {

        const exerciseRawData = getFakeExerciseData(15);
        const exerciseAttributesToShow = ['powtorzenia', 'serie', 'obciazenie'];
        const exercisesPool = [0, 1, 2];

        return <div>
            <div>
                {exercisesPool.map((exerciseId) => (
                    <div key={exerciseId}>
                        <div>
                            <h1>Twoje Postępy dla ćwiczenia  id={exerciseId} </h1>
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
                ))}
            </div>


        </div>
    }
}