using System;
using System.Collections.Generic;

namespace QLDiem.Models
{
    public partial class Lop
    {
        public Lop()
        {
            HocSinhs = new HashSet<HocSinh>();
        }

        public string Malop { get; set; } = null!;
        public string? Tenlop { get; set; }
        public string? Gvcn { get; set; }
        public int? Siso { get; set; }
        public int? NamHocId { get; set; }

        public virtual NamHoc? NamHoc { get; set; }
        public virtual ICollection<HocSinh> HocSinhs { get; set; }
    }
}
