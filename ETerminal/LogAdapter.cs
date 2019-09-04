using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETerminal
{
    class LogAdapter
    {
        List<Log> logs;
        public LogAdapter() { }
        public LogAdapter(byte[] socketData)
        {
            logs = new List<Log>();

            if (socketData.Length > 0)
            {
                if (socketData[7] == 0)
                {
                    Int64 nLogCount = HexToDec(socketData[8].ToString().Trim().PadLeft(2, '0') + socketData[9].ToString().Trim().PadLeft(2, '0')
                        + socketData[10].ToString().Trim().PadLeft(2, '0') + socketData[11].ToString().Trim().PadLeft(2, '0'));

                    Int64 nI;

                    for (nI = 0; nI < nLogCount; nI++)
                    {
                        byte[] dataLogs = { socketData[12 + nI * 16], socketData[13 + nI * 16], socketData[14 + nI * 16], socketData[15 + nI * 16], socketData[16 + nI * 16]
                        , socketData[17 + nI * 16], socketData[18 + nI * 16], socketData[19 + nI * 16], socketData[20 + nI * 16], socketData[21 + nI * 16], socketData[22 + nI * 16]
                        , socketData[23 + nI * 16], socketData[24 + nI * 16], socketData[25 + nI * 16], socketData[26 + nI * 16], socketData[27 + nI * 16]};

                        Log log = new Log(dataLogs, nI + 1);
                        logs.Add(log);
                    }
                }
            }
        }

        public List<Log> GetLogs()
        {
            return logs;
        }

        public static long HexToDec(string strHEX)
        {
            int x = strHEX.Length;
            int y, z;
            long rtn = 0;
            for (y = 0; y < strHEX.Length; y++)
            {
                x = strHEX.Length - y - 1;
                z = (int)strHEX.Substring(y, 1).ToCharArray()[0];
                if (z >= 65)
                    rtn = rtn + Convert.ToInt64(Math.Pow(16, x) * (z - 55));
                else
                    rtn = rtn + Convert.ToInt64(Math.Pow(16, x) * (z - 48));
            }
            return rtn;
        }

    }

    class Log
    {
        public Int64 LogNo { get; set; }
        public int Sec { get; set; }
        public int Min { get; set; }
        public int Hr { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime LogDate { get; set; }
        public DoorIndicator InOut { get; set; }
        public VerificationSource AccessType { get; set; }
        public int Verify { get; set; }
        public int FunctionCode { get; set; }
        public string UserID { get; set; }
        public UInt32 UserIDNum { get; set; }
        public string EmployeeID { get; set; }
        public int SlaveTID { get; set; }
        public int DoorNumber { get; set; }


        public Log() { }

        public Log(byte[] rawLog, Int64 Num)
        {
            if (rawLog.Length == 16)
            {
                LogNo = Num;
                Sec = rawLog[0];
                Min = rawLog[1];
                Hr = rawLog[2];
                Day = rawLog[3];
                Month = rawLog[4];
                Year = 2000 + rawLog[5];
                LogDate = new DateTime(Year, Month, Day, Hr, Min, Sec);
                InOut = (DoorIndicator)rawLog[6];
                AccessType = (VerificationSource)rawLog[7];
                Verify = rawLog[8];
                FunctionCode = rawLog[9];
                UserIDNum = (((UInt32)rawLog[10] << 24) + ((UInt32)rawLog[11] << 16) + ((UInt32)rawLog[12] << 8) + (UInt32)rawLog[13]);
                UserID = UserIDNum.ToString().Trim().PadLeft(10, '0');
                SlaveTID = rawLog[14];
                DoorNumber = rawLog[15];
            }

        }

        public enum DoorIndicator
        {
            None = 0,
            In = 1,
            Out = 2
        }

        public enum VerificationSource
        {
            None = 0,
            Finger = 1,
            Card = 2,
            CF = 3,
            PIN = 4,
            FP = 5,
            CP = 6,
            CFP = 7
        }
    }
}
