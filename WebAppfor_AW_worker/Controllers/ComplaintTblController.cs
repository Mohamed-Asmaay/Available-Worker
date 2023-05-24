using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppfor_AW_worker.Data;
using WebAppfor_AW_worker.Models;

namespace WebAppfor_AW_worker.Controllers
{
    public class ComplaintTblController : Controller
    {
        private readonly WebAppfor_AW_workerContext _context;

        public ComplaintTblController(WebAppfor_AW_workerContext context)
        {
            _context = context;
        }

        // GET: ComplaintTbl
        public async Task<IActionResult> Index()
        {
            var webAppfor_AW_workerContext = _context.ComplaintTbl.Include(c => c.Req).Include(c => c.Us);
            return View(await webAppfor_AW_workerContext.ToListAsync());
        }

        // GET: ComplaintTbl/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ComplaintTbl == null)
            {
                return NotFound();
            }

            var complaintTbl = await _context.ComplaintTbl
                .Include(c => c.Req)
                .Include(c => c.Us)
                .FirstOrDefaultAsync(m => m.ComId == id);
            if (complaintTbl == null)
            {
                return NotFound();
            }

            return View(complaintTbl);
        }

        // GET: ComplaintTbl/Create
        public IActionResult Create()
        {
            ViewData["ReqId"] = new SelectList(_context.Set<RequestTbl>(), "ReqId", "ReqDescription");
            ViewData["UsId"] = new SelectList(_context.Set<UserTbl>(), "UsId", "RegionName");
            return View();
        }

        // POST: ComplaintTbl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComId,UsId,ReqId,ComDate,ComDescription")] ComplaintTbl complaintTbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complaintTbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReqId"] = new SelectList(_context.Set<RequestTbl>(), "ReqId", "ReqDescription", complaintTbl.ReqId);
            ViewData["UsId"] = new SelectList(_context.Set<UserTbl>(), "UsId", "RegionName", complaintTbl.UsId);
            return View(complaintTbl);
        }

        // GET: ComplaintTbl/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ComplaintTbl == null)
            {
                return NotFound();
            }

            var complaintTbl = await _context.ComplaintTbl.FindAsync(id);
            if (complaintTbl == null)
            {
                return NotFound();
            }
            ViewData["ReqId"] = new SelectList(_context.Set<RequestTbl>(), "ReqId", "ReqDescription", complaintTbl.ReqId);
            ViewData["UsId"] = new SelectList(_context.Set<UserTbl>(), "UsId", "RegionName", complaintTbl.UsId);
            return View(complaintTbl);
        }

        // POST: ComplaintTbl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComId,UsId,ReqId,ComDate,ComDescription")] ComplaintTbl complaintTbl)
        {
            if (id != complaintTbl.ComId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complaintTbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplaintTblExists(complaintTbl.ComId))
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
            ViewData["ReqId"] = new SelectList(_context.Set<RequestTbl>(), "ReqId", "ReqDescription", complaintTbl.ReqId);
            ViewData["UsId"] = new SelectList(_context.Set<UserTbl>(), "UsId", "RegionName", complaintTbl.UsId);
            return View(complaintTbl);
        }

        // GET: ComplaintTbl/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ComplaintTbl == null)
            {
                return NotFound();
            }

            var complaintTbl = await _context.ComplaintTbl
                .Include(c => c.Req)
                .Include(c => c.Us)
                .FirstOrDefaultAsync(m => m.ComId == id);
            if (complaintTbl == null)
            {
                return NotFound();
            }

            return View(complaintTbl);
        }

        // POST: ComplaintTbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ComplaintTbl == null)
            {
                return Problem("Entity set 'WebAppfor_AW_workerContext.ComplaintTbl'  is null.");
            }
            var complaintTbl = await _context.ComplaintTbl.FindAsync(id);
            if (complaintTbl != null)
            {
                _context.ComplaintTbl.Remove(complaintTbl);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplaintTblExists(int id)
        {
          return _context.ComplaintTbl.Any(e => e.ComId == id);
        }
    }
}
