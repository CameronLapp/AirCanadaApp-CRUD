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
    public class FlightDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FlightDatas
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.FlightData.ToListAsync());
        }

        // GET: FlightDatas/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightData = await _context.FlightData
                .FirstOrDefaultAsync(m => m.FlightDataId == id);
            if (flightData == null)
            {
                return NotFound();
            }

            return View(flightData);
        }

        // GET: FlightDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FlightDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightDataId,FlightNumber,DepartureCity,ArrivalCity,DepartureTime,ArrivalTime,Price")] FlightData flightData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flightData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flightData);
        }

        // GET: FlightDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightData = await _context.FlightData.FindAsync(id);
            if (flightData == null)
            {
                return NotFound();
            }
            return View(flightData);
        }

        // POST: FlightDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightDataId,FlightNumber,DepartureCity,ArrivalCity,DepartureTime,ArrivalTime,Price")] FlightData flightData)
        {
            if (id != flightData.FlightDataId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flightData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightDataExists(flightData.FlightDataId))
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
            return View(flightData);
        }

        // GET: FlightDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightData = await _context.FlightData
                .FirstOrDefaultAsync(m => m.FlightDataId == id);
            if (flightData == null)
            {
                return NotFound();
            }

            return View(flightData);
        }

        // POST: FlightDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flightData = await _context.FlightData.FindAsync(id);
            if (flightData != null)
            {
                _context.FlightData.Remove(flightData);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightDataExists(int id)
        {
            return _context.FlightData.Any(e => e.FlightDataId == id);
        }

        public object DeleteConfirmed(string flightNumber)
        {
            throw new NotImplementedException();
        }
    }
}
