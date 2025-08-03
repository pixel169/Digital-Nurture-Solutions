export default function ListofPlayers({ players }) {
  return (
    <ul>
      {players.map((item, i) => (
        <li key={i}>
          Mr. {item.name} {item.score}
        </li>
      ))}
    </ul>
  );
}