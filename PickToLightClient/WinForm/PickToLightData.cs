using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SNA.WinForm.PickToLightClient
{
    public class PickToLightData
    {
        //public static string prodConnectionString = @"Server=GT-transACTION\SQLExpress;Database=PickToLight; Uid=PickToLight; pwd=P1ckT0L1ght;";
        //public static string devConnectionString = @"Server=GT-transACTION\SQLExpress;Database=PickToLightDev; Uid=PickToLight; pwd=P1ckT0L1ght;";
        //public static string prodConnectionString = @"Server=HQ-SQL02;Database=PickToLight; Uid=PickToLight; pwd=P1ckT0L1ght;";
        //public static string devConnectionString = @"Server=HQ-SQL02;Database=PickToLightDev; Uid=PickToLight; pwd=P1ckT0L1ght;";
        ////public static string shayneHomeServerConnectionString = @"Server=192.168.1.4;Database=PickToLightDev; Uid=PickToLight; pwd=P1ckT0L1ght;";
        public string SQLDBConnectionString { get; set; }

        public PickToLightData(string databaseConnectionString)
        {
            //Does it matter? Set the local/private variable or the property??
            //_sqlConnectionString = databaseConnectionString;
            //sqlConnectionString = databaseConnectionString;
            SQLDBConnectionString = databaseConnectionString;
        }

        public int CreateLogEntry(string Source, string ThreadName, int ThreadID, string Data)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.SQLDBConnectionString))
                {
                    //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
                    //using (SqlCommand cmd = new SqlCommand("INSERT INTO [Log]([Source],[ThreadName],[ThreadID],[Data],[Created]) output INSERTED.ID VALUES(@Source,@ThreadName,@ThreadID,@Data,@Created)", connection))
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [Log]([Source],[ThreadName],[ThreadID],[Data]) output INSERTED.ID VALUES(@Source,@ThreadName,@ThreadID,@Data)", connection))
                    {
                        cmd.CommandTimeout = 2;
                        cmd.Parameters.AddWithValue("@Source", Source);
                        cmd.Parameters.AddWithValue("@ThreadName", ThreadName);
                        cmd.Parameters.AddWithValue("@ThreadID", ThreadID);
                        cmd.Parameters.AddWithValue("@Data", Data);
                        //CURRENT DATE/TIME, populated by database as default value, so not really necessary!
                        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
                        //cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }

                        int insertedID = (int)cmd.ExecuteScalar();

                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        return (insertedID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (-1);
        }

        public int CreateScanManifestEntry(ManifestSQL manifest)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.SQLDBConnectionString))
                {
                    string insertCommand =
                        "INSERT INTO [ScanManifest](" +
                            "[Barcode]" +
                            ",[OrderNumber]" +
                            ",[NAMC]" +
                            ",[SupplierCode]" +
                            ",[DockCode]" +
                            ",[MainRoute]" +
                            ",[SupplierShipDock]" +
                            ",[PalletizationCode]" +
                            ",[OrderSequence]" +
                            ",[DeviceName]" +
                            ",[DeviceIdentifier]" +
                            ",[Scanned]" +
                            ",[ScannedBy]" +
                        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
                        //    ",[Created]" +
                            ",[CreatedBy]" +
                            ",[Decoded]" +
                            ",[ImagePath])" +
                            " output INSERTED.ID " +
                        "VALUES(" +
                            "@Barcode," +
                            "@OrderNumber," +
                            "@NAMC," +
                            "@SupplierCode," +
                            "@DockCode," +
                            "@MainRoute," +
                            "@SupplierShipDock," +
                            "@PalletizationCode," +
                            "@OrderSequence," +
                            "@DeviceName," +
                            "@DeviceIdentifier," +
                            "@Scanned," +
                            "@ScannedBy," +
                        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
                        //    "@Created," +
                            "@CreatedBy," +
                            "@Decoded," +
                            "@ImagePath)";
                    using (SqlCommand cmd = new SqlCommand(insertCommand, connection))
                    {
                        cmd.CommandTimeout = 2;
                        cmd.Parameters.AddWithValue("@Barcode", manifest.Barcode);
                        cmd.Parameters.AddWithValue("@OrderNumber", manifest.OrderNumber);
                        cmd.Parameters.AddWithValue("@NAMC", manifest.NAMC);
                        cmd.Parameters.AddWithValue("@SupplierCode", manifest.SupplierCode);
                        cmd.Parameters.AddWithValue("@DockCode", manifest.DockCode);
                        cmd.Parameters.AddWithValue("@MainRoute", manifest.MainRoute);
                        cmd.Parameters.AddWithValue("@SupplierShipDock", manifest.SupplierShipDock);
                        cmd.Parameters.AddWithValue("@PalletizationCode", manifest.PalletizationCode);
                        cmd.Parameters.AddWithValue("@OrderSequence", manifest.OrderSequence);
                        cmd.Parameters.AddWithValue("@DeviceName", manifest.DeviceName);
                        cmd.Parameters.AddWithValue("@DeviceIdentifier", manifest.DeviceIdentifier);
                        cmd.Parameters.AddWithValue("@Scanned", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ScannedBy", manifest.ScannedBy);
                        //CURRENT DATE/TIME, populated by database as default value, so not really necessary!
                        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
                        //cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", manifest.CreatedBy);
                        cmd.Parameters.AddWithValue("@Decoded", manifest.Decoded);
                        cmd.Parameters.AddWithValue("@ImagePath", manifest.ImagePath);
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }

                        int insertedID = (int)cmd.ExecuteScalar();

                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        return (insertedID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (-1);
        }

        public int CreateToyotaOWKScanEntry(ToyotaOWKSQL toyotaOWK)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.SQLDBConnectionString))
                {
                    string insertCommand =
                        "INSERT INTO [ScanOWK](" +
                            "[Barcode]" +
                            ",[SupplierInformation]" +
                            ",[SupplierCode]" +
                            ",[DockCode]" +
                            ",[KanbanNumber]" +
                            ",[PartNumber]" +
                            ",[LineSideAddress]" +
                            ",[StoreAddress]" +
                            ",[LotSize]" +
                            ",[SupplierName]" +
                            ",[MainRoute]" +
                            ",[SubRoute]" +
                            ",[UnloadDate]" +
                            ",[ShipDate]" +
                            ",[ShipTime]" +
                            ",[OrderNumber]" +
                            ",[BoxNumber]" +
                            ",[BoxTotal]" +
                            ",[NAMCDestination]" +
                            ",[InternalKanbanKey]" +
                            ",[LabelLocationIndicator]" +
                            ",[NAMCData1]" +
                            ",[NAMCData2]" +
                            ",[PalletizationCode]" +
                            ",[NAMCData3]" +
                            ",[NAMCData4]" +
                            ",[Filler]" +
                            ",[DeviceName]" +
                            ",[DeviceIdentifier]" +
                            ",[Scanned]" +
                            ",[ScannedBy]" +
                        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
                        //    ",[Created]" +
                            ",[CreatedBy]" +
                            ",[Decoded]" +
                            ",[ImagePath])" +
                            " output INSERTED.ID " +
                        "VALUES(" +
                            "@Barcode," +
                            "@SupplierInformation," +
                            "@SupplierCode," +
                            "@DockCode," +
                            "@KanbanNumber," +
                            "@PartNumber," +
                            "@LineSideAddress," +
                            "@StoreAddress," +
                            "@LotSize," +
                            "@SupplierName," +
                            "@MainRoute," +
                            "@SubRoute," +
                            "@UnloadDate," +
                            "@ShipDate," +
                            "@ShipTime," +
                            "@OrderNumber," +
                            "@BoxNumber," +
                            "@BoxTotal," +
                            "@NAMCDestination," +
                            "@InternalKanbanKey," +
                            "@LabelLocationIndicator," +
                            "@NAMCData1," +
                            "@NAMCData2," +
                            "@PalletizationCode," +
                            "@NAMCData3," +
                            "@NAMCData4," +
                            "@Filler," +
                            "@DeviceName," +
                            "@DeviceIdentifier," +
                            "@Scanned," +
                            "@ScannedBy," +
                        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
                        //    "@Created," +
                            "@CreatedBy," +
                            "@Decoded," +
                            "@ImagePath)";
                    using (SqlCommand cmd = new SqlCommand(insertCommand, connection))
                    {
                        cmd.CommandTimeout = 2;
                        cmd.Parameters.AddWithValue("@Barcode", toyotaOWK.Barcode);
                        cmd.Parameters.AddWithValue("@SupplierInformation", toyotaOWK.SupplierInformation);
                        cmd.Parameters.AddWithValue("@SupplierCode", toyotaOWK.SupplierCode);
                        cmd.Parameters.AddWithValue("@DockCode", toyotaOWK.DockCode);
                        cmd.Parameters.AddWithValue("@KanbanNumber", toyotaOWK.KanbanNumber);
                        cmd.Parameters.AddWithValue("@PartNumber", toyotaOWK.PartNumber);
                        cmd.Parameters.AddWithValue("@LineSideAddress", toyotaOWK.LineSideAddress);
                        cmd.Parameters.AddWithValue("@StoreAddress", toyotaOWK.StoreAddress);
                        cmd.Parameters.AddWithValue("@LotSize", toyotaOWK.LotSize);
                        cmd.Parameters.AddWithValue("@SupplierName", toyotaOWK.SupplierName);
                        cmd.Parameters.AddWithValue("@MainRoute", toyotaOWK.MainRoute);
                        cmd.Parameters.AddWithValue("@SubRoute", toyotaOWK.SubRoute);
                        cmd.Parameters.AddWithValue("@UnloadDate", toyotaOWK.UnloadDate);
                        cmd.Parameters.AddWithValue("@ShipDate", toyotaOWK.ShipDate);
                        cmd.Parameters.AddWithValue("@ShipTime", toyotaOWK.ShipTime);
                        cmd.Parameters.AddWithValue("@OrderNumber", toyotaOWK.OrderNumber);
                        cmd.Parameters.AddWithValue("@BoxNumber", toyotaOWK.BoxNumber);
                        cmd.Parameters.AddWithValue("@BoxTotal", toyotaOWK.BoxTotal);
                        cmd.Parameters.AddWithValue("@NAMCDestination", toyotaOWK.NAMCDestination);
                        cmd.Parameters.AddWithValue("@InternalKanbanKey", toyotaOWK.InternalKanbanKey);
                        cmd.Parameters.AddWithValue("@LabelLocationIndicator", toyotaOWK.LabelLocationIndicator);
                        cmd.Parameters.AddWithValue("@NAMCData1", toyotaOWK.NAMCData1);
                        cmd.Parameters.AddWithValue("@NAMCData2", toyotaOWK.NAMCData2);
                        cmd.Parameters.AddWithValue("@PalletizationCode", toyotaOWK.PalletizationCode);
                        cmd.Parameters.AddWithValue("@NAMCData3", toyotaOWK.NAMCData3);
                        cmd.Parameters.AddWithValue("@NAMCData4", toyotaOWK.NAMCData4);
                        cmd.Parameters.AddWithValue("@Filler", toyotaOWK.Filler);
                        cmd.Parameters.AddWithValue("@DeviceName", toyotaOWK.DeviceName);
                        cmd.Parameters.AddWithValue("@DeviceIdentifier", toyotaOWK.DeviceIdentifier);
                        cmd.Parameters.AddWithValue("@Scanned", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ScannedBy", toyotaOWK.ScannedBy);
                        //CURRENT DATE/TIME, populated by database as default value, so not really necessary!
                        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
                        //cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", toyotaOWK.CreatedBy);
                        cmd.Parameters.AddWithValue("@Decoded", toyotaOWK.Decoded);
                        cmd.Parameters.AddWithValue("@ImagePath", toyotaOWK.ImagePath);
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }

                        int insertedID = (int)cmd.ExecuteScalar();

                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        return (insertedID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (-1);
        }

        public int CreateScanInternalKanbanEntry(InternalKanbanSQL internalKanban)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.SQLDBConnectionString))
                {
                    string insertCommand =
                        "INSERT INTO [ScanInternalKanban](" +
                            "[InternalPartNumber]" +
                            ",[KanbanNumber]" +
                            ",[DeviceName]" +
                            ",[DeviceIdentifier]" +
                            ",[Scanned]" +
                            ",[ScannedBy]" +
                        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
                        //    ",[Created]" +
                            ",[CreatedBy])" +
                            " output INSERTED.ID " +
                        "VALUES(" +
                            "@InternalPartNumber," +
                            "@KanbanNumber," +
                            "@DeviceName," +
                            "@DeviceIdentifier," +
                            "@Scanned," +
                            "@ScannedBy," +
                        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
                        //    "@Created," +
                            "@CreatedBy)";
                    using (SqlCommand cmd = new SqlCommand(insertCommand, connection))
                    {
                        cmd.CommandTimeout = 2;
                        cmd.Parameters.AddWithValue("@InternalPartNumber", internalKanban.InternalPartNumber);
                        cmd.Parameters.AddWithValue("@KanbanNumber", internalKanban.KanbanNumber);
                        cmd.Parameters.AddWithValue("@DeviceName", internalKanban.DeviceName);
                        cmd.Parameters.AddWithValue("@DeviceIdentifier", internalKanban.DeviceIdentifier);
                        cmd.Parameters.AddWithValue("@Scanned", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ScannedBy", internalKanban.ScannedBy);
                        //CURRENT DATE/TIME, populated by database as default value, so not really necessary!
                        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
                        //cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", internalKanban.CreatedBy);
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }

                        int insertedID = (int)cmd.ExecuteScalar();

                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        return (insertedID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (-1);
        }

        public int CreateScanUnKnownEntry(string barcode, string expectedScanType, string scanType, string deviceName, string deviceIdentifier, string scannedBy, string createdBy)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.SQLDBConnectionString))
                {
                    string insertCommand =
                        "INSERT INTO [ScanUnKnown](" +
                            "[Barcode]" +
                            ",[ExpectedScanType]" +
                            ",[ScanType]" +
                            ",[DeviceName]" +
                            ",[DeviceIdentifier]" +
                            ",[Scanned]" +
                            ",[ScannedBy]" +
                        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
                        //    ",[Created]" +
                            ",[CreatedBy])" +
                            " output INSERTED.ID " +
                        "VALUES(" +
                            "@Barcode," +
                            "@ExpectedScanType," +
                            "@ScanType," +
                            "@DeviceName," +
                            "@DeviceIdentifier," +
                            "@Scanned," +
                            "@ScannedBy," +
                        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
                        //    "@Created," +
                            "@CreatedBy)";
                    using (SqlCommand cmd = new SqlCommand(insertCommand, connection))
                    {
                        cmd.CommandTimeout = 2;
                        cmd.Parameters.AddWithValue("@Barcode", barcode);
                        cmd.Parameters.AddWithValue("@ExpectedScanType", expectedScanType);
                        cmd.Parameters.AddWithValue("@ScanType", scanType);
                        cmd.Parameters.AddWithValue("@DeviceName", deviceName);
                        cmd.Parameters.AddWithValue("@DeviceIdentifier", deviceIdentifier);
                        cmd.Parameters.AddWithValue("@Scanned", DateTime.Now);
                        cmd.Parameters.AddWithValue("@ScannedBy", scannedBy);
                        //CURRENT DATE/TIME, populated by database as default value, so not really necessary!
                        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
                        //cmd.Parameters.AddWithValue("@Created", DateTime.Now);
                        cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }

                        int insertedID = (int)cmd.ExecuteScalar();

                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                        return (insertedID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (-1);
        }

        public bool ValidateCredentials(string UserName, string Password)
        {

            try
            {
                DataSet ds = new DataSet();

                using (SqlConnection connection = new SqlConnection(this.SQLDBConnectionString))
                {
                    string selectCommand = "SELECT * FROM [User] WHERE UserName=@UserName AND Password=@Password";
                    //SqlCommand cmd = new SqlCommand("Select * from tbl_Login where UserName=@username and Password=@password", con);
                    using (SqlCommand cmd = new SqlCommand(selectCommand, connection))
                    {
                        cmd.CommandTimeout = 2;
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        if (connection.State != ConnectionState.Open)
                        {
                            connection.Open();
                        }
                        SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                        //DataSet ds = new DataSet();
                        adapt.Fill(ds);
                        if (connection.State == System.Data.ConnectionState.Open)
                        {
                            connection.Close();
                        }
                    }
                }
                int count = ds.Tables[0].Rows.Count;
                //If count is equal to 1, than show frmMain form
                if (count == 1)
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return (false);
        }
    }

    public class ManifestSQL
    {
        public string Barcode { get; set; }
        public string OrderNumber { get; set; }
        public string NAMC { get; set; }
        public string SupplierCode { get; set; }
        public string DockCode { get; set; }
        public string MainRoute { get; set; }
        public string SupplierShipDock { get; set; }
        public string PalletizationCode { get; set; }
        public string OrderSequence { get; set; }
        public string DeviceName { get; set; }
        public string DeviceIdentifier { get; set; }
        public DateTime Scanned { get; set; }
        public string ScannedBy { get; set; }
        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
        //public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public bool Decoded { get; set; }
        public string ImagePath { get; set; }  //REMOVE THIS HERE and in SQL Table.. not needed!

        public IDictionary<string, int> KanbansScanned { get; set; }

        public ManifestSQL() //The Constructor without a source, initialize the _KanbansScanned Dictionary
        {
            this.KanbansScanned = new Dictionary<string, int>();
        }

        public ManifestSQL(ManifestSQL source) //Another Constructor with source values to use.
        {
            this.KanbansScanned = new Dictionary<string, int>();
            foreach (var item in source.KanbansScanned)
            {
                //txtOutput.Text += item.Key + ": " + item.Value + "\r\n";
                this.KanbansScanned.Add(item.Key, item.Value);
            }
            this.Barcode = source.Barcode;
            this.OrderNumber = source.OrderNumber;
            this.NAMC = source.NAMC;
            this.SupplierCode = source.SupplierCode;
            this.DockCode = source.DockCode;
            this.MainRoute = source.MainRoute;
            this.SupplierShipDock = source.SupplierShipDock;
            this.PalletizationCode = source.PalletizationCode;
            this.OrderSequence = source.OrderSequence;
            this.DeviceName = source.DeviceName;
            this.DeviceIdentifier = source.DeviceIdentifier;
            this.Scanned = source.Scanned;
            this.ScannedBy = source.ScannedBy;
            //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
            //this.Created = source.Created;
            this.CreatedBy = source.CreatedBy;
            this.Decoded = source.Decoded;
            this.ImagePath = source.ImagePath;
        }

        /// <summary>
        /// Call with the kanban number and it will add that kanban to KanbansScanned with a value of 1, or if it already exists, it will increment the value by 1. This makes it very easy to count how many scans have been done for each Kanban.
        /// </summary>
        /// <param name="kanban"></param>
        public void IncrementKanban(string kanban)
        {
            if (this.KanbansScanned.ContainsKey(kanban))
            {
                int currentValue = this.KanbansScanned[kanban];
                this.KanbansScanned[kanban] = currentValue + 1;
            }
            else
            {
                this.KanbansScanned.Add(kanban, 1);
            }
        }

        public bool IsDifferent(ManifestSQL source)
        {
            //Beginning 2/27/18 The Manifests are no longer copy, each Skid will have an "A" and a "B".
            //Currently, this appears in the "OrderSqeuence" field.
            //
            //This was fixed 2/27/18 - 8:00 PM, before production on 2/28/18
            //
            //2/27/18 - if(this.Barcode != source.Barcode) return true
            if(this.OrderNumber != source.OrderNumber) return true;
            if(this.NAMC != source.NAMC) return true;
            if(this.SupplierCode != source.SupplierCode) return true;
            if(this.DockCode != source.DockCode) return true;
            if(this.MainRoute != source.MainRoute) return true;
            if(this.SupplierShipDock != source.SupplierShipDock) return true;
            if(this.PalletizationCode != source.PalletizationCode) return true;
            //2/27/18 - if(this.OrderSequence != source.OrderSequence) return true;
            if(this.DeviceName != source.DeviceName) return true;
            if(this.DeviceIdentifier != source.DeviceIdentifier) return true;
            //if(this.Scanned != source.Scanned) return true;    // This one???
            if(this.ScannedBy != source.ScannedBy) return true;
            //if(this.Created != source.Created) return true;    // This one???
            //2/27/18 - if(this.CreatedBy != source.CreatedBy) return true;
            //2/27/18 - if(this.Decoded != source.Decoded) return true;
            //2/27/18 - if(this.ImagePath != source.ImagePath) return true;
            return false;
        }
    }

    public class ToyotaOWKSQL
    {
        public string Barcode { get; set; }
        public string SupplierInformation { get; set; }
        public string SupplierCode { get; set; }
        public string DockCode { get; set; }
        public string KanbanNumber { get; set; }
        public string PartNumber { get; set; }
        public string LineSideAddress { get; set; }
        public string StoreAddress { get; set; }
        public string LotSize { get; set; }
        public string SupplierName { get; set; }
        public string MainRoute { get; set; }
        public string SubRoute { get; set; }
        public string UnloadDate { get; set; }
        public string ShipDate { get; set; }
        public string ShipTime { get; set; }
        public string OrderNumber { get; set; }
        public string BoxNumber { get; set; }
        public string BoxTotal { get; set; }
        public string NAMCDestination { get; set; }
        public string InternalKanbanKey { get; set; }
        public string LabelLocationIndicator { get; set; }
        public string NAMCData1 { get; set; }
        public string NAMCData2 { get; set; }
        public string PalletizationCode { get; set; }
        public string NAMCData3 { get; set; }
        public string NAMCData4 { get; set; }
        public string Filler { get; set; }
        public string DeviceName { get; set; }
        public string DeviceIdentifier { get; set; }
        public DateTime Scanned { get; set; }
        public string ScannedBy { get; set; }
        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
        //public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public bool Decoded { get; set; }
        public string ImagePath { get; set; }  //REMOVE THIS HERE and in SQL Table.. not needed!
    }

    public class InternalKanbanSQL
    {
        public string InternalPartNumber { get; set; }
        public string KanbanNumber { get; set; }
        public string DeviceName { get; set; }
        public string DeviceIdentifier { get; set; }
        public DateTime Scanned { get; set; }
        public string ScannedBy { get; set; }
        //Removed "Created" 1-11-18, use the default value on the SQl Server!!!
        //public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}
