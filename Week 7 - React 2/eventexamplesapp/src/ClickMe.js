import React from 'react';

export default function ClickMe() {
  const handleClick = () => alert('I was clicked');
  return <button onClick={handleClick}>Click on me</button>;
}