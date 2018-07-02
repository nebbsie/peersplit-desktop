using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace peersplit_desktop.Model.APIResponse
{
    class FilmResponse
    {
        public bool success { get; set; }
        public List<Film> data { get; set; }
    }

    class Film
    {
        private double _size;
        public double size
        {
            get
            {
                double mult = Math.Pow(10.0, 2);
                double result = Math.Truncate(mult * _size) / mult;
                return result;
            }
            set { _size = value / 1024; }
      
        }

        public int id { get; set; }
        public string filename { get; set; }
        public int chunks { get; set; }
        public bool ownerID { get; set; }
    }
}
