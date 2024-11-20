using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroceryApp.Context;
using GroceryApp.Models;

namespace GroceryApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            // Get data of both categories and items.
            var categories = await _context.Category.ToListAsync();
            var items = await _context.Item.ToListAsync();

            // Create data structure using IndexModel to be used in the Index.cshtml file.
            var data = new IndexModel
            {
                Categories = categories,
                Item = items,
            };
            return View(data);
        }

        // GET: Items/Create
        public async Task<IActionResult> Create()
        {
            // Get SQL data of all the categories.
            var categories = await _context.Category.ToListAsync();

            // Create data structure using CreateModel and the categories in the database, along with the Item field names. 
            var data = new CreateModel
            {
                Categories = categories,
                Item = new Items(),
            };

            // Returns to Create.cshtml file.
            return View(data);
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ItemName, ItemDescription, CategoryId")] Items item)
        {
            // First check to see if form is valid.
            if (ModelState.IsValid)
            {
                // Use Items model to store the input.
                var data = new Items
                {
                    ItemName = item.ItemName,
                    ItemDescription = item.ItemDescription,
                    CategoryId = item.CategoryId 
                };

                // Add the record to the database.
                _context.Item.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            // Check if id is passed.
            if (id == null)
            {
                return NotFound();
            }

            // Get item by id that is passed.
            var items = await _context.Item.FindAsync(id);

            // Get all categories.
            var categories = await _context.Category.ToListAsync();
            if (items == null)
            {
                return NotFound();
            }

            // Pass data from 1 record and all category names.
            var data = new EditModel
            {
                Categories = categories,
                Item = items,
            };

            return View(data);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemName,ItemDescription,CategoryId")] Items item)
        {
            // Get data from form.
            var data = new Items
            {
                Id = id,
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                CategoryId = item.CategoryId
            };

            if (ModelState.IsValid)
            {
                try
                {
                    // Try to update data.
                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }

                // Throw error if form is submitted.
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(data);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var items = await _context.Item.FindAsync(id);
            if (items != null)
            {
                _context.Item.Remove(items);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemsExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }

    }
}
