using Pally;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETerminal
{
    class DeviceControl
    {
        private List<TerminalDevice> Devices { get; set; }
        private static readonly DeviceControl DevC = new DeviceControl();

        static DeviceControl() { }
        private DeviceControl() { }

        public static DeviceControl GetDevControl
        {

            get { return DevC; }
        }

        private void PopulateFromNetwork()
        {
            if (Devices != null)
            {
                foreach (var d in Devices)
                {
                    AccessAndFill(d);
                }
            }

        }

        private void AccessAndFill(TerminalDevice d)
        {
            oTerminal myTerminal = new oTerminal(d.IPAddress, d.ID, d.Port, d.AccKey);

            if (myTerminal.isAlive())
            {
                d.SerialNumber = myTerminal.GetSerialNumber();
                d.CurrentRegUsersCount = myTerminal.QueryCurrentRegUsers();
                d.LogsCount = myTerminal.QueryNumberOfLog();
                d.MaxLogs = myTerminal.QueryMaxAllowRegUsers();
                d.MaxLogs = myTerminal.QueryMaxLogCapacity();
                d.TerminalPassword = myTerminal.GetTerminalPassword();
                d.WebLogOnPassword = myTerminal.GetWEBLogOnPassword(1);
                d.DateOfTerminal = myTerminal.QueryDateOfTheTerminal();
                d.TimeOfTerminal = myTerminal.QueryTimeOfTheTerminal();

            }
        }

        public string GetDeviceInfo(TerminalDevice device)
        {
            StringBuilder infoBuilder = new StringBuilder();

            oTerminal myTerminal = new oTerminal(device.IPAddress, device.ID, device.Port, device.AccKey);

            if (myTerminal.isAlive())
            {


                infoBuilder.Append("Serial No: " + myTerminal.GetSerialNumber());
                infoBuilder.Append("Current Reg. Users: " + myTerminal.QueryCurrentRegUsers().ToString());
                infoBuilder.Append("Number of Log: " + myTerminal.QueryNumberOfLog().ToString());
                infoBuilder.Append("Max. allow Reg. Users: " + myTerminal.QueryMaxAllowRegUsers().ToString());
                infoBuilder.Append("Max. Log Capacity: " + myTerminal.QueryMaxLogCapacity().ToString());
                infoBuilder.Append("Terminal Password: " + myTerminal.GetTerminalPassword());
                infoBuilder.Append("WEB Password: " + myTerminal.GetWEBLogOnPassword(1));
                infoBuilder.Append("Date: " + myTerminal.QueryDateOfTheTerminal());
                infoBuilder.Append("Time: " + myTerminal.QueryTimeOfTheTerminal());

            }

            return infoBuilder.ToString();

        }

        public int GetRegisteredUsersIDs(TerminalDevice device)
        {
            int userCount = 0;

            oTerminal myTerminal = new oTerminal(device.IPAddress, device.ID, device.Port, device.AccKey);

            if (myTerminal.isAlive())
            {
                myTerminal.INT_ReceiveTimeOut = 10000;
                try
                {
                    UserAdapter userAdapter = new UserAdapter();
                    userAdapter.ComputeUserID(myTerminal.GetUserIDList());
                    device.UsersIDs = userAdapter.GetIDlist();
                    if (device.UsersIDs != null)
                        userCount = device.UsersIDs.Count();
                }
                catch (Exception ex)
                {
                    DataAccess.InsertError(new ExceptionDB() { Date = DateTime.Now, Message = "DeviceControl-GetRegisteredUsersIDs: " + ex.Message });
                }
            }

            return userCount;
        }

        public bool GetOneLog(TerminalDevice device, bool delete = false)
        {

            bool ok = false;

            oTerminal myTerminal = new oTerminal(device.IPAddress, device.ID, device.Port, device.AccKey);

            if (myTerminal.isAlive())
            {
                myTerminal.INT_ReceiveTimeOut = 10000;
                try
                {
                    device.LastLog = null;
                    device.SetOneLog((byte[])myTerminal.RetOneLog(delete));
                    ok = true;
                }
                catch (Exception ex)
                {
                    DataAccess.InsertError(new ExceptionDB() { Date = DateTime.Now, Message = "GetOneLog-GetOneLog: " + ex.Message });
                }
            }

            return ok;
        }

        public int GetLogs(TerminalDevice device)
        {
            int logCount = 0;

            oTerminal myTerminal = new oTerminal(device.IPAddress, device.ID, device.Port, device.AccKey);

            if (myTerminal.isAlive())
            {
                myTerminal.INT_ReceiveTimeOut = 10000;
                try
                {
                    device.SetLogs(myTerminal.RetAllLog() as ArrayList);
                    logCount = device.Logs.Count();
                }
                catch (Exception ex)
                {
                    DataAccess.InsertError(new ExceptionDB() { Date = DateTime.Now, Message = "DeviceControl-GetLogs: " + ex.Message });
                }
            }

            return logCount;
        }

        public int GetUserData(TerminalDevice device, string cardID)
        {
            int userCount = 0;
            cardID = cardID.PadLeft(10, '0');
            oTerminal myTerminal = new oTerminal(device.IPAddress, device.ID, device.Port, device.AccKey);

            if (myTerminal.isAlive())
            {
                myTerminal.INT_ReceiveTimeOut = 10000;
                try
                {
                    device.AddUserData(myTerminal.GetUserData(cardID), cardID);
                    userCount = device.Users.Count();
                }
                catch (Exception ex)
                {
                    DataAccess.InsertError(new ExceptionDB() { Date = DateTime.Now, Message = "DeviceControl-GetUserData: " + ex.Message });
                }
            }
            return userCount;
        }

        public bool ClearLogs(TerminalDevice device)
        {
            bool ok = false;

            oTerminal myTerminal = new oTerminal(device.IPAddress, device.ID, device.Port, device.AccKey);

            if (myTerminal.isAlive())
            {
                ok = myTerminal.DeleteAllLog();
            }

            return ok;
        }
    }


}
