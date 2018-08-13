using Interfaces;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Factory
{
  
    public class DataProviderFactory
    {
        private IUnityContainer container { get; set; }
        public DataProviderFactory()
        {
            container = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection(Constants.Unity);
            section.Configure(container, Constants.DataAccessProviderContainer);
        }
        public IDataProvider CreateDataProvider()
        {
            return container.Resolve<IDataProvider>();
        }
        public ILog CreateLogProvider()
        {
            return container.Resolve<ILog>();
        }
        private static class Constants
        {
            public const string DataAccessProviderContainer = "DataAccessProvider";
            public const string Unity = "unity";
        }
    }
}
