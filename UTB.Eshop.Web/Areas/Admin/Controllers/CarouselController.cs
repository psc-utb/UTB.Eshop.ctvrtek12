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
        public IActionResult Select()
        {
            List<CarouselItem> carouselItems = DatabaseFake.CarouselItems;
            return View(carouselItems);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarouselItem carouselItemFromForm)
        {
            //tento if a else u entity frameworku smazeme
            if (DatabaseFake.CarouselItems.Count > 0)
                carouselItemFromForm.ID = DatabaseFake.CarouselItems.Last().ID + 1;
            else
                carouselItemFromForm.ID = 1;

            DatabaseFake.CarouselItems.Add(carouselItemFromForm);
            return RedirectToAction(nameof(Select));
        }


        public IActionResult Edit(int ID)
        {
            CarouselItem carouselItem = DatabaseFake.CarouselItems.FirstOrDefault(carItem => carItem.ID == ID);

            if (carouselItem != null)
            {
                return View(carouselItem);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(CarouselItem carouselItemFromForm)
        {
            CarouselItem carouselItem = DatabaseFake.CarouselItems.FirstOrDefault(carItem => carItem.ID == carouselItemFromForm.ID);

            if (carouselItem != null)
            {
                carouselItem.ImageSrc = carouselItemFromForm.ImageSrc;
                carouselItem.ImageAlt = carouselItemFromForm.ImageAlt;

                return RedirectToAction(nameof(Select));
            }

            return NotFound();
        }


        public IActionResult Delete(int ID)
        {
            CarouselItem carouselItem = DatabaseFake.CarouselItems.FirstOrDefault(carItem => carItem.ID == ID);

            if (carouselItem != null)
            {
                DatabaseFake.CarouselItems.Remove(carouselItem);
                return RedirectToAction(nameof(Select));
            }

            return NotFound();
        }
    }
}
