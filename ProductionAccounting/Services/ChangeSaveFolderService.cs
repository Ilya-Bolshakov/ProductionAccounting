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
            var stream = File.Open("savePdfsFolder.txt", FileMode.Create);
            var sr = new StreamWriter(stream);
            sr.Write(FolderPath);
            sr.Close();
            stream.Close();
        }
    }
}
