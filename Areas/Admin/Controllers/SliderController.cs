using MegaOne.Contexts;
using MegaOne.Helpers;
using MegaOne.Models;
using MegaOne.ViewModels.SliderVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace MegaOne.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {

        MegaOneDBContext _db { get; }
        IWebHostEnvironment _env { get; }
        public SliderController(MegaOneDBContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _db.Sliders.Select(s => new SliderListItemVM
            {
                Id = s.Id,
                Description = s.Description,
                Image = s.Image,
                IsLeft = s.IsLeft,
                Text = s.Text,
                Title = s.Title
            }).ToListAsync();
            return View(items);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SliderCreateVM vm)
        {
            string fileName = Path.Combine(PathConstants.Slider, vm.ImageFile.FileName);
            using (FileStream fs = System.IO.File.Create(Path.Combine(_env.WebRootPath, fileName)))
            {
                await vm.ImageFile.CopyToAsync(fs);
            };
            Slider slider = new Slider
            {
                Title = vm.Title,
                Text = vm.Text,
                IsLeft = vm.IsLeft,
                Image = await vm.ImageFile.SaveAsync(PathConstants.Slider),
                Description = vm.Description,
            };
            await _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _db.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            _db.Sliders.Remove(data);
            await _db.SaveChangesAsync();
            return Ok();
        }



        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            var data = await _db.Sliders
                .SingleOrDefaultAsync(p => p.Id == id);
            //.Include(p => p.ProductColors)
            //    .ThenInclude(pc => pc.Color)
            //.Include(p => p.Category)
            if (data == null) return NotFound();

            var vm = new SliderUpdateVM
            {
              
                Description=data.Description,
                Id=data.Id,
                IsLeft = data.IsLeft,
                Text = data.Text,
                Title = data.Title,
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, SliderUpdateVM vm)
        {
            if (id == null || id <= 0) return BadRequest();
          
            var data = await _db.Sliders
                .SingleOrDefaultAsync(p => p.Id == id);
           
            data.Title=vm.Title;
            data.Text=vm.Text;
            data.IsLeft=vm.IsLeft;
            data.Description=vm.Description;
            data.Id=vm.Id;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
