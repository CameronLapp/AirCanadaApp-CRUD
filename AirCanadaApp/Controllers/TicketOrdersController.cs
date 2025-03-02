using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirCanadaApp.Data;
using AirCanadaApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace AirCanadaApp.Controllers
{
    [Authorize]
    public class TicketOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TicketOrders
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TicketOrder.Include(t => t.FlightData);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TicketOrders/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketOrder = await _context.TicketOrder
                .Include(t => t.FlightData)
                .FirstOrDefaultAsync(m => m.TicketOrderId == id);
            if (ticketOrder == null)
            {
                return NotFound();
            }

            return View(ticketOrder);
        }

        // GET: TicketOrders/Create
        public IActionResult Create()
        {
            ViewData["FlightDataId"] = new SelectList(_context.FlightData, "FlightDataId", "ArrivalCity");
            return View();
        }

        // POST: TicketOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketOrderId,SeatNumber,Class,IsAccessible,SpecialInfo,IsAvailable,FlightDataId")] TicketOrder ticketOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightDataId"] = new SelectList(_context.FlightData, "FlightDataId", "ArrivalCity", ticketOrder.FlightDataId);
            return View(ticketOrder);
        }

        // GET: TicketOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketOrder = await _context.TicketOrder.FindAsync(id);
            if (ticketOrder == null)
            {
                return NotFound();
            }
            ViewData["FlightDataId"] = new SelectList(_context.FlightData, "FlightDataId", "ArrivalCity", ticketOrder.FlightDataId);
            return View(ticketOrder);
        }

        // POST: TicketOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketOrderId,SeatNumber,Class,IsAccessible,SpecialInfo,IsAvailable,FlightDataId")] TicketOrder ticketOrder)
        {
            if (id != ticketOrder.TicketOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketOrderExists(ticketOrder.TicketOrderId))
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
            ViewData["FlightDataId"] = new SelectList(_context.FlightData, "FlightDataId", "ArrivalCity", ticketOrder.FlightDataId);
            return View(ticketOrder);
        }

        // GET: TicketOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketOrder = await _context.TicketOrder
                .Include(t => t.FlightData)
                .FirstOrDefaultAsync(m => m.TicketOrderId == id);
            if (ticketOrder == null)
            {
                return NotFound();
            }

            return View(ticketOrder);
        }

        // POST: TicketOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketOrder = await _context.TicketOrder.FindAsync(id);
            if (ticketOrder != null)
            {
                _context.TicketOrder.Remove(ticketOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketOrderExists(int id)
        {
            return _context.TicketOrder.Any(e => e.TicketOrderId == id);
        }
    }
}
