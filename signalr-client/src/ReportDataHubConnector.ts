import * as signalR from "@microsoft/signalr";
import ReportDataResponse from "./ReportDataResponse";

const URL = "http://localhost:5152/hubs/data-report"; //or whatever your backend port is
class ReportDataHubConnector {

  public connection: signalR.HubConnection;

  public serialId: string;

  //create with singleton pattern
  private static _instance: ReportDataHubConnector;
  public static getInstance(serial: string) {
    if (this._instance) {
      this._instance.restartInstance(serial);
      return this._instance;
    } else {
      return this._instance ??= new ReportDataHubConnector(serial);
    }
  }

  public static hasInstance = () => this._instance != null;

  constructor(serialId: string) {
    this.serialId = serialId;

    this.connection = this.setConnection(serialId);
    this.connection.start().catch((err) => document.write(err));

    this.reportDataUpdateEvents = (onReportDataReceived) => {
      this.connection.on("ReceiveDataReport", (objJsonString) => {
        onReportDataReceived(objJsonString);
        console.log(objJsonString);
        
      });
    };
    // this.reportDataUpdateEvents = (onReportDataReceived) => {
    //   this.connection.on("ReceiveDataReport", ([reportData]:[ReportDataResponse]) => {
    //     onReportDataReceived(reportData);
    //     console.log(reportData);
        
    //   });
    // };
    this.connection.onclose(() => console.log("This connected closed"));
  }

  private setConnection(serialId): signalR.HubConnection {
    return new signalR.HubConnectionBuilder()
      .withUrl(`${URL}?searialId=${serialId}`, {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .withAutomaticReconnect()
      .build();
  }

  public reportDataUpdateEvents: (
    onReportDataReceived: (reportData) => void
  ) => void;

  public restartInstance(serialId: string) {
    this.connection.stop();
    console.log("This connection is close");
    
    this.connection = this.setConnection(serialId);
    this.connection
      .start()
      .then(() => console.log("ReportDataHubConnector was restarted: "))
      .catch((err) => document.write(err));
  }
}
export default ReportDataHubConnector;
