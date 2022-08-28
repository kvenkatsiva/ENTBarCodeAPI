using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENTBarCodeAPI.DAL;
using ENTBarCodeAPI.Models;

namespace ENTBarCodeAPI.Controllers
{
    public class BarCodeController : Controller
    {
        BarCodeDAL _barCodeDAL = new BarCodeDAL();

        // GET: BarCode
        public ActionResult Index()
        {
            var barcodelist = _barCodeDAL.GetAllBarcodes();

            if (barcodelist.Count == 0)
            {
                TempData["InfoMessage"]= "Current Bar Code not Available in the Database";
            }

            return View(barcodelist);
        }

        // GET: BarCode/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var barcode = _barCodeDAL.GetBarcodeByID(id).FirstOrDefault();
                if (barcode == null)
                {
                    TempData["InfoMessage"] = "Bar Code Not Available in the Database With Bar Code ID  " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(barcode);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: BarCode/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BarCode/Create
        [HttpPost]
        public ActionResult Create(Barcode barcode)
        {
            bool IsInserted = false;

            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _barCodeDAL.InsertBarCode(barcode);

                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "BarCode Details Saved Successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Saved BarCode Details";
                    }                   
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
           
        }

        // GET: BarCode/Edit/ID
        public ActionResult Edit(int id)
        {
            var barcodes = _barCodeDAL.GetBarcodeByID(id).FirstOrDefault();

            if(barcodes == null)
            {
                TempData["InfoMessage"] = "Bar Code Not Available in the Database With Bar Code ID  " + id.ToString();
                return RedirectToAction("Index");
            }
            return View(barcodes);
        }

        // POST: BarCode/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateBarCode(Barcode barcode)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _barCodeDAL.UpdateBarCode(barcode);

                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "BarCode Details Updated Successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Updated BarCode Details";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }

        // GET: BarCode/Delete/ID
        public ActionResult Delete(int id)
        {
            try
            {
                var barcode = _barCodeDAL.GetBarcodeByID(id).FirstOrDefault();
                if (barcode == null)
                {
                    TempData["InfoMessage"] = "Bar Code Not Available in the Database with this Bar Code ID  " + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(barcode);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: BarCode/Delete/ID Conformation
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _barCodeDAL.DeleteBarCode(id);
                if (result.Contains("Delete"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
