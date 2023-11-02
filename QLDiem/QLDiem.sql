create database QLDiem
use QLDiem
-- Tạo bảng NamHoc
CREATE TABLE NamHoc (
    NamHocID INT PRIMARY KEY,
    NamHoc NVARCHAR(50)
);

-- Tạo bảng Lop
CREATE TABLE Lop (
    Malop NVARCHAR(50) PRIMARY KEY,
    Tenlop NVARCHAR(50),
    GVCN NVARCHAR(50),
    Siso INT,
    NamHocID INT,
    FOREIGN KEY (NamHocID) REFERENCES NamHoc(NamHocID)
);

-- Tạo bảng HocSinh
CREATE TABLE HocSinh (
    MaHS NVARCHAR(50) PRIMARY KEY,
    TenHS NVARCHAR(50),
    Malop NVARCHAR(50),
    Ngaysinh DATE,
    Gioitinh NVARCHAR(50),
    HotenBo NVARCHAR(50),
    HotenMe NVARCHAR(50),
    Diachi NVARCHAR(50),
    Dienthoai NVARCHAR(50),
    NgayvaoDoan DATE,
    Ghichu NVARCHAR(MAX),
    FOREIGN KEY (Malop) REFERENCES Lop(Malop)
);

-- Tạo bảng MonHoc
CREATE TABLE MonHoc (
    Mamon NVARCHAR(50) PRIMARY KEY,
    Tenmon NVARCHAR(50)
);

CREATE TABLE GiaoVien (
    MaGV NVARCHAR(50) PRIMARY KEY,
    TenGV NVARCHAR(50),
    Ngaysinh DATE,
    Gioitinh NVARCHAR(50),
    MaMon NVARCHAR(50),
    Diachi NVARCHAR(50),
    Dienthoai NVARCHAR(50),
    FOREIGN KEY (MaMon) REFERENCES MonHoc(Mamon)
);

-- Tạo bảng HocSinh_Diem
CREATE TABLE HocSinh_Diem (
    Mahocsinh NVARCHAR(50),
    Kyhoc INT,
	NamHocID INT,
    Mamon NVARCHAR(50),
    Diem15p FLOAT,
    DiemMieng FLOAT,
    Diem1Tiet FLOAT,
    DiemGK FLOAT,
    DiemthiHK FLOAT,
    DiemTBM FLOAT,
    PRIMARY KEY (Mahocsinh,Kyhoc, Mamon),
    FOREIGN KEY (Mahocsinh) REFERENCES HocSinh(MaHS),
    FOREIGN KEY (Mamon) REFERENCES MonHoc(Mamon),
	FOREIGN KEY (NamHocID) REFERENCES NamHoc(NamHocID)
);


CREATE TABLE HanhKiem (
    MaHanhKiem NVARCHAR(50) PRIMARY KEY,
    TenHanhKiem NVARCHAR(50)
);

CREATE TABLE KQ_HocKy (
    MaHS NVARCHAR(50),
    Kyhoc INT,
    NamHocID INT,
    MaHanhKiem NVARCHAR(50),
    DiemTrungBinhHocKy FLOAT,
    PRIMARY KEY (MaHS, MaHanhKiem, NamHocID),
    FOREIGN KEY (MaHS) REFERENCES HocSinh(MaHS),
    FOREIGN KEY (MaHanhKiem) REFERENCES HanhKiem(MaHanhKiem),
    FOREIGN KEY (NamHocID) REFERENCES NamHoc(NamHocID)
);

CREATE TABLE KQ_NamHoc (
    MaHS NVARCHAR(50),
    NamHocID INT,
    MaHanhKiem NVARCHAR(50),
    DiemTrungBinhNamHoc FLOAT,
    PRIMARY KEY (MaHS, MaHanhKiem, NamHocID),
    FOREIGN KEY (MaHS) REFERENCES HocSinh(MaHS),
    FOREIGN KEY (MaHanhKiem) REFERENCES HanhKiem(MaHanhKiem),
    FOREIGN KEY (NamHocID) REFERENCES NamHoc(NamHocID)
);


