import React from "react";
import "./heading.css";

class Heading extends React.Component {
  constructor() {
    super();
    this.state = { link1: "Link1" };
  }
  render() {
    return (
      <div>
        <nav>
          <h1 style={{ color: "blue", backgroundColor: "silver" }}>
            {this.props.headerTitle}
          </h1>
          <ul className="navigation">
            <li>{this.state.link1}</li>
            <li>Blog</li>
            <li>Artykuły</li>
          </ul>
        </nav>
      </div>
    );
  }
}
export default Heading;
