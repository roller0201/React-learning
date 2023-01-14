import React from "react";
import { Link } from "react-router-dom";

export default class NotFound extends React.Component {
  render() {
    return (
      <div>
        <p>
          <h3>404 not found</h3>
          <link to="/">Home Page</link>
        </p>
      </div>
    );
  }
}
