using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLDiem.Models
{
    public partial class GiaoVien
    {
        public GiaoVien()
        {
            TblUsers = new HashSet<TblUser>();
        }

        [Required(ErrorMessage = "Bạn chưa nhập mã giáo viên")]

        public string MaGv { get; set; } = null!;

        [Required(ErrorMessage = "Bạn chưa nhập tên giáo viên")]

        public string? TenGv { get; set; }

        [Range(typeof(DateTime), "1/1/1970", "12/31/2002", ErrorMessage = "Ngày sinh không hợp lệ")]
        [DataType(DataType.Date, ErrorMessage = "Ngày sinh không hợp lệ")]
        [Required(ErrorMessage = "Ngày sinh không hợp lệ")]
        public DateTime? Ngaysinh { get; set; }

        [Required(ErrorMessage = "Bạn chưa chọn giới tính")]

        public string? Gioitinh { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mã môn")]

        public string? MaMon { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
        public string? Diachi { get; set; }

        [StringLength(11, MinimumLength = 10)]
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        public string? Dienthoai { get; set; }

        public virtual MonHoc? MaMonNavigation { get; set; }
        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
