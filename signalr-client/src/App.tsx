import React, { useEffect, useState } from "react";
import "./App.css";
import SignalRConnector from "./signalr-connection";

function App() {
  const { reportDataUpdateEvents } = SignalRConnector();
  const [message, setMessage] = useState("initial value");

  useEffect(() => {
    // events((_, message) => setMessage(message));
    reportDataUpdateEvents((reportData) => 
      setMessage(reportData));
  });

  return (
    <div className="App">
      <span>
        message from signalR: <span style={{ color: "green" }}>{message}</span>{" "}
      </span>
      <br />
      <button>
        send date{" "}
      </button>
    </div>
  );
}
export default App;
