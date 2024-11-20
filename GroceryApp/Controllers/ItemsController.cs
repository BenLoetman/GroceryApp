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
            var viewModel = new IndexModel
            {
                Categories = categories,
                Item = items,
            };
            return View(viewModel);
        }

        // GET: Items/Create
        public async Task<IActionResult> Create()
        {
            var categories = await _context.Category.ToListAsync();
            var viewModel = new IndexViewModel
            {
                Categories = categories,
                Item = new Items(),
            };
            return View(viewModel);
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ItemName, ItemDescription, CategoryId")] Items item)
        {
            if (ModelState.IsValid)
            {
                var newItem = new Items
                {
                    ItemName = item.ItemName,
                    ItemDescription = item.ItemDescription,
                    CategoryId = item.CategoryId 
                };
                _context.Item.Add(newItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var items = await _context.Item.FindAsync(id);
            var categories = await _context.Category.ToListAsync();
            if (items == null)
            {
                return NotFound();
            }

            var viewModel = new EditModel
            {
                Categories = categories,
                Item = items,
            };

            return View(viewModel);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemName,ItemDescription,CategoryId")] Items item)
        {
            var newItem = new Items
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
                    _context.Update(newItem);
                    await _context.SaveChangesAsync();
                }
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
            return View(newItem);
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
