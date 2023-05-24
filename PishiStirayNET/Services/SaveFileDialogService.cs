using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using Ookii.Dialogs.Wpf;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PishiStiray.Services
{
    public class SaveFileDialogService
    {
        public string FilePath { get; set; }
        public string ImageSaveFileDialog()
        {
            OpenFileDialog saveFileDialog = new();
            saveFileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";

            var result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                string filepath = saveFileDialog.FileName;

                string resourcepath = Path.GetFullPath("Resources");

                byte[] data = default(byte[]);

                try
                {
                    using (FileStream fileStream = File.Create($"{resourcepath}/{Path.GetFileName(filepath)}")) { }
                    using (var stream = File.Open(filepath, FileMode.Open))
                    {
                        var reader = new StreamReader(stream);
                        using (var memstream = new MemoryStream())
                        {
                            reader.BaseStream.CopyTo(memstream);
                            data = memstream.ToArray();
                        }
                    }

                    File.WriteAllBytes($"{resourcepath}/{Path.GetFileName(filepath)}", data);
                }
                catch { }

                FilePath = Path.GetFileName(filepath);
                return FilePath;
            }
            return FilePath;
        }
        public string PDFSaveFileDialog()
        {
            VistaFolderBrowserDialog saveFileDialog = new();

            var result = saveFileDialog.ShowDialog();
            if(result == true)
            {
                return saveFileDialog.SelectedPath;
            }
            return "no folder";
        }
    }
}