CREATE TABLE tblUser (
    TaiKhoan NVARCHAR(50) PRIMARY KEY,
    MatKhau NVARCHAR(50),
	MaGV NVARCHAR(50),
	FOREIGN KEY (MaGV) REFERENCES GiaoVien(MaGV)
);
INSERT INTO NamHoc (NamHocID, NamHoc)
VALUES
    (1, N'2020-2021'),
    (2, N'2021-2022'),
    (3, N'2022-2023');

INSERT INTO Lop (Malop, Tenlop, GVCN, Siso,NamHocID)
VALUES
    (N'L1N1', N'10A3', N'MGV3', 35, 1),
    (N'L1N2', N'10A1', N'MGV1', 30, 2),
    (N'L2N1', N'10A2', N'MGV2', 35, 2);


INSERT INTO MonHoc (Mamon, Tenmon)
VALUES
    (N'MH1', N'Toán'),
    (N'MH2', N'Văn'),
    (N'MH3', N'Lý'),
    (N'MH4', N'Hóa'),
    (N'MH5', N'Sinh'),
    (N'MH6', N'Anh văn');

INSERT INTO HanhKiem (MaHanhKiem, TenHanhKiem)
VALUES
    (N'HK1', N'Tốt'),
    (N'HK2', N'Khá'),
    (N'HK3', N'Trung bình'),
    (N'HK4', N'Yếu kém');

INSERT INTO GiaoVien (MaGV, TenGV, Ngaysinh, Gioitinh, MaMon, Diachi, Dienthoai)
VALUES
	(N'admin', N'admin', '1985-12-01', N'Nam', null, N'Hà Nội', N'0231241412'),
    (N'MGV1', N'Nguyễn Thị Trang', '1980-02-10', N'Nữ', N'MH1', N'Hà Nội', N'0123456789'),
    (N'MGV2', N'Phạm Văn Công', '1975-05-20', N'Nam', N'MH2', N'Hồ Chí Minh', N'0987654321'),
    (N'MGV3', N'Lê Văn Đạt', '1982-08-15', N'Nam', N'MH3', N'Hải Phòng', N'0123456789'),
    (N'MGV4', N'Trần Thị Lành', '1978-03-25', N'Nữ', N'MH4', N'Hà Nội', N'0987654321'),
    (N'MGV5', N'Huỳnh Văn Chiến', '1985-12-01', N'Nam', N'MH1', N'Đà Nẵng', N'0123456789');

INSERT INTO tblUser (TaiKhoan, MatKhau, MaGV)
VALUES 
	(N'nguyenthitrang',N'nguyenthitrang',N'MGV1'),
	(N'levandat',N'levandat',N'MGV3'),
	(N'admin',N'admin',N'admin');

