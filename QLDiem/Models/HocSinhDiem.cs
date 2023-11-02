using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QLDiem.Models
{
    public partial class HocSinhDiem
    {
        [Required(ErrorMessage = "Bạn chưa nhập mã học sinh")]
        public string Mahocsinh { get; set; } = null!;

        [Required(ErrorMessage = "Bạn chưa nhập mã kỳ học")]

        public int Kyhoc { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập năm học")]
        public int NamHocID { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mã môn")]

        public string Mamon { get; set; } = null!;

        [Range(0.0, 10.0, ErrorMessage = "Giá trị phải trong khoảng từ 0 đến 10")]
        [Required(ErrorMessage = "Bạn chưa nhập điểm 15 phút")]
        public double? Diem15p { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Giá trị phải trong khoảng từ 0 đến 10")]
        [Required(ErrorMessage = "Bạn chưa nhập điểm miệng")]
        public double? DiemMieng { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Giá trị phải trong khoảng từ 0 đến 10")]
        [Required(ErrorMessage = "Bạn chưa nhập điểm 1 tiết")]
        public double? Diem1Tiet { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Giá trị phải trong khoảng từ 0 đến 10")]
        [Required(ErrorMessage = "Bạn chưa nhập điểm giữa kì")]
        public double? DiemGk { get; set; }

        [Range(0.0, 10.0, ErrorMessage = "Giá trị phải trong khoảng từ 0 đến 10")]
        [Required(ErrorMessage = "Bạn chưa nhập điểm thi học kì")]
        public double? DiemthiHk { get; set; }
        public double? DiemTbm { get; set; }

        public virtual HocSinh MahocsinhNavigation { get; set; } = null!;
        public virtual MonHoc MamonNavigation { get; set; } = null!;
        public virtual NamHoc NamHocIDNavigation { get; set; } = null!;
    }
}
