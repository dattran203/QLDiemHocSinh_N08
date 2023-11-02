using System;
using System.Collections.Generic;

namespace QLDiem.Models
{
    public partial class MonHoc
    {
        public MonHoc()
        {
            GiaoViens = new HashSet<GiaoVien>();
            HocSinhDiems = new HashSet<HocSinhDiem>();
        }

        public string Mamon { get; set; } = null!;
        public string? Tenmon { get; set; }

        public virtual ICollection<GiaoVien> GiaoViens { get; set; }
        public virtual ICollection<HocSinhDiem> HocSinhDiems { get; set; }
    }
}