-- Thêm dữ liệu cho 10 học sinh và chỉnh sửa MaLop
INSERT INTO HocSinh (MaHS, TenHS, Malop, Ngaysinh, Gioitinh, HotenBo, HotenMe, Diachi, Dienthoai, NgayvaoDoan, Ghichu)
VALUES
    (N'HS1', N'Nguyễn Văn An', N'L1N1', '2006-01-15', N'Nam', N'Nguyễn Văn Công', N'Trần Thị Linh', N'Hà Nội', N'0123456789', '2020-05-01', NULL),
    (N'HS2', N'Trần Thị Hằng', N'L1N1', '2007-03-20', N'Nữ', N'Trần Văn Chiến', N'Nguyễn Thị Nhài', N'Hồ Chí Minh', N'0987654321', '2020-04-15', NULL),
    (N'HS3', N'Phạm Văn Cường', N'L1N2', '2006-02-10', N'Nam', N'Phạm Văn Giang', N'Nguyễn Thị Lam', N'Hà Nội', N'0123456589', '2020-05-01', NULL),
    (N'HS4', N'Lê Thị Hương', N'L1N2', '2007-04-25', N'Nữ', N'Lê Văn Đạt', N'Phạm Thị Hồng', N'Hải Phòng', N'0987654321', '2020-03-10', NULL),
    (N'HS5', N'Hoàng Văn Duy', N'L1N2', '2006-03-05', N'Nam', N'Hoàng Văn Anh', N'Nguyễn Thị Anh', N'Đà Nẵng', N'0123456789', '2020-05-01', NULL),
    (N'HS6', N'Trần Thị Lan', N'L1N2', '2006-03-20', N'Nữ', N'Trần Văn Bình', N'Nguyễn Thị Dung', N'Hà Nội', N'0123456789', '2020-05-01', NULL),
    (N'HS7', N'Nguyễn Văn Sơn', N'L1N2', '2007-05-10', N'Nam', N'Nguyễn Văn Bách', N'Nguyễn Thị Hoa Mai', N'Hà Nội', N'0987654321', '2020-04-15', NULL),
    (N'HS8', N'Phạm Thị Hoa', N'L1N2', '2006-02-10', N'Nữ', N'Phạm Văn Chung', N'Trần Thị Hoa', N'Hà Nội', N'0123456789', '2020-05-01', NULL),
    (N'HS9', N'Lê Văn Bình', N'L1N2', '2006-02-10', N'Nam', N'Lê Văn Huy', N'Trần Thị Hiền', N'Hà Nội', N'0123456789', '2020-05-01', NULL),
    (N'HS10', N'Nguyễn Thị Thảo', N'L1N2', '2006-04-25', N'Nữ', N'Nguyễn Văn Tuấn', N'Trần Thị Lành', N'Hải Phòng', N'0987654321', '2020-03-10', NULL);


