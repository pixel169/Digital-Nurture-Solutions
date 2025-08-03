import React from 'react';

export default function SayWelcome() {
  const say = (msg) => alert(msg);
  return <button onClick={() => say('Welcome')}>Say Welcome</button>;
}