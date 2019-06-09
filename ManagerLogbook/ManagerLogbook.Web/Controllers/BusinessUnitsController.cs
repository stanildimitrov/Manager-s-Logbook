﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagerLogbook.Services.Contracts;
using ManagerLogbook.Web.Models;
using Microsoft.AspNetCore.Mvc;
using ManagerLogbook.Web.Mappers;

namespace ManagerLogbook.Web.Controllers
{
    public class BusinessUnitsController : Controller
    {
        private readonly IBusinessUnitService businessUnitService;
        private readonly IReviewService reviewService;

        public BusinessUnitsController(IBusinessUnitService businessUnitService,
                                       IReviewService reviewService)
        {
            this.businessUnitService = businessUnitService ?? throw new ArgumentNullException(nameof(businessUnitService));
            this.reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
        }

        public async Task<IActionResult> Details(int id)
        {
            var businessUnit = await this.businessUnitService.GetBusinessUnitById(id);

            var model = businessUnit.MapFrom();

            var reviewDTOs = await this.reviewService.GetAllReviewsByBusinessUnitIdAsync(id);

            model.Reviews = reviewDTOs.Select(x => x.MapFrom()).ToList();

            //model.Review = new ReviewViewModel();
            
            return View(model);
        }

        public async Task<IActionResult> GetReviewsList(int id)
        {
            var businessUnit = new BusinessUnitViewModel();

            var reviewDTOs = await this.reviewService.GetAllReviewsByBusinessUnitIdAsync(id);

            businessUnit.Reviews = reviewDTOs.Select(x => x.MapFrom()).ToList();

            return PartialView("_ReviewsPartial", businessUnit);
        }
    }
}