import React, { useEffect, useState } from "react";
import "./App.css";
import ReportDataHubConnector from "./ReportDataHubConnector";
import ReportDataResponse from "./ReportDataResponse";

function App() {
  const [inputValue, setInputValue] = useState("");
  const [message, setMessage] = useState("init value");
  const [messsageArr, setMessageArr] = useState([]);
  const [serialId, setSerialId] = useState<string>();

  useEffect(() => {
    if (serialId) {
      const connector = ReportDataHubConnector.getInstance(serialId);

      console.log(`at useEffect : hub with serialId : ${serialId}`);
      connector.reportDataUpdateEvents((reportData) => setMessage(reportData));
    }
  }, [serialId]);

  useEffect(() => {
    setMessageArr((prevState) => ([...prevState, message]));
  }, [message]);

  return (
    <div className="App">
      <span>
        message from signalR:{" "}
        <span style={{ color: "green" }}>{messsageArr.join("\r\n")}</span>{" "}
      </span>
      <br />
      <input
        type="text"
        value={inputValue}
        onChange={(e) => setInputValue(e.target.value)}
      />
      <button onClick={() => setSerialId(inputValue)}>Set Serial</button>
    </div>
  );
}
export default App;
