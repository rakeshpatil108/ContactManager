using Factory;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public sealed class LogHandler
    {
        private static readonly object padlock = new object();
        private ILog _LogProvider;
        private static LogHandler instance = null;

        public static LogHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new LogHandler();
                        }
                    }
                }
                return instance;
            }
        }
        LogHandler()
        {
            DataProviderFactory dataProviderFactory = new DataProviderFactory();
            _LogProvider = dataProviderFactory.CreateLogProvider();
        }
        public void WriteLog(string Message)
        {
            _LogProvider.LogException(Message);
        }

    }
}
