import React from 'react';
import '../Stylesheets/mystyle.css';

function CalculateScore(props) {
    const average = props.Total / props.Goal;

    return (
        <div className="score-box">
            <h2>Score Calculator</h2>
            <p><strong>Name:</strong> {props.Name}</p>
            <p><strong>School:</strong> {props.School}</p>
            <p><strong>Total Score:</strong> {props.Total}</p>
            <p><strong>Goal:</strong> {props.Goal}</p>
            <p><strong>Average Score:</strong> {average.toFixed(2)}</p>
        </div>
    );
}

export default CalculateScore;
