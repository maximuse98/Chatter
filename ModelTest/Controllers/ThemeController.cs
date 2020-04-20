using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelTest.Models;

namespace ModelTest.Controllers
{
    [Authorize]
    public class ThemeController : Controller
    {
        private readonly ApplicationContext db;

        public ThemeController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                Theme theme = await db.Themes.Include(t => t.Messages).FirstOrDefaultAsync(p => p.Id == id);
                if (theme != null)
                {
                    return View(theme);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Send(string text, int id)
        {
            if(text == null || text.Trim() == "")
            {
                return RedirectToAction("Index", new { id });
            }

            Theme theme = await db.Themes.FirstOrDefaultAsync(p => p.Id == id);

            Message message = new Message { Text = text, User = User.Identity.Name, Theme = theme, Time = DateTime.Now };
            db.Messages.Add(message);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", new {id});
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Message message = await db.Messages.Include(p => p.Theme).FirstOrDefaultAsync(p => p.Id == id);
                if (message != null || message.User == User.Identity.Name)
                    return View(message);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Message message, int themeId)
        {
            if(message.Text == null || message.Text.Trim() == "")
            {
                return RedirectToAction("Index", new {id = themeId });
            }
            message.User = User.Identity.Name;
            message.Time = DateTime.Now;

            db.Messages.Update(message);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = themeId });
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id, int themeId)
        {
            if (id != null)
            {
                Message message = await db.Messages.Include(t => t.Theme).FirstOrDefaultAsync(p => p.Id == id);
                if (message != null)
                {
                    Theme theme = await db.Themes.Include(p => p.Messages).FirstOrDefaultAsync(p => p.Id == themeId);
                    db.Messages.Remove(message);
                    await db.SaveChangesAsync();

                    // удаление темы, усли в ней не осталось сообщений
                    if (theme.Messages.Count == 0)
                    {
                        db.Themes.Remove(theme);
                        await db.SaveChangesAsync();

                        return RedirectToAction("Index", "Home");
                    }

                    return RedirectToAction("Index", new { id = themeId });
                }
            }
            return NotFound();
        }
    }
}