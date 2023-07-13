import React from "react";
import { BrowserRouter as Router } from "react-router-dom";
import "./App.css";
import ApplicationViews from "./components/applicationViews";
import Header from "./components/header";

function App() {
  return (
    <div className="App">
      <Router>
        <Header />
        <ApplicationViews />
      </Router>
    </div>
  );
}

export default App;