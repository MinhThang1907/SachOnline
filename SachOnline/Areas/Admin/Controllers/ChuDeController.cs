﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace SachOnline.Areas.Admin.Controllers
{
    public class ChuDeController : Controller
    {
        QLBanSachEntities1 db = new QLBanSachEntities1();
        // GET: Admin/ChuDe
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.CHUDEs.ToList().OrderBy(n => n.MaCD).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(CHUDE chude, FormCollection f)
        {
            if (f["sTenCD"] == null)
            {
                ViewBag.ThongBao = "Hãy nhập tên chủ đề.";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    chude.TenChuDe = f["sTenCD"];
                    db.CHUDEs.Add(chude);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View();

            }
        }
        public ActionResult Details(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var sach = db.SACHes.Where(s => s.MaCD == id);
            if (sach.Count() > 0)
            {
                ViewBag.ThongBao = "Chủ đề này đang có trong bảng Sách <br> " + " Nếu muốn xóa thì phải xóa hết sách có mã chủ đề này trong bảng Sách";
                return View(sach);
            }
            db.CHUDEs.Remove(chude);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            int maChuDe = int.Parse(f["iMaCD"]);
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == maChuDe);
            if (ModelState.IsValid)
            {
                chude.TenChuDe = f["sTenCD"];
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chude);
        }
    }
}