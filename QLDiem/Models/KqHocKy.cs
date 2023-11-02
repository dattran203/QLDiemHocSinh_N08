using System;
using System.Collections.Generic;

namespace QLDiem.Models
{
    public partial class KqHocKy
    {
        public string MaHs { get; set; } = null!;
        public int? Kyhoc { get; set; }
        public int NamHocId { get; set; }
        public string MaHanhKiem { get; set; } = null!;
        public double? DiemTrungBinhHocKy { get; set; }

        public virtual HanhKiem MaHanhKiemNavigation { get; set; } = null!;
        public virtual HocSinh MaHsNavigation { get; set; } = null!;
        public virtual NamHoc NamHoc { get; set; } = null!;
    }
}
