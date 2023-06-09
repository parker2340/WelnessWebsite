﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WelnessWebsite.Data;
using WelnessWebsite.Models;
using WelnessWebsite.Functions;
using Microsoft.AspNetCore.Http;


namespace WelnessWebsite.Controllers
{
    public class UsersController : Controller
    {
        private readonly WelnessWebsiteContext _context;

        public UsersController(WelnessWebsiteContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.User != null ? 
                          View(await _context.User.ToListAsync()) :
                          Problem("Entity set 'WelnessWebsiteContext.User'  is null.");
        }

        [HttpPost]
        public async Task<IActionResult> index(User newUser)
        {
            var functions = new MyFunctions();


                // Find the user by username
                var user = await _context.User.FirstOrDefaultAsync(u => u.Email == newUser.Email);


                if (user != null)
                {
                    // Hash the user-entered password
                    string hashedPassword = functions.HashPassword(newUser.Password);

                    // Compare the hashed password with the one stored in the user entity
                    if (hashedPassword == user.Password)
                    {
                        // Passwords match, create a login session and store user ID
                        HttpContext.Session.SetInt32("UserId", user.ID);

                    // Redirect to the desired page (e.g., Home/Index)
                    return RedirectToAction("Index", "Home", new { id = user.ID });
                }
            }

                // Invalid username or password


            return View(newUser);

        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.ID == userId);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }   

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {

                var functions = new MyFunctions();

                // Hash the password
                user.Password = functions.HashPassword(user.Password);

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Email,Password")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            var existingUser = await _context.User.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(user.Password))
            {
                var functions = new MyFunctions();
                user.Password = functions.HashPassword(user.Password);
            }
            else
            {
                user.Password = existingUser.Password;
            }

            try
            {
                _context.Entry(existingUser).CurrentValues.SetValues(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Details));
        }



        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'WelnessWebsiteContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.User?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
