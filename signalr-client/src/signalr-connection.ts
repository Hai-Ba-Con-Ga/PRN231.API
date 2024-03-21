import * as signalR from "@microsoft/signalr";
import ReportDataResponse from "./ReportDataResponse";


const URL = "http://localhost:5152/hubs/data-report"; //or whatever your backend port is
const serialId = 1001;

class SignalRConnector {
  private connection: signalR.HubConnection;
  static instance: SignalRConnector;

  public reportDataUpdateEvents: (
    onReportDataReceived: (objJsonString) => void
  ) => void;

  constructor() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`${URL}?searialId=${serialId}`, {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .withAutomaticReconnect()
      .build();

    this.connection.start().catch((err) => document.write(err));

    this.reportDataUpdateEvents = (onReportDataReceived) => {
      this.connection.on(
        "ReceiveDataReport",
        (objJsonString) => {
          onReportDataReceived(objJsonString);
          console.log(objJsonString);
        }
      );
    };
  }

  // public newMessage = (messages: string) => {
  //   this.connection
  //     .send("newMessage", "foo", messages)
  //     .then((x) => console.log("sent"));
  // };

  public static getInstance(): SignalRConnector {
    if (!SignalRConnector.instance)
      SignalRConnector.instance = new SignalRConnector();
    return SignalRConnector.instance;
  }
  
}
export default SignalRConnector.getInstance;
