﻿using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch.WebUI.Controllers
{

    [Authorize]
    public class CategoriesController : Controller
    {

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategories();
            return View(categories);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Add(categoryDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(categoryDTO);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null) return NotFound();

            var categoryVM = await _categoryService.GetById(id);

            if (categoryVM == null) return NotFound();


            return View(categoryVM);
        }




        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.Update(categoryDTO);
                }
                catch(Exception)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(categoryDTO);
        }





        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
                return NotFound();

            var categoryDto = await _categoryService.GetById(id);

            if (categoryDto == null) return NotFound();

            return View(categoryDto);
        }




        [HttpPost(),ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _categoryService.Remove(id);
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
                return NotFound();

            var categoryDto = await _categoryService.GetById(id);

            if (categoryDto == null)
                return NotFound();


            return View(categoryDto);
        }





    }
}
