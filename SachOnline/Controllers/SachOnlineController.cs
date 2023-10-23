using SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace SachOnline.Controllers
{
    public class SachOnlineController : Controller
    {
        QLBanSachEntities1 db = new QLBanSachEntities1();
        private List<SACH> LaySachMoi(int count)
        {
            return db.SACHes.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        // GET: SachOnline
        public ActionResult Index(int ? page)
        {
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sach = db.SACHes.OrderBy(s => s.MaSach);
            return View(sach.ToPagedList(iPageNum, iSize));
        }
        public ActionResult ChuDePartial()
        {
            var listChuDe = from cd in db.CHUDEs select cd;
            return PartialView(listChuDe);
        }
        public ActionResult NhaXuatBanPartial()
        {
            var listNhaXuatBan = from nxb in db.NHAXUATBANs select nxb;
            return PartialView(listNhaXuatBan);
        }
        public ActionResult SachBanNhieuPartial()
        {
            var listSachMoi = LaySachMoi(6);
            return PartialView(listSachMoi);
        }
        public ActionResult NavBar()
        {
            return PartialView();
        }
        public ActionResult SachTheoChuDe(int id, int ? page)
        {
            ViewBag.MaCD = id;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sach = db.SACHes.Where(s => s.MaCD == id).OrderBy(s => s.MaSach);
            return View(sach.ToPagedList(iPageNum, iSize));
        }
        public ActionResult SachTheoNhaXuatBan(int id, int ? page)
        {
            ViewBag.MaNXB = id;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sach = db.SACHes.Where(s => s.MaNXB == id).OrderBy(s => s.MaSach);
            return View(sach.ToPagedList(iPageNum, iSize));
        }
        public ActionResult ChiTietSach(int id)
        {
            var sach = from s in db.SACHes where s.MaSach == id select s;
            return View(sach.Single());
        }
        public ActionResult LoginLogout()
        {
            return PartialView();
        }
    }
}