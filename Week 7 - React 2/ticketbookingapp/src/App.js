import React, { useState } from 'react';
import LoginButton  from './LoginButton';
import LogoutButton from './LogoutButton';
import Greeting     from './Greeting';

export default function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  return (
    <div>
      {isLoggedIn ? (
        <>
          <Greeting isLoggedIn={isLoggedIn} />
          <LogoutButton onClick={() => setIsLoggedIn(false)} />
        </>
      ) : (
        <>
          <Greeting isLoggedIn={isLoggedIn} />
          <LoginButton onClick={() => setIsLoggedIn(true)} />
        </>
      )}
    </div>
  );
}