using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETerminal
{
    class TerminalController
    {
        public void StartCollectingLogs(bool all = false, bool clean = false)
        {
            var devControl = DeviceControl.GetDevControl;

            List<DeviceDB> devices = DataAccess.GetInstalledDevices();
            List<Log> logsAll = new List<Log>();
            foreach (var deviceDb in devices)
            {
                TerminalDevice device = new TerminalDevice(deviceDb);
                if (all)
                {
                    if (devControl.GetLogs(device) > 0)
                    {
                        logsAll.AddRange(device.Logs);
                    }
                }
                else
                {
                    if (devControl.GetOneLog(device, clean))
                    {
                        logsAll.Add(device.LastLog);
                    }
                }

            }
            if (logsAll.Count() > 0)
                DataAccess.InsertLogs(logsAll);
        }

        public void StartCollectUserData()//heavy load method - use with caution!!!!
        {
            var devControl = DeviceControl.GetDevControl;

            List<DeviceDB> devices = DataAccess.GetInstalledDevices();
            List<User> usersData = new List<User>();
            foreach (var deviceDb in devices)
            {
                TerminalDevice device = new TerminalDevice(deviceDb);

                //devControl.GetUserData(device, "1235226");

                if (devControl.GetRegisteredUsersIDs(device) > 0)
                {
                    foreach (var cardID in device.UsersIDs)
                    {
                        devControl.GetUserData(device, cardID);
                    }
                    usersData.AddRange(device.Users);
                }

            }
            if (usersData.Count() > 0)
                DataAccess.InsertUserData(usersData);

        }

        public void StartCleaninDevicesLogs()
        {
            var devControl = DeviceControl.GetDevControl;

            List<DeviceDB> devices = DataAccess.GetInstalledDevices();
            foreach (var deviceDb in devices)
            {
                TerminalDevice device = new TerminalDevice(deviceDb);

                devControl.ClearLogs(device);
            }
        }
    }
}
