import React from 'react';

const offices = [
  { Name: 'DBS', Rent: 50000, Address: 'Chennai' }
];

export default function App() {
  return (
    <div>
      <h1>Office Space, at Affordable Range</h1>

      <img
        src="https://images.unsplash.com/photo-1497366754035-f200968a6e72?q=80&w=1169&auto=format&fit=crop"
        alt="Office"
        style={{ width: '25%' }}
      />

      {offices.map((item, i) => (
        <div key={i}>
          <h1>Name: {item.Name}</h1>
          <h3 style={{ color: item.Rent <= 60000 ? 'red' : 'green' }}>
            Rent: Rs.{item.Rent}
          </h3>
          <h3>Address: {item.Address}</h3>
        </div>
      ))}
    </div>
  );
}