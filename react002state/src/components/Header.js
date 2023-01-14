import React from "react";
import "./header.css";

export default class Header extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      link: "home",
      linkClicked: false,
    };
  }

  toggle = (event) => {
    this.setState((state) => ({ linkClicked: !state.linkClicked }));
  };
  render() {
    return (
      <div>
        <nav>
          <ul>
            <li className={this.state.linkClicked ? "clicked" : ""}>
              <a href="#" id="link" onClick={this.toggle}>
                {this.state.link}
              </a>
              <p>{this.state.linkClicked ? "clicked" : "not clicked"}</p>
            </li>
          </ul>
        </nav>
      </div>
    );
  }
}
