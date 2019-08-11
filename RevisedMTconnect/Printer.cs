using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTConnectDevices = MTConnect.MTConnectDevices;
using MTConnectStreams = MTConnect.MTConnectStreams;
using MTConnect.Clients;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;


namespace RevisedMTconnect
{
    class Printer
    {
        public enum eWhatPrint { EveryCycle, IfPrinting, NotZero1, NotZeroMultiple, ChangedOne, ChangedMultiple };
        public eWhatPrint whatPrint = eWhatPrint.EveryCycle;

        MTConnectClient client;
        public string baseUrl = "http://10.10.116.78:5000";   // The base address for the MTConnect Agent
        int SuccessTime = 1000;
        int FailTime = 30000;
        public float SerialNumber = 1028;
        DateTime CurrentTimeStamp = DateTime.Now;
        string CurrentlightTowerState = "NA";
        float Currentm1TAct = 0;
        float Lastm1TAct = 0;
        float Currentm1TCom = 0;
        float Currentm1DCycle = 0;
        float Lastm1DCycle = 0;
        string Currentm1Material = "NA";
        string Currentm1MaterialId = "NA";
        string Currentm1Tip = "NA";
        float Currentm1TipOdometer = 0;
        float Currentm1Follow = 0;
        float Lastm1Follow = 0;
        bool Writem1Follow = true;
        float Currents1TAct = 0;
        float Currents1TCom = 0;
        float Currents1DCycle = 0;
        string Currents1Material = "NA";
        string Currents1MaterialId = "NA";
        string Currents1Tip = "NA";
        float Currents1TipOdometer = 0;
        float Currents1Follow = 0;
        float Lasts1Follow = 0;
        bool WriteSupportFollowing = true;
        float CurrentxAct = 0;
        float LastxAct = 0;
        bool WritexFollow = true;
        float CurrentxCom = 0;
        float CurrentxPosLim = 0;
        float CurrentxNegLim = 0;
        float CurrentxFollow = 0;
        float LastxFollow = 0;
        float CurrentyAct = 0;
        float LastyAct = 0;
        float CurrentyCom = 0;
        float CurrentyPosLim = 0;
        float CurrentyNegLim = 0;
        float CurrentyFollow = 0;
        float LastyFollow = 0;
        bool WriteyFollow = true;
        float CurrentzAct = 0;
        float CurrentzCom = 0;
        float CurrentzPosLim = 0;
        float CurrentzNegLim = 0;
        float CurrentzFollow = 0;
        float LastzFollow = 0;
        bool WritezFollow = true;
        float Currentmb1InitialVolume = 0;
        float Currentmb1CurrentVolume = 0;
        string Currentmb1Mat = "NA";
        string Currentmb1MatId = "NA";
        string Currentmb1SN = "NA";
        string Currentmb1MfgDate = "NA";
        string Currentmb1MfgLot = "NA";
        string Currentmb1LoadState = "NA";
        float Currentmb2InitialVolume = 0;
        float Currentmb2CurrentVolume = 0;
        string Currentmb2Mat = "NA";
        string Currentmb2MatId = "NA";
        string Currentmb2SN = "NA";
        string Currentmb2MfgDate = "NA";
        string Currentmb2MfgLot = "NA";
        string Currentmb2LoadState = "NA";
        float Currentsb1InitialVolume = 0;
        float Currentsb1CurrentVolume = 0;
        string Currentsb1Mat = "NA";
        string Currentsb1MatId = "NA";
        string Currentsb1SN = "NA";
        string Currentsb1MfgDate = "NA";
        string Currentsb1MfgLot = "NA";
        string Currentsb1LoadState = "NA";
        float Currentsb2InitialVolume = 0;
        float Currentsb2CurrentVolume = 0;
        string Currentsb2Mat = "NA";
        string Currentsb2MatId = "NA";
        string Currentsb2SN = "NA";
        string Currentsb2MfgDate = "NA";
        string Currentsb2MfgLot = "NA";
        string Currentsb2LoadState = "NA";
        string CurrentheadBoardFirmwareVersion = "NA";
        string CurrentheadBoardSerialNumber = "NA";
        string CurrentheadType = "NA";
        string CurrentmioBoardFirmwareVersion = "NA";
        string CurrentacBoardFirmwareVersion = "NA";
        string Currentm1DriveBlockFirmwareVersion = "NA";
        string Currentm1DriveBlockManufacturingYear = "NA";
        string Currentm1DriveBlockManufacturingWeek = "NA";
        string Currentm1DriveBlockSerialNumber = "NA";
        string Currentm2DriveBlockFirmwareVersion = "NA";
        string Currentm2DriveBlockManufacturingYear = "NA";
        string Currentm2DriveBlockManufacturingWeek = "NA";
        string Currentm2DriveBlockSerialNumber = "NA";
        string Currents1DriveBlockFirmwareVersion = "NA";
        string Currents1DriveBlockManufacturingYear = "NA";
        string Currents1DriveBlockManufacturingWeek = "NA";
        string Currents1DriveBlockSerialNumber = "NA";
        string Currents2DriveBlockFirmwareVersion = "NA";
        string Currents2DriveBlockManufacturingYear = "NA";
        string Currents2DriveBlockManufacturingWeek = "NA";
        string Currents2DriveBlockSerialNumber = "NA";
        string CurrentmaterialName = "NA";
        string CurrenttipName = "NA";
        float Currentlayers = 0;
        float CurrentestTime = 0;
        string CurrentsubmissionTime = "NA";
        string CurrentelaTime = "NA";
        string CurrentpctComp = "NA";
        float CurrentcurLayer = 0;
        string CurrentcurrentJobGuid = "NA";
        string CurrentcmbLock = "NA";
        string CurrentpathExecution = "NA";
        string CurrentpathControllerMode = "NA";
        string CurrentpathProgram = "NA";
        string CurrentprinterState = "NA";
        string CurrenthomedState = "NA";
        string Currentlogic = "NA";
        string CurrentlicensedMaterials = "NA";
        string CurrentswVersion = "NA";
        float CurrentovenAct = 0;
        float LastOvenAct = 0;
        bool WriteOvenAct = true;
        float CurrentovenCom = 0;
        float CurrentovenDCycle = 0;
        string CurrentinnerVacuumState = "NA";
        string CurrentouterVacuumState = "NA";
        string CurrentdoorOpenState = "NA";
        string CurrentdoorLockState = "NA";
        string CurrentcoverOpenState = "NA";
        string CurrentcoverLockState = "NA";
        string CurrentdewPoint = "NA";
        float CurrentupsInputVoltage = 0;
        float LastupsInputVoltage = 0;
        bool WriteupsInputVoltage = true;
        float CurrentupsOutputVoltage = 0;
        float CurrentupsOutputLoad = 0;
        float CurrentupsOutputFrequency = 0;
        string CurrentupsState = "NA";
        string CurrentacContactorState = "NA";
        string Currentavail = "NA";
        string Currentdev_asset_chg = "NA";
        string Currentdev_asset_rem = "NA";


