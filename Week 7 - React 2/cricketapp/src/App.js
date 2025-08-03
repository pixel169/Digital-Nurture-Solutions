import { useState } from 'react';
import ListofPlayers from './ListofPlayers';
import Scorebelow70 from './Scorebelow70';
import OddPlayers from './OddPlayers';
import EvenPlayers from './EvenPlayers';
import ListofIndianPlayers from './ListofIndianPlayers';
export default function App() {
  const [flag, setFlag] = useState(true);
  const players = [
    { name: 'Jack',      score: 50 },
    { name: 'Michael',   score: 70 },
    { name: 'John',      score: 40 },
    { name: 'Ann',       score: 61 },
    { name: 'Elisabeth', score: 61 },
    { name: 'Sachin',    score: 95 },
    { name: 'Dhoni',     score: 100 },
    { name: 'Virat',     score: 84 },
    { name: 'Jadeja',    score: 64 },
    { name: 'Raina',     score: 75 },
    { name: 'Rohit',     score: 80 }
  ];
  const IndianTeam = ['Sachin1', 'Dhoni2', 'Virat3', 'Rohit4', 'Yuvaraj5', 'Raina6'];
  return (
    <>
      <button onClick={() => setFlag(!flag)}>
        Toggle Flag (now: {flag.toString()})
      </button>

      {flag ? (
        <div>
          <h1>List of Players</h1>
          <ListofPlayers players={players} />

          <hr />

          <h1>List of Players having Scores Less than 70</h1>
          <Scorebelow70 players={players} />
        </div>
      ) : (
        <div>
          <h1>Indian Team</h1>

          <h2>Odd Players</h2>
          <OddPlayers IndianTeam={IndianTeam} />

          <hr />

          <h2>Even Players</h2>
          <EvenPlayers IndianTeam={IndianTeam} />

          <hr />

          <h1>List of Indian Players Merged:</h1>
          <ListofIndianPlayers IndianPlayers={IndianTeam} />
        </div>
      )}
    </>
  );
}