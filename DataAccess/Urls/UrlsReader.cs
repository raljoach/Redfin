using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redfin.DataAccess.Urls
{
    public class UrlsReader
    {
        private string[] files;
        public string CurrentFile { get; private set; }

        public UrlsReader(string filePath)
        {
            if(string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException(
                    "Error: filePath cannot be null, empty or blank");
            }
            if (!File.Exists(filePath))
            {
                throw new ArgumentException(
                    string.Format("Error: File path does not exist: '{0}'!", filePath));
            }
            files = new string[] { filePath };
        }
        public virtual IEnumerable<Tuple<Uri, string>> ReadLine()
        {
            throw new NotImplementedException();
        }
    }
}
