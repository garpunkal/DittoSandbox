using System.IO;
using System.Text;
using DittoSandbox.Web.Logic.Search.Config;
using Examine;
using Newtonsoft.Json;
using Umbraco.Core;
using Umbraco.Web.Models;

namespace DittoSandbox.Web.Logic.Search
{
    public class EventHandlers : IApplicationEventHandler
    {
        public void OnApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            ExamineManager.Instance.IndexProviderCollection["ExternalIndexer"].GatheringNodeData += (s, e) =>
            {
                if (e.Fields.ContainsKey(StaticValues.Properties.Path))
                    e.Fields[StaticValues.Properties.SearchPath] = e.Fields[StaticValues.Properties.Path].Replace(',', ' ');

                // Extract the filename from media items
                if (e.Fields.ContainsKey(StaticValues.Properties.UmbracoFile))
                {
                    try
                    {
                        var imageCrop = JsonConvert.DeserializeObject<ImageCropDataSet>(e.Fields[StaticValues.Properties.UmbracoFile]);
                        e.Fields[StaticValues.Properties.UmbracoFileName] = Path.GetFileName(imageCrop.Src);
                    }
                    catch
                    {
                        e.Fields[StaticValues.Properties.UmbracoFileName] = Path.GetFileName(e.Fields[StaticValues.Properties.UmbracoFile]);
                    }
                }

                // Stuff all the fields into a single field for easier searching
                var combinedFields = new StringBuilder();
                foreach (var keyValuePair in e.Fields)
                    combinedFields.AppendLine(keyValuePair.Value);

                e.Fields.Add(StaticValues.Properties.Contents, combinedFields.ToString());
            };
        }

        public void OnApplicationInitialized(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }

        public void OnApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
        }
    }
}