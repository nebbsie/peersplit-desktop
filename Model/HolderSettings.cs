using System.Collections.Generic;

namespace peersplit_desktop.Model
{
    public class HolderSettings : Settings
    {
        public string name { get; set; }
        public int id { get; set; }
        public bool activeHolder { get; set; }
        public int storageAmount { get; set; }
        public List<Chunk> chunks { get; set; }

        public HolderSettings()
        {
            name = "not_used";
            id = 0;
            storageAmount = 20;
            activeHolder = false;
            chunks = new List<Chunk>();
        }

    }

}
