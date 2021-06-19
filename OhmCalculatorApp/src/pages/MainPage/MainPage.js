import './MainPage.css'
import Resistor from '../../components/Resistor/Resistor'
import ResistorConfiguration from '../../models/ResistorConfiguration';
import { useEffect, useState } from 'react';
import BandColorSelector from '../../components/BandColorSelector/BandColorSelector';
import Button from 'react-bootstrap/Button';
import { Alert } from 'react-bootstrap';

const apiUrl = 'http://localhost:5000/api'

const MainPage = () => {
    const state = useState({ resistorConfiguration: [], bandColorSelectorConfiguration: [] });
    const [mainPageState, setMainPageState] = state;

    const [calculate, setCalculate] = useState();

    const calcState = useState({ isCalculating: false, calculationResult: null })
    const [calculationState, setCalculationState] = calcState;

    useEffect(() => {
        const onError = (err) => {
            alert('An error occured. The page will be reloaded.');
            window.location.reload();
        };

        fetch(apiUrl + '/ResistorDefaults')
        .then(results => results.json())
        .then(data => {
            const resistorConfigurations = [];

            for (const configuration of data) {
                resistorConfigurations.push(new ResistorConfiguration({rgb: configuration.color.rgb, id: configuration.color.id}, configuration.position));
            }

            fetch(apiUrl + '/ColorSelectorConfigurations')
            .then(results => results.json())
            .then(data => {
                setMainPageState({...mainPageState, resistorConfiguration: resistorConfigurations, bandColorSelectorConfiguration: data});
            }, onError);
        }, onError);
    }, []);

    useEffect(() => {
        if (!mainPageState.resistorConfiguration || !mainPageState.resistorConfiguration.length) {
            return;
        }
        
        const firstId = mainPageState.resistorConfiguration[0].color.id;
        const secondId = mainPageState.resistorConfiguration[1].color.id;
        const multiplierId = mainPageState.resistorConfiguration[2].color.id;
        const toleranceId = mainPageState.resistorConfiguration[3].color.id;

        fetch(apiUrl + `/calculator?firstId=${firstId}&secondId=${secondId}&multiplierId=${multiplierId}&toleranceId=${toleranceId}`)
        .then(results => results.json())
        .then(data => {
            setCalculationState({ isCalculating: false, calculationResult: { successful: true, response: data.result } });
        }, error => {
            setCalculationState({ isCalculating: false, calculationResult: { successful: false, error: error.toString() } });
        });
    }, [calculate]);

    return (
        <div className="MainPage">
            <header>
            <h1>Ohms Calculator</h1>
            </header>
            <Resistor configuration={ mainPageState.resistorConfiguration }/>
            <BandColorSelector configuration={ mainPageState.bandColorSelectorConfiguration } onColorSelected={ (rowIndex, colorIndex) => onColorSelected(state, rowIndex, colorIndex) }/>
            { renderCalculateSection(calcState, setCalculate) }
            { renderResultsSection(calcState) }
        </div>
    )
}

const onColorSelected = (state, rowIndex, colorIndex) => {
    const [mainPageState, setMainPageState] = state;

    const newMainPageState = { ...mainPageState };
    newMainPageState.resistorConfiguration[rowIndex].color = mainPageState.bandColorSelectorConfiguration[rowIndex].colors[colorIndex];

    setMainPageState(newMainPageState);
}

const renderCalculateSection = (state, setCalculate) => {
    const [calculationState] = state;
    const isCalculating = calculationState.isCalculating;

    if (isCalculating) {
        return (
            <div className="MainPage-section">
                Calculating...
            </div>
        );
    } else {
        return (
            <div className="MainPage-section">
                <Button variant="primary" onClick={() => onCalculateButtonClick(state, setCalculate)}>Calculate</Button>
            </div>
        );
    }
}

const onCalculateButtonClick = (state, setCalculate) => {
    const [calculationState, setCalculationState] = state;

    setCalculate({});
    setCalculationState({ isCalculating: true, calculationResult: null });
}

const renderResultsSection = (state) => {
    const [calculationResultState] = state;
    const calculationResult = calculationResultState.calculationResult;

    if (calculationResult) {
        if (calculationResult.successful) {
            return (
                <div className="MainPage-section">
                    <Alert variant="success">Result: {calculationResult.response}</Alert>
                </div>
            );
        } else {
            return (
                <div className="MainPage-section">
                    <Alert variant="error">Error: {calculationResult.error}</Alert>
                </div>
            );
        }
    }
}

export default MainPage;