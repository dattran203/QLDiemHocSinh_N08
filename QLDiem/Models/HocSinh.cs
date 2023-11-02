using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLDiem.Models
{
    public partial class HocSinh
    {
        public HocSinh()
        {
            HocSinhDiems = new HashSet<HocSinhDiem>();
            KqHocKies = new HashSet<KqHocKy>();
            KqNamHocs = new HashSet<KqNamHoc>();
        }
        [Required(ErrorMessage = "Bạn chưa nhập mã học sinh")]
        public string MaHs { get; set; } = null!;

        [StringLength(100, MinimumLength = 4, ErrorMessage = "Số kí tự phải nằm trong khoảng từ 4 đến 100")]
        [Required(ErrorMessage = "Bạn chưa nhập tên")]
        public string? TenHs { get; set; }

        [Required(ErrorMessage = "Bạn chưa chọn nhập mã lớp")]
        public string? Malop { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Ngày sinh không hợp lệ")]
        [Required(ErrorMessage = "Ngày sinh không hợp lệ")]
        public DateTime? Ngaysinh { get; set; }

        [Required(ErrorMessage = "Bạn chưa chọn giới tính")]
        public string? Gioitinh { get; set; }

        [StringLength(100, MinimumLength = 4, ErrorMessage = "Số kí tự phải nằm trong khoảng từ 4 đến 100")]
        [Required(ErrorMessage = "Bạn chưa nhập tên bố")]
        public string? HotenBo { get; set; }

        [StringLength(100, MinimumLength = 4, ErrorMessage = "Số kí tự phải nằm trong khoảng từ 4 đến 100")]
        [Required(ErrorMessage = "Bạn chưa nhập tên mẹ")]
        public string? HotenMe { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Bạn chưa nhập địa chỉ")]
        public string? Diachi { get; set; }

        [StringLength(11, MinimumLength = 10)]
        [Required(ErrorMessage = "Bạn chưa nhập số điện thoại")]
        public string? Dienthoai { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Ngày vào đoàn không hợp lệ")]
        [Required(ErrorMessage = "Ngày vào đoàn không hợp lệ")]
        public DateTime? NgayvaoDoan { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Ghichu { get; set; }

        public virtual Lop? MalopNavigation { get; set; }
        public virtual ICollection<HocSinhDiem> HocSinhDiems { get; set; }
        public virtual ICollection<KqHocKy> KqHocKies { get; set; }
        public virtual ICollection<KqNamHoc> KqNamHocs { get; set; }
    }
}
