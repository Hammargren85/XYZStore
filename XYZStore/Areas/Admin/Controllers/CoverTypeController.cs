using Microsoft.AspNetCore.Mvc;
using XYZStore.DataAccess;
using XYZStore.Models;
using System.Linq;
using XYZStore.DataAccess.Repository.IRepository;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using XYZStore.Utility;

namespace XYZStore.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CoverTypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CoverTypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
        return View(objCoverTypeList);
    }
    //GET
    public IActionResult Create()
    {
        return View();
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType Created Successfully!";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    //GET
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var CoverTypeFromDbFirst = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        //var CategoryFromDb = _db.Categories.Find(id);
        //var CategoryFromDb = _db.Categories.SingleOrDefault(u => u.Id == id);

        if (CoverTypeFromDbFirst == null)
        {
            return NotFound();

        }

        return View(CoverTypeFromDbFirst);
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(CoverType obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.CoverType.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "CoverType Updated Successfully!";
            return RedirectToAction("Index");
        }
        return View(obj);
    }
    //GET
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var CoverTypeFromDb = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        //var CategoryFromDb = _db.Categories.Find(id);
        ////var CategoryFromDb = _db.Categories.SingleOrDefault(u => u.Id == id);
        ///
        return View(CoverTypeFromDb);
    }
    //POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _unitOfWork.CoverType.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.CoverType.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "CoverType Deleted Successfully!";
        return RedirectToAction("Index");
    }
}
