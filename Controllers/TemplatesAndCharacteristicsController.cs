using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStore.DB;
using OnlineStore.Models;
using OnlineStore.Models.ModelsDTO;
using OnlineStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    [Authorize(Roles = "admin")]
    public class TemplatesAndCharacteristicsController : Controller
    {
        private readonly OnlineStoreContext _context;
        private readonly IMapper _mapper;

        public TemplatesAndCharacteristicsController(OnlineStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var templates = await _context.Templates.ToListAsync();

            return View(templates);
        }

        public async Task<IActionResult> DetailsTemplate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates
                .FirstOrDefaultAsync(t => t.Id == id);

            if (template == null)
            {
                return NotFound();
            }

            var templatesListDto = _mapper.Map<TemplatesListDto>(template);

            ViewBag.Id = template.Id;
            ViewBag.Title = template.Title;

            return View(templatesListDto);
        }


        public IActionResult CreateTemplate()
        {

            return View(/*new TemplatesListDto { ListDto = new List<TemplateDto>() { new TemplateDto()} }*/);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTemplate(List<TemplateDto> listDto, string title)
        {
            TemplatesListDto templatesListDto = new TemplatesListDto { ListDto = listDto };

            Template template = _mapper.Map<Template>(templatesListDto);
            template.Title = title;

            await _context.AddAsync(template);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> EditTemplate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates
                .FirstOrDefaultAsync(t => t.Id == id);

            if (template == null)
            {
                return NotFound();
            }

            var templatesListDto = _mapper.Map<TemplatesListDto>(template);

            ViewBag.Id = template.Id;
            ViewBag.Title = template.Title;

            return View(templatesListDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTemplate(List<TemplateDto> listDto, int? id, string title)
        {
            if (id == null)
            {
                return BadRequest();
            }

            TemplatesListDto templatesListDto = new TemplatesListDto { ListDto = listDto };
            Template template = _mapper.Map<Template>(templatesListDto);
            template.Id = id.Value;
            template.Title = title;

            _context.Entry<Template>(template).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteTemplate(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates.FirstOrDefaultAsync(t => t.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            var templatesListDto = _mapper.Map<TemplatesListDto>(template);

            ViewBag.Id = template.Id;
            ViewBag.Title = template.Title;

            return View(templatesListDto);
        }

        [HttpPost, ActionName("DeleteTemplate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTemplateConfirmed(int? id)
        {

            var template = await _context.Templates.FindAsync(id);
            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        /////////////////////////////////////////////////////////////////////////////


        public async Task<IActionResult> AddTemplateToProductsByCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates.FirstOrDefaultAsync(t => t.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            var templatesListDto = _mapper.Map<TemplatesListDto>(template);

            ViewBag.Id = template.Id;
            ViewBag.Title = template.Title;

            var categories = await _context.Categories
                .Include(c => c.Template)
                .Where(c => c.Categories.Count == 0)
                .ToListAsync();

            //categories.Insert(0, new Category { Id = 0, Title = "Not Seleted" });
            //SelectList categoriesList = new SelectList(categories, "Id", "Title");
            IList<SelectListItem> selectListItems = categories
                .Select(c => new SelectListItem
                {
                    Text = c.Template != null ? c.Title + $" (The category allready contains \"{c.Template.Title}\" template)" : c.Title,
                    Value = c.Id.ToString(),
                    Disabled = c.Template != null
                }).ToList();
            SelectList categoriesList = new SelectList(selectListItems, "Value", "Text"/*, categories.FirstOrDefault(c => c.Template.Id == template.Id)*/);

            ViewBag.CategoriesList = categoriesList;

            return View(templatesListDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTemplateToProductsByCategory(int? id, int categoryId)
        {
            if (id == null)
            {
                return NotFound();
            }
            Template template = await _context.Templates.FirstOrDefaultAsync(t => t.Id == id);

            if (template == null)
            {
                return NotFound();
            }

             

            TemplatesListDto templatesListDto = _mapper.Map<TemplatesListDto>(template);

            CharacteristicsListDto characteristicsListDto = new CharacteristicsListDto() { ListDto = new List<CharacteristicDto>() };

            foreach (var templateDto in templatesListDto.ListDto)
            {
                characteristicsListDto.ListDto.Add(new CharacteristicDto
                {
                    Property = templateDto.Property,
                    // Value = "",
                    TakePartInSort = templateDto.TakePartInSort
                });
            }


            Category category = await _context.Categories
                .Include(c => c.Template)
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            category.TemplateId = template.Id;
            _context.Categories.Update(category);

            ////////////////////////////////////////
            var product = await _context.CategoryProducts
                .Include(cp => cp.Product)
                    .ThenInclude(p => p.Characteristic)
                .Where(cp => cp.CategoryId == categoryId)
                .Select(p => p.Product).FirstOrDefaultAsync(p => p.Characteristic != null);

            Characteristic characteristic;

            if (product != null)
            {
                CharacteristicsListDto characteristicsListDtoToUpdate = _mapper.Map<CharacteristicsListDto>(product.Characteristic);

                foreach (var item in characteristicsListDto.ListDto)
                {
                    if (characteristicsListDtoToUpdate.ListDto.Where(i => i.Property != item.Property).FirstOrDefault().Property != item.Property) // (!)
                    {
                        characteristicsListDtoToUpdate.ListDto.Add(item);
                    }
                }

                characteristic = _mapper.Map<Characteristic>(characteristicsListDtoToUpdate);
            }
            else
            {
                characteristic = _mapper.Map<Characteristic>(characteristicsListDto);
            }
            ////////////////////////////////////////////////////////////////////////////////////////

            //Characteristic characteristic = _mapper.Map<Characteristic>(characteristicsListDto);

            List<Product> products = await _context.CategoryProducts
                .Include(cp => cp.Category)
                .Include(cp => cp.Product)
                    .ThenInclude(p => p.Characteristic)
                .Where(cp => cp.CategoryId == categoryId)
                .Select(p => p.Product)
                .ToListAsync();

            //_context.Characteristics.Load();
            foreach (var p in products)
            {
                Characteristic toUpdateCharacteristic = new Characteristic
                {
                    ProductId = p.Id,
                    SerializedCharactetistics = characteristic.SerializedCharactetistics
                };

                _context.Characteristics.Add(toUpdateCharacteristic);
            }



            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }
    }
}
