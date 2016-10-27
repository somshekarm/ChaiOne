using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManifestModels
{
    public class BaseModel
    {        
        public string Uri { get; private set; }
        public void AddUri(string uri)
        {
            Uri = uri;
        }
    }
}
