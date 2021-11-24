using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TrashVacBackEnd.Core.Repository
{
    public class FileLogRepository : ILogRepository
    {
        public long WriteToDoorAccessLog(string rfId, string doorId, Guid userId, bool accessStatus)
        {
            var logWriter = File.AppendText(@"C:\KalleTemp\TVLog\accesslog.log");
            logWriter.WriteLine($"{DateTime.Now}\t{rfId}\t{doorId}\t{userId}\t{accessStatus}");
            logWriter.Close();
            return 0;

        }
    }
}
