﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Models;

namespace ProjetoMVC
{
    public class PromocaosController : Controller
    {
        private readonly PromocaoContext _context;

        public PromocaosController(PromocaoContext context)
        {
            _context = context;
        }

        // GET: Promocaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Promocoes.ToListAsync());
        }

        // GET: Promocaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocao = await _context.Promocoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promocao == null)
            {
                return NotFound();
            }

            return View(promocao);
        }

        // GET: Promocaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Promocaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PacotesCompletos,PacotesIndividuais,PromocaoVerao")] Promocao promocao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promocao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promocao);
        }

        // GET: Promocaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocao = await _context.Promocoes.FindAsync(id);
            if (promocao == null)
            {
                return NotFound();
            }
            return View(promocao);
        }

        // POST: Promocaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PacotesCompletos,PacotesIndividuais,PromocaoVerao")] Promocao promocao)
        {
            if (id != promocao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promocao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromocaoExists(promocao.Id))
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
            return View(promocao);
        }

        // GET: Promocaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promocao = await _context.Promocoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (promocao == null)
            {
                return NotFound();
            }

            return View(promocao);
        }

        // POST: Promocaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promocao = await _context.Promocoes.FindAsync(id);
            _context.Promocoes.Remove(promocao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromocaoExists(int id)
        {
            return _context.Promocoes.Any(e => e.Id == id);
        }
    }
}
