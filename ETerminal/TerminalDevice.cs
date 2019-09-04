using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETerminal
{
    class TerminalDevice
    {
        public int Port { get; set; }
        public int ID { get; set; }
        public string IPAddress { get; set; }
        public string AccKey { get; set; }

        public string SerialNumber { get; set; }
        public long CurrentRegUsersCount { get; set; }
        public long LogsCount { get; set; }
        public long MaxRegUsersCount { get; set; }
        public long MaxLogs { get; set; }
        public string TerminalPassword { get; set; }
        public string WebLogOnPassword { get; set; }
        public string TimeOfTerminal { get; set; }
        public string DateOfTerminal { get; set; }
        public DateTime TerminalDateTime { get; set; }

        public Log LastLog { get; set; }
        public List<Log> Logs { get; set; }
        public List<User> Users { get; set; }
        public List<string> UsersIDs { get; set; }

        public List<string> Errors { get; set; }

        public TerminalDevice(string ip, int id, int port)
        {
            this.ID = id;
            this.IPAddress = ip;
            this.Port = port;
        }

        public TerminalDevice(DeviceDB device)
        {
            ID = device.DID;
            IPAddress = device.IPAddress;
            Port = device.Port;
            AccKey = device.Key;
        }

        public void SetLogs(ArrayList logList)
        {
            if (logList != null)
            {
                var socketData = (byte[])logList[0];
                LogAdapter logAdapter = new LogAdapter(socketData);

                Logs = logAdapter.GetLogs();
            }
            else
                Logs = new List<Log>();
        }

        public void SetOneLog(byte[] logRaw)
        {            
            Log log = new Log(logRaw, -1);
            LastLog = log;
        }

        public void AddUserData(Array userdata, string CardID)
        {
            if (userdata != null)
            {
                UserAdapter userAdapter = new UserAdapter((byte[])userdata);

                if (Users == null)
                {
                    Users = new List<User>();
                }

                if (!string.IsNullOrEmpty(userAdapter.GetUser().EmployeeID))
                {
                    userAdapter.GetUser().CardID = CardID;
                    if (Users.Where(x => x.EmployeeID == userAdapter.GetUser().EmployeeID && x.CardID == userAdapter.GetUser().CardID).Count() == 0)
                        Users.Add(userAdapter.GetUser());

                }
            }
        }

    }

}
