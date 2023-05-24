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
    public class RequestTblController : Controller
    {
        private readonly WebAppfor_AW_workerContext _context;

        public RequestTblController(WebAppfor_AW_workerContext context)
        {
            _context = context;
        }

        // GET: RequestTbl
        public async Task<IActionResult> Index()
        {
            var webAppfor_AW_workerContext = _context.RequestTbl.Include(r => r.Us).Include(r => r.Wr);
            return View(await webAppfor_AW_workerContext.ToListAsync());
        }

        // GET: RequestTbl/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RequestTbl == null)
            {
                return NotFound();
            }

            var requestTbl = await _context.RequestTbl
                .Include(r => r.Us)
                .Include(r => r.Wr)
                .FirstOrDefaultAsync(m => m.ReqId == id);
            if (requestTbl == null)
            {
                return NotFound();
            }

            return View(requestTbl);
        }

        // GET: RequestTbl/Create
        public IActionResult Create()
        {
            ViewData["UsId"] = new SelectList(_context.UserTbl, "UsId", "RegionName");
            ViewData["WrId"] = new SelectList(_context.WorkerTbl, "WrId", "JobName");
            return View();
        }

        // POST: RequestTbl/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReqTime,ReqProblem,ReqDescription,ReqCost,WrId,UsId,ReqAccept,ReqDecline,ReqConfirmation")] RequestTbl requestTbl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requestTbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsId"] = new SelectList(_context.UserTbl, "UsId", "RegionName", requestTbl.UsId);
            ViewData["WrId"] = new SelectList(_context.WorkerTbl, "WrId", "JobName", requestTbl.WrId);
            return View(requestTbl);
        }

        // GET: RequestTbl/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RequestTbl == null)
            {
                return NotFound();
            }

            var requestTbl = await _context.RequestTbl.FindAsync(id);
            if (requestTbl == null)
            {
                return NotFound();
            }
            ViewData["UsId"] = new SelectList(_context.UserTbl, "UsId", "RegionName", requestTbl.UsId);
            ViewData["WrId"] = new SelectList(_context.WorkerTbl, "WrId", "JobName", requestTbl.WrId);
            return View(requestTbl);
        }

        // POST: RequestTbl/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReqTime,ReqProblem,ReqDescription,ReqCost,WrId,UsId,ReqAccept,ReqDecline,ReqConfirmation")] RequestTbl requestTbl)
        {
            if (id != requestTbl.ReqId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestTbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestTblExists(requestTbl.ReqId))
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
            ViewData["UsId"] = new SelectList(_context.UserTbl, "UsId", "RegionName", requestTbl.UsId);
            ViewData["WrId"] = new SelectList(_context.WorkerTbl, "WrId", "JobName", requestTbl.WrId);
            return View(requestTbl);
        }

        // GET: RequestTbl/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RequestTbl == null)
            {
                return NotFound();
            }

            var requestTbl = await _context.RequestTbl
                .Include(r => r.Us)
                .Include(r => r.Wr)
                .FirstOrDefaultAsync(m => m.ReqId == id);
            if (requestTbl == null)
            {
                return NotFound();
            }

            return View(requestTbl);
        }

        // POST: RequestTbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RequestTbl == null)
            {
                return Problem("Entity set 'WebAppfor_AW_workerContext.RequestTbl'  is null.");
            }
            var requestTbl = await _context.RequestTbl.FindAsync(id);
            if (requestTbl != null)
            {
                _context.RequestTbl.Remove(requestTbl);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestTblExists(int id)
        {
          return _context.RequestTbl.Any(e => e.ReqId == id);
        }
    }
}
