import React, { Component } from 'react';
import Counter from './Counter';
import SayWelcome from './SayWelcome';
import ClickMe from './ClickMe';
import CurrencyConverter from './CurrencyConverter';

export default class EventExamples extends Component {
  render() {
    return (
      <>
        <Counter />
        <SayWelcome />
        <ClickMe />
        <CurrencyConverter />
      </>
    );
  }
}