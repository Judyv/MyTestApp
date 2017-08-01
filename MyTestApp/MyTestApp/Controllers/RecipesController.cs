using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTestApp.Data;
using MyTestApp.Models;

namespace MyTestApp.Controllers
{
    public class RecipesController : Controller
    {
        private readonly TestContext _context;

        public RecipesController(TestContext context)
        {
            _context = context;    
        }

        // GET: Recipes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recipes.ToListAsync());
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(s => s.Steps)
                .Include(s => s.Ingredients)
                .ThenInclude(s => s.Unit)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        private void PopulateItemsDropDownList(object selectedItem = null)
        {
            var unitsQuery = from i in _context.Units
                             orderby i.UnitName
                             select i;

            ViewBag.unitList = new SelectList(unitsQuery.AsNoTracking(), "ID", "UnitName", selectedItem);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
     
            var recipe = await _context.Recipes
                .Include(s => s.Steps)
                .Include(s => s.Ingredients)
                .ThenInclude(s => s.Unit)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (recipe == null)
            {
                return NotFound();
            }
            PopulateItemsDropDownList();

            return View(recipe);
        }

        // POST: Recipes/Edit/5   change
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Ingredients,Steps")] Recipe recipe)
        {
            if (id != recipe.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .SingleOrDefaultAsync(m => m.ID == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.SingleOrDefaultAsync(m => m.ID == id);
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.ID == id);
        }

        public ActionResult AddNewIngredient()
        {
            return PartialView("IngredientRow", new Ingredient());
        }
    }
}
