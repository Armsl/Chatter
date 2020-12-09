using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatterSite.Models;
using Core3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatterSite.Controllers
{
    public class ChatterController : Controller
    {
        #region Properties
        //Database Instance calling
        public readonly ApplicationDbContext _dbContexts;
        //User Instance
        public readonly UserManager<User> _user;
        #endregion

        #region Constructor
        public ChatterController(ApplicationDbContext dbContexts, UserManager<User> user)
        {
            _dbContexts = dbContexts;
            _user = user;
        }
        #endregion

        #region Public Methods
        public async Task<IActionResult> Index()
        {
                var currentUser = await _user.GetUserAsync(User);
                if (currentUser != null)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        ViewBag.CurrentUserName = currentUser.UserName;
                    }
                    var messages = await _dbContexts.Messages.OrderByDescending(m => m.CurrentTime).Take(50).ToListAsync();
                    return View(messages);
                }
                return View();
        }

        [Authorize]
        public async Task<IActionResult> Create(Message message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    message.UserName = User.Identity.Name;
                    var sender = await _user.GetUserAsync(User);
                    message.UserID = sender.Id;
                    await _dbContexts.Messages.AddAsync(message);
                    await _dbContexts.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Error", "Home");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}