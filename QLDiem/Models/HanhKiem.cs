using System;
using System.Collections.Generic;

namespace QLDiem.Models
{
    public partial class HanhKiem
    {
        public HanhKiem()
        {
            KqHocKies = new HashSet<KqHocKy>();
            KqNamHocs = new HashSet<KqNamHoc>();
        }

        public string MaHanhKiem { get; set; } = null!;
        public string? TenHanhKiem { get; set; }

        public virtual ICollection<KqHocKy> KqHocKies { get; set; }
        public virtual ICollection<KqNamHoc> KqNamHocs { get; set; }
    }
}
