import React from "react";
import { Link } from "react-router-dom";

export default class Navigation extends React.Component {
  render() {
    return (
      <nav>
        <ul>
          <li>
            {" "}
            <Link to="/">Home</Link>
          </li>
          <li>
            {" "}
            <Link to="/articles">Articles</Link>
          </li>
          <li>
            <Link to="bad_link">bad link</Link>
          </li>
        </ul>
      </nav>
    );
  }
}
