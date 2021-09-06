using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PickToLightData_TestHarness
{
    /// <summary>
    /// These classes are used to store the Serialized/DeSerialized JSON Requests/Responses
    /// They were created using "http://json2csharp.com"
    /// </summary>

    public class RequestSkidBuild
    {
        public string order { get; set; }
        public string supplier { get; set; }
        public string plant { get; set; }
        public string dock { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<APIexception> exceptions { get; set; }
        public List<Skid> skids { get; set; }
        public RequestSkidBuild()
        {
        }

        public RequestSkidBuild(string example)
        {
            if (example.Equals("ExampleFromGarry"))
            {
                //[
                //{"order":"2018090501SA", "supplier":"22602", "plant":"02TMI", "dock":"M8","exceptions":[{"exceptionCode":"","comments":""},{"exceptionCode":"","comments":""}],
                // "skids": 
                //	[{"palletization":"D8","skidId":"001","kanbans":

                //        [{"lineSideAddress":"K16-02-DTL","partNumber":"698100C07000","kanban":"MC34","qpc":4,"boxNumber":1,"manifestNumber":"","rfId":""}
                //		,{"lineSideAddress":"K16-02-DTL","partNumber":"698100C07000","kanban":"MC34","qpc":4,"boxNumber":2,"manifestNumber":"","rfId":""}
                //		,{"lineSideAddress":"K16-02-DTL","partNumber":"698100C07000","kanban":"MC34","qpc":4,"boxNumber":3,"manifestNumber":"","rfId":""}
                //		,{"lineSideAddress":"K16-02-DTL","partNumber":"698100C07000","kanban":"MC34","qpc":4,"boxNumber":4,"manifestNumber":"","rfId":""}
                //		,{"lineSideAddress":"K16-02-DTL","partNumber":"698100C07000","kanban":"MC34","qpc":4,"boxNumber":5,"manifestNumber":"","rfId":""}
                //		,{"lineSideAddress":"K16-02-DTL","partNumber":"698100C07000","kanban":"MC34","qpc":4,"boxNumber":6,"manifestNumber":"","rfId":""}
                //		,{"lineSideAddress":"K16-02-DTL","partNumber":"698100C07000","kanban":"MC34","qpc":4,"boxNumber":7,"manifestNumber":"","rfId":""}
                //		,{"lineSideAddress":"K16-02-DTL","partNumber":"698100C07000","kanban":"MC34","qpc":4,"boxNumber":8,"manifestNumber":"","rfId":""}
                //		,{"lineSideAddress":"K16-02-DTL","partNumber":"698100C07000","kanban":"MC34","qpc":4,"boxNumber":9,"manifestNumber":"","rfId":""}
                //		,{"lineSideAddress":"K16-02-DTL","partNumber":"698100C07000","kanban":"MC34","qpc":4,"boxNumber":10,"manifestNumber":"","rfId":""}
                //		,{"lineSideAddress":"K16-02-DTL","partNumber":"698100C07000","kanban":"MC34","qpc":4,"boxNumber":11,"manifestNumber":"","rfId":""}],
                //
                //Not implemented yet, since it may change, just comment out for now!!
                //
                //////		"rfidDetails":[{"rfid":"","type":""},{"rfid":"","type":"" }]}]}
                //]


                this.order = "2018090501SA";
                this.supplier = "22602";
                this.plant = "02TMI";
                this.dock = "M8";

                //Tell the Serializer to ignore this by adding the Json Attribute: [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
                //to the "exceptions" property in the RequestSkidBuild class.
                this.exceptions = null;
                //this.exceptions = new List<APIexception>();
                //APIexception exceptionItem1 = new APIexception();
                //exceptionItem1.exceptionCode = "";
                //exceptionItem1.comments = "";
                //this.exceptions.Add(exceptionItem1);
                //APIexception exceptionItem2 = new APIexception();
                //exceptionItem2.exceptionCode = "";
                //exceptionItem2.comments = "";
                //this.exceptions.Add(exceptionItem2);

                skids = new List<Skid>();

                Skid skidItem = new Skid();
                skidItem.palletization = "D8";
                skidItem.skidId = "001";

                skidItem.kanbans = new List<Kanban>();

                Kanban kanbanItem1 = new Kanban();
                kanbanItem1.lineSideAddress = "K16-02-DTL";
                kanbanItem1.partNumber = "698100C07000";
                kanbanItem1.kanban = "MC34";
                kanbanItem1.qpc = 4;
                kanbanItem1.boxNumber = 1;
                kanbanItem1.manifestNumber = null; //manifestNumber = "";
                kanbanItem1.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem1);
                Kanban kanbanItem2 = new Kanban();
                kanbanItem2.lineSideAddress = "K16-02-DTL";
                kanbanItem2.partNumber = "698100C07000";
                kanbanItem2.kanban = "MC34";
                kanbanItem2.qpc = 4;
                kanbanItem2.boxNumber = 2;
                kanbanItem2.manifestNumber = null; //manifestNumber = "";
                kanbanItem2.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem2);
                Kanban kanbanItem3 = new Kanban();
                kanbanItem3.lineSideAddress = "K16-02-DTL";
                kanbanItem3.partNumber = "698100C07000";
                kanbanItem3.kanban = "MC34";
                kanbanItem3.qpc = 4;
                kanbanItem3.boxNumber = 3;
                kanbanItem3.manifestNumber = null; //manifestNumber = "";
                kanbanItem3.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem3);
                Kanban kanbanItem4 = new Kanban();
                kanbanItem4.lineSideAddress = "K16-02-DTL";
                kanbanItem4.partNumber = "698100C07000";
                kanbanItem4.kanban = "MC34";
                kanbanItem4.qpc = 4;
                kanbanItem4.boxNumber = 4;
                kanbanItem4.manifestNumber = null; //manifestNumber = "";
                kanbanItem4.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem4);
                Kanban kanbanItem5 = new Kanban();
                kanbanItem5.lineSideAddress = "K16-02-DTL";
                kanbanItem5.partNumber = "698100C07000";
                kanbanItem5.kanban = "MC34";
                kanbanItem5.qpc = 4;
                kanbanItem5.boxNumber = 5;
                kanbanItem5.manifestNumber = null; //manifestNumber = "";
                kanbanItem5.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem5);
                Kanban kanbanItem6 = new Kanban();
                kanbanItem6.lineSideAddress = "K16-02-DTL";
                kanbanItem6.partNumber = "698100C07000";
                kanbanItem6.kanban = "MC34";
                kanbanItem6.qpc = 4;
                kanbanItem6.boxNumber = 6;
                kanbanItem6.manifestNumber = null; //manifestNumber = "";
                kanbanItem6.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem6);
                Kanban kanbanItem7 = new Kanban();
                kanbanItem7.lineSideAddress = "K16-02-DTL";
                kanbanItem7.partNumber = "698100C07000";
                kanbanItem7.kanban = "MC34";
                kanbanItem7.qpc = 4;
                kanbanItem7.boxNumber = 7;
                kanbanItem7.manifestNumber = null; //manifestNumber = "";
                kanbanItem7.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem7);
                Kanban kanbanItem8 = new Kanban();
                kanbanItem8.lineSideAddress = "K16-02-DTL";
                kanbanItem8.partNumber = "698100C07000";
                kanbanItem8.kanban = "MC34";
                kanbanItem8.qpc = 4;
                kanbanItem8.boxNumber = 8;
                kanbanItem8.manifestNumber = null; //manifestNumber = "";
                kanbanItem8.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem8);
                Kanban kanbanItem9 = new Kanban();
                kanbanItem9.lineSideAddress = "K16-02-DTL";
                kanbanItem9.partNumber = "698100C07000";
                kanbanItem9.kanban = "MC34";
                kanbanItem9.qpc = 4;
                kanbanItem9.boxNumber = 9;
                kanbanItem9.manifestNumber = null; //manifestNumber = "";
                kanbanItem9.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem9);
                Kanban kanbanItem10 = new Kanban();
                kanbanItem10.lineSideAddress = "K16-02-DTL";
                kanbanItem10.partNumber = "698100C07000";
                kanbanItem10.kanban = "MC34";
                kanbanItem10.qpc = 4;
                kanbanItem10.boxNumber = 10;
                kanbanItem10.manifestNumber = null; //manifestNumber = "";
                kanbanItem10.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem10);
                Kanban kanbanItem11 = new Kanban();
                kanbanItem11.lineSideAddress = "K16-02-DTL";
                kanbanItem11.partNumber = "698100C07000";
                kanbanItem11.kanban = "MC34";
                kanbanItem11.qpc = 4;
                kanbanItem11.boxNumber = 11;
                kanbanItem11.manifestNumber = null; //manifestNumber = "";
                kanbanItem11.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem11);

                //
                //Not implemented yet, since it may change, just comment out for now!!
                //
                //skidItem.rfidDetails = new List<RfidDetail>();

                //RfidDetail rfidItem1 = new RfidDetail();
                //rfidItem1.rfId = null; //rfId = "";
                //rfidItem1.type = "";
                //skidItem.rfidDetails.Add(rfidItem1);
                //RfidDetail rfidItem2 = new RfidDetail();
                //rfidItem2.rfId = null; //rfId = "";
                //rfidItem2.type = "";
                //skidItem.rfidDetails.Add(rfidItem2);

                this.skids.Add(skidItem);
            }
            else if (example.Equals("ExampleRevB"))
            {
                //"orders":[{"order":"2018032336","supplier":"99999" ,"plant":"01TMK","dock":"N1","pickUp":"2018-03-22T12:10","skids": [
                //{"palletization":"D","skidId":"007", "exceptions": [{"exceptionCode":"", "comments":""},{"exceptionCode":"", "comments":""}] }]}]}

                //Each Request will be an object containing the SKID details and list of Kanban
                //(array).
                //JSON example:
                //{
                //"order":"2018032336","supplier":"99999","plant":"01TMK","dock":"N1",
                //"exceptions": [{"exceptionCode":"10", "comments":""}, {"exceptionCode":"20", "comments":""}],
                //"skids": [
                //    {"palletization":"D","skidId":"007","kanbans": [
                //        {"lineSideAddress":"ENW-05","partNumber":"329060605000","kanban":"NQ49","qpc":12,"boxNumber":1,"manifestNumber":"", "rfId": ""},
                //        {"lineSideAddress":"ENW-05","partNumber":"329060605000","kanban":"NQ49","qpc":12,"boxNumber":2,"manifestNumber":"", "rfId": ""},
                //        {"lineSideAddress":"ENW-05","partNumber":"329060605000","kanban":"NQ49","qpc":12,"boxNumber":3,"manifestNumber":"", "rfId": ""},
                //        {"lineSideAddress":"ENW-05","partNumber":"329060605000","kanban":"NQ49","qpc":12,"boxNumber":4,"manifestNumber":"", "rfId": ""},
                //        {"lineSideAddress":"ENW-05","partNumber":"329060605000","kanban":"NQ49","qpc":12,"boxNumber":5,"manifestNumber":"", "rfId": ""},
                //        {"lineSideAddress":"ENW-05","partNumber":"329060605000","kanban":"NQ49","qpc":12,"boxNumber":6,"manifestNumber":"", "rfId": ""},
                //        {"lineSideAddress":"ENW-05","partNumber":"329060605000","kanban":"NQ49","qpc":12,"boxNumber":7,"manifestNumber":"", "rfId": ""},
                //        {"lineSideAddress":"ENW-05","partNumber":"329060602000","kanban":"NPX0","qpc":12,"boxNumber":1,"manifestNumber":"", "rfId": ""}],
                //
                //Not implemented yet, since it may change, just comment out for now!!
                //
                //////    "rfidDetails": [{"rfid": "", "type": ""}, {"rfid": "", "type": "" }]
                //    }]
                //}


                this.order = "2018032336";
                this.supplier = "99999";
                this.plant = "01TMC";
                this.dock = "N1";


                this.exceptions = new List<APIexception>();

                APIexception exceptionItem1 = new APIexception();
                exceptionItem1.exceptionCode = "10";
                exceptionItem1.comments = "";
                this.exceptions.Add(exceptionItem1);
                APIexception exceptionItem2 = new APIexception();
                exceptionItem2.exceptionCode = "20";
                exceptionItem2.comments = "";
                this.exceptions.Add(exceptionItem2);


                skids = new List<Skid>();

                Skid skidItem = new Skid();
                skidItem.palletization = "D";
                skidItem.skidId = "007";

                skidItem.kanbans = new List<Kanban>();

                Kanban kanbanItem1 = new Kanban();
                kanbanItem1.lineSideAddress = "ENW-05";
                kanbanItem1.partNumber = "329060605000";
                kanbanItem1.kanban = "NQ49";
                kanbanItem1.qpc = 12;
                kanbanItem1.boxNumber = 1;
                kanbanItem1.manifestNumber = null; //manifestNumber = "";
                kanbanItem1.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem1);
                Kanban kanbanItem2 = new Kanban();
                kanbanItem2.lineSideAddress = "ENW-05";
                kanbanItem2.partNumber = "329060605000";
                kanbanItem2.kanban = "NQ49";
                kanbanItem2.qpc = 12;
                kanbanItem2.boxNumber = 2;
                kanbanItem2.manifestNumber = null; //manifestNumber = "";
                kanbanItem2.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem2);
                Kanban kanbanItem3 = new Kanban();
                kanbanItem3.lineSideAddress = "ENW-05";
                kanbanItem3.partNumber = "329060605000";
                kanbanItem3.kanban = "NQ49";
                kanbanItem3.qpc = 12;
                kanbanItem3.boxNumber = 3;
                kanbanItem3.manifestNumber = null; //manifestNumber = "";
                kanbanItem3.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem3);
                Kanban kanbanItem4 = new Kanban();
                kanbanItem4.lineSideAddress = "ENW-05";
                kanbanItem4.partNumber = "329060605000";
                kanbanItem4.kanban = "NQ49";
                kanbanItem4.qpc = 12;
                kanbanItem4.boxNumber = 4;
                kanbanItem4.manifestNumber = null; //manifestNumber = "";
                kanbanItem4.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem4);
                Kanban kanbanItem5 = new Kanban();
                kanbanItem5.lineSideAddress = "ENW-05";
                kanbanItem5.partNumber = "329060605000";
                kanbanItem5.kanban = "NQ49";
                kanbanItem5.qpc = 12;
                kanbanItem5.boxNumber = 5;
                kanbanItem5.manifestNumber = null; //manifestNumber = "";
                kanbanItem5.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem5);
                Kanban kanbanItem6 = new Kanban();
                kanbanItem6.lineSideAddress = "ENW-05";
                kanbanItem6.partNumber = "329060605000";
                kanbanItem6.kanban = "NQ49";
                kanbanItem6.qpc = 12;
                kanbanItem6.boxNumber = 6;
                kanbanItem6.manifestNumber = null; //manifestNumber = "";
                kanbanItem6.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem6);
                Kanban kanbanItem7 = new Kanban();
                kanbanItem7.lineSideAddress = "ENW-05";
                kanbanItem7.partNumber = "329060605000";
                kanbanItem7.kanban = "NQ49";
                kanbanItem7.qpc = 12;
                kanbanItem7.boxNumber = 7;
                kanbanItem7.manifestNumber = null; //manifestNumber = "";
                kanbanItem7.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem7);
                Kanban kanbanItem8 = new Kanban();
                kanbanItem8.lineSideAddress = "ENW-05";
                kanbanItem8.partNumber = "329060602000";
                kanbanItem8.kanban = "NPX0";
                kanbanItem8.qpc = 12;
                kanbanItem8.boxNumber = 1;
                kanbanItem8.manifestNumber = null; //manifestNumber = "";
                kanbanItem8.rfId = null; //rfId = "";
                skidItem.kanbans.Add(kanbanItem8);

                //
                //Not implemented yet, since it may change, just comment out for now!!
                //
                //skidItem.rfidDetails = new List<RfidDetail>();

                //RfidDetail rfidItem1 = new RfidDetail();
                //rfidItem1.rfId = null; //rfId = "";
                //rfidItem1.type = "";
                //skidItem.rfidDetails.Add(rfidItem1);
                //RfidDetail rfidItem2 = new RfidDetail();
                //rfidItem2.rfId = null; //rfId = "";
                //rfidItem2.type = "";
                //skidItem.rfidDetails.Add(rfidItem2);

                this.skids.Add(skidItem);
            }
        }
    }

    public class APIexception //If rename this to "scsException" or something else, will "Newtonsoft.Json" still work?? 
    {
        public string exceptionCode { get; set; }
        public string comments { get; set; }
    }

    public class Skid
    {
        public string palletization { get; set; }
        public string skidId { get; set; }
        public List<Kanban> kanbans { get; set; }
        //
        //Not implemented yet, since it may change, just comment out for now!!
        //
        //public List<RfidDetail> rfidDetails { get; set; }
    }

    public class Kanban
    {
        public string lineSideAddress { get; set; }
        public string partNumber { get; set; }
        public string kanban { get; set; }
        public int qpc { get; set; }
        public int boxNumber { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] //This allows us to set manifestNumber to null and have it omitted from the JSON Request!
        public string manifestNumber { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] //This allows us to set rfId to null and have it omitted from the JSON Request!
        public string rfId { get; set; }
    }

    //
    //Not implemented yet, since it may change, just comment out for now!!
    //
    //public class RfidDetail
    //{
    //    public string rfid { get; set; }
    //    public string type { get; set; }
    //}

    public class RequestShipmentLoad
    {
        public string supplier { get; set; }
        public string route { get; set; }
        public string run { get; set; }
        public string trailerNumber { get; set; }
        public bool dropHook { get; set; }
        public string sealNumber { get; set; }
        public string supplierTeamFirstName { get; set; }
        public string supplierTeamLastName { get; set; }
        public string lpCode { get; set; }
        public string driverTeamFirstName { get; set; }
        public string driverTeamLastName { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<APIexception> exceptions { get; set; }
        public List<Order> orders { get; set; }
    }

    public class ShipmentSkid
    {
        public string palletization { get; set; }
        public string skidId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<APIexception> exceptions { get; set; }
    }

    public class Order
    {
        public string order { get; set; }
        public string supplier { get; set; }
        public string plant { get; set; }
        public string dock { get; set; }
        public string pickUp { get; set; }
        public List<ShipmentSkid> skids { get; set; }
    }

    public class APIresponse
    {
        //public System.Net.WebException webException { get; set; }
        public System.Exception dotNetException { get; set; }

        public int code { get; set; }
        public List<Message> messages { get; set; }
        public string confirmationNumber { get; set; }
        public List<ConfirmedOrder> confirmedOrders { get; set; }
        public ConfirmedTrailer confirmedTrailer { get; set; }

        public string httpCode { get; set; }
        public string httpMessage { get; set; }
        public string moreInformation { get; set; }
    }

    public class Message
    {
        public string keyObject { get; set; }
        public List<string> message { get; set; }
        public object type { get; set; }  //NULL in every example I have!
    }

    public class ConfirmedOrder
    {
        public string order { get; set; }
        public string supplier { get; set; }
        public string plant { get; set; }
        public string dock { get; set; }
    }

    public class ConfirmedTrailer
    {
        public string supplier { get; set; }
        public string route { get; set; }
        public string run { get; set; }
        public string trailerNumber { get; set; }
        public string pickUp { get; set; }
    }
}