        //Setting Started value to false
        private bool Started = false;

        //Start method for MT Connect. Was used for the previous database
        // IS THIS NEEDED ?????
        private void Start()
        {
            // Create a new MTCOnnect Client using the baseUrl
            client = new MTConnectClient(baseUrl);
            client.Start();

            Started = true;
            Console.WriteLine("Started complete");
        }

        private void StartMTOnly()
        {
            // Create a new MTConnectClient using the baseUrl
            client = new MTConnectClient(baseUrl);

            client.Start();
            Started = true;
            //Console.WriteLine("Start Complete");

        }

        public void Stop()
        {
            client.Stop();
        }


        public void RunAsThread()
        {
            int i = 0;
            bool DataCaptureSuccess;
            while (true) //Run Forever
            {
                i = i + 1;

                DataCaptureSuccess = PrintCurrentWithFunctions(); //OR PrintCurrentWithFunctions

                if (DataCaptureSuccess == true)
                {
                    //Console.WriteLine("Success Serial:" + SerialNumber + " IP=" + baseUrl + "  i=" + i + " TimeStamp: " + DateTime.Now.ToLongTimeString());
                    System.Threading.Thread.Sleep(SuccessTime);
                }
                else
                {
                    //Wait a Long time if the server has problems
                    System.Threading.Thread.Sleep(SuccessTime);
                }
            }
        }

