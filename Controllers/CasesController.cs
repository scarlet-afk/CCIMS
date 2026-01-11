using CCIMS.Data;
using CCIMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CCIMS.Controllers
{
    [Authorize]
    public class CasesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public CasesController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Cases
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cases.ToListAsync());
        }

        // GET: Cases/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caseItem = await _context.Cases
                .Include(c => c.InvestigationLogs)
                .FirstOrDefaultAsync(m => m.CaseID == id);

            if (caseItem == null)
            {
                return NotFound();
            }

            return View(caseItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Investigator")]
        public async Task<IActionResult> AddLog(int caseId, string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return RedirectToAction(nameof(Details), new { id = caseId });
            }
            var investigatorName = User.Identity?.Name;

            var user = await _userManager.GetUserAsync(User);

            var log = new InvestigationLog
            {
                CaseID = caseId,
                Notes = description,
                CreatedAt = DateTime.Now,
                InvestigatorName = investigatorName,
                InvestigatorId = user.Id
            };

            _context.InvestigationLogs.Add(log);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Cases", new { id = caseId });
        }


        // GET: Cases/Create
        [Authorize(Roles = "Investigator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CaseID,Title,Category,Location,DateField,Status,Description")] Case @case)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@case);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@case);
        }

        // GET: Cases/Edit/5
        [Authorize(Roles = "Investigator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case = await _context.Cases.FindAsync(id);
            if (@case == null)
            {
                return NotFound();
            }
            return View(@case);
        }

        // POST: Cases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Case updatedCase)
        {
            if (id != updatedCase.CaseID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                return View(updatedCase);
            }

            var existingCase = await _context.Cases
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CaseID == id);

            if (existingCase == null)
            {
                return NotFound();
            }

            var oldStatus = existingCase.Status;
            try
            {
                _context.Update(updatedCase);

                if (oldStatus != updatedCase.Status)
                {
                    var user = await _userManager.GetUserAsync(User);
                    var log = new InvestigationLog
                    {
                        CaseID = updatedCase.CaseID,
                        ActionTaken = "Status changed",
                        Notes = $"Case status changed from {oldStatus} to {updatedCase.Status}",
                        CreatedAt = DateTime.Now,
                        InvestigatorId = user.Id,
                        InvestigatorName = user.Email
                    };
                    _context.InvestigationLogs.Add(log);
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseExists(updatedCase.CaseID))
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

        // GET: Cases/Delete/5
        [Authorize(Roles = "Investigator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @case = await _context.Cases
                .FirstOrDefaultAsync(m => m.CaseID == id);
            if (@case == null)
            {
                return NotFound();
            }

            return View(@case);
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @case = await _context.Cases.FindAsync(id);
            if (@case != null)
            {
                _context.Cases.Remove(@case);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaseExists(int id)
        {
            return _context.Cases.Any(e => e.CaseID == id);
        }
    }
}
