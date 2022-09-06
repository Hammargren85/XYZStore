﻿using Microsoft.AspNetCore.Mvc;
using XYZStore.DataAccess;
using XYZStore.Models;
using System.Linq;
using XYZStore.DataAccess.Repository.IRepository;

namespace XYZStore.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
        return View(objCategoryList);
    }
    //GET
    public IActionResult Create()
    {
        return View();
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The Display Order cannot exactly match the name.");
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Created Successfully!";
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
        var CategoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        //var CategoryFromDb = _db.Categories.Find(id);
        //var CategoryFromDb = _db.Categories.SingleOrDefault(u => u.Id == id);

        if (CategoryFromDb == null)
        {
            return NotFound();

        }

        return View(CategoryFromDb);
    }
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The Display Order cannot exactly match the name.");
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category Updated Successfully!";
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
        var CategoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        //var CategoryFromDb = _db.Categories.Find(id);
        ////var CategoryFromDb = _db.Categories.SingleOrDefault(u => u.Id == id);
        ///
        return View(CategoryFromDb);
    }
    //POST
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
        _unitOfWork.Category.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category Deleted Successfully!";
        return RedirectToAction("Index");
    }
}
