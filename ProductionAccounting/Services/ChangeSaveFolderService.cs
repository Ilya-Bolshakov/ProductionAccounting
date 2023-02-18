using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProductionAccounting.Services.Interfaces;

namespace ProductionAccounting.Services
{
    class ChangeSaveFolderService : IChangeSaveFolderService
    {
        public string FolderPath { get; private set; }
        public bool ShowDialog()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FolderPath = fbd.SelectedPath;
                return true;
            }
            return false;
        }

        public void EditFolder()
        {
            var appSettingsPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "appsettings.json");
            var json = File.ReadAllText(appSettingsPath);

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(new ExpandoObjectConverter());
            jsonSettings.Converters.Add(new StringEnumConverter());

            dynamic config = JsonConvert.DeserializeObject<ExpandoObject>(json, jsonSettings);

            var expando = config as IDictionary<string, object>;
            expando["SavePdfsFolder"] = FolderPath;

            var newJson = JsonConvert.SerializeObject(config, Formatting.Indented, jsonSettings);

            File.WriteAllText(appSettingsPath, newJson);
        }
    }
}
