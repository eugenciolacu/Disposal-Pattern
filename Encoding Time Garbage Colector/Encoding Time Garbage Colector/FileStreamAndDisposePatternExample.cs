using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Encoding_Time_Garbage_Colector
{
    public class FileStreamAndDisposePatternExample : IDisposable
    {
        private FileStream fileStream;
        private string _path;
        
        private bool _disposed = false; 

        public FileStreamAndDisposePatternExample (string path)
        {
            _path = @$"{path}\time.txt";
        }

        ~FileStreamAndDisposePatternExample() => Dispose(false);

        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose (bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                fileStream.Close();
            }

            _disposed = true;

            // Call base.Dispose(disposing) if have parent
        }

        public void AddDateToFile ()
        {
            fileStream = File.OpenWrite(_path);
            byte[] time = Encoding.Unicode.GetBytes(string.Format("{0:F}", DateTime.Now));
            fileStream.Write(time);
            fileStream.Close();
        }

        public string GetDateFromFile ()
        {
            fileStream = File.OpenRead(_path);
            byte[] tmp = new byte[fileStream.Length];
            fileStream.Read(tmp);
            //fileStream.Close();
            return Encoding.Unicode.GetString(tmp);
        }
    }
}
