import React, { Component } from 'react';

export default class Counter extends Component {
  state = { count: 0 };

  increment = () => {
    this.setState(({ count }) => ({ count: count + 1 }));
    this.sayHello();
  };

  decrement = () => this.setState(({ count }) => ({ count: count - 1 }));

  sayHello = () => alert('Hello! Static message');

  render() {
    return (
      <>
        <h2>Counter: {this.state.count}</h2>
        <button onClick={this.increment}>Increment</button>
        <button onClick={this.decrement}>Decrement</button>
      </>
    );
  }
}