        // --- Event Handlers ---

        void DevicesSuccessful(MTConnectDevices.Document document)
        {
            foreach (var device in document.Devices)
            {
                var dataItems = device.GetDataItems();
                foreach (var dataItem in dataItems) Console.WriteLine(dataItem.Id + " : " + dataItem.Name);
            }
        }

        void StreamsSuccessful(MTConnectStreams.Document document)
        {
            foreach (var deviceStream in document.DeviceStreams)
            {
                foreach (var dataItem in deviceStream.DataItems) Console.WriteLine(dataItem.DataItemId + " = " + dataItem.CDATA);
            }
        }

        //Here is where the actions happens
        //
        public bool PrintCurrentWithFunctions()
        {
            try
            {
                bool NeedToWrite;

                //Calling class that loops through the device and device components and gets current status/data.
                ReadAllDataToCurrent();

                NeedToWrite = DecideWhatDataToWrite();

                // If NeedToWrite has a value of true and returns something from Decode
                if (NeedToWrite == true)
                {
                    WriteDataIfNeeded();
                }
                UpdateLastFromCurrent();

                return true;
            }
            catch (Exception Ex)
            {              
                Console.WriteLine("Close : Serial Number: " + SerialNumber.ToString() + " ,Reason: " + Ex.ToString());
                System.Threading.Thread.Sleep(FailTime);
                client.Stop();
                Started = false;
                return false;
            }
        }

