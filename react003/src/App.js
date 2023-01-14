import React from "react";
import logo from "./logo.svg";
import "./App.css";

class App extends React.Component {
  //mounting1
  constructor(props) {
    super(props);
    this.state = {};
  }
  //mounting 2 update1
  static getDerivedStateFromProps(props, state) {
    return null;
  }
  //update 2
  shouldComponentUpdate(nextProps, nextState) {
    return true;
  }
  // mounting 3, update3
  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <p>
            Edit <code>src/App.js</code> and save to reload.
          </p>
          <a
            className="App-link"
            href="https://reactjs.org"
            target="_blank"
            rel="noopener noreferrer"
          >
            Learn React
          </a>
        </header>
      </div>
    );
  }
  //update 4
  getSnapshotBeforeUpdate(prevProps, prevState) {
    return null;
  }
  // mounting 4
  componentDidMount() {}
  // update 5
  componentDidUpdate() {}
}

export default App;