INSERT INTO HocSinh_Diem (Mahocsinh, Kyhoc, Mamon, Diem15p, DiemMieng, Diem1Tiet, DiemGK, DiemthiHK)
VALUES
    (N'HS1', 1, N'MH1', 9.5, 8.0, 7.5, 8.0, 8.5),
    (N'HS1', 1, N'MH2', 7.5, 7.0, 7.0, 7.5, 7.5),
    (N'HS1', 1, N'MH3', 8.0, 7.5, 8.0, 7.0, 8.0),
    (N'HS1', 1, N'MH4', 7.0, 6.5, 7.5, 7.0, 7.0),
    (N'HS1', 1, N'MH5', 8.5, 8.0, 7.5, 8.0, 8.5),
    (N'HS1', 1, N'MH6', 7.0, 6.5, 6.0, 6.5, 7.0),
	(N'HS1', 2, N'MH1', 8.5, 7.5, 7.5, 7.0, 8.0),
    (N'HS1', 2, N'MH2', 7.0, 6.5, 7.0, 7.5, 7.5),
    (N'HS1', 2, N'MH3', 7.5, 7.0, 7.5, 7.0, 7.0),
    (N'HS1', 2, N'MH4', 8.0, 7.5, 8.0, 8.5, 8.5),
    (N'HS1', 2, N'MH5', 8.0, 7.5, 7.5, 7.0, 7.5),
    (N'HS1', 2, N'MH6', 9.0, 8.5, 8.5, 8.0, 8.0),
	(N'HS2', 1, N'MH1', 8.0, 7.5, 8.0, 7.5, 8.0),
    (N'HS2', 1, N'MH2', 7.5, 7.0, 7.0, 7.5, 7.5),
    (N'HS2', 1, N'MH3', 8.5, 8.0, 8.5, 8.0, 8.5),
    (N'HS2', 1, N'MH4', 7.0, 6.5, 7.0, 6.5, 7.0),
    (N'HS2', 1, N'MH5', 9.0, 8.5, 9.0, 8.0, 9.0),
    (N'HS2', 1, N'MH6', 7.5, 7.0, 7.5, 7.0, 7.5),
	(N'HS2', 2, N'MH1', 8.0, 7.5, 7.5, 7.0, 7.5),
    (N'HS2', 2, N'MH2', 7.5, 7.0, 7.0, 7.5, 7.5),
    (N'HS2', 2, N'MH3', 8.5, 8.0, 8.5, 8.0, 8.0),
    (N'HS2', 2, N'MH4', 7.0, 6.5, 7.0, 6.5, 7.0),
    (N'HS2', 2, N'MH5', 9.0, 8.5, 9.0, 8.0, 9.0),
    (N'HS2', 2, N'MH6', 7.5, 7.0, 7.5, 7.0, 7.5),
	(N'HS3', 1, N'MH1', 8.0, 7.5, 7.5, 7.0, 7.5),
    (N'HS3', 1, N'MH2', 7.5, 7.0, 7.0, 7.5, 7.5),
    (N'HS3', 1, N'MH3', 8.5, 8.0, 8.5, 8.0, 8.0),
    (N'HS3', 1, N'MH4', 7.0, 6.5, 7.0, 6.5, 7.0),
    (N'HS3', 1, N'MH5', 9.0, 8.5, 9.0, 8.0, 9.0),
    (N'HS3', 1, N'MH6', 7.5, 7.0, 7.5, 7.0, 7.5),
	(N'HS3', 2, N'MH1', 8.5, 7.5, 7.5, 7.0, 8.0),
    (N'HS3', 2, N'MH2', 7.0, 6.5, 7.0, 7.5, 7.5),
    (N'HS3', 2, N'MH3', 7.5, 7.0, 7.5, 7.0, 7.0),
    (N'HS3', 2, N'MH4', 8.0, 7.5, 8.0, 8.5, 8.5),
    (N'HS3', 2, N'MH5', 8.0, 7.5, 7.5, 7.0, 7.5),
    (N'HS3', 2, N'MH6', 9.0, 8.5, 8.5, 8.0, 8.0),
	(N'HS4', 1, N'MH1', 7.5, 7.0, 7.0, 7.5, 7.5),
    (N'HS4', 1, N'MH2', 8.0, 7.5, 7.5, 7.0, 7.0),
    (N'HS4', 1, N'MH3', 7.0, 6.5, 7.0, 6.5, 7.0),
    (N'HS4', 1, N'MH4', 8.0, 7.5, 7.5, 7.0, 7.5),
    (N'HS4', 1, N'MH5', 7.5, 7.0, 7.0, 7.5, 7.5),
    (N'HS4', 1, N'MH6', 8.5, 8.0, 8.5, 8.0, 8.0),
	(N'HS4', 2, N'MH1', 7.0, 6.5, 7.0, 7.5, 7.5),
    (N'HS4', 2, N'MH2', 8.5, 8.0, 8.5, 8.0, 8.0),
    (N'HS4', 2, N'MH3', 9.0, 8.5, 9.0, 8.0, 9.0),
    (N'HS4', 2, N'MH4', 7.5, 7.0, 7.5, 7.0, 7.5),
    (N'HS4', 2, N'MH5', 7.0, 6.5, 7.0, 6.5, 7.0),
    (N'HS4', 2, N'MH6', 8.0, 7.5, 7.5, 7.0, 7.5),
	(N'HS5', 1, N'MH1', 7.5, 7.0, 7.0, 7.5, 7.5),
    (N'HS5', 1, N'MH2', 8.0, 7.5, 7.5, 7.0, 7.0),
    (N'HS5', 1, N'MH3', 7.0, 6.5, 7.0, 6.5, 7.0),
    (N'HS5', 1, N'MH4', 8.0, 7.5, 7.5, 7.0, 7.5),
    (N'HS5', 1, N'MH5', 7.5, 7.0, 7.0, 7.5, 7.5),
    (N'HS5', 1, N'MH6', 8.5, 8.0, 8.5, 8.0, 8.0),
	(N'HS5', 2, N'MH1', 7.0, 6.5, 7.0, 7.5, 7.5),
    (N'HS5', 2, N'MH2', 8.5, 8.0, 8.5, 8.0, 8.0),
    (N'HS5', 2, N'MH3', 9.0, 8.5, 9.0, 8.0, 9.0),
    (N'HS5', 2, N'MH4', 7.5, 7.0, 7.5, 7.0, 7.5),
    (N'HS5', 2, N'MH5', 7.0, 6.5, 7.0, 6.5, 7.0),
    (N'HS5', 2, N'MH6', 8.0, 7.5, 7.5, 7.0, 7.5),
	(N'HS6', 1, N'MH1', 8.0, 7.5, 7.5, 7.0, 7.5),
    (N'HS6', 1, N'MH2', 7.5, 7.0, 7.0, 7.5, 7.5),
    (N'HS6', 1, N'MH3', 8.5, 8.0, 8.5, 8.0, 8.0),
    (N'HS6', 1, N'MH4', 7.0, 6.5, 7.0, 6.5, 7.0),
    (N'HS6', 1, N'MH5', 9.0, 8.5, 9.0, 8.0, 9.0),
    (N'HS6', 1, N'MH6', 7.5, 7.0, 7.5, 7.0, 7.5),
	(N'HS6', 2, N'MH1', 8.5, 7.5, 7.5, 7.0, 8.0),
    (N'HS6', 2, N'MH2', 7.0, 6.5, 7.0, 7.5, 7.5),
    (N'HS6', 2, N'MH3', 7.5, 7.0, 7.5, 7.0, 7.0),
    (N'HS6', 2, N'MH4', 8.0, 7.5, 8.0, 8.5, 8.5),
    (N'HS6', 2, N'MH5', 8.0, 7.5, 7.5, 7.0, 7.5),
    (N'HS6', 2, N'MH6', 9.0, 8.5, 8.5, 8.0, 8.0),
	(N'HS7', 1, N'MH1', 8.0, 7.5, 7.5, 7.0, 7.5),
    (N'HS7', 1, N'MH2', 7.5, 7.0, 7.0, 7.5, 7.5),
    (N'HS7', 1, N'MH3', 8.5, 8.0, 8.5, 8.0, 8.0),
    (N'HS7', 1, N'MH4', 7.0, 6.5, 7.0, 6.5, 7.0),
    (N'HS7', 1, N'MH5', 9.0, 8.5, 9.0, 8.0, 9.0),
    (N'HS7', 1, N'MH6', 7.5, 7.0, 7.5, 7.0, 7.5),
	(N'HS7', 2, N'MH1', 8.5, 7.5, 7.5, 7.0, 8.0),
    (N'HS7', 2, N'MH2', 7.0, 6.5, 7.0, 7.5, 7.5),
    (N'HS7', 2, N'MH3', 7.5, 7.0, 7.5, 7.0, 7.0),
    (N'HS7', 2, N'MH4', 8.0, 7.5, 8.0, 8.5, 8.5),
    (N'HS7', 2, N'MH5', 8.0, 7.5, 7.5, 7.0, 7.5),
    (N'HS7', 2, N'MH6', 9.0, 8.5, 8.5, 8.0, 8.0),
	(N'HS8', 1, N'MH1', 7.5, 7.0, 7.0, 7.5, 7.5),
    (N'HS8', 1, N'MH2', 8.0, 7.5, 7.5, 7.0, 7.0),
    (N'HS8', 1, N'MH3', 7.0, 6.5, 7.0, 6.5, 7.0),
    (N'HS8', 1, N'MH4', 8.0, 7.5, 7.5, 7.0, 7.5),
    (N'HS8', 1, N'MH5', 7.5, 7.0, 7.0, 7.5, 7.5),
    (N'HS8', 1, N'MH6', 8.5, 8.0, 8.5, 8.0, 8.0),
	(N'HS8', 2, N'MH1', 7.0, 6.5, 7.0, 7.5, 7.5),
    (N'HS8', 2, N'MH2', 8.5, 8.0, 8.5, 8.0, 8.0),
    (N'HS8', 2, N'MH3', 9.0, 8.5, 9.0, 8.0, 9.0),
    (N'HS8', 2, N'MH4', 7.5, 7.0, 7.5, 7.0, 7.5),
    (N'HS8', 2, N'MH5', 7.0, 6.5, 7.0, 6.5, 7.0),
    (N'HS8', 2, N'MH6', 8.0, 7.5, 7.5, 7.0, 7.5),
	(N'HS9', 1, N'MH1', 8.0, 7.5, 7.5, 7.0, 7.5),
    (N'HS9', 1, N'MH2', 7.5, 7.0, 7.0, 7.5, 7.5),
    (N'HS9', 1, N'MH3', 8.5, 8.0, 8.5, 8.0, 8.0),
    (N'HS9', 1, N'MH4', 7.0, 6.5, 7.0, 6.5, 7.0),
    (N'HS9', 1, N'MH5', 9.0, 8.5, 9.0, 8.0, 9.0),
    (N'HS9', 1, N'MH6', 7.5, 7.0, 7.5, 7.0, 7.5),
	(N'HS9', 2, N'MH1', 8.5, 7.5, 7.5, 7.0, 8.0),
    (N'HS9', 2, N'MH2', 7.0, 6.5, 7.0, 7.5, 7.5),
    (N'HS9', 2, N'MH3', 7.5, 7.0, 7.5, 7.0, 7.0),
    (N'HS9', 2, N'MH4', 8.0, 7.5, 8.0, 8.5, 8.5),
    (N'HS9', 2, N'MH5', 8.0, 7.5, 7.5, 7.0, 7.5),
    (N'HS9', 2, N'MH6', 9.0, 8.5, 8.5, 8.0, 8.0),
	(N'HS10', 1, N'MH1', 8.0, 7.5, 7.5, 7.0, 7.0),
    (N'HS10', 1, N'MH2', 7.5, 7.0, 7.0, 7.5, 7.0),
    (N'HS10', 1, N'MH3', 8.5, 8.0, 8.5, 8.0, 8.0),
    (N'HS10', 1, N'MH4', 7.0, 6.5, 7.0, 6.5, 7.0),
    (N'HS10', 1, N'MH5', 9.0, 8.5, 9.0, 8.0, 9.0),
    (N'HS10', 1, N'MH6', 7.5, 7.0, 7.5, 7.0, 7.5),
	(N'HS10', 2, N'MH1', 8.5, 7.5, 7.5, 7.0, 8.0),
    (N'HS10', 2, N'MH2', 7.0, 6.5, 7.0, 7.5, 7.5),
    (N'HS10', 2, N'MH3', 7.5, 7.0, 7.5, 7.0, 7.0),
    (N'HS10', 2, N'MH4', 8.0, 7.5, 8.0, 8.5, 8.5),
    (N'HS10', 2, N'MH5', 8.0, 7.5, 7.5, 7.0, 7.5),
    (N'HS10', 2, N'MH6', 9.0, 8.5, 8.5, 8.0, 8.0);

