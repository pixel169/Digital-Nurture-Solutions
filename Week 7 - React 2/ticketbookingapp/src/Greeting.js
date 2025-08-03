import UserGreeting  from './UserGreeting';
import GuestGreeting from './GuestGreeting';

export default function Greeting({ isLoggedIn }) {
  return isLoggedIn ? <UserGreeting /> : <GuestGreeting />;
}