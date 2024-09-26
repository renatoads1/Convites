using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Convite.Data;
using Convite.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Convite.Controllers
{
    public class EnderecosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnderecosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Enderecos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Enderecos.Include(e => e.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Enderecos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _context.Enderecos
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }



        [Authorize]
        // GET: Enderecos/Create
        public IActionResult Create()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity.Name;
            

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Enderecos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Logradouro,Numero,Cidade,Estado,CEP,UserId")] Endereco endereco)
        {
            try
            {
                _context.Add(endereco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e) {
               
            }
            //if (ModelState.IsValid)
            //{
            //    _context.Add(endereco);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", endereco.UserId);
            return View(endereco);
        }

        // GET: Enderecos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", endereco.UserId);
            return View(endereco);
        }

        // POST: Enderecos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logradouro,Numero,Cidade,Estado,CEP,UserId")] Endereco endereco)
        {
            if (id != endereco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(endereco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnderecoExists(endereco.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", endereco.UserId);
            return View(endereco);
        }

        // GET: Enderecos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var endereco = await _context.Enderecos
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endereco == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // POST: Enderecos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco != null)
            {
                _context.Enderecos.Remove(endereco);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoExists(int id)
        {
            return _context.Enderecos.Any(e => e.Id == id);
        }
    }
}
