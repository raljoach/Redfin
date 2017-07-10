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
        private string[] _files;

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
            _files = new string[] { filePath };
        }
        public virtual IEnumerable<Tuple<Uri, string>> Read()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<Tuple<Uri,string>> ReadBulk(Action callback)
        {
            throw new NotImplementedException();
        }

        private StreamReader _sr=null;
        private Action<string> _callback;
        private int _current;
        private bool _newFile;
        public virtual async Task ReadAsync(Action<string> callback)
        {
            _callback = callback;
            _current = 0;
            _newFile = true;
            // this line will cause the method to be suspended and return caller at this point
            await Next();
        }

        public async Task Next()
        {
            string line = string.Empty;
            if(_newFile)
            {
                if(_current==_files.Length)
                {
                    _callback(null);
                    return;
                }
                var file = _files[_current];
                if(_sr!=null)
                {
                    _sr.Close();
                }
                _sr = new StreamReader(new FileStream(file, FileMode.Open, FileAccess.Read/*, FileShare.Read*/));
                _newFile = false;   
            }
            // this line will cause the method to be suspended and return caller at this point
            line = await _sr.ReadLineAsync();
            if(line==null)
            {
                _current++;
                _newFile = true;
                Next();
            }
            _callback(line);
        }
    }
}
