using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GreatInnovators.Data;
using GreatInnovators.Models;
using GreatInnovators.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;

namespace GreatInnovators.Controllers
{
    public class GuidesController : Controller
    {
        private static readonly HashSet<String> AllowedExtensions = new HashSet<String> { ".jpg", ".jpeg", ".png", ".gif" };
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public GuidesController(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Guides
        public async Task<IActionResult> Index()
        {
            return this.View(await this._context.Guides.ToListAsync());
        }

        // GET: Guides/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var guide = await this._context.Guides
                .Include(x => x.Languages)
                .Include(x => x.Cities)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (guide == null)
            {
                return this.NotFound();
            }

            return this.View(guide);
        }

        // GET: Guides/Create
        public IActionResult Create()
        {
            return this.View(new GuideEditModel());
        }

        // POST: Guides/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GuideEditModel model)
        {
            var fileName = Path.GetFileName(ContentDispositionHeaderValue.Parse(model.File.ContentDisposition).FileName.Value.Trim('"'));
            var fileExt = Path.GetExtension(fileName);
            if (!GuidesController.AllowedExtensions.Contains(fileExt))
            {
                this.ModelState.AddModelError(nameof(model.File), "This file type is prohibited");
            }   

            if (this.ModelState.IsValid)
            {
                var guide = new Guide
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    Languages = new Collection<GuideLanguage>(),
                    Country = model.Country,
                    Cities = new Collection<GuideCity>(),
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Password = model.Password,
                    Rating = 0,
                    AdditionalInfo = model.AdditionalInfo
                };

                if (model.Languages != null)
                {
                    foreach (var language in model.Languages.Split(',').Select(x => x.Trim()).Where(x => !String.IsNullOrEmpty(x)))
                    {
                        var b = new Language();
                        b.Name = language;
                        guide.Languages.Add(new GuideLanguage
                        {
                            GuideId = guide.Id,
                            Guide = guide,
                            Language = b
                        });
                    }
                }

                if (model.Cities != null)
                {
                    foreach (var city in model.Cities.Split(',').Select(x => x.Trim()).Where(x => !String.IsNullOrEmpty(x)))
                    {
                        var b = new City();
                        b.Name = city;
                        guide.Cities.Add(new GuideCity
                        {
                            GuideId = guide.Id,
                            Guide = guide,
                            City = b
                        });
                    }
                }

                var attachmentPath = Path.Combine(this.hostingEnvironment.WebRootPath, "attachments", guide.Id.ToString("N") + fileExt);
                guide.FilePath = $"/attachments/{guide.Id:N}{fileExt}";
                using (var fileStream = new FileStream(attachmentPath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read))
                {
                    await model.File.CopyToAsync(fileStream);
                }


                this._context.Guides.Add(guide);
                await this._context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        // GET: Guides/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var guide = await this._context.Guides
                .Include(x => x.Languages)
                .Include(x => x.Cities)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (guide == null)
            {
                return this.NotFound();
            }

            var model = new GuideEditModel
            {
                FirstName = guide.FirstName,
                LastName = guide.LastName,
                Age = guide.Age,
                Languages = String.Join(", ", guide.Cities.OrderBy(x => x.CityId).Select(x => x.City.Name)),
                Country = guide.Country,
                Cities = String.Join(", ", guide.Languages.OrderBy(x => x.LanguageId).Select(x => x.Language.Name)),
                PhoneNumber = guide.PhoneNumber,
                Email = guide.Email,
                Password = guide.Password,
                AdditionalInfo = guide.AdditionalInfo
            };

            return this.View(model);
        }

        // POST: Guides/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, GuideEditModel model)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var guide = await this._context.Guides
                 .Include(x => x.Languages)
                 .Include(x => x.Cities)
                 .SingleOrDefaultAsync(m => m.Id == id);

            if (guide == null)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                guide.FirstName = model.FirstName;
                guide.LastName = model.LastName;
                guide.Age = model.Age;
                guide.Country = model.Country;
                guide.PhoneNumber = model.PhoneNumber;
                guide.Email = model.Email;
                guide.Password = model.Password;
                guide.AdditionalInfo = model.AdditionalInfo;
                //var phoneId = hospital.Phones.Any() ? hospital.Phones.Max(x => x.PhoneId) + 1 : 1;
                //hospital.Phones.Clear();
                if (model.Languages != null)
                {
                    foreach (var language in model.Languages.Split(',').Select(x => x.Trim()).Where(x => !String.IsNullOrEmpty(x)))
                    {
                        var b = new Language();
                        b.Name = language;
                        guide.Languages.Add(new GuideLanguage
                        {
                            GuideId = guide.Id,
                            Guide = guide,
                            Language = b
                        });
                    }
                }

                if (model.Cities != null)
                {
                    foreach (var city in model.Cities.Split(',').Select(x => x.Trim()).Where(x => !String.IsNullOrEmpty(x)))
                    {
                        var b = new City();
                        b.Name = city;
                        guide.Cities.Add(new GuideCity
                        {
                            GuideId = guide.Id,
                            Guide = guide,
                            City = b
                        });
                    }
                }

                await this._context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        // GET: Guides/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var guide = await this._context.Guides
                .SingleOrDefaultAsync(m => m.Id == id);
            if (guide == null)
            {
                return this.NotFound();
            }

            return this.View(guide);
        }

        // POST: Hospitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var guide = await this._context.Guides.SingleOrDefaultAsync(m => m.Id == id);
            this._context.Guides.Remove(guide);
            await this._context.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }

        private bool GuideExists(Guid id)
        {
            return _context.Guides.Any(e => e.Id == id);
        }
    }
}
