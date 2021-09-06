using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PickToLightData
{
    class MethodsForEntityClasses
    {
    }

    //Note: This is extending the REAL ScanOWK Class.
    //      (Which is the correct approach, since its basically just a POCO (Plan Old Class Obect) with properties.)
    public partial class ScanOWK
    {
        public static int CreateScanEntry(string DeviceName, string DeviceIdentifier, string ScannedBy, string CreatedBy, string Barcode, string imagePath)
        {
            //int ID
            //string Barcode
            //string SupplierInformation
            //string SupplierCode
            //string DockCode
            //string KanbanNumber
            //string PartNumber
            //string LineSideAddress
            //string StoreAddress
            //string LotSize
            //string SupplierName
            //string MainRoute
            //string SubRoute
            //string UnloadDate
            //string ShipDate
            //string ShipTime
            //string OrderNumber
            //string BoxNumber
            //string BoxTotal
            //string NAMCDestination
            //string InternalKanbanKey
            //string LabelLocationIndicator
            //string NAMCData1
            //string NAMCData2
            //string PalletizationCode
            //string NAMCData3
            //string NAMCData4
            //string Filler
            //string DeviceName
            //string DeviceIdentifier
            //Nullable<System.DateTime> Scanned
            //string ScannedBy
            //System.DateTime Created
            //string CreatedBy
            var newScanOWK = new ScanOWK();
            newScanOWK.Barcode = Barcode;
            if (Barcode.StartsWith("C"))
            {
                newScanOWK.Decoded = true;
                newScanOWK.SupplierInformation = Barcode.Substring(1, 30).Trim();
                newScanOWK.SupplierCode = Barcode.Substring(31, 5).Trim();
                newScanOWK.DockCode = Barcode.Substring(36, 2).Trim();
                newScanOWK.KanbanNumber = Barcode.Substring(38, 4).Trim();
                newScanOWK.PartNumber = Barcode.Substring(42, 12).Trim();
                newScanOWK.LineSideAddress = Barcode.Substring(54, 10).Trim();
                newScanOWK.StoreAddress = Barcode.Substring(64, 10).Trim();
                newScanOWK.LotSize = Barcode.Substring(74, 5).Trim();
                newScanOWK.SupplierName = Barcode.Substring(79, 20).Trim();
                newScanOWK.MainRoute = Barcode.Substring(99, 9).Trim();
                newScanOWK.SubRoute = Barcode.Substring(108, 9).Trim();
                newScanOWK.UnloadDate = Barcode.Substring(117,8).Trim();
                newScanOWK.ShipDate = Barcode.Substring(125, 8).Trim();
                newScanOWK.ShipTime = Barcode.Substring(133, 6).Trim();
                newScanOWK.OrderNumber = Barcode.Substring(139, 12).Trim();
                newScanOWK.BoxNumber = Barcode.Substring(151, 4).Trim();
                newScanOWK.BoxTotal = Barcode.Substring(155, 4).Trim();
                newScanOWK.NAMCDestination = Barcode.Substring(159, 5).Trim();
                newScanOWK.InternalKanbanKey = Barcode.Substring(164, 17).Trim();
                newScanOWK.LabelLocationIndicator = Barcode.Substring(181, 2).Trim();
                newScanOWK.NAMCData1 = Barcode.Substring(183, 10).Trim();
                newScanOWK.NAMCData2 = Barcode.Substring(193, 2).Trim();
                newScanOWK.PalletizationCode = Barcode.Substring(195, 2).Trim();
                newScanOWK.NAMCData3 = Barcode.Substring(197, 10).Trim();
                newScanOWK.NAMCData4 = Barcode.Substring(207, 2).Trim();
                newScanOWK.Filler = Barcode.Substring(209, 41).Trim();
            }
            else
            {
                newScanOWK.Decoded = false;
            }
            newScanOWK.DeviceName = DeviceName;
            newScanOWK.DeviceIdentifier = DeviceIdentifier;
            newScanOWK.Scanned = DateTime.Now;
            newScanOWK.ScannedBy = ScannedBy;
            //newScanOWK.Created = SET BY DB!  "StoreGeneratedPattern = Computed" in EF Designer for the Column!!!
            newScanOWK.CreatedBy = CreatedBy;
            newScanOWK.ImagePath = imagePath;
            
            using (PickToLightEntities context = new PickToLightEntities())
            {
                context.ScanOWKs.Add(newScanOWK);
                context.SaveChanges();
                return (newScanOWK.ID); //return the newly created ID!
            }
        }

        public static ScanOWK GetScanEntry(int idScanOWK)
        {
            ScanOWK scanOWK;
            using (var ctx = new PickToLightEntities())
            {
                scanOWK = (from s in ctx.ScanOWKs
                           where s.ID == idScanOWK
                           select s).Single<ScanOWK>();
            }
            return scanOWK;
        }

        //The "Manifest" Table doesn't store the SubRoute.
        //And.. I'm removing this method from here and adding it to the class that needs it.
        //I'm not a big fan of "out" parameters and returning values in parameters...
        //In fact, I'm not super happy about how I designed this Assembly and Class either.
        //But, I don't have time to just do a complete refactor. Baby steps Shayne...baby steps.
        //Be sure and remove all these comments in a future changeset. :)
        //
        ////Move this to the Manifest Table, once we start populating it??
        ////where OrderNumber = manifestNum
        ////ORDER BY ID DESC
        //public static void PopulateManifestRoutes(string manifestNum, out string mainRoute, out string subRoute)
        //{
        //    mainRoute = "";
        //    subRoute = "";

        //    ScanOWK scanOWK;
        //    using (var ctx = new PickToLightEntities())
        //    {
        //        scanOWK = (from s in ctx.ScanOWKs
        //                   where s.OrderNumber == manifestNum
        //                   orderby s.ID descending
        //                   select s).FirstOrDefault<ScanOWK>();
        //    }
        //    if(scanOWK != null)
        //    {
        //        if(string.IsNullOrEmpty(scanOWK.OrderNumber) == false )
        //        {
        //            mainRoute = scanOWK.MainRoute;
        //            subRoute = scanOWK.SubRoute;
        //        }
        //    }
        //}
    }

    //Note: This is extending the REAL Log Class.
    //      (Which is the correct approach, since its basically just a POCO (Plan Old Class Obect) with properties.)
    public partial class Log
    {
        public static void CreateLogEntry(string Source, string ThreadName, int ThreadID, string Data)
        {
            //Create Entry in SQL Log!!!
            var newLog = new Log();
            newLog.Source = Source;
            newLog.ThreadName = ThreadName;
            newLog.ThreadID = ThreadID;
            newLog.Data = Data;
            //newLog.Created = CURRENT DATE/TIME, populated by database.   "StoreGeneratedPattern = Computed" in EF Designer for the Column!!!
            
            using (PickToLightEntities context = new PickToLightEntities())
            {
                context.Logs.Add(newLog);
                context.SaveChanges();
            }
        }
    }

    //Note: This is extending the REAL ScanManifest Class.
    //      (Which is the correct approach, since its basically just a POCO (Plan Old Class Obect) with properties.)
    public partial class ScanManifest
    {
        public static ScanManifest GetScanEntry(int idScanManifest)
        {
            ScanManifest scanManifest;
            using (var ctx = new PickToLightEntities())
            {
                scanManifest = (from s in ctx.ScanManifests
                           where s.ID == idScanManifest
                           select s).Single<ScanManifest>();
            }
            return scanManifest;
        }
    }

    //Note: This is extending the REAL SkidBuildOrder Class.
    //      (Which is the correct approach, since its basically just a POCO (Plan Old Class Obect) with properties.)
    public partial class SkidBuildOrder
    {
        ////public partial class SkidBuildOrder
        //{
        //
        //    public int ID { get; set; }
        //    public string OrderNumber { get; set; }
        //    public string SupplierCode { get; set; }
        //    public string Plant { get; set; }
        //    public string DockCode { get; set; }
        //    public string ConfirmationNumber { get; set; }
        //    public System.DateTime Created { get; set; }
        //    public string CreatedBy { get; set; }
        //    public System.DateTime Modified { get; set; }
        //    public string ModifiedBy { get; set; }
        //
        //    public virtual ICollection<SkidBuildOrderException> SkidBuildOrderExceptions { get; set; }
        //    public virtual ICollection<SkidBuildOrderResponse> SkidBuildOrderResponses { get; set; }
        //    public virtual ICollection<SkidBuildSkid> SkidBuildSkids { get; set; }
        //}

        public static void UpdateConfirmationNumber(int SkidBuildOrderID, string confirmationNumber)
        {
            using (var ctx = new PickToLightEntities())
            {
                var order = (from o in ctx.SkidBuildOrders
                             where o.ID == SkidBuildOrderID
                             select o).Single<SkidBuildOrder>();

                if (string.IsNullOrEmpty(confirmationNumber))
                {
                    order.ConfirmationNumber = null;
                }
                else
                {
                    order.ConfirmationNumber = confirmationNumber;
                }
                ctx.SaveChanges();
            }
        }

        public static SkidBuildOrder GetSkidBuildOrder(int idSkidBuildOrder)
        {
            SkidBuildOrder skidBuildOrder;
            using (var ctx = new PickToLightEntities())
            {
                skidBuildOrder = (from s in ctx.SkidBuildOrders
                                  where s.ID == idSkidBuildOrder
                                  select s).Include(e => e.SkidBuildOrderExceptions).Include(s => s.SkidBuildSkids.Select(k => k.SkidBuildKanbans)).Single<SkidBuildOrder>();
            }
            return skidBuildOrder;
        }

        public static SkidBuildOrder GetSkidBuildOrder(string plant, string supplierCode, string orderNumber, string dockCode)
        {
            SkidBuildOrder skidBuildOrder;
            using (var ctx = new PickToLightEntities())
            {
                skidBuildOrder = (from s in ctx.SkidBuildOrders
                                  //where s.ID == idSkidBuildOrder
                                  where s.Plant.Equals(plant)
                                    && s.SupplierCode.Equals(supplierCode)
                                    && s.OrderNumber.Equals(orderNumber)
                                    && s.DockCode.Equals(dockCode)
                                  select s).SingleOrDefault<SkidBuildOrder>();
            }
            return skidBuildOrder;
        }
    }

    //Note: This is extending the REAL ToyotaShipments Class.
    //      (Which is the correct approach, since its basically just a POCO (Plan Old Class Obect) with properties.)
    public partial class ToyotaShipment
    {
        //public partial class ToyotaShipment
        //{
        //    public int ID { get; set; }
        //    public string NAMC { get; set; }
        //    public string SUBROUTE { get; set; }
        //    public string SHIPDATE { get; set; }
        //    public decimal SHIPTIME { get; set; }
        //    public string SNAPARTNUM { get; set; }
        //    public string CUSTPARTNUM { get; set; }
        //    public string ORDERNUM { get; set; }
        //    public string DOCKCODE { get; set; }
        //    public decimal ORDERQTY { get; set; }
        //    public decimal PANQTY { get; set; }
        //    public decimal PANSREQ { get; set; }
        //    public bool Revised { get; set; }
        //    public string Comments { get; set; }
        //    public bool Completed { get; set; }
        //    public System.DateTime Created { get; set; }
        //    public System.DateTime Modified { get; set; }
        //}

        public static void UpdateToyotaShipmentsCompleted(List<ToyotaShipment> toyotaOrderShipments, string confirmationNumber)
        {
            if (toyotaOrderShipments == null)
            {
                return;
            }
            using (var ctx = new PickToLightEntities())
            {
                foreach (var orderShipment in toyotaOrderShipments)
                {
                    var shipment = (from s in ctx.ToyotaShipments
                                    where s.ID == orderShipment.ID
                                    select s).Single<ToyotaShipment>();


                    //If it was already makred as Completed, should we add to the Comments?? Nah, maybe later...
                    //Right now, I believe the Comments are only used for the User to enter "comments".
                    //Could also be used if the spToyotaShipment Stored Procedure wanted to update the Row, but it has already been "Confirmed".
                    //(Maybe in that instance, we update the Comments, set "Revised = True" and Clear the ConfirmatoinNumber???
                    //if(shipment.Completed)
                    //{
                    //    if(shipment.Comments.Contains("Multiple ConfirmationNumbers"))
                    //    {

                    //    }
                    //    else
                    //    {

                    //    }
                    //}

                    //What about the "Revised" column? If there was already a ConfirmationNumber, do we modify the Revised column?
                    //Nah... lets only use that Column when the spToyotaShipment Stored Procedure "updates" the Row (and it hasn't already been Completed)!
                    //if(shipment.Completed)
                    //{
                    //    shipment.Revised = true;
                    //}
                    if (string.IsNullOrEmpty(confirmationNumber))
                    {
                        shipment.Completed = false;
                    }
                    else
                    {
                        shipment.Completed = true;
                    }
                    shipment.Modified = DateTime.Now; //I think we wamt to update this Column???
                }
                ctx.SaveChanges();
            }
        }
    }

    //Note: This is extending the REAL ShipmentLoadTrailer Class.
    //      (Which is the correct approach, since its basically just a POCO (Plan Old Class Obect) with properties.)
    public partial class ShipmentLoadTrailer
    {
        //public partial class ShipmentLoadTrailer
        //{
        //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //    public ShipmentLoadTrailer()
        //    {
        //        this.ShipmentLoadOrders = new HashSet<ShipmentLoadOrder>();
        //        this.ShipmentLoadTrailerExceptions = new HashSet<ShipmentLoadTrailerException>();
        //        this.ShipmentLoadTrailerResponses = new HashSet<ShipmentLoadTrailerResponse>();
        //    }

        //    public int ID { get; set; }
        //    public string SupplierCode { get; set; }
        //    public string Route { get; set; }
        //    public string Run { get; set; }
        //    public string TrailerNumber { get; set; }
        //    public Nullable<bool> DropHook { get; set; }
        //    public string SealNumber { get; set; }
        //    public string SupplierTeamFirstName { get; set; }
        //    public string SupplierTeamLastName { get; set; }
        //    public string LPCode { get; set; }
        //    public string DriverTeamFirstName { get; set; }
        //    public string DriverTeamLastName { get; set; }
        //    public string ConfirmationNumber { get; set; }
        //    public System.DateTime Created { get; set; }
        //    public string CreatedBy { get; set; }
        //    public System.DateTime Modified { get; set; }
        //    public string ModifiedBy { get; set; }

        //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //    public virtual ICollection<ShipmentLoadOrder> ShipmentLoadOrders { get; set; }
        //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //    public virtual ICollection<ShipmentLoadTrailerException> ShipmentLoadTrailerExceptions { get; set; }
        //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //    public virtual ICollection<ShipmentLoadTrailerResponse> ShipmentLoadTrailerResponses { get; set; }
        //}

        //public partial class ShipmentLoadOrder
        //{
        //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //    public ShipmentLoadOrder()
        //    {
        //        this.ShipmentLoadOrderExceptions = new HashSet<ShipmentLoadOrderException>();
        //        this.ShipmentLoadSkids = new HashSet<ShipmentLoadSkid>();
        //    }
        //
        //    public int ID { get; set; }
        //    public int ShipmentLoadTrailerID { get; set; }
        //    public string OrderNumber { get; set; }
        //    public string SupplierCode { get; set; }
        //    public string Plant { get; set; }
        //    public string DockCode { get; set; }
        //    public Nullable<System.DateTime> PickUp { get; set; }
        //    public System.DateTime Created { get; set; }
        //    public string CreatedBy { get; set; }
        //    public System.DateTime Modified { get; set; }
        //    public string ModifiedBy { get; set; }
        //
        //    public virtual ShipmentLoadTrailer ShipmentLoadTrailer { get; set; }
        //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //    public virtual ICollection<ShipmentLoadOrderException> ShipmentLoadOrderExceptions { get; set; }
        //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //    public virtual ICollection<ShipmentLoadSkid> ShipmentLoadSkids { get; set; }
        //}

        //public partial class ShipmentLoadSkid
        //{
        //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //    public ShipmentLoadSkid()
        //    {
        //        this.ShipmentLoadSkidExceptions = new HashSet<ShipmentLoadSkidException>();
        //    }
        //
        //    public int ID { get; set; }
        //    public int ShipmentLoadOrderID { get; set; }
        //    public string SkidId { get; set; }
        //    public string PalletizationCode { get; set; }
        //    public System.DateTime Created { get; set; }
        //    public string CreatedBy { get; set; }
        //    public System.DateTime Modified { get; set; }
        //    public string ModifiedBy { get; set; }
        //
        //    public virtual ShipmentLoadOrder ShipmentLoadOrder { get; set; }
        //    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //    public virtual ICollection<ShipmentLoadSkidException> ShipmentLoadSkidExceptions { get; set; }
        //}

        public static ShipmentLoadTrailer GetShipmentLoadTrailer(int idShipmentLoadTrailer)
        {
            ShipmentLoadTrailer shipmentLoadTrailer;
            using (var ctx = new PickToLightEntities())
            {
                shipmentLoadTrailer = (from s in ctx.ShipmentLoadTrailers
                                       where s.ID == idShipmentLoadTrailer
                                       select s).Include(o => o.ShipmentLoadOrders.Select(e => e.ShipmentLoadOrderExceptions)) //TODO... I think this can be removed. Ask Garry!!! ALSO, Remove from EF diagram and Model!
                                       .Include(o => o.ShipmentLoadOrders.Select(sk => sk.ShipmentLoadSkids.Select(ske => ske.ShipmentLoadSkidExceptions)))
                                       .Include(e => e.ShipmentLoadTrailerExceptions)
                                       .Include(r => r.ShipmentLoadTrailerResponses.Select(a => a.APIResponse).Select(am => am.APIResponseMessages))
                                       //.Include(r => r.ShipmentLoadTrailerResponses.Select(a => a.APIResponse.Select(am => am.APIResponseMessages)))
                                       .Single<ShipmentLoadTrailer>();

                //ShipmentLoadTrailer
                //this.ShipmentLoadOrders
                //this.ShipmentLoadOrders.ShipmentLoadOrderExceptions //TODO... I think this can be removed. Ask Garry!!! ALSO, Remove from EF diagram and Model!
                //this.ShipmentLoadOrders.ShipmentLoadSkids 
                //this.ShipmentLoadOrders.ShipmentLoadSkids.ShipmentLoadSkidExceptions
                //this.ShipmentLoadTrailerExceptions
                //this.ShipmentLoadTrailerResponses
                //this.ShipmentLoadTrailerResponses.APIResponse
                //this.ShipmentLoadTrailerResponses.APIResponse.APIResponseMessages
            }
            return shipmentLoadTrailer;
        }

        //public static ShipmentLoadTrailer GetShipmentLoadTrailer(string supplierCode, string subRoute, string shipDate, string shipTime)
        //(Ignoring  SubRoute for now, because it isn't sent from the Canadian NAMCs anyway.)
        public static ShipmentLoadTrailer GetShipmentLoadTrailer(string supplierCode, string shipDate, string shipTime)
        {
            ShipmentLoadTrailer shipmentLoadTrailer = null;

            using (var ctx = new PickToLightEntities())
            {
                #region LinqPad Test
                /////////////////////////////////////
                //Below was tested in LinqPad!!!  ///
                //Below was tested in LinqPad!!!  ///
                //Below was tested in LinqPad!!!  ///
                //Below was tested in LinqPad!!!  ///
                /////////////////////////////////////
                //
                ////int idSkidBuildOrder;
                ////idSkidBuildOrder = 21;
                //string supplierCode = "22602";
                //string subRoute = "SPCL02";
                ////string subRoute = "";
                //string shipDate = "2018-12-10";
                //string shipTime = "702"; //"702"  (or "697" - "707")
                //string route = "";
                //string run = "";
                //if (String.IsNullOrEmpty(subRoute) == false)
                //{

                //    route = subRoute.Remove(subRoute.Length - 2);
                //    run = subRoute.Substring(subRoute.Length - 2);
                //}
                //DateTime shipDateTime = DateTime.ParseExact(shipDate + " " + shipTime.PadLeft(4, '0'), "yyyy-MM-dd HHmm", System.Globalization.CultureInfo.InvariantCulture);
                //DateTime shipDateTime_5minAgo = shipDateTime.AddMinutes(-5);
                //DateTime shipDateTime_5minFromNow = shipDateTime.AddMinutes(5);
                //ShipmentLoadTrailer shipmentLoadTrailer;
                //IQueryable<ShipmentLoadTrailer> shipmentLoadTrailersQuery;
                //List<ShipmentLoadTrailer> shipmentLoadTrailers;
                //using (var ctx = new PickToLightEntities())
                //{
                //    //shipmentLoadTrailersQuery = (from s in ctx.ShipmentLoadTrailers
                //    //					where s.SupplierCode.Equals(supplierCode)
                //    //					&& (String.IsNullOrEmpty(subRoute) || (s.Route.Equals(route) && s.Run.Equals(run)))
                //    //					select s).Include(o => o.ShipmentLoadOrders.Select(e => e.ShipmentLoadOrderExceptions)) 
                //    //                   .Include(o => o.ShipmentLoadOrders.Select(sk => sk.ShipmentLoadSkids.Select(ske => ske.ShipmentLoadSkidExceptions)))
                //    //                   .Include(e => e.ShipmentLoadTrailerExceptions)
                //    //                   .Include(r => r.ShipmentLoadTrailerResponses.Select(a => a.APIResponse).Select(am => am.APIResponseMessages));

                //    shipmentLoadTrailersQuery = (from s in ctx.ShipmentLoadTrailers
                //                                 where s.SupplierCode.Equals(supplierCode)
                //                                     && (String.IsNullOrEmpty(subRoute) || (s.Route.Equals(route) && s.Run.Equals(run)))
                //                                 select s).Where(p => p.ShipmentLoadOrders.Any(c => c.PickUp.Value >= shipDateTime_5minAgo && c.PickUp.Value <= shipDateTime_5minFromNow));
                //    shipmentLoadTrailers = shipmentLoadTrailersQuery.ToList();
                //}
                //shipmentLoadTrailers.Dump();
                //
                /////////////////////////////////////
                #endregion LinqPad Test

                DateTime shipDateTime = DateTime.ParseExact(shipDate + " " + shipTime.PadLeft(4, '0'), "yyyy-MM-dd HHmm", System.Globalization.CultureInfo.InvariantCulture);
                DateTime shipDateTime_5minAgo = shipDateTime.AddMinutes(-5);
                DateTime shipDateTime_5minFromNow = shipDateTime.AddMinutes(5);

                List<ShipmentLoadTrailer> shipmentLoadTrailers;

                //if (String.IsNullOrEmpty(subRoute))
                //{
                //    shipmentLoadTrailers = (from s in ctx.ShipmentLoadTrailers
                //                            where s.SupplierCode.Equals(supplierCode)
                //                            select s).Where(p => p.ShipmentLoadOrders.Any(c => c.PickUp.Value >= shipDateTime_5minAgo && c.PickUp.Value <= shipDateTime_5minFromNow))
                //                            .ToList();
                //                            //.Include(o => o.ShipmentLoadOrders.Select(e => e.ShipmentLoadOrderExceptions))
                //                            //.Include(o => o.ShipmentLoadOrders.Select(sk => sk.ShipmentLoadSkids.Select(ske => ske.ShipmentLoadSkidExceptions)))
                //                            //.Include(e => e.ShipmentLoadTrailerExceptions)
                //                            //.Include(r => r.ShipmentLoadTrailerResponses.Select(a => a.APIResponse).Select(am => am.APIResponseMessages));
                //}
                //else
                string route = "";
                string run = "";
                //(Ignoring  SubRoute for now, because it isn't sent from the Canadian NAMCs anyway.)
                //if (String.IsNullOrEmpty(subRoute) == false)
                //{
                    
                //    route = subRoute.Remove(subRoute.Length - 2);
                //    run = subRoute.Substring(subRoute.Length - 2);
                //}
                shipmentLoadTrailers = (from s in ctx.ShipmentLoadTrailers
                                        where s.SupplierCode.Equals(supplierCode)
                                            //(Ignoring  SubRoute for now, because it isn't sent from the Canadian NAMCs anyway.)
                                            //&& (String.IsNullOrEmpty(subRoute) || (s.Route.Equals(route) && s.Run.Equals(run)))
                                        select s).Where(p => p.ShipmentLoadOrders.Any(c => c.PickUp.Value >= shipDateTime_5minAgo && c.PickUp.Value <= shipDateTime_5minFromNow))
                                        .ToList();

                if (shipmentLoadTrailers.Count == 0)
                {
                    return null;
                }
                if (shipmentLoadTrailers.Count > 1)
                {
                    //throw new ShipmentLoadCustomProgramException("ERROR with database! Please inform IT ASAP!!! The data must be corrected, there is more than one ShipmentLoadTrailer for SupplierCode [" + supplierCode + "] SubRoute [ " + subRoute + "] ShipDate [" + shipDate + "] ShipTime [" + shipTime + "].");
                    //(Ignoring  SubRoute for now, because it isn't sent from the Canadian NAMCs anyway.)
                    throw new ShipmentLoadCustomProgramException("ERROR with database! Please inform IT ASAP!!! The data must be corrected, there is more than one ShipmentLoadTrailer for SupplierCode [" + supplierCode + "] ShipDate [" + shipDate + "] ShipTime [" + shipTime + "].");
                }
                if (shipmentLoadTrailers.Count == 1)
                {
                    return shipmentLoadTrailers.First();
                }

                //ShipmentLoadTrailer
                //this.ShipmentLoadOrders
                //this.ShipmentLoadOrders.ShipmentLoadOrderExceptions //TODO... I think this can be removed. Ask Garry!!! ALSO, Remove from EF diagram and Model!
                //this.ShipmentLoadOrders.ShipmentLoadSkids 
                //this.ShipmentLoadOrders.ShipmentLoadSkids.ShipmentLoadSkidExceptions
                //this.ShipmentLoadTrailerExceptions
                //this.ShipmentLoadTrailerResponses
                //this.ShipmentLoadTrailerResponses.APIResponse
                //this.ShipmentLoadTrailerResponses.APIResponse.APIResponseMessages
            }
            return shipmentLoadTrailer;
        }

        //public static RequestShipmentLoad GetShipmentLoadTrailerPlan(string supplierCode, string subRoute, string shipDate, string shipTime)
        //(Ignoring  SubRoute for now, because it isn't sent from the Canadian NAMCs anyway.)
        public static RequestShipmentLoad GetShipmentLoadTrailerPlan(string supplierCode, string shipDate, string shipTime)
        {
            RequestShipmentLoad shipmentLoadTrailerPlan = null;

            using (var ctx = new PickToLightEntities())
            {
                #region LinqPad Test
                /////////////////////////////////////
                //Below was tested in LinqPad!!!  ///
                //Below was tested in LinqPad!!!  ///
                //Below was tested in LinqPad!!!  ///
                //Below was tested in LinqPad!!!  ///
                /////////////////////////////////////
                ////Console.WriteLine("742 =" + "742");
                ////Console.WriteLine("742 =" + "742".PadLeft(4,'0'));
                ////Console.WriteLine("742 =" + "742".PadLeft(3,'0'));
                ////Console.WriteLine("742 =" + "742".PadLeft(5,'0'));

                ////string supplierCode = "31323";
                ////string subRoute = "SYTL01";
                ////string subRoute = "";
                ////string shipDate = "2018-11-26";
                ////string shipTime = "742"; //"742" (or "737" - "747")

                //string supplierCode = "22602";
                //string subRoute = "SPCL02";
                ////string subRoute = "";
                //string shipDate = "2018-12-10";
                //string shipTime = "702"; //"702"  (or "697" - "707")

                ////DateTime shipDateTime = DateTime.ParseExact(shipDate + " " + shipTime.PadLeft(4, '0'), "yyyy-MM-dd HHmm", System.Globalization.CultureInfo.InvariantCulture);
                ////Console.WriteLine("shipDateTime = " + shipDateTime.ToString());
                //Console.WriteLine("shipDate = " + shipDate.ToString());

                ////DateTime shipDateTime_5minAgo = shipDateTime.AddMinutes(-5);
                ////DateTime shipDateTime_5minFromNow = shipDateTime.AddMinutes(5);
                //int shipTime_5minAgo = int.Parse(shipTime) - 5;
                //int shipTime_5minFromNow = int.Parse(shipTime) + 5;

                ////Console.WriteLine("shipDateTime_5minAgo = " + shipDateTime_5minAgo.ToString());
                ////Console.WriteLine("shipDateTime_5minFromNow = " + shipDateTime_5minFromNow.ToString());
                //Console.WriteLine("shipTime_5minAgo = " + shipTime_5minAgo.ToString());
                //Console.WriteLine("shipTime_5minFromNow = " + shipTime_5minFromNow.ToString());

                ////IQueryable <ShipmentLoadTrailer> shipmentLoadTrailersQuery;
                ////List <ShipmentLoadTrailer> shipmentLoadTrailers;
                ////IQueryable <ToyotaShipment> toyotaShipmentsQuery;
                ////List <ToyotaShipment> toyotaShipments;
                //using (var ctx = new PickToLightEntities())
                //{
                //    Console.WriteLine("subRoute = " + subRoute);
                //    var query = (from ts in ctx.ToyotaShipments
                //                 from xref in ctx.NAMC_TRPT_CrossRef
                //                     .Where(xr => xr.TRPT == ts.NAMC && xr.SupplierCode == supplierCode)
                //                     .DefaultIfEmpty()
                //                 from sbo in ctx.SkidBuildOrders
                //                     .Where(o => o.OrderNumber == ts.ORDERNUM && o.DockCode == ts.DOCKCODE && o.Plant == xref.NAMCDestination && o.SupplierCode == xref.SupplierCode)
                //                     .DefaultIfEmpty()
                //                 //from sbs in ctx.SkidBuildSkids
                //                 //	.Where(s => s.SkidBuildOrderID.Equals(sbo.ID))
                //                 //	//OrderNumber == ts.ORDERNUM && o.DockCode == ts.DOCKCODE && o.Plant == xref.NAMCDestination && o.SupplierCode == xref.SupplierCode)
                //                 //	//Include(s => s.SkidBuildSkids.Select(k => k.SkidBuildKanbans))
                //                 //	.DefaultIfEmpty()
                //                 //from sbk in ctx.SkidBuildKanbans
                //                 //	.Where(k => k.SkidBuildSkidID.Equals(sbs.ID) && k.PartNumber.Equals(ts.CUSTPARTNUM))
                //                 //	//OrderNumber == ts.ORDERNUM && o.DockCode == ts.DOCKCODE && o.Plant == xref.NAMCDestination && o.SupplierCode == xref.SupplierCode)
                //                 //	//Include(s => s.SkidBuildSkids.Select(k => k.SkidBuildKanbans))
                //                 //	.DefaultIfEmpty()
                //                 where xref.SupplierCode.Equals(supplierCode)
                //                         && ts.SHIPDATE.Equals(shipDate)
                //                         && ts.SHIPTIME >= shipTime_5minAgo
                //                         && ts.SHIPTIME <= shipTime_5minFromNow
                //                         && (String.IsNullOrEmpty(subRoute) || ts.SUBROUTE.Equals(subRoute))
                //                 select new
                //                 {
                //                     ID = ts.ID,
                //                     SUPPLIERCODE = supplierCode,
                //                     TRPT = ts.NAMC,
                //                     NAMC = xref.NAMCDestination,
                //                     SUBROUTE = ts.SUBROUTE,
                //                     SHIPDATE = ts.SHIPDATE,
                //                     SHIPTIME = ts.SHIPTIME,
                //                     SNAPARTNUM = ts.SNAPARTNUM,
                //                     CUSTPARTNUM = ts.CUSTPARTNUM,
                //                     ORDERNUM = ts.ORDERNUM,
                //                     DOCKCODE = ts.DOCKCODE,
                //                     //ORDERQTY = ts.ORDERQTY, PANQTY = ts.PANQTY, PANSREQ = ts.PANSREQ, Revised = ts.Revised,
                //                     Completed = ts.Completed,
                //                     sboID = (sbo.ID == null ? 0 : sbo.ID),
                //                     sboConfirmationNumber = sbo.ConfirmationNumber,
                //                     //SkidBuildOrder = sbo,
                //                     SkidBuildSkids = sbo.SkidBuildSkids
                //                     //sboSkidId = sbo.SkidBuildSkids.Select(s => s.SkidId), 
                //                     //sboPalletizationCode = sbo.SkidBuildSkids.Select(s => s.PalletizationCode)

                //                     //SkidBuildOrder = sbo,
                //                     //SkidBuildSkids = sbo.SkidBuildSkids, //WORKS! 
                //                     //SkidBuildSkids = sbs,
                //                     //SkidBuildKanbans = sbk
                //                     //SkidBuildSkids = sbo.SkidBuildSkids.Where(sbs => sbs.PartNumber.Equals(ts.CUSTPARTNUM))
                //                     //SkidBuildKanbans = sbo.SkidBuildSkids.SkidBuildKanbans.Where(k => k.PartNumber.Equals(ts.CUSTPARTNUM))
                //                     //var partsOrdered = query.OrderBy(q => q.ORDERNUM).ThenBy(q => q.DOCKCODE).ThenBy(q => q.SNAPARTNUM) //.ToList();
                //                     //.Include(o => o.SkidBuildOrder)
                //                     //.Include(s => s.SkidBuildSkids.Select(k => k.SkidBuildKanbans))
                //                     //.ToList();

                //                 });

                //    Console.WriteLine("EDI Part Orders = " + query.ToList().Count + " (Number of Parts Ordered in this Shipment Load)");

                //    var completed = (from rows in query
                //                     where rows.Completed == true
                //                     select rows).ToList();
                //    Console.WriteLine("EDI Part Orders Completed = " + completed.Count + " (Number of Part Orders COMPLETED (have Confirmations) in this Shipment Load)");

                //    var notCompleted = (from rows in query
                //                        where rows.Completed == false
                //                        select rows).ToList();
                //    Console.WriteLine("EDI Part Orders NOT Completed = " + notCompleted.Count + " (Number of Part Orders NOT completed (MISSING a Skid Build OR Confirmation) in this Shipment Load)");

                //    var missingSkidBuildOrder = (from rows in query
                //                                 where rows.sboID == 0 //Is set to 0 if null, in query above!
                //                                 select rows).ToList();
                //    Console.WriteLine("EDI Part Orders MISSING SkidBuildOrder = " + missingSkidBuildOrder.Count + " (Number of Part Orders MISSING a Skid Build in this Shipment Load)");

                //    var confirmationEndsP = (from rows in query
                //                             where rows.sboConfirmationNumber.EndsWith("P")
                //                             select rows).ToList();
                //    Console.WriteLine("EDI Part Orders with Confirmation ending in P = " + confirmationEndsP.Count + " (Number of Part Orders WITH a Confirmation in this Shipment Load)");

                //    //NO! Skid Builds can't have a "ShipmentLoadTrailer" Confirmation!!
                //    //var confirmationEndsL = (from rows in query
                //    //					where rows.sboConfirmationNumber.EndsWith("L")
                //    //					select rows).ToList();
                //    //Console.WriteLine("EDI Part Orders with Confirmation ending in L = " + confirmationEndsL.Count);

                //    var confirmationIsNull = (from rows in query
                //                              where rows.sboID > 0  //Check for the SkidBuildOrder first, lets not include missing SkidBuildOrders in this query!
                //                                    && rows.sboConfirmationNumber == null
                //                              select rows).ToList();
                //    Console.WriteLine("EDI Part Orders WITH SkidBuildOrder BUT Confirmation NULL = " + confirmationIsNull.Count + " (Number of Part Orders WITH SkidBuildOrder, BUT MISSING a Confirmation (because its NULL) in this Shipment Load)");

                //    var confirmationNotEndsP = (from rows in query
                //                                where rows.sboID > 0 //Check for the SkidBuildOrder first, lets not include missing SkidBuildOrders in this query!
                //                                      && !(rows.sboConfirmationNumber.EndsWith("P"))
                //                                select rows).ToList();
                //    Console.WriteLine("EDI Part Orders WITH SkidBuildOrder, BUT Confirmation NOT ending in P = " + confirmationNotEndsP.Count + " (Number of Part Orders WITH SkidBuildOrder, BUT Confirmation NOT ending in P (NOT INCLUDING NULLS) in this Shipment Load)");

                //    //NO! Skid Builds can't have a "ShipmentLoadTrailer" Confirmation!!
                //    //var confirmationNotEndsL = (from rows in query
                //    //					where !(rows.sboConfirmationNumber.EndsWith("L"))
                //    //					select rows).ToList();
                //    //Console.WriteLine("EDI Part Orders with Confirmation NOT ending in L = " + confirmationNotEndsL.Count);

                //    //NO! Skid Builds can't have a "ShipmentLoadTrailer" Confirmation!!
                //    //var confirmationIsNullOrNotEndsPorL = (from rows in query
                //    //									where rows.sboConfirmationNumber == null
                //    //										|| (!(rows.sboConfirmationNumber.EndsWith("P")) && !(rows.sboConfirmationNumber.EndsWith("L")))
                //    //									select rows).ToList();
                //    //Console.WriteLine("EDI Part Orders with Confirmation IS NULL or NOT ending in P or L = " + confirmationIsNullOrNotEndsPorL.Count);

                //    var confirmationIsNullOrNotEndsP = (from rows in query
                //                                        where rows.sboID > 0 &&  //Check for the SkidBuildOrder first, lets not include missing SkidBuildOrders in this query!
                //                                              (rows.sboConfirmationNumber == null
                //                                               || !(rows.sboConfirmationNumber.EndsWith("P")))
                //                                        select rows).ToList();
                //    Console.WriteLine("EDI Part Orders WITH SkidBuildOrder, BUT Confirmation IS NULL or NOT ending in P = " + confirmationIsNullOrNotEndsP.Count + " (Number of Part Orders (WITH SkidBuildOrder), BUT MISSING a Confirmation in this Shipment Load)");


                //    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------");
                //    Console.WriteLine("EDI Part Orders Completed = " + query.Count(q => q.Completed == true) + " (Number of Part Orders COMPLETED (have Confirmations) in this Shipment Load)");
                //    Console.WriteLine("EDI Part Orders NOT Completed = " + query.Count(q => q.Completed == false) + " (Number of Part Orders NOT completed (MISSING a Skid Build OR Confirmation) in this Shipment Load)");
                //    Console.WriteLine("EDI Part Orders MISSING SkidBuildOrder = " + query.Count(q => q.sboID == 0) + " (Number of Part Orders MISSING a Skid Build in this Shipment Load)");
                //    Console.WriteLine("EDI Part Orders with Confirmation ending in P = " + query.Count(q => q.sboConfirmationNumber.EndsWith("P")) + " (Number of Part Orders WITH a Confirmation in this Shipment Load)");
                //    Console.WriteLine("EDI Part Orders with Confirmation null = " + query.Count(q => q.sboConfirmationNumber.Equals(null)) + " (Number of Part Orders MISSING a Confirmation (because its null) in this Shipment Load)");
                //    Console.WriteLine("EDI Part Orders with Confirmation NOT ending in P = " + query.Count(q => q.sboConfirmationNumber.EndsWith("P")) + " (Number of Part Orders MISSING a Confirmation (NOT INCLUDING NULLS???) in this Shipment Load)");
                //    Console.WriteLine("EDI Part Orders with Confirmation IS NULL or NOT ending in P = " + query.Count(q => q.sboConfirmationNumber.Equals(null) || !q.sboConfirmationNumber.EndsWith("P")) + " (Number of Part Orders MISSING a Confirmation in this Shipment Load)");
                //    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------");

                //    var orders = (from rows in query
                //                  group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp
                //                  select grp).ToList();
                //    Console.WriteLine("EDI Orders = " + orders.Count + " (Number of Orders in this Shipment Load)");

                //    var ordersCompleted = (from rows in query
                //                           where rows.Completed == true
                //                           group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp
                //                           select grp).ToList();
                //    Console.WriteLine("EDI Orders Completed = " + ordersCompleted.Count + " (Number of Orders COMPLETED (have Confirmations) in this Shipment Load )");

                //    var ordersNotCompleted = (from rows in query
                //                              where rows.Completed == false
                //                              group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp
                //                              select grp).ToList();
                //    Console.WriteLine("EDI Orders NOT Completed = " + ordersNotCompleted.Count + " (Number of Orders NOT completed (MISSING a Skid Build OR Confirmation) in this Shipment Load)");

                //    var ordersMissingSkidBuildOrder = (from rows in query
                //                                       where rows.sboID == 0 //Is set to 0 if null, in query above!
                //                                       group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp
                //                                       select grp).ToList();
                //    Console.WriteLine("EDI Orders MISSING SkidBuildOrder = " + ordersMissingSkidBuildOrder.Count + " (Number of Orders MISSING a Skid Build in this Shipment Load)");

                //    var ordersConfirmationEndsP = (from rows in query
                //                                   where rows.sboConfirmationNumber.EndsWith("P")
                //                                   group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp
                //                                   select grp).ToList();
                //    Console.WriteLine("EDI Orders with Confirmation ending in P = " + ordersConfirmationEndsP.Count + " (Number of Orders WITH a Confirmation in this Shipment Load)");

                //    //NO! Skid Builds can't have a "ShipmentLoadTrailer" Confirmation!!
                //    //var ordersConfirmationEndsL = (from rows in query
                //    //							where rows.sboConfirmationNumber.EndsWith("L")
                //    //							group rows by new {rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE} into grp
                //    //							select grp).ToList();
                //    //Console.WriteLine("EDI Orders with Confirmation ending in L = " + ordersConfirmationEndsL.Count);

                //    var ordersConfirmationIsNull = (from rows in query
                //                                    where rows.sboID > 0  //Check for the SkidBuildOrder first, lets not include missing SkidBuildOrders in this query!
                //                                          && rows.sboConfirmationNumber == null
                //                                    group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp
                //                                    select grp).ToList();
                //    Console.WriteLine("EDI Orders WITH SkidBuildOrder, BUT Confirmation NULL = " + ordersConfirmationIsNull.Count + " (Number of Orders WITH SkidBuildOrder, BUT MISSING a Confirmation (because its NULL) in this Shipment Load)");

                //    var ordersConfirmationNotEndsP = (from rows in query
                //                                      where rows.sboID > 0  //Check for the SkidBuildOrder first, lets not include missing SkidBuildOrders in this query!
                //                                            && !(rows.sboConfirmationNumber.EndsWith("P"))
                //                                      group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp
                //                                      select grp).ToList();
                //    Console.WriteLine("EDI Orders WITH SkidBuildOrder, BUT Confirmation NOT ending in P = " + ordersConfirmationNotEndsP.Count + " (Number of Orders WITH SkidBuildOrder, BUT Confirmation NOT ending in P (NOT INCLUDING NULLS) in this Shipment Load)");

                //    //NO! Skid Builds can't have a "ShipmentLoadTrailer" Confirmation!!
                //    //var ordersConfirmationNotEndsL = (from rows in query
                //    //							where !(rows.sboConfirmationNumber.EndsWith("L"))
                //    //							group rows by new {rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE} into grp
                //    //							select grp).ToList();
                //    //Console.WriteLine("EDI Orders with Confirmation NOT ending in L = " + ordersConfirmationNotEndsL.Count);

                //    //NO! Skid Builds can't have a "ShipmentLoadTrailer" Confirmation!!
                //    //var ordersConfirmationIsNullOrNotEndsPorL = (from rows in query
                //    //											where rows.sboConfirmationNumber == null
                //    //												|| (!(rows.sboConfirmationNumber.EndsWith("P")) && !(rows.sboConfirmationNumber.EndsWith("L")))
                //    //											group rows by new {rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE} into grp
                //    //											select grp).ToList();
                //    //Console.WriteLine("EDI Orders with Confirmation IS NULL or NOT ending in P or L = " + ordersConfirmationIsNullOrNotEndsPorL.Count);

                //    var ordersConfirmationIsNullOrNotEndsP = (from rows in query
                //                                              where rows.sboID > 0 &&  //Check for the SkidBuildOrder first, lets not include missing SkidBuildOrders in this query!
                //                                                    (rows.sboConfirmationNumber == null
                //                                                     || !(rows.sboConfirmationNumber.EndsWith("P")))
                //                                              group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp
                //                                              select grp).ToList();
                //    Console.WriteLine("EDI Orders WITH SkidBuildOrder, BUT Confirmation IS NULL or NOT ending in P = " + ordersConfirmationIsNullOrNotEndsP.Count + " (Number of Orders (WITH SkidBuildOrder), BUT MISSING a Confirmation in this Shipment Load)");

                //    //WORKS!!!
                //    //(from rows in query
                //    //	group rows by new {rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE, rows.SkidBuildOrder} into grp
                //    //	select new {
                //    //		GroupKey = grp.Key,
                //    //		ORDERNUM = grp.Select(g => g.ORDERNUM).FirstOrDefault(),
                //    //		DOCKCODE = grp.Select(g => g.DOCKCODE).FirstOrDefault(),
                //    //		SkidBuildOrders = grp.Select(g => g.SkidBuildOrder),
                //    //		SkidBuildSkids = grp.Select(g => g.SkidBuildSkids)
                //    //		}).ToList().Dump();

                //    (from rows in query
                //     group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp
                //     select new
                //     {
                //         GroupKey = grp.Key,
                //         SupplierCode = grp.Select(g => g.SUPPLIERCODE).FirstOrDefault(),
                //         NAMC = grp.Select(g => g.NAMC).FirstOrDefault(),
                //         TRPT = grp.Select(g => g.TRPT).FirstOrDefault(),
                //         SubRoute = grp.Select(g => g.SUBROUTE).FirstOrDefault(),
                //         OrderNumber = grp.Select(g => g.ORDERNUM).FirstOrDefault(),
                //         DockCode = grp.Select(g => g.DOCKCODE).FirstOrDefault(),
                //         ////SkidBuildSkids = grp.Select(g => g.SkidBuildSkids), //Works, but dont need all (the collection) of the Skids, only need the First()
                //         //SkidBuildOrderSkidIds = grp.Select(g => g.SkidBuildSkids).FirstOrDefault(), //.Select(k => k.SkidId) //.FirstOrDefault(),
                //         ////SkidIds = grp.Select(g => g.SkidBuildSkids).FirstOrDefault().Select(k => k.SkidId), //.Select(k => k.SkidId) //.FirstOrDefault(),
                //         Skids = from skidIds in (grp.Select(g => g.SkidBuildSkids).FirstOrDefault())
                //                   select new
                //                   {
                //                       SkidId = skidIds.SkidId,
                //                       PalletizationCode = skidIds.PalletizationCode
                //                   }
                //     }).ToList().Dump();

                //    ////query.ToList().Dump();
                //    query.OrderBy(q => q.ID).ToList().Dump();
                //    ordersConfirmationIsNullOrNotEndsP.Dump();
                //    ////orders.OrderBy(o => o.ORDERNUM).ThenBy(o => o.DOCKCODE).Dump();
                //    orders.Dump();

                //    //		var partsOrdered = query.OrderBy(q => q.ORDERNUM).ThenBy(q => q.DOCKCODE).ThenBy(q => q.SNAPARTNUM).ToList();
                //    //.Include(o => o.SkidBuildOrder)
                //    //.Include(s => s.SkidBuildSkids.Select(k => k.SkidBuildKanbans))
                //    //.ToList();
                //    ////.Include(e => e.SkidBuildOrderExceptions)   //Probably need this one too!!!
                //    //foreach (var partOrdered in query.OrderBy(o => o.ORDERNUM).ThenBy(o => o.DOCKCODE).ThenBy(o => o.SNAPARTNUM).ToList())
                //    //		foreach (var partOrdered in partsOrdered)
                //    //	            {
                //    //				int box = 1;
                //    //					Console.WriteLine("SupplierCode: " + partOrdered.SUPPLIERCODE + " TRPT:" + partOrdered.TRPT + " NAMC:" + partOrdered.NAMC
                //    //							+ "SUBROUTE: " + partOrdered.SUBROUTE + " SHIPDATE:" + partOrdered.SHIPDATE + " SHIPTIME:" + partOrdered.SHIPTIME
                //    //							+ "ORDERNUM: " + partOrdered.ORDERNUM + " DOCKCODE:" + partOrdered.DOCKCODE + " sboID:" + partOrdered.sboID
                //    //							+ "sboConfirmationNumber: " + partOrdered.sboConfirmationNumber
                //    //							+ " SkidID:" + partOrdered.SkidBuildOrder.SkidBuildSkids.Select(sbs => sbs.SkidId)
                //    //							+ " PalletizationCode:" + partOrdered.SkidBuildOrder.SkidBuildSkids.Select(sbs => sbs.PalletizationCode)
                //    //+ " SkidID:" + partOrdered.SkidBuildOrder.SkidBuildSkids.SkidBuildKanbans.Where(sbk => sbk.PartNumber.Equals(partOrdered.CUSTPARTNUM) && sbk.BoxNumber.Equals(box)).SkidBuildSkid.SkidId
                //    //+ " PalletizationCode:" + partOrdered.SkidBuildOrder.SkidBuildSkids.SkidBuildKanbans.Where(sbk => sbk.PartNumber.Equals(partOrdered.CUSTPARTNUM) && sbk.BoxNumber.Equals(box)).SkidBuildSkid.SkidId.PalletizationCode
                //    //							);
                //    //				}

                //    //.Include(o => o.ShipmentLoadOrders.Select(e => e.ShipmentLoadOrderExceptions)) 
                //    //.Include(o => o.ShipmentLoadOrders.Select(sk => sk.ShipmentLoadSkids.Select(ske => ske.ShipmentLoadSkidExceptions)))
                //    //.Include(e => e.ShipmentLoadTrailerExceptions)
                //    //.Include(r => r.ShipmentLoadTrailerResponses.Select(a => a.APIResponse).Select(am => am.APIResponseMessages);
                //    //}
                //    //toyotaShipments = toyotaShipmentsQuery.ToList();
                //}
                ////toyotaShipments.Dump();
                /////////////////////////////////////
                #endregion LinqPad Test


                //Now we need to create the ShipmentLoadTrailer "Plan".
                //    First, lets Query all the ToyotaShipments for this Shipment Load and check for:
                //      Missing ShipmentLoadTrailer "Plan" for supplied data (SupplierCode, ShipDate, ShipTime, SubRoute)
                //          (Ignoring  SubRoute for now, because it isn't sent from the Canadian NAMCs anyway.)
                //      One or more "ToyotaShipments NOT Completed" ERRORS! (Completed = False)
                //      One or more "Missing SkidBuildOrder" ERRORS! (no SkidBuildOrderId)
                //      One or more "Missing SkidBuildOrder Confirmation" ERRORS!
                //          No ConfirmationNumber?
                //          --OR--
                //          Should I check for ConfirmationNumber NOT end with "P" or "L"???
                //          NO!! Because SkidBuildOrder only ends in "P". (Unless we add something like "Order Changed Need Confirmation".)


                //string supplierCode = "31323";
                //string subRoute = "SYTL01";
                //string subRoute = "";
                //string shipDate = "2018-11-26";
                //string shipTime = "742"; //"742" (or "737" - "747")
                //
                //string supplierCode = "22602";
                //string subRoute = "SPCL02";
                ////string subRoute = "";
                //string shipDate = "2018-12-10";
                //string shipTime = "702"; //"702"  (or "697" - "707")

                //DateTime shipDateTime_5minAgo = shipDateTime.AddMinutes(-5);
                //DateTime shipDateTime_5minFromNow = shipDateTime.AddMinutes(5);
                int shipTime_5minAgo = int.Parse(shipTime) - 5;
                int shipTime_5minFromNow = int.Parse(shipTime) + 5;

                //Will use this later on to populate PickUp on the ShipmentLooadOrders...
                DateTime shipDateTime = DateTime.ParseExact(shipDate + " " + shipTime.PadLeft(4, '0'), "yyyy-MM-dd HHmm", System.Globalization.CultureInfo.InvariantCulture);
                //string pickUpDateTime = shipDateTime.ToString("yyyy-MM-ddTHH:mmK");
                //Don't need "K"??
                string pickUpDateTime = shipDateTime.ToString("yyyy-MM-ddTHH:mm");
                //Console.WriteLine("shipDateTime = " + shipDateTime.ToString());
                //Console.WriteLine("pickUpDateTime = " + pickUpDateTime);

                List<ToyotaShipment> toyotaShipments;

                //
                //    First, lets Query all the ToyotaShipments for this Shipment Load and check for:
                //
                //Console.WriteLine("subRoute = " + subRoute);
                var query = (from ts in ctx.ToyotaShipments
                             from xref in ctx.NAMC_TRPT_CrossRef
                                 .Where(xr => xr.TRPT == ts.NAMC && xr.SupplierCode == supplierCode)
                                 .DefaultIfEmpty()
                             from sbo in ctx.SkidBuildOrders
                                 .Where(o => o.OrderNumber == ts.ORDERNUM && o.DockCode == ts.DOCKCODE && o.Plant == xref.NAMCDestination && o.SupplierCode == xref.SupplierCode)
                                 .DefaultIfEmpty()
                             where xref.SupplierCode.Equals(supplierCode)
                                     && ts.SHIPDATE.Equals(shipDate)
                                     && ts.SHIPTIME >= shipTime_5minAgo
                                     && ts.SHIPTIME <= shipTime_5minFromNow
                                     //(Ignoring  SubRoute for now, because it isn't sent from the Canadian NAMCs anyway.)
                                     //&& (String.IsNullOrEmpty(subRoute) || ts.SUBROUTE.Equals(subRoute))
                             select new
                             {
                                 ID = ts.ID,
                                 SUPPLIERCODE = supplierCode,
                                 TRPT = ts.NAMC,
                                 NAMC = xref.NAMCDestination,
                                 SUBROUTE = ts.SUBROUTE,
                                 SHIPDATE = ts.SHIPDATE,
                                 SHIPTIME = ts.SHIPTIME,
                                 SNAPARTNUM = ts.SNAPARTNUM,
                                 CUSTPARTNUM = ts.CUSTPARTNUM,
                                 ORDERNUM = ts.ORDERNUM,
                                 DOCKCODE = ts.DOCKCODE,
                                 //ORDERQTY = ts.ORDERQTY, PANQTY = ts.PANQTY, PANSREQ = ts.PANSREQ, Revised = ts.Revised,
                                 Completed = ts.Completed,
                                 sboID = (sbo == null ? 0 : sbo.ID),
                                 sboConfirmationNumber = sbo.ConfirmationNumber,
                                 //SkidBuildOrder = sbo,
                                 SkidBuildSkids = sbo.SkidBuildSkids
                                 //sboSkidId = sbo.SkidBuildSkids.Select(s => s.SkidId), 
                                 //sboPalletizationCode = sbo.SkidBuildSkids.Select(s => s.PalletizationCode)

                                 //SkidBuildOrder = sbo,
                                 //SkidBuildSkids = sbo.SkidBuildSkids, //WORKS! 
                                 //SkidBuildSkids = sbs,
                                 //SkidBuildKanbans = sbk
                                 //SkidBuildSkids = sbo.SkidBuildSkids.Where(sbs => sbs.PartNumber.Equals(ts.CUSTPARTNUM))
                                 //SkidBuildKanbans = sbo.SkidBuildSkids.SkidBuildKanbans.Where(k => k.PartNumber.Equals(ts.CUSTPARTNUM))
                                 //var partsOrdered = query.OrderBy(q => q.ORDERNUM).ThenBy(q => q.DOCKCODE).ThenBy(q => q.SNAPARTNUM) //.ToList();
                                 //.Include(o => o.SkidBuildOrder)
                                 //.Include(s => s.SkidBuildSkids.Select(k => k.SkidBuildKanbans))
                                 //.ToList();
                             });

                //      Missing ShipmentLoadTrailer "Plan" for supplied data (SupplierCode, ShipDate, ShipTime, SubRoute)
                //          (Ignoring  SubRoute for now, because it isn't sent from the Canadian NAMCs anyway.)
                //      One or more "ToyotaShipments NOT Completed" ERRORS! (Completed = False)
                //      One or more "Missing SkidBuildOrder" ERRORS! (no SkidBuildOrderId)
                //      One or more "Missing SkidBuildOrder Confirmation" ERRORS!
                //          No ConfirmationNumber?
                //          --OR--
                //          Should I check for ConfirmationNumber NOT end with "P" or "L"???
                //          NO!! Because SkidBuildOrder only ends in "P". (Unless we add something like "Order Changed Need Confirmation".)

                int partOrders = query.ToList().Count;

                //      Missing ShipmentLoadTrailer "Plan" for supplied data (SupplierCode, ShipDate, ShipTime, SubRoute)
                //          (Ignoring  SubRoute for now, because it isn't sent from the Canadian NAMCs anyway.)
                if (partOrders <= 0 )
                {
                    //missingSkidBuildOrderBuffer
                    //confirmationIsNullOrNotEndsPBuffer
                    throw new ShipmentLoadCustomProgramException(
                          "There aren't ANY Parts/Orders in the system to build a Shipment Load with the supplied data."
                        //+ "<br>(Supplied Data... SupplierCode: " + supplierCode + " SubRoute: " + subRoute + " ShipDate: " + shipDate + " ShipTime: " + shipTime + ")"
                        //(Ignoring  SubRoute for now, because it isn't sent from the Canadian NAMCs anyway.)
                        + "<br>(Supplied Data... SupplierCode: " + supplierCode + " ShipDate: " + shipDate + " ShipTime: " + shipTime + ")"
                        );
                }

                var partOrdersNotCompletedQuery = (from rows in query where rows.Completed == false select rows);
                int partOrdersNotCompleted = partOrdersNotCompletedQuery.ToList().Count;

                var partOrdersMissingSkidBuildOrderQuery = (from rows in query where rows.sboID == 0 select rows); //Is set to 0 if null, in query above!
                int partOrdersMissingSkidBuildOrder = partOrdersMissingSkidBuildOrderQuery.ToList().Count;

                //var partOrdersConfirmationIsNullOrNotEndsPQuery = (from rows in query where rows.sboConfirmationNumber == null || !(rows.sboConfirmationNumber.EndsWith("P")) select rows);
                //Only include shipments that HAVE a SkidBuildOrder in this query/count...
                var partOrdersConfirmationIsNullOrNotEndsPQuery = (from rows in query where rows.sboID > 0 && rows.sboConfirmationNumber == null || !(rows.sboConfirmationNumber.EndsWith("P")) select rows);
                int partOrdersConfirmationIsNullOrNotEndsP = partOrdersConfirmationIsNullOrNotEndsPQuery.ToList().Count;

                var ordersQuery = (from rows in query group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp select grp);
                int orders = ordersQuery.ToList().Count;

                var ordersNotCompletedQuery = (from rows in query where rows.Completed == false group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp select grp);
                int ordersNotCompleted = ordersNotCompletedQuery.ToList().Count;

                var ordersMissingSkidBuildOrderQuery = (from rows in query where rows.sboID == 0 group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp select grp); //Is set to 0 if null, in query above!
                int ordersMissingSkidBuildOrder = ordersMissingSkidBuildOrderQuery.ToList().Count;

                //var ordersConfirmationIsNullOrNotEndsPQuery = (from rows in query where rows.sboConfirmationNumber == null || !(rows.sboConfirmationNumber.EndsWith("P")) group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp select grp);
                //Only include shipments that HAVE a SkidBuildOrder in this query/count...
                var ordersConfirmationIsNullOrNotEndsPQuery = (from rows in query where rows.sboID > 0 && (rows.sboConfirmationNumber == null || !(rows.sboConfirmationNumber.EndsWith("P"))) group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp select grp);
                int ordersConfirmationIsNullOrNotEndsP = ordersConfirmationIsNullOrNotEndsPQuery.ToList().Count;

                //partOrders
                //partOrdersNotCompleted
                //partOrdersMissingSkidBuildOrder
                //partOrdersConfirmationIsNullOrNotEndsP
                //
                //orders
                //ordersNotCompleted
                //ordersMissingSkidBuildOrder
                //ordersConfirmationIsNullOrNotEndsP

                //If any "Missing SkidBuildOrder" ERRORS, build the message (to be used in the Exception/Error)...
                StringBuilder missingSkidBuildOrderBuffer = new StringBuilder();
                if (partOrdersMissingSkidBuildOrder > 0 || ordersMissingSkidBuildOrder > 0)
                {
                    //var partOrdersMissingSkidBuildOrderQuery = (from rows in query where rows.sboID == 0 select rows); //Is set to 0 if null, in query above!
                    //foreach (var part in partOrdersMissingSkidBuildOrderQuery.ToList())
                    //Wait...Sort by Order, DockCode, SNA Part #
                    foreach (var part in partOrdersMissingSkidBuildOrderQuery.OrderBy(o => o.ORDERNUM).ThenBy(o => o.DOCKCODE).ThenBy(o => o.SNAPARTNUM).ToList())
                    {
                        missingSkidBuildOrderBuffer.Append("<br>-----Order #:" + part.ORDERNUM + "  Dock #:" + part.DOCKCODE + "  SNA Part #:" + part.SNAPARTNUM + "  Cust Part #:" + part.CUSTPARTNUM);
                    }

                    //Don't need the "Order Group", we just did the "Part". Besides, it isn't as easy because of the "Anonymous Type Projection"...
                    //var ordersMissingSkidBuildOrderQuery = (from rows in query where rows.sboID == 0 group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp select grp); //Is set to 0 if null, in query above!
                    //foreach (var part in partOrdersMissingSkidBuildOrderQuery.ToList())
                    //{
                    //    missingSkidBuildOrderBuffer.Append("Order #:" + part.ORDERNUM + "  Dock #:" + part.DOCKCODE + "  SNA Part #:" + part.SNAPARTNUM + "  Cust Part #:" + part.CUSTPARTNUM);
                    //}
                }

                //If any "Missing SkidBuildOrder Confirmation" ERRORS, build the message (to be used in the Exception/Error)...
                StringBuilder confirmationIsNullOrNotEndsPBuffer = new StringBuilder();
                if (partOrdersConfirmationIsNullOrNotEndsP > 0 || ordersConfirmationIsNullOrNotEndsP > 0)
                {
                    //var partOrdersConfirmationIsNullOrNotEndsPQuery = (from rows in query where rows.sboConfirmationNumber == null || !(rows.sboConfirmationNumber.EndsWith("P")) select rows);

                    //foreach (var part in partOrdersConfirmationIsNullOrNotEndsPQuery.ToList())
                    //Wait...Sort by Order, DockCode, SNA Part #
                    foreach (var part in partOrdersConfirmationIsNullOrNotEndsPQuery.OrderBy(o => o.ORDERNUM).ThenBy(o => o.DOCKCODE).ThenBy(o => o.SNAPARTNUM).ToList())
                    {
                        confirmationIsNullOrNotEndsPBuffer.Append("<br>-----Order #:" + part.ORDERNUM + "  Dock #:" + part.DOCKCODE + "  SNA Part #:" + part.SNAPARTNUM + "  Cust Part #:" + part.CUSTPARTNUM);
                    }

                    //Don't need the "Order Group", we just did the "Part". Besides, it isn't as easy because of the "Anonymous Type Projection"...
                    //var ordersConfirmationIsNullOrNotEndsPQuery = (from rows in query where rows.sboConfirmationNumber == null || !(rows.sboConfirmationNumber.EndsWith("P")) group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp select grp);
                    //foreach (var part in ordersConfirmationIsNullOrNotEndsPQuery.ToList())
                    //{
                    //    confirmationIsNullOrNotEndsPBuffer.Append("Order #:" + part.ORDERNUM + "  Dock #:" + part.DOCKCODE + "  SNA Part #:" + part.SNAPARTNUM + "  Cust Part #:" + part.CUSTPARTNUM);
                    //}
                }

                //      One or more "ToyotaShipments NOT Completed" ERRORS! (Completed = False)
                //      One or more "Missing SkidBuildOrder" ERRORS! (no SkidBuildOrderId)
                //      One or more "Missing SkidBuildOrder Confirmation" ERRORS!
                //          ConfirmationNumber is Null OR does NOT end with "P".
                //          NOTE: Don't check for ending with "L", because SkidBuildOrder only ends in "P"!
                //                (Unless we set it to something like "Order Changed Need Confirmation".)
                if( partOrdersNotCompleted > 0 || ordersNotCompleted > 0 )
                {
                    //missingSkidBuildOrderBuffer
                    //confirmationIsNullOrNotEndsPBuffer
                    throw new ShipmentLoadCustomProgramException(
                          "Some of the Parts/Orders for this Shipment Load have not been marked COMPLETED!"
                        + "<br>This Shipment Load consists of " + partOrders.ToString() + " parts, which make up " + orders.ToString() + " orders."
                        + "<br>There are " + partOrdersNotCompleted.ToString() + " parts and " + ordersNotCompleted.ToString() + " orders NOT completed."
                        + ( (partOrdersMissingSkidBuildOrder > 0 || ordersMissingSkidBuildOrder > 0) ?
                            "<br>There are " + partOrdersMissingSkidBuildOrder.ToString() + " parts and " + ordersMissingSkidBuildOrder.ToString() + " orders missing a SkidBuildOrder."
                            + "<br>Below are Parts/Orders MISSING a SkidBuildOrder:"
                            + missingSkidBuildOrderBuffer.ToString()
                            + "<br>"
                            : "")
                        + ( (partOrdersConfirmationIsNullOrNotEndsP > 0 || ordersConfirmationIsNullOrNotEndsP > 0) ?
                            "<br>There are " + partOrdersConfirmationIsNullOrNotEndsP.ToString() + " parts and " + ordersConfirmationIsNullOrNotEndsP.ToString() + " orders missing a (valid) Confirmation Number."
                            + "<br>Below are Parts/Orders MISSING a (valid) Confirmation Number:"
                            + confirmationIsNullOrNotEndsPBuffer.ToString()
                            : "")
                        );
                }

                //      One or more "Missing SkidBuildOrder" ERRORS! (no SkidBuildOrderId)
                //      One or more "Missing SkidBuildOrder Confirmation" ERRORS!
                //          ConfirmationNumber is Null OR does NOT end with "P".
                //          NOTE: Don't check for ending with "L", because SkidBuildOrder only ends in "P"!
                //                (Unless we set it to something like "Order Changed Need Confirmation".)
                if (partOrdersMissingSkidBuildOrder > 0 || ordersMissingSkidBuildOrder > 0)
                {
                    //missingSkidBuildOrderBuffer
                    //confirmationIsNullOrNotEndsPBuffer
                    throw new ShipmentLoadCustomProgramException(
                          "Some of the Parts/Orders for this Shipment Load are MISSING a SkidBuildOrder!"
                        + "<br>This Shipment Load consists of " + partOrders.ToString() + " parts, which make up " + orders.ToString() + " orders."
                        + "<br>Below are Parts/Orders MISSING a SkidBuildOrder:"
                        + missingSkidBuildOrderBuffer.ToString()
                        + "<br>"
                        + ((partOrdersConfirmationIsNullOrNotEndsP > 0 || ordersConfirmationIsNullOrNotEndsP > 0) ?
                            "<br>There are " + partOrdersConfirmationIsNullOrNotEndsP.ToString() + " parts and " + ordersConfirmationIsNullOrNotEndsP.ToString() + " orders MISSING a SkidBuildOrder Confirmation Number."
                            + "<br>Below are Parts/Orders MISSING a (valid) Confirmation Number:"
                            + confirmationIsNullOrNotEndsPBuffer.ToString()
                            : "")
                        );
                }

                //      One or more "Missing SkidBuildOrder Confirmation" ERRORS!
                //          ConfirmationNumber is Null OR does NOT end with "P".
                //          NOTE: Don't check for ending with "L", because SkidBuildOrder only ends in "P"!
                //                (Unless we set it to something like "Order Changed Need Confirmation".)
                if (partOrdersConfirmationIsNullOrNotEndsP > 0 || ordersConfirmationIsNullOrNotEndsP > 0)
                {
                    //missingSkidBuildOrderBuffer
                    //confirmationIsNullOrNotEndsPBuffer
                    throw new ShipmentLoadCustomProgramException(
                          "Some of the Parts/Orders for this Shipment Load are MISSING a (valid) Confirmation Number."
                        + "<br>This Shipment Load consists of " + partOrders.ToString() + " parts, which make up " + orders.ToString() + " orders."
                        + "<br>Below are Parts/Orders MISSING a (valid) Confirmation Number:"
                        + confirmationIsNullOrNotEndsPBuffer.ToString()
                        );
                }

                //ShipmentLoadTrailer
                //this.ShipmentLoadOrders
                //this.ShipmentLoadOrders.ShipmentLoadOrderExceptions //TODO... I think this can be removed. Ask Garry!!! ALSO, Remove from EF diagram and Model!
                //this.ShipmentLoadOrders.ShipmentLoadSkids 
                //this.ShipmentLoadOrders.ShipmentLoadSkids.ShipmentLoadSkidExceptions
                //this.ShipmentLoadTrailerExceptions
                //this.ShipmentLoadTrailerResponses
                //this.ShipmentLoadTrailerResponses.APIResponse
                //this.ShipmentLoadTrailerResponses.APIResponse.APIResponseMessages

                ////////////////////////////////////////Build the ShipmentLoadTrailerPlan (RequestShipmentLoad)!!!!!////////////////////////////////////////

                //public class RequestShipmentLoad
                //{
                //    public string supplier { get; set; }
                //    public string route { get; set; }
                //    public string run { get; set; }
                //    public string trailerNumber { get; set; }
                //    public bool dropHook { get; set; }
                //    public string sealNumber { get; set; }
                //    public string supplierTeamFirstName { get; set; }
                //    public string supplierTeamLastName { get; set; }
                //    public string lpCode { get; set; }
                //    public string driverTeamFirstName { get; set; }
                //    public string driverTeamLastName { get; set; }
                //    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
                //    public List<APIexception> exceptions { get; set; }
                //    public List<Order> orders { get; set; }
                //}

                //public class ShipmentSkid
                //{
                //    public string palletization { get; set; }
                //    public string skidId { get; set; }
                //    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
                //    public List<APIexception> exceptions { get; set; }
                //}

                //public class Order
                //{
                //    public string order { get; set; }
                //    public string supplier { get; set; }
                //    public string plant { get; set; }
                //    public string dock { get; set; }
                //    public string pickUp { get; set; }
                //    public List<ShipmentSkid> skids { get; set; }
                //}

                //public class APIexception //If rename this to "scsException" or something else, will "Newtonsoft.Json" still work?? 
                //{
                //    public string exceptionCode { get; set; }
                //    public string comments { get; set; }
                //}

                //ShipmentLoadTrailer
                //this.ShipmentLoadOrders
                //this.ShipmentLoadOrders.ShipmentLoadOrderExceptions //TODO... I think this can be removed. Ask Garry!!! ALSO, Remove from EF diagram and Model!
                //this.ShipmentLoadOrders.ShipmentLoadSkids 
                //this.ShipmentLoadOrders.ShipmentLoadSkids.ShipmentLoadSkidExceptions
                //this.ShipmentLoadTrailerExceptions
                //this.ShipmentLoadTrailerResponses
                //this.ShipmentLoadTrailerResponses.APIResponse
                //this.ShipmentLoadTrailerResponses.APIResponse.APIResponseMessages

                var shipmentLoadOrders = (from rows in query
                                         group rows by new { rows.SUPPLIERCODE, rows.NAMC, rows.TRPT, rows.SUBROUTE, rows.ORDERNUM, rows.DOCKCODE } into grp
                                         select new
                                         {
                                             GroupKey = grp.Key,
                                             SupplierCode = grp.Select(g => g.SUPPLIERCODE).FirstOrDefault(),
                                             NAMC = grp.Select(g => g.NAMC).FirstOrDefault(),
                                             TRPT = grp.Select(g => g.TRPT).FirstOrDefault(),
                                             SubRoute = grp.Select(g => g.SUBROUTE).FirstOrDefault(),
                                             OrderNumber = grp.Select(g => g.ORDERNUM).FirstOrDefault(),
                                             DockCode = grp.Select(g => g.DOCKCODE).FirstOrDefault(),
                                             ////SkidBuildSkids = grp.Select(g => g.SkidBuildSkids), //Works, but dont need all (the collection) of the Skids, only need the First()
                                             //SkidBuildOrderSkidIds = grp.Select(g => g.SkidBuildSkids).FirstOrDefault(), //.Select(k => k.SkidId) //.FirstOrDefault(),
                                             ////SkidIds = grp.Select(g => g.SkidBuildSkids).FirstOrDefault().Select(k => k.SkidId), //.Select(k => k.SkidId) //.FirstOrDefault(),
                                             Skids = from skidIds in (grp.Select(g => g.SkidBuildSkids).FirstOrDefault())
                                                       select new
                                                       {
                                                           SkidId = skidIds.SkidId,
                                                           PalletizationCode = skidIds.PalletizationCode
                                                       }
                                         }); //.ToList();

                shipmentLoadTrailerPlan = new RequestShipmentLoad();
                shipmentLoadTrailerPlan.supplier = supplierCode;    //Populate with Parameter
                shipmentLoadTrailerPlan.route = "";                 //Populate with Parameter (or one of the Orders, if parameter empty)
                shipmentLoadTrailerPlan.run = "";                   //Populate with Parameter (or one of the Orders, if parameter empty)
                shipmentLoadTrailerPlan.trailerNumber = "";         //Default to empty.
                shipmentLoadTrailerPlan.dropHook = false;           //Default to false.
                shipmentLoadTrailerPlan.sealNumber = "";            //Default to empty.
                shipmentLoadTrailerPlan.supplierTeamFirstName = ""; //Default to empty.
                shipmentLoadTrailerPlan.supplierTeamLastName = "";  //Default to empty.
                shipmentLoadTrailerPlan.lpCode = "";                //Default to empty.
                shipmentLoadTrailerPlan.driverTeamFirstName = "";   //Default to empty.
                shipmentLoadTrailerPlan.driverTeamLastName = "";    //Default to empty.
                shipmentLoadTrailerPlan.orders = new List<Order>(); //Default to empty List.
                shipmentLoadTrailerPlan.exceptions = null;          //Default to null.

                //(Ignoring  SubRoute for now, because it isn't sent from the Canadian NAMCs anyway.)
                //(So, it isn't a parameter to this method...only use the one on the SkidBuilds)
                //if (String.IsNullOrEmpty(subRoute) == false)
                //{
                //    shipmentLoadTrailerPlan.route = subRoute.Remove(subRoute.Length - 2);
                //    shipmentLoadTrailerPlan.run = subRoute.Substring(subRoute.Length - 2);
                //}

                //
                //The "shipmentLoadOrders" query is a collection of Orders. Each item is one Order.
                //The "SkidIs" collection (on each Order) is all of the Skids for that Order.
                //All we have to do is simply add each Order and all the Skids for each Order.
                //
                //foreach (var partOrdered in query.OrderBy(o => o.ORDERNUM).ThenBy(o => o.DOCKCODE).ThenBy(o => o.SNAPARTNUM).ToList())
                foreach (var order in shipmentLoadOrders.OrderBy(o => o.OrderNumber).ThenBy(o => o.DockCode).ToList())
                {
                    //May need to load SubRoute from an Order, because the parameter can be empty/null and some orders (Canada) can be empty/null.
                    //(Ignoring  SubRoute for now, because it isn't sent from the Canadian NAMCs anyway.)
                    if (String.IsNullOrEmpty(shipmentLoadTrailerPlan.route))
                    {
                        if (String.IsNullOrEmpty(order.SubRoute) == false)
                        {
                            shipmentLoadTrailerPlan.route = order.SubRoute.Remove(order.SubRoute.Length - 2);
                            shipmentLoadTrailerPlan.run = order.SubRoute.Substring(order.SubRoute.Length - 2);
                        }
                    }

                    //
                    //Every item (order) is a unique Order for this ShipmentLoad.
                    //
                    //So, add the Order, then add each of the Skid's for this Order.
                    //
                    Order orderItem = new Order();
                    //public class Order
                    //{
                    //    public string order { get; set; }
                    //    public string supplier { get; set; }
                    //    public string plant { get; set; }
                    //    public string dock { get; set; }
                    //    public string pickUp { get; set; }
                    //    public List<ShipmentSkid> skids { get; set; }
                    //}
                    orderItem.order = order.OrderNumber;
                    orderItem.supplier = order.SupplierCode;
                    orderItem.plant = order.NAMC;
                    orderItem.dock = order.DockCode;
                    orderItem.pickUp = pickUpDateTime; //order.PickUp.Value.ToString("yyyy-MM-ddTHH:mm"); //RFC3339, but only Date, Hours, Min (with a "T" between them)

                    orderItem.skids = new List<ShipmentSkid>();
                    foreach (var skid in order.Skids)
                    {
                        ShipmentSkid skidItem = new ShipmentSkid();
                        //public class ShipmentSkid
                        //{
                        //    public string palletization { get; set; }
                        //    public string skidId { get; set; }
                        //    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
                        //    public List<APIexception> exceptions { get; set; }
                        //}
                        skidItem.palletization = skid.PalletizationCode;
                        skidItem.skidId = skid.SkidId;

                        skidItem.exceptions = null;
                        //if (skid.ShipmentLoadSkidExceptions != null)
                        //{
                        //    if (skid.ShipmentLoadSkidExceptions.Count > 0)
                        //    {
                        //        skidItem.exceptions = new List<APIexception>();
                        //        foreach (var exception in skid.ShipmentLoadSkidExceptions)
                        //        {
                        //            APIexception exceptionItem = new APIexception();
                        //            exceptionItem.exceptionCode = exception.Code;
                        //            exceptionItem.comments = exception.Comments;
                        //            skidItem.exceptions.Add(exceptionItem);
                        //        }
                        //    }
                        //}
                        orderItem.skids.Add(skidItem);
                    }
                    shipmentLoadTrailerPlan.orders.Add(orderItem);
                }

                //public class APIexception //If rename this to "scsException" or something else, will "Newtonsoft.Json" still work?? 
                //{
                //    public string exceptionCode { get; set; }
                //    public string comments { get; set; }
                //}

                //shipmentLoadTrailerPlan.exceptions = null;
                //if (shipmentLoadTrailerPlan.ShipmentLoadTrailerExceptions != null)
                //{
                //    if (shipmentLoadTrailerPlan.ShipmentLoadTrailerExceptions.Count > 0)
                //    {
                //        shipmentLoadTrailerPlan.exceptions = new List<APIexception>();
                //        foreach (var exception in shipmentLoadTrailerPlan.ShipmentLoadTrailerExceptions)
                //        {
                //            APIexception exceptionItem = new APIexception();
                //            exceptionItem.exceptionCode = exception.Code;
                //            exceptionItem.comments = exception.Comments;
                //            shipmentLoadTrailerPlan.exceptions.Add(exceptionItem);
                //        }
                //    }
                //}


                //WE DONT NEED TO LOAD THESE FOR THIS!!!!
                //WE DONT NEED TO LOAD THESE FOR THIS!!!!
                //WE DONT NEED TO LOAD THESE FOR THIS!!!!
                //WE DONT NEED TO LOAD THESE FOR THIS!!!!
                //
                //this.ShipmentLoadTrailerResponses
                //this.ShipmentLoadTrailerResponses.APIResponse
                //this.ShipmentLoadTrailerResponses.APIResponse.APIResponseMessages

                
                //throw new ShipmentLoadCustomProgramException("No ERRORS found for ShipmentLoadPlan...continue developing the logic for GetShipmentLoadTrailerPlan!");
            }
            return shipmentLoadTrailerPlan;
        }

        public static void UpdateConfirmationNumber(int idShipmentLoadTrailer, string confirmationNumber)
        {
            using (var ctx = new PickToLightEntities())
            {
                var shipmentLoadTrailer = (from slt in ctx.ShipmentLoadTrailers
                                           where slt.ID == idShipmentLoadTrailer
                                           select slt).Single<ShipmentLoadTrailer>();

                if (string.IsNullOrEmpty(confirmationNumber))
                {
                    shipmentLoadTrailer.ConfirmationNumber = null;
                }
                else
                {
                    shipmentLoadTrailer.ConfirmationNumber = confirmationNumber;
                }
                ctx.SaveChanges();
            }
        }
    }

    public class ShipmentLoadCustomProgramException : Exception
    {
        public ShipmentLoadCustomProgramException()
        {
        }
        public ShipmentLoadCustomProgramException(string message)
            : base(message)
        {
        }

        public ShipmentLoadCustomProgramException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

}