-- Thêm dữ liệu cho tất cả học sinh và năm học 1
INSERT INTO KQ_HocKy (MaHS, Kyhoc, NamHocID, MaHanhKiem)
VALUES
    (N'HS1', 1, 1, N'HK1'),
    (N'HS1', 2, 1, N'HK2'),
    (N'HS1', 1, 2, N'HK1'),
    (N'HS1', 2, 2, N'HK2'),
    (N'HS1', 1, 3, N'HK1'),
    (N'HS1', 2, 3, N'HK2'),
    
    -- Thêm dữ liệu cho HS2
    (N'HS2', 1, 1, N'HK1'),
    (N'HS2', 2, 1, N'HK2'),
    (N'HS2', 1, 2, N'HK1'),
    (N'HS2', 2, 2, N'HK2'),
    (N'HS2', 1, 3, N'HK1'),
    (N'HS2', 2, 3, N'HK2'),
    
    -- Thêm dữ liệu cho HS3
    (N'HS3', 1, 1, N'HK1'),
    (N'HS3', 2, 1, N'HK2'),
    (N'HS3', 1, 2, N'HK1'),
    (N'HS3', 2, 2, N'HK2'),
    (N'HS3', 1, 3, N'HK1'),
    (N'HS3', 2, 3, N'HK2'),
    
    -- Thêm dữ liệu cho HS4
    (N'HS4', 1, 1, N'HK1'),
    (N'HS4', 2, 1, N'HK2'),
    (N'HS4', 1, 2, N'HK1'),
    (N'HS4', 2, 2, N'HK2'),
    (N'HS4', 1, 3, N'HK1'),
    (N'HS4', 2, 3, N'HK2'),
    
    -- Thêm dữ liệu cho HS5
    (N'HS5', 1, 1, N'HK1'),
    (N'HS5', 2, 1, N'HK2'),
    (N'HS5', 1, 2, N'HK1'),
    (N'HS5', 2, 2, N'HK2'),
    (N'HS5', 1, 3, N'HK1'),
    (N'HS5', 2, 3, N'HK2'),
    
    -- Thêm dữ liệu cho HS6
    (N'HS6', 1, 1, N'HK1'),
    (N'HS6', 2, 1, N'HK2'),
    (N'HS6', 1, 2, N'HK1'),
    (N'HS6', 2, 2, N'HK2'),
    (N'HS6', 1, 3, N'HK1'),
    (N'HS6', 2, 3, N'HK2'),
    
    -- Thêm dữ liệu cho HS7
    (N'HS7', 1, 1, N'HK1'),
    (N'HS7', 2, 1, N'HK2'),
    (N'HS7', 1, 2, N'HK1'),
    (N'HS7', 2, 2, N'HK2'),
    (N'HS7', 1, 3, N'HK1'),
    (N'HS7', 2, 3, N'HK2'),
    
    -- Thêm dữ liệu cho HS8
    (N'HS8', 1, 1, N'HK1'),
    (N'HS8', 2, 1, N'HK2'),
    (N'HS8', 1, 2, N'HK1'),
    (N'HS8', 2, 2, N'HK2'),
    (N'HS8', 1, 3, N'HK1'),
    (N'HS8', 2, 3, N'HK2'),
    
    -- Thêm dữ liệu cho HS9
    (N'HS9', 1, 1, N'HK1'),
    (N'HS9', 2, 1, N'HK2'),
    (N'HS9', 1, 2, N'HK1'),
    (N'HS9', 2, 2, N'HK2'),
    (N'HS9', 1, 3, N'HK1'),
    (N'HS9', 2, 3, N'HK2'),
    
    -- Thêm dữ liệu cho HS10
    (N'HS10', 1, 1, N'HK1'),
    (N'HS10', 2, 1, N'HK2'),
    (N'HS10', 1, 2, N'HK1'),
    (N'HS10', 2, 2, N'HK2'),
    (N'HS10', 1, 3, N'HK1'),
    (N'HS10', 2, 3, N'HK2');

