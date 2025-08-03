const T20Players = ['First Player', 'Second Player', 'Third Player'];
const RanjiTrophyPlayers = ['Fourth Player', 'Fifth Player', 'Sixth Player'];

const merged = [...T20Players, ...RanjiTrophyPlayers];

export default function ListofIndianPlayers({ IndianPlayers }) {
  return (
    <ul>
      {merged.map((p, i) => (
        <li key={i}>Mr. {p}</li>
      ))}
    </ul>
  );
}