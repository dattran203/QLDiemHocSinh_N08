using System;
using System.Collections.Generic;

namespace QLDiem.Models
{
    public partial class KqNamHoc
    {
        public string MaHs { get; set; } = null!;
        public int NamHocId { get; set; }
        public string MaHanhKiem { get; set; } = null!;
        public double? DiemTrungBinhNamHoc { get; set; }

        public virtual HanhKiem MaHanhKiemNavigation { get; set; } = null!;
        public virtual HocSinh MaHsNavigation { get; set; } = null!;
        public virtual NamHoc NamHoc { get; set; } = null!;
    }
}