        //Update the previous ("current value") with the most current value and store it.
        void UpdateLastFromCurrent()
        {
            LastxAct = CurrentxAct;
            LastyAct = CurrentyAct;
            Lastm1TAct = Currentm1TAct;
            Lastm1DCycle = Currentm1DCycle;
            LastxFollow = CurrentxFollow;
            LastyFollow = CurrentyFollow;
            LastzFollow = CurrentzFollow;
            Lastm1Follow = Currentm1Follow;
            Lasts1Follow = Currents1Follow;
            LastupsInputVoltage = CurrentupsInputVoltage;
            LastOvenAct = CurrentovenAct;

        }

        
        // Run loops through all the components.
        void ReadAllDataToCurrent()
        {
            var currentMtConnectSample = new Current(baseUrl).Execute();  //Read Current Value of MTConnect
            //Console.WriteLine("GetCurrent");
            

            if (currentMtConnectSample != null) //make sure some thing is there
            {
                foreach (var deviceStream in currentMtConnectSample.DeviceStreams) //we only use one device stream per printer  / Loop Through all the devices
                {
                    foreach (var dataItem in deviceStream.DataItems)  //Loop Through all data items in the device
                    {
                        switch (dataItem.DataItemId) //parsing data from the current mt-connect data into a readable variable
                        {
                            case "lightTowerState":
                                CurrentlightTowerState = dataItem.CDATA;
                                break;
                            case "m1TAct":
                                Currentm1TAct = float.Parse(dataItem.CDATA);
                                break;
                            case "m1TActLast":
                                Lastm1TAct = float.Parse(dataItem.CDATA);
                                break;
                            case "m1TCom":
                                Currentm1TCom = float.Parse(dataItem.CDATA);
                                break;
                            case "m1DCycle":
                                Currentm1DCycle = float.Parse(dataItem.CDATA);
                                break;
                            case "m1DCycleLast":
                                Lastm1DCycle = float.Parse(dataItem.CDATA);
                                break;
                            case "m1Material":
                                Currentm1Material = dataItem.CDATA;
                                break;
                            case "m1MaterialId":
                                Currentm1MaterialId = dataItem.CDATA;
                                break;
                            case "m1Tip":
                                Currentm1Tip = dataItem.CDATA;
                                break;
                            case "m1TipOdometer":
                                Currentm1TipOdometer = float.Parse(dataItem.CDATA);
                                break;
                            case "m1Follow":
                                Currentm1Follow = float.Parse(dataItem.CDATA);
                                break;
                            case "s1TAct":
                                Currents1TAct = float.Parse(dataItem.CDATA);
                                break;
                            case "s1TCom":
                                Currents1TCom = float.Parse(dataItem.CDATA);
                                break;
                            case "s1DCycle":
                                Currents1DCycle = float.Parse(dataItem.CDATA);
                                break;
                            case "s1Material":
                                Currents1Material = dataItem.CDATA;
                                break;
                            case "s1MaterialId":
                                Currents1MaterialId = dataItem.CDATA;
                                break;
                            case "s1Tip":
                                Currents1Tip = dataItem.CDATA;
                                break;
                            case "s1TipOdometer":
                                Currents1TipOdometer = float.Parse(dataItem.CDATA);
                                break;
                            case "s1Follow":
                                Currents1Follow = float.Parse(dataItem.CDATA);
                                break;
                            case "xAct":
                                CurrentxAct = float.Parse(dataItem.CDATA);
                                break;
                            case "xActLast":
                                LastxAct = float.Parse(dataItem.CDATA);
                                break;
                            case "xCom":
                                CurrentxCom = float.Parse(dataItem.CDATA);
                                break;
                            case "xPosLim":
                                CurrentxPosLim = float.Parse(dataItem.CDATA);
                                break;
                            case "xNegLim":
                                CurrentxNegLim = float.Parse(dataItem.CDATA);
                                break;
                            case "xFollow":
                                CurrentxFollow = float.Parse(dataItem.CDATA);
                                break;
                            case "yAct":
                                CurrentyAct = float.Parse(dataItem.CDATA);
                                break;
                            case "yActLast":
                                LastyAct = float.Parse(dataItem.CDATA);
                                break;
                            case "yCom":
                                CurrentyCom = float.Parse(dataItem.CDATA);
                                break;
                            case "yPosLim":
                                CurrentyPosLim = float.Parse(dataItem.CDATA);
                                break;
                            case "yNegLim":
                                CurrentyNegLim = float.Parse(dataItem.CDATA);
                                break;
                            case "yFollow":
                                CurrentyFollow = float.Parse(dataItem.CDATA);
                                break;
                            case "zAct":
                                CurrentzAct = float.Parse(dataItem.CDATA);
                                break;
                            case "zCom":
                                CurrentzCom = float.Parse(dataItem.CDATA);
                                break;
                            case "zPosLim":
                                CurrentzPosLim = float.Parse(dataItem.CDATA);
                                break;
                            case "zNegLim":
                                CurrentzNegLim = float.Parse(dataItem.CDATA);
                                break;
                            case "zFollow":
                                CurrentzFollow = float.Parse(dataItem.CDATA);
                                break;
                            case "mb1InitialVolume":
                                Currentmb1InitialVolume = float.Parse(dataItem.CDATA);
                                break;
                            case "mb1CurrentVolume":
                                Currentmb1CurrentVolume = float.Parse(dataItem.CDATA);
                                break;
                            case "mb1Mat":
                                Currentmb1Mat = dataItem.CDATA;
                                break;
                            case "mb1MatId":
                                Currentmb1MatId = dataItem.CDATA;
                                break;
                            case "mb1SN":
                                Currentmb1SN = dataItem.CDATA;
                                break;
                            case "mb1MfgDate":
                                Currentmb1MfgDate = dataItem.CDATA;
                                break;
                            case "mb1MfgLot":
                                Currentmb1MfgLot = dataItem.CDATA;
                                break;
                            case "mb1LoadState":
                                Currentmb1LoadState = dataItem.CDATA;
                                break;
                            case "mb2InitialVolume":
                                Currentmb2InitialVolume = float.Parse(dataItem.CDATA);
                                break;
                            case "mb2CurrentVolume":
                                Currentmb2CurrentVolume = float.Parse(dataItem.CDATA);
                                break;
                            case "mb2Mat":
                                Currentmb2Mat = dataItem.CDATA;
                                break;
                            case "mb2MatId":
                                Currentmb2MatId = dataItem.CDATA;
                                break;
                            case "mb2SN":
                                Currentmb2SN = dataItem.CDATA;
                                break;
                            case "mb2MfgDate":
                                Currentmb2MfgDate = dataItem.CDATA;
                                break;
                            case "mb2MfgLot":
                                Currentmb2MfgLot = dataItem.CDATA;
                                break;
                            case "mb2LoadState":
                                Currentmb2LoadState = dataItem.CDATA;
                                break;
                            case "sb1InitialVolume":
                                Currentsb1InitialVolume = float.Parse(dataItem.CDATA);
                                break;
                            case "sb1CurrentVolume":
                                Currentsb1CurrentVolume = float.Parse(dataItem.CDATA);
                                break;
                            case "sb1Mat":
                                Currentsb1Mat = dataItem.CDATA;
                                break;
                            case "sb1MatId":
                                Currentsb1MatId = dataItem.CDATA;
                                break;
                            case "sb1SN":
                                Currentsb1SN = dataItem.CDATA;
                                break;
                            case "sb1MfgDate":
                                Currentsb1MfgDate = dataItem.CDATA;
                                break;
                            case "sb1MfgLot":
                                Currentsb1MfgLot = dataItem.CDATA;
                                break;
                            case "sb1LoadState":
                                Currentsb1LoadState = dataItem.CDATA;
                                break;
                            case "sb2InitialVolume":
                                Currentsb2InitialVolume = float.Parse(dataItem.CDATA);
                                break;
                            case "sb2CurrentVolume":
                                Currentsb2CurrentVolume = float.Parse(dataItem.CDATA);
                                break;
                            case "sb2Mat":
                                Currentsb2Mat = dataItem.CDATA;
                                break;
                            case "sb2MatId":
                                Currentsb2MatId = dataItem.CDATA;
                                break;
                            case "sb2SN":
                                Currentsb2SN = dataItem.CDATA;
                                break;
                            case "sb2MfgDate":
                                Currentsb2MfgDate = dataItem.CDATA;
                                break;
                            case "sb2MfgLot":
                                Currentsb2MfgLot = dataItem.CDATA;
                                break;
                            case "sb2LoadState":
                                Currentsb2LoadState = dataItem.CDATA;
                                break;
                            case "headBoardFirmwareVersion":
                                CurrentheadBoardFirmwareVersion = dataItem.CDATA;
                                break;
                            case "headBoardSerialNumber":
                                CurrentheadBoardSerialNumber = dataItem.CDATA;
                                break;
                            case "headType":
                                CurrentheadType = dataItem.CDATA;
                                break;
                            case "mioBoardFirmwareVersion":
                                CurrentmioBoardFirmwareVersion = dataItem.CDATA;
                                break;
                            case "acBoardFirmwareVersion":
                                CurrentacBoardFirmwareVersion = dataItem.CDATA;
                                break;
                            case "m1DriveBlockFirmwareVersion":
                                Currentm1DriveBlockFirmwareVersion = dataItem.CDATA;
                                break;
                            case "m1DriveBlockManufacturingYear":
                                Currentm1DriveBlockManufacturingYear = dataItem.CDATA;
                                break;
                            case "m1DriveBlockManufacturingWeek":
                                Currentm1DriveBlockManufacturingWeek = dataItem.CDATA;
                                break;
                            case "m1DriveBlockSerialNumber":
                                Currentm1DriveBlockSerialNumber = dataItem.CDATA;
                                break;
                            case "m2DriveBlockFirmwareVersion":
                                Currentm2DriveBlockFirmwareVersion = dataItem.CDATA;
                                break;
                            case "m2DriveBlockManufacturingYear":
                                Currentm2DriveBlockManufacturingYear = dataItem.CDATA;
                                break;
                            case "m2DriveBlockManufacturingWeek":
                                Currentm2DriveBlockManufacturingWeek = dataItem.CDATA;
                                break;
                            case "m2DriveBlockSerialNumber":
                                Currentm2DriveBlockSerialNumber = dataItem.CDATA;
                                break;
                            case "s1DriveBlockFirmwareVersion":
                                Currents1DriveBlockFirmwareVersion = dataItem.CDATA;
                                break;
                            case "s1DriveBlockManufacturingYear":
                                Currents1DriveBlockManufacturingYear = dataItem.CDATA;
                                break;
                            case "s1DriveBlockManufacturingWeek":
                                Currents1DriveBlockManufacturingWeek = dataItem.CDATA;
                                break;
                            case "s1DriveBlockSerialNumber":
                                Currents1DriveBlockSerialNumber = dataItem.CDATA;
                                break;
                            case "s2DriveBlockFirmwareVersion":
                                Currents2DriveBlockFirmwareVersion = dataItem.CDATA;
                                break;
                            case "s2DriveBlockManufacturingYear":
                                Currents2DriveBlockManufacturingYear = dataItem.CDATA;
                                break;
                            case "s2DriveBlockManufacturingWeek":
                                Currents2DriveBlockManufacturingWeek = dataItem.CDATA;
                                break;
                            case "s2DriveBlockSerialNumber":
                                Currents2DriveBlockSerialNumber = dataItem.CDATA;
                                break;
                            case "materialName":
                                CurrentmaterialName = dataItem.CDATA;
                                break;
                            case "tipName":
                                CurrenttipName = dataItem.CDATA;
                                break;
                            case "layers":
                                Currentlayers = float.Parse(dataItem.CDATA);
                                break;
                            case "estTime":
                                CurrentestTime = float.Parse(dataItem.CDATA);
                                break;
                            case "submissionTime":
                                CurrentsubmissionTime = dataItem.CDATA;
                                break;
                            case "elaTime":
                                CurrentelaTime = dataItem.CDATA;
                                break;
                            case "pctComp":
                                CurrentpctComp = dataItem.CDATA;
                                break;
                            case "curLayer":
                                CurrentcurLayer = float.Parse(dataItem.CDATA);
                                break;
                            case "currentJobGuid":
                                CurrentcurrentJobGuid = dataItem.CDATA;
                                break;
                            case "cmbLock":
                                CurrentcmbLock = dataItem.CDATA;
                                break;
                            case "pathExecution":
                                CurrentpathExecution = dataItem.CDATA;
                                break;
                            case "pathControllerMode":
                                CurrentpathControllerMode = dataItem.CDATA;
                                break;
                            case "pathProgram":
                                CurrentpathProgram = dataItem.CDATA;
                                break;
                            case "printerState":
                                CurrentprinterState = dataItem.CDATA;
                                break;
                            case "homedState":
                                CurrenthomedState = dataItem.CDATA;
                                break;
                            case "logic":
                                Currentlogic = dataItem.CDATA;
                                break;
                            case "licensedMaterials":
                                CurrentlicensedMaterials = dataItem.CDATA;
                                break;
                            case "swVersion":
                                CurrentswVersion = dataItem.CDATA;
                                break;
                            case "ovenAct":
                                CurrentovenAct = float.Parse(dataItem.CDATA);
                                break;
                            case "ovenCom":
                                CurrentovenCom = float.Parse(dataItem.CDATA);
                                break;
                            case "ovenDCycle":
                                CurrentovenDCycle = float.Parse(dataItem.CDATA);
                                break;
                            case "innerVacuumState":
                                CurrentinnerVacuumState = dataItem.CDATA;
                                break;
                            case "outerVacuumState":
                                CurrentouterVacuumState = dataItem.CDATA;
                                break;
                            case "doorOpenState":
                                CurrentdoorOpenState = dataItem.CDATA;
                                break;
                            case "doorLockState":
                                CurrentdoorLockState = dataItem.CDATA;
                                break;
                            case "coverOpenState":
                                CurrentcoverOpenState = dataItem.CDATA;
                                break;
                            case "coverLockState":
                                CurrentcoverLockState = dataItem.CDATA;
                                break;
                            case "dewPoint":
                                CurrentdewPoint = dataItem.CDATA;
                                break;
                            case "upsInputVoltage":
                                CurrentupsInputVoltage = float.Parse(dataItem.CDATA);
                                break;
                            case "upsInputVoltageLast":
                                LastupsInputVoltage = float.Parse(dataItem.CDATA);
                                break;
                            case "upsOutputVoltage":
                                CurrentupsOutputVoltage = float.Parse(dataItem.CDATA);
                                break;
                            case "upsOutputLoad":
                                CurrentupsOutputLoad = float.Parse(dataItem.CDATA);
                                break;
                            case "upsOutputFrequency":
                                CurrentupsOutputFrequency = float.Parse(dataItem.CDATA);
                                break;
                            case "upsState":
                                CurrentupsState = dataItem.CDATA;
                                break;
                            case "acContactorState":
                                CurrentacContactorState = dataItem.CDATA;
                                break;
                            case "avail":
                                Currentavail = dataItem.CDATA;
                                break;
                            case "dev_asset_chg":
                                Currentdev_asset_chg = dataItem.CDATA;
                                break;
                            case "dev_asset_rem":
                                Currentdev_asset_rem = dataItem.CDATA;
                                break;
                        }

                    } //Next Data Item

                } //Next Device
                  //Assume the CurrentXAct and etc are now set to current mtconnect data
                CurrentTimeStamp = DateTime.Now; //Grab the current time
            }

        }

