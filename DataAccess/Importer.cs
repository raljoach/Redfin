using Redfin.DataAccess.DB;
using Redfin.DataAccess.Urls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redfin.DataAccess
{
    public class Importer
    {
        private UrlsReader _reader;
        private RedfinContext _context;

        public Importer(UrlsReader reader, RedfinContext context)
        {
            _reader = reader;
            _context = context;
        }

        public virtual void AddRecord(string url, string rating, RedfinContext mlsContext)
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
