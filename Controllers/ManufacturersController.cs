using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DB;
using OnlineStore.Models;
using OnlineStore.Models.ViewModels;

namespace OnlineStore.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly OnlineStoreContext _context;

        public ManufacturersController(OnlineStoreContext context)
        {
            _context = context;
        }

        // GET: Manufacturers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Manufacturers.ToListAsync());
        }


        // GET: Manufacturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }


        // GET: Manufacturers/Create
        public IActionResult Create()
        {
            ManufacturerViewModel viewModel = new ManufacturerViewModel
            {
                Manufacturer = new Manufacturer()
            };
            return View(viewModel);
        }

        // POST: Manufacturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManufacturerViewModel model)
        {
            if (model.Image == null)
            {
                ModelState.AddModelError("Image", "No image have been chosen");
            }

            if (ModelState.IsValid)
            {
                Manufacturer manufacturer = new Manufacturer
                {
                    Title = model.Manufacturer.Title
                };

                byte[] data = null;
                using (BinaryReader br = new BinaryReader(model.Image.OpenReadStream()))
                {
                    data = br.ReadBytes((int)model.Image.Length);
                    manufacturer.Image = data;
                }

                _context.Manufacturers.Add(manufacturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Manufacturers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null)
            {
                return NotFound();
            }
            ManufacturerViewModel viewModel = new ManufacturerViewModel
            {
                Manufacturer = manufacturer
            };

            return View(viewModel);
        }

        // POST: Manufacturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ManufacturerViewModel model)
        {
            if (id != model.Manufacturer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Image != null)
                    {
                        byte[] data = null;
                        using (BinaryReader br = new BinaryReader(model.Image.OpenReadStream()))
                        {
                            data = br.ReadBytes((int)model.Image.Length);
                            model.Manufacturer.Image = data;
                        }
                    }


                    _context.Update(model.Manufacturer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManufacturerExists(model.Manufacturer.Id))
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
            return View(model);
        }

        // GET: Manufacturers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var manufacturer = await _context.Manufacturers.FindAsync(id);

            ////////////////////////////////////
            // ClientSetNull в OnModelCreating не работает, приходится удалять связи руками ниже. Nullable<ManufacturerId> тоже не работает
            var products = _context.Products.Where(p => p.ManufacturerId == manufacturer.Id);

            foreach (var item in products)
            {
                item.Manufacturer = null;
            }
            ////////////////////////////////////

            _context.Manufacturers.Remove(manufacturer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManufacturerExists(int id)
        {
            return _context.Manufacturers.Any(e => e.Id == id);
        }
    }
}
