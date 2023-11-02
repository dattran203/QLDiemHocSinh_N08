using System;
using System.Collections.Generic;

namespace QLDiem.Models
{
    public partial class NamHoc
    {
        public NamHoc()
        {
            KqHocKies = new HashSet<KqHocKy>();
            KqNamHocs = new HashSet<KqNamHoc>();
            Lops = new HashSet<Lop>();
        }

        public int NamHocId { get; set; }
        public string? NamHoc1 { get; set; }

        public virtual ICollection<KqHocKy> KqHocKies { get; set; }
        public virtual ICollection<KqNamHoc> KqNamHocs { get; set; }
        public virtual ICollection<Lop> Lops { get; set; }
    }
}
