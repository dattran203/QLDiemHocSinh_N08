using System;
using System.Collections.Generic;

namespace QLDiem.Models
{
    public partial class TblUser
    {
        public string TaiKhoan { get; set; } = null!;
        public string? MatKhau { get; set; }
        public string? MaGv { get; set; }

        public virtual GiaoVien? MaGvNavigation { get; set; }
    }
}
