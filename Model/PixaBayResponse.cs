using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppParsePicture.Model
{
    public class PixaBayResponse
    {
        public int Total { get; set; }
        public int TotalHits { get; set; }
        public PixaBayImage[] Hits { get; set; }

        public PixaBayResponse()
        {

        }
    }
}
