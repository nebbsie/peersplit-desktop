using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace peersplit_desktop.Model.APIResponse
{
    class LoginResponse
    {
        public bool success { get; set; }
        public Data data { get; set; }
    }

    class Data
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public int storageUsage { get; set; }
        public int storageTier { get; set; }
    }
}
