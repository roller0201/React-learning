import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import Heading from "./components/heading/Heading";
import HeadingStyledComponent from "./components/headingStyledComponents/HeadingStyledComponent";
import Footer from "./components/footer/Footer";
import reportWebVitals from "./reportWebVitals";

const companyData = {
  email: "contact@example.com",
  city: "Warsaw",
  street: "Ujazdowskie",
};

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <HeadingStyledComponent />
    <Heading headerTitle="Welcome on page" />
    <App />
    <Footer companyData={companyData} contact="admin@example.com" />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
