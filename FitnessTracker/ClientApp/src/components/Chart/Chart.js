import React from 'react';
import {Bar, Line} from 'react-chartjs-2';
import {getRandomNumber} from "./utils";

const putAttrOccurancesToArray = (exerciseData, attribute) => {
    const labels = [];
    for (const exercise of exerciseData) {
        labels.push(exercise[attribute]);
    }
    return labels
}


export default class Chart extends React.Component {
    constructor(props) {
        super(props);
    }

    state = {
        chartData: {
            labels: [],
            datasets: []
        }
    }

    componentDidMount() {
        const rawData = this.props.rawData;
        const chartSetsAttributes = this.props.attributesToShow;

        const datesLabels = putAttrOccurancesToArray(rawData, 'date');
        const chartSets = [];


        for (const attrName of chartSetsAttributes) {
            const red = getRandomNumber(0, 255);
            const green = getRandomNumber(0, 255);
            const blue = getRandomNumber(0, 255);

            const randomColor = `rgba(${red},${green},${blue},`;

            const occurrencesArray = putAttrOccurancesToArray(rawData, attrName);
            chartSets.push({
                label: attrName,
                backgroundColor: randomColor + 0.2 + `)`,
                borderColor: randomColor + 1 + `)`,
                borderWidth: 1,
                hoverBackgroundColor: randomColor + 0.4 + `)`,
                hoverBorderColor: randomColor + 0.1 + `)`,
                data: occurrencesArray
            })
        }

        this.setState({
            chartData: {
                labels: datesLabels,
                datasets: chartSets
            }
        })
    }

    renderChart(chartType, data) {
        switch (chartType) {
            case "line":
                return <Line data={data}/>
            case "bar":
                return <Bar data={data}/>
            default:
                return <div> Chart type not found! </div>
        }
    }


    render() {
        const {type} = this.props;

        return <>
            {
                this.renderChart(type, this.state.chartData)
            }
        </>
    }
}