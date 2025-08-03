export default function OddPlayers({ IndianTeam }) {
  const [first, , third, , fifth] = IndianTeam; // pick 0,2,4
  return (
    <ul>
      <li>First: {first}</li>
      <li>Third: {third}</li>
      <li>Fifth: {fifth}</li>
    </ul>
  );
}