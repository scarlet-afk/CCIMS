using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CCIMS.Data;
using CCIMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;


namespace CCIMS.Controllers
{
    [Authorize(Roles = "Investigator")]
    public class InvestigationLogsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public InvestigationLogsController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //Add log
        [HttpPost]
        [Authorize(Roles = "Investigator")]
        public async Task<IActionResult> AddLog(int caseId, string actionTaken, string notes)
        {
            Console.WriteLine("🔥 AddLog POST HIT");
            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return Unauthorized();
            }

            var log = new InvestigationLog
            {
                CaseID = caseId,
                ActionTaken = actionTaken,
                Notes = notes,
                InvestigatorId = user.Id,
                CreatedAt = DateTime.UtcNow
            };

            var @case = await _context.Cases.FindAsync(caseId);
            if (@case == null)
            {
                return NotFound();
            }
            if (@case.Status == CaseStatus.Closed)
            {
                return Forbid();
            }

            _context.InvestigationLogs.Add(log);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Cases", new { id = caseId });
        }


        // GET: InvestigationLogs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InvestigationLogs.Include(i => i.Case);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvestigationLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigationLog = await _context.InvestigationLogs
                .Include(i => i.Case)
                .FirstOrDefaultAsync(m => m.LogID == id);
            if (investigationLog == null)
            {
                return NotFound();
            }

            return View(investigationLog);
        }

        //// GET: InvestigationLogs/Create
        //[Authorize(Roles = "Investigator")]
        //public IActionResult Create()
        //{
        //    ViewData["CaseID"] = new SelectList(_context.Cases, "CaseID", "Title");
        //    return View();
        //}

        
        // POST: InvestigationLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        // GET: InvestigationLogs/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var investigationLog = await _context.InvestigationLogs.FindAsync(id);
        //    if (investigationLog == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CaseID"] = new SelectList(_context.Cases, "CaseID", "Title", investigationLog.CaseID);
        //    return View(investigationLog);
        //}

        // POST: InvestigationLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("LogID,CaseID,ActionDatte,ActionTaken,Notes")] InvestigationLog investigationLog)
        //{
        //    if (id != investigationLog.LogID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(investigationLog);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!InvestigationLogExists(investigationLog.LogID))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CaseID"] = new SelectList(_context.Cases, "CaseID", "Title", investigationLog.CaseID);
        //    return View(investigationLog);
        //}

        //// GET: InvestigationLogs/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var investigationLog = await _context.InvestigationLogs
        //        .Include(i => i.Case)
        //        .FirstOrDefaultAsync(m => m.LogID == id);
        //    if (investigationLog == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(investigationLog);
        //}

        //// POST: InvestigationLogs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var investigationLog = await _context.InvestigationLogs.FindAsync(id);
        //    if (investigationLog != null)
        //    {
        //        _context.InvestigationLogs.Remove(investigationLog);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool InvestigationLogExists(int id)
        //{
        //    return _context.InvestigationLogs.Any(e => e.LogID == id);
        //}
    }
}
