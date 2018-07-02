using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace peersplit_desktop.Model
{
    public class UserSettings
    {
        public UserSettings() { }

        public string _username { get; set; }
        public string _password { get; set; }
        public string _peersplitFolder { get; set; }
        public int _id { get; set; }
        public string _email { get; set; }
        public int _storageUsage { get; set; }
        public int _storageTier { get; set; }
        public bool _allowStorage { get; set; }
        public int _storageAmount { get; set; }
    }
}
