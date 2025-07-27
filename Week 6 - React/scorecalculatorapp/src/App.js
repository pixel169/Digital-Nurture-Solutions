import React from 'react';
import CalculateScore from './Components/CalculateScore';

function App() {
  return (
    <div className="App">
      <CalculateScore
        Name="John Doe"
        School="Greenwood High"
        Total={450}
        Goal={5}
      />
    </div>
  );
}

export default App;
