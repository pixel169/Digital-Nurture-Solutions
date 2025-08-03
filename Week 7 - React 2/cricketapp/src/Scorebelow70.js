export default function Scorebelow70({ players }) {
  const below70 = players.filter(p => p.score < 70);

  return (
    <ul>
      {below70.map((item, i) => (
        <li key={i}>
          Mr. {item.name} {item.score}
        </li>
      ))}
    </ul>
  );
}