        //Check for delta values between Last and Current values.
        //If delta values detected, jump to WriteData if needed. 
        bool DecideWhatDataToWrite()
        {
            bool ShouldIWrite = false; //Set ShouldIWrite to false 
            //Y Following
            if (LastyFollow != CurrentyFollow)
            {
                ShouldIWrite = true;
                //Insert values of Y-following into the Y table.
                WriteyFollow = true;
            }
            else
            {
                WriteyFollow = false;
            }
            //X Following
            if (LastxFollow != CurrentxFollow)
            {
               ShouldIWrite = true;
                WritexFollow = true;
            }
            else
            {
                WritexFollow = false;
            }
            //Z Following
            if (LastzFollow != CurrentzFollow)
            {
                ShouldIWrite = true;
                WritezFollow = true;
            }
            else
            {
                WritezFollow = false;
            }
            // Model Following
            if (Lastm1Follow != Currentm1Follow)
            {
                ShouldIWrite = true;
                Writem1Follow = true;
            }
            else
            {
                Writem1Follow = false;
            }
            // Support Following
            if (Lasts1Follow != Currents1Follow)
            {
                ShouldIWrite = true;
                WriteSupportFollowing = true;
            }
            else
            {
                WriteSupportFollowing = false;
            }
            // UPS INPUT VOLTAGE
            if (LastupsInputVoltage != CurrentupsInputVoltage)
            {
                ShouldIWrite = true;
                WriteupsInputVoltage = true;
            }
            else
            {
                WriteupsInputVoltage = false;
            }
            //Oven Temperature
            if (LastOvenAct != CurrentovenAct)
            {
                ShouldIWrite = true;
                WriteOvenAct = true;
            }
            else
            {
                WriteOvenAct = false;
            }

            return ShouldIWrite;
        }

