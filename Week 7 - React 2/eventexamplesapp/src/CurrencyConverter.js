import React, { useState } from 'react';

export default function CurrencyConverter() {
  const [amount, setAmount]   = useState('');
  const [currency, setCurrency] = useState('Euro');
  const [result, setResult]   = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    const rate = 90; // fixed INR → EUR
    setResult((amount / rate).toFixed(2));
  };

  return (
    <>
      <h2>Currency Convertor!!!</h2>
      <form onSubmit={handleSubmit}>
        <label>
          Amount
          <input value={amount} onChange={(e) => setAmount(e.target.value)} />
        </label>
        <br />
        <label>
          Currency
          <select value={currency} onChange={(e) => setCurrency(e.target.value)}>
            <option>Euro</option>
          </select>
        </label>
        <br />
        <button type="submit">Submit</button>
      </form>
      {result && (
        <p>Converting to Euro: Amount is {result} Euro</p>
      )}
    </>
  );
}