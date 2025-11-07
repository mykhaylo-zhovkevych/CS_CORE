using Newtonsoft.Json;
using System.Xml;
using System.IO;
using AdapterStockApp.External;

namespace AdapterStockApp
{
    public class StockAdapter : IReporter
    {

        private AnalyticsLibraryProvider _analytics;

        public StockAdapter(AnalyticsLibraryProvider analytics)
        {
            _analytics = analytics;
        }


        public string GetReport()
        {
            var processedData = _analytics.PreProcessData();

            XmlDocument xmlDoc = JsonConvert.DeserializeXmlNode(processedData, "Root");

            using (StringWriter sw = new StringWriter())
            using (XmlTextWriter writer = new XmlTextWriter(sw) { Formatting = System.Xml.Formatting.Indented })
            {
                xmlDoc.Save(writer);
                string formattedXml = sw.ToString();

                return formattedXml;
            }
        }
    }
}