        void WriteDataIfNeeded()
        {
            //MYSQL DATABASE
            //THIS DATA GOES TO THE DATABASE   
            //string with the connection string assigned to the database
            string connStr = "server=localhost;user=root;database=revised_connect;port=3306;password=mtconnect";

            //MySqlConnection object created the object to pass my connection string.
            MySqlConnection conn2 = new MySqlConnection(connStr);

            try
            {
                //CurrentTimeStamp = DateTime.Now;
                Console.WriteLine("Connecting to MySQL...");
                conn2.Open();

                //This command class will handle the query and connection object
                MySqlCommand myCmd = new MySqlCommand();

                myCmd.Connection = conn2;
                myCmd.Parameters.AddWithValue("@serial_number", SerialNumber);

                
                if (WritexFollow == true)
                {
                    // Write to MySQL database tables.
                    myCmd.CommandText = "INSERT INTO xfollowing(Serial_number, xFollowingcol) VALUES(@Serial_number, @xFollowingcol)";
                    //myCmd.Parameters.AddWithValue("@serial_number", SerialNumber);
                    myCmd.Parameters.AddWithValue("@xFollowingcol", CurrentxFollow);
                    myCmd.ExecuteNonQuery();
                }

                if (WriteyFollow == true)
                {
                    myCmd.CommandText = "INSERT INTO yfollowing(Serial_number, yfollowingcol) VALUES(@Serial_number, @yfollowingcol)";
                    myCmd.Parameters.AddWithValue("@yfollowingcol", CurrentyFollow);
                    myCmd.ExecuteNonQuery();
                }

                if (WritezFollow == true)
                {
                    myCmd.CommandText = "INSERT INTO zfollowing(Serial_number, zfollowingcol) VALUES(@Serial_number, @zfollowingcol)";
                    myCmd.Parameters.AddWithValue("@zfollowingcol", CurrentzFollow);
                    myCmd.ExecuteNonQuery();
                }

                if (Writem1Follow == true)
                {
                    myCmd.CommandText = "INSERT INTO modelfollowing(Serial_number, modelfollowingcol) VALUES(@Serial_number, @modelfollowingcol)";
                    myCmd.Parameters.AddWithValue("@modelfollowingcol", Currentm1Follow);
                    myCmd.ExecuteNonQuery();
                }

                if (WriteupsInputVoltage == true)
                {
                    myCmd.CommandText = "INSERT INTO input_ups_voltage(Serial_number, input_ups_voltagecol) VALUES(@serial_number, @input_ups_voltagecol)";
                    myCmd.Parameters.AddWithValue("@input_ups_voltagecol", CurrentupsInputVoltage);
                    myCmd.ExecuteNonQuery();
                }

                if (WriteOvenAct == true)
                {
                    myCmd.CommandText = "INSERT INTO oventemp(Serial_number, oventempcol) VALUES(@Serial_number, @oventempcol)";
                    myCmd.Parameters.AddWithValue("@oventempcol", CurrentovenAct);
                    myCmd.ExecuteNonQuery();
                }

                if (WriteSupportFollowing == true)
                {
                    myCmd.CommandText = "INSERT INTO supportfollowing(Serial_number, supportfollowingcol) VALUES(@Serial_number, @supportfollowingcol)";
                    myCmd.Parameters.AddWithValue("@supportfollowingcol", Currents1Follow);
                    myCmd.ExecuteNonQuery();
                    myCmd.Prepare();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn2.Close();
            Console.WriteLine("Done.");

        }


    }
}
