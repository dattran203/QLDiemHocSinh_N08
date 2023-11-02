using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QLDiem.Models
{
    public partial class QLDiemContext : DbContext
    {
        public QLDiemContext()
        {
        }

        public QLDiemContext(DbContextOptions<QLDiemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<GiaoVien> GiaoViens { get; set; } = null!;
        public virtual DbSet<HanhKiem> HanhKiems { get; set; } = null!;
        public virtual DbSet<HocSinh> HocSinhs { get; set; } = null!;
        public virtual DbSet<HocSinhDiem> HocSinhDiems { get; set; } = null!;
        public virtual DbSet<KqHocKy> KqHocKies { get; set; } = null!;
        public virtual DbSet<KqNamHoc> KqNamHocs { get; set; } = null!;
        public virtual DbSet<Lop> Lops { get; set; } = null!;
        public virtual DbSet<MonHoc> MonHocs { get; set; } = null!;
        public virtual DbSet<NamHoc> NamHocs { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-5BRT6K0\\SQLEXPRESS;Initial Catalog=QLDiem;Persist Security Info=True;User ID=sa;Password=h1235578919;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GiaoVien>(entity =>
            {
                entity.HasKey(e => e.MaGv)
                    .HasName("PK__GiaoVien__2725AEF3CC7A5178");

                entity.ToTable("GiaoVien");

                entity.Property(e => e.MaGv)
                    .HasMaxLength(50)
                    .HasColumnName("MaGV");

                entity.Property(e => e.Diachi).HasMaxLength(50);

                entity.Property(e => e.Dienthoai).HasMaxLength(50);

                entity.Property(e => e.Gioitinh).HasMaxLength(50);

                entity.Property(e => e.MaMon).HasMaxLength(50);

                entity.Property(e => e.Ngaysinh).HasColumnType("date");

                entity.Property(e => e.TenGv)
                    .HasMaxLength(50)
                    .HasColumnName("TenGV");

                entity.HasOne(d => d.MaMonNavigation)
                    .WithMany(p => p.GiaoViens)
                    .HasForeignKey(d => d.MaMon)
                    .HasConstraintName("FK__GiaoVien__MaMon__534D60F1");
            });

            modelBuilder.Entity<HanhKiem>(entity =>
            {
                entity.HasKey(e => e.MaHanhKiem)
                    .HasName("PK__HanhKiem__2B2C87ED493C872F");

                entity.ToTable("HanhKiem");

                entity.Property(e => e.MaHanhKiem).HasMaxLength(50);

                entity.Property(e => e.TenHanhKiem).HasMaxLength(50);
            });

            modelBuilder.Entity<HocSinh>(entity =>
            {
                entity.HasKey(e => e.MaHs)
                    .HasName("PK__HocSinh__2725A6EF8DA46E88");

                entity.ToTable("HocSinh");

                entity.Property(e => e.MaHs)
                    .HasMaxLength(50)
                    .HasColumnName("MaHS");

                entity.Property(e => e.Diachi).HasMaxLength(50);

                entity.Property(e => e.Dienthoai).HasMaxLength(50);

                entity.Property(e => e.Gioitinh).HasMaxLength(50);

                entity.Property(e => e.HotenBo).HasMaxLength(50);

                entity.Property(e => e.HotenMe).HasMaxLength(50);

                entity.Property(e => e.Malop).HasMaxLength(50);

                entity.Property(e => e.Ngaysinh).HasColumnType("date");

                entity.Property(e => e.NgayvaoDoan).HasColumnType("date");

                entity.Property(e => e.TenHs)
                    .HasMaxLength(50)
                    .HasColumnName("TenHS");

                entity.HasOne(d => d.MalopNavigation)
                    .WithMany(p => p.HocSinhs)
                    .HasForeignKey(d => d.Malop)
                    .HasConstraintName("FK__HocSinh__Malop__4E88ABD4");
            });

            modelBuilder.Entity<HocSinhDiem>(entity =>
            {
                entity.HasKey(e => new { e.Mahocsinh, e.Kyhoc, e.Mamon })
                    .HasName("PK__HocSinh___D18ED638EBB72AFD");

                entity.ToTable("HocSinh_Diem");

                entity.Property(e => e.Mahocsinh).HasMaxLength(50);

                entity.Property(e => e.Mamon).HasMaxLength(50);

                entity.Property(e => e.DiemGk).HasColumnName("DiemGK");

                entity.Property(e => e.DiemTbm).HasColumnName("DiemTBM");

                entity.Property(e => e.DiemthiHk).HasColumnName("DiemthiHK");

                entity.HasOne(d => d.MahocsinhNavigation)
                    .WithMany(p => p.HocSinhDiems)
                    .HasForeignKey(d => d.Mahocsinh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HocSinh_D__Mahoc__5629CD9C");

                entity.HasOne(d => d.MamonNavigation)
                    .WithMany(p => p.HocSinhDiems)
                    .HasForeignKey(d => d.Mamon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HocSinh_D__Mamon__571DF1D5");
            });

            modelBuilder.Entity<KqHocKy>(entity =>
            {
                entity.HasKey(e => new { e.MaHs, e.MaHanhKiem, e.NamHocId })
                    .HasName("PK__KQ_HocKy__A02CE21A504FE5C9");

                entity.ToTable("KQ_HocKy");

                entity.Property(e => e.MaHs)
                    .HasMaxLength(50)
                    .HasColumnName("MaHS");

                entity.Property(e => e.MaHanhKiem).HasMaxLength(50);

                entity.Property(e => e.NamHocId).HasColumnName("NamHocID");

                entity.HasOne(d => d.MaHanhKiemNavigation)
                    .WithMany(p => p.KqHocKies)
                    .HasForeignKey(d => d.MaHanhKiem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KQ_HocKy__MaHanh__5CD6CB2B");

                entity.HasOne(d => d.MaHsNavigation)
                    .WithMany(p => p.KqHocKies)
                    .HasForeignKey(d => d.MaHs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KQ_HocKy__MaHS__5BE2A6F2");

                entity.HasOne(d => d.NamHoc)
                    .WithMany(p => p.KqHocKies)
                    .HasForeignKey(d => d.NamHocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KQ_HocKy__NamHoc__5DCAEF64");
            });

            modelBuilder.Entity<KqNamHoc>(entity =>
            {
                entity.HasKey(e => new { e.MaHs, e.MaHanhKiem, e.NamHocId })
                    .HasName("PK__KQ_NamHo__A02CE21AAEDFAC6A");

                entity.ToTable("KQ_NamHoc");

                entity.Property(e => e.MaHs)
                    .HasMaxLength(50)
                    .HasColumnName("MaHS");

                entity.Property(e => e.MaHanhKiem).HasMaxLength(50);

                entity.Property(e => e.NamHocId).HasColumnName("NamHocID");

                entity.HasOne(d => d.MaHanhKiemNavigation)
                    .WithMany(p => p.KqNamHocs)
                    .HasForeignKey(d => d.MaHanhKiem)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KQ_NamHoc__MaHan__619B8048");

                entity.HasOne(d => d.MaHsNavigation)
                    .WithMany(p => p.KqNamHocs)
                    .HasForeignKey(d => d.MaHs)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KQ_NamHoc__MaHS__60A75C0F");

                entity.HasOne(d => d.NamHoc)
                    .WithMany(p => p.KqNamHocs)
                    .HasForeignKey(d => d.NamHocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__KQ_NamHoc__NamHo__628FA481");
            });

            modelBuilder.Entity<Lop>(entity =>
            {
                entity.HasKey(e => e.Malop)
                    .HasName("PK__Lop__3313BBCD7D617C7E");

                entity.ToTable("Lop");

                entity.Property(e => e.Malop).HasMaxLength(50);

                entity.Property(e => e.Gvcn)
                    .HasMaxLength(50)
                    .HasColumnName("GVCN");

                entity.Property(e => e.NamHocId).HasColumnName("NamHocID");

                entity.Property(e => e.Tenlop).HasMaxLength(50);

                entity.HasOne(d => d.NamHoc)
                    .WithMany(p => p.Lops)
                    .HasForeignKey(d => d.NamHocId)
                    .HasConstraintName("FK__Lop__NamHocID__4BAC3F29");
            });

            modelBuilder.Entity<MonHoc>(entity =>
            {
                entity.HasKey(e => e.Mamon)
                    .HasName("PK__MonHoc__33DA29C2FB9F6A2C");

                entity.ToTable("MonHoc");

                entity.Property(e => e.Mamon).HasMaxLength(50);

                entity.Property(e => e.Tenmon).HasMaxLength(50);
            });

            modelBuilder.Entity<NamHoc>(entity =>
            {
                entity.ToTable("NamHoc");

                entity.Property(e => e.NamHocId)
                    .ValueGeneratedNever()
                    .HasColumnName("NamHocID");

                entity.Property(e => e.NamHoc1)
                    .HasMaxLength(50)
                    .HasColumnName("NamHoc");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.TaiKhoan)
                    .HasName("PK__tblUser__D5B8C7F1C36DE513");

                entity.ToTable("tblUser");

                entity.Property(e => e.TaiKhoan).HasMaxLength(50);

                entity.Property(e => e.MaGv)
                    .HasMaxLength(50)
                    .HasColumnName("MaGV");

                entity.Property(e => e.MatKhau).HasMaxLength(50);

                entity.HasOne(d => d.MaGvNavigation)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.MaGv)
                    .HasConstraintName("FK__tblUser__MaGV__656C112C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
