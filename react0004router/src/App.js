import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { Link } from "react-router-dom";
import Home from "./Components/Home/Home";
import Articles from "./Components/Articles/Articles";
import Navigation from "./Components/Navigation/Navigation";
import NotFound from "./Components/NotFound/Notfound";

function App() {
  return (
    <Router>
      <Navigation />
      <Routes>
        <Route path="/articles" element={<Articles />} />
        <Route path="/article" element={<Articles />} />
        <Route exact path="/" element={<Home />} />
        <Route path="*" element={<NotFound />} />
      </Routes>
    </Router>
  );
}

export default App;
