export default function EvenPlayers({ IndianTeam }) {
  const [, second, , fourth, , sixth] = IndianTeam; // pick 1,3,5
  return (
    <ul>
      <li>Second: {second}</li>
      <li>Fourth: {fourth}</li>
      <li>Sixth: {sixth}</li>
    </ul>
  );
}