INSERT INTO KQ_NamHoc (MaHS, NamHocID, MaHanhKiem)
VALUES
    -- Dữ liệu cho HS1
    (N'HS1', 1, N'HK1'),
    (N'HS1', 1, N'HK2'),
    (N'HS1', 1, N'HK3'),
    (N'HS1', 2, N'HK1'),
    (N'HS1', 2, N'HK2'),
    (N'HS1', 2, N'HK3'),
    (N'HS1', 3, N'HK1'),
    (N'HS1', 3, N'HK2'),
    (N'HS1', 3, N'HK3'),
    
    -- Dữ liệu cho HS2
    (N'HS2', 1, N'HK1'),
    (N'HS2', 1, N'HK2'),
    (N'HS2', 1, N'HK3'),
    (N'HS2', 2, N'HK1'),
    (N'HS2', 2, N'HK2'),
    (N'HS2', 2, N'HK3'),
    (N'HS2', 3, N'HK1'),
    (N'HS2', 3, N'HK2'),
    (N'HS2', 3, N'HK3'),

    -- Dữ liệu cho HS3
    (N'HS3', 1, N'HK1'),
    (N'HS3', 1, N'HK2'),
    (N'HS3', 1, N'HK3'),
    (N'HS3', 2, N'HK1'),
    (N'HS3', 2, N'HK2'),
    (N'HS3', 2, N'HK3'),
    (N'HS3', 3, N'HK1'),
    (N'HS3', 3, N'HK2'),
    (N'HS3', 3, N'HK3'),

    -- Dữ liệu cho HS4
    (N'HS4', 1, N'HK1'),
    (N'HS4', 1, N'HK2'),
    (N'HS4', 1, N'HK3'),
    (N'HS4', 2, N'HK1'),
    (N'HS4', 2, N'HK2'),
    (N'HS4', 2, N'HK3'),
    (N'HS4', 3, N'HK1'),
    (N'HS4', 3, N'HK2'),
    (N'HS4', 3, N'HK3'),

    -- Dữ liệu cho HS5
    (N'HS5', 1, N'HK1'),
    (N'HS5', 1, N'HK2'),
    (N'HS5', 1, N'HK3'),
    (N'HS5', 2, N'HK1'),
    (N'HS5', 2, N'HK2'),
    (N'HS5', 2, N'HK3'),
    (N'HS5', 3, N'HK1'),
    (N'HS5', 3, N'HK2'),
    (N'HS5', 3, N'HK3'),

    -- Dữ liệu cho HS6
    (N'HS6', 1, N'HK1'),
    (N'HS6', 1, N'HK2'),
    (N'HS6', 1, N'HK3'),
    (N'HS6', 2, N'HK1'),
    (N'HS6', 2, N'HK2'),
    (N'HS6', 2, N'HK3'),
    (N'HS6', 3, N'HK1'),
    (N'HS6', 3, N'HK2'),
    (N'HS6', 3, N'HK3'),

    -- Dữ liệu cho HS7
    (N'HS7', 1, N'HK1'),
    (N'HS7', 1, N'HK2'),
    (N'HS7', 1, N'HK3'),
    (N'HS7', 2, N'HK1'),
    (N'HS7', 2, N'HK2'),
    (N'HS7', 2, N'HK3'),
    (N'HS7', 3, N'HK1'),
    (N'HS7', 3, N'HK2'),
    (N'HS7', 3, N'HK3'),

    -- Dữ liệu cho HS8
    (N'HS8', 1, N'HK1'),
    (N'HS8', 1, N'HK2'),
    (N'HS8', 1, N'HK3'),
    (N'HS8', 2, N'HK1'),
    (N'HS8', 2, N'HK2'),
    (N'HS8', 2, N'HK3'),
    (N'HS8', 3, N'HK1'),
    (N'HS8', 3, N'HK2'),
    (N'HS8', 3, N'HK3'),

    -- Dữ liệu cho HS9
    (N'HS9', 1, N'HK1'),
    (N'HS9', 1, N'HK2'),
    (N'HS9', 1, N'HK3'),
    (N'HS9', 2, N'HK1'),
    (N'HS9', 2, N'HK2'),
    (N'HS9', 2, N'HK3'),
    (N'HS9', 3, N'HK1'),
    (N'HS9', 3, N'HK2'),
    (N'HS9', 3, N'HK3'),

    -- Dữ liệu cho HS10
    (N'HS10', 1, N'HK1'),
    (N'HS10', 1, N'HK2'),
    (N'HS10', 1, N'HK3'),
    (N'HS10', 2, N'HK1'),
    (N'HS10', 2, N'HK2'),
    (N'HS10', 2, N'HK3'),
    (N'HS10', 3, N'HK1'),
    (N'HS10', 3, N'HK2'),
    (N'HS10', 3, N'HK3');
 --cần chạy hàm update
UPDATE hs
SET hs.NamHocID = lop.NamHocID
FROM HocSinh_Diem hs
JOIN HocSinh hsinfo ON hs.Mahocsinh = hsinfo.MaHS
JOIN Lop lop ON hsinfo.MaLop = lop.Malop;
