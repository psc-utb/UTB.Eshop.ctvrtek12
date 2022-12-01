using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UTB.Eshop.Domain.Abstraction;
using UTB.Eshop.Web.Models.Database;
using UTB.Eshop.Web.Models.Entities;
using UTB.Eshop.Web.Models.Identity;
using UTB.Eshop.Web.Models.ViewModels;

namespace UTB.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class CarouselController : Controller
    {
        readonly EshopDbContext eshopDbContext;
        IFileUpload fileUpload;
        ICheckFileContent checkFileContent;
        ICheckFileLength checkFileLength;
        public CarouselController(EshopDbContext eshopDbContext, IFileUpload fileUpload, ICheckFileContent checkFileContent, ICheckFileLength checkFileLength)
        {
            this.eshopDbContext = eshopDbContext;
            this.fileUpload = fileUpload;
            this.checkFileContent = checkFileContent;
            this.checkFileLength = checkFileLength;
        }

        public IActionResult Select()
        {
            List<CarouselItem> carouselItems = eshopDbContext.CarouselItems.ToList();
            return View(carouselItems);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarouselItemImageRequired carouselItemFromForm)
        {
            ModelState.Remove(nameof(CarouselItem.ImageSrc));
            if (ModelState.IsValid)
            {
                if (checkFileLength.CheckFileLength(carouselItemFromForm.Image, 5_000_000))
                {
                    fileUpload.ContentType = "image";
                    fileUpload.FileLength = 5_000_000;
                    carouselItemFromForm.ImageSrc = await fileUpload.FileUploadAsync(carouselItemFromForm.Image, Path.Combine("img", "carousel"));

                    ModelState.Clear();
                    if (TryValidateModel(carouselItemFromForm))
                    {
                        eshopDbContext.CarouselItems.Add(carouselItemFromForm);
                        eshopDbContext.SaveChanges();
                        return RedirectToAction(nameof(Select));
                    }
                }
            }

            return View(carouselItemFromForm);
        }


        public IActionResult Edit(int ID)
        {
            CarouselItem carouselItem = eshopDbContext.CarouselItems.FirstOrDefault(carItem => carItem.ID == ID);

            if (carouselItem != null)
            {
                return View(carouselItem);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarouselItem carouselItemFromForm)
        {
            CarouselItem carouselItem = eshopDbContext.CarouselItems.FirstOrDefault(carItem => carItem.ID == carouselItemFromForm.ID);

            if (carouselItem != null)
            {

                ModelState.Remove(nameof(CarouselItem.ImageSrc));
                if (ModelState.IsValid)
                {
                    if (carouselItemFromForm.Image != null)
                    {
                        if (checkFileLength.CheckFileLength(carouselItemFromForm.Image, 5_000_000))
                        {
                            fileUpload.ContentType = "image";
                            fileUpload.FileLength = 5_000_000;
                            carouselItemFromForm.ImageSrc = await fileUpload.FileUploadAsync(carouselItemFromForm.Image, Path.Combine("img", "carousel"));

                            ModelState.Clear();
                            if (TryValidateModel(carouselItemFromForm))
                            {
                                carouselItem.ImageSrc = carouselItemFromForm.ImageSrc;
                            }
                            else
                                return View(carouselItemFromForm);
                        }
                        else
                            return View(carouselItemFromForm);
                    }

                    carouselItem.ImageAlt = carouselItemFromForm.ImageAlt;

                    eshopDbContext.SaveChanges();

                    return RedirectToAction(nameof(Select));
                }
                else
                    return View(carouselItemFromForm);
            }

            return NotFound();
        }


        public IActionResult Delete(int ID)
        {
            CarouselItem carouselItem = eshopDbContext.CarouselItems.FirstOrDefault(carItem => carItem.ID == ID);

            if (carouselItem != null)
            {
                eshopDbContext.CarouselItems.Remove(carouselItem);
                eshopDbContext.SaveChanges();
                return RedirectToAction(nameof(Select));
            }

            return NotFound();
        }
    }
}
