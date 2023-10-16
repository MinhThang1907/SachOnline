using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SachOnline.Models
{
    public class GioHang
    {
        QLBanSachEntities1 db = new QLBanSachEntities1();

        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sHinhMinhHoa { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien { get; set; }
        public GioHang(int ms)
        {
            iMaSach = ms;
            SACH s = db.SACHes.Single(n => n.MaSach == iMaSach);
            sTenSach = s.TenSach;
            sHinhMinhHoa = s.HinhMinhHoa;
            dDonGia = double.Parse(s.DonGia.ToString());
            iSoLuong = 1;
            dThanhTien = iSoLuong * dDonGia;
        }
                
    }
}