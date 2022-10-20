using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTB.Eshop.Web.Models.Database;
using UTB.Eshop.Web.Models.Entities;

namespace UTB.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarouselController : Controller
    {
        readonly EshopDbContext eshopDbContext;
        public CarouselController(EshopDbContext eshopDbContext)
        {
            this.eshopDbContext = eshopDbContext;
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
        public IActionResult Create(CarouselItem carouselItemFromForm)
        {
            eshopDbContext.CarouselItems.Add(carouselItemFromForm);
            eshopDbContext.SaveChanges();
            return RedirectToAction(nameof(Select));
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
        public IActionResult Edit(CarouselItem carouselItemFromForm)
        {
            CarouselItem carouselItem = eshopDbContext.CarouselItems.FirstOrDefault(carItem => carItem.ID == carouselItemFromForm.ID);

            if (carouselItem != null)
            {
                carouselItem.ImageSrc = carouselItemFromForm.ImageSrc;
                carouselItem.ImageAlt = carouselItemFromForm.ImageAlt;

                eshopDbContext.SaveChanges();

                return RedirectToAction(nameof(Select));
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
