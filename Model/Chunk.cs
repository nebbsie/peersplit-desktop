using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace peersplit_desktop.Model
{
    public class Chunk
    {
        public int id { get; set; }
        public string chunkName { get; set; }
        public int fk_holderID { get; set; }
        public int fk_fileID { get; set; }

        public Chunk() { }
    }
}
