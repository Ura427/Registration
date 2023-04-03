using Login_in.Data;
using Login_in.Models;
using Microsoft.AspNetCore.Mvc;

namespace Login_in.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly UsersDbContext _usersDbContext;

        public RegistrationController(UsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }




        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            if(user.UserName == null)
            {
                ModelState.AddModelError("UserName", "Username field is required");
            }

            if(user.Email == null)
            {
                ModelState.AddModelError("Email", "Email field is required");
            }

            if(_usersDbContext.Users.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("Email", "This email is already used");
            }

            if(_usersDbContext.Users.Any(u => u.UserName == user.UserName))
            {
                ModelState.AddModelError("UserName", "This username is already taken");
            }

            if (ModelState.IsValid)
            {
                _usersDbContext.Users.Add(user);
                _usersDbContext.SaveChanges();
                TempData["success"] = "Sighned up successfully";
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }








        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(User user)
        {
            if (user.UserName == null)
            {
                ModelState.AddModelError("UserName", "Username field is required");
            }

            if (user.Email == null)
            {
                ModelState.AddModelError("Email", "Email field is required");
            }
            if (ModelState.IsValid)
            {
                if (!_usersDbContext.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "This email is not registered");
                }

                if (!_usersDbContext.Users.Any(u => u.UserName == user.UserName))
                {
                    ModelState.AddModelError("UserName", "This username doesn't exist");
                }
                if (ModelState.IsValid)
                {
                    if (!(_usersDbContext.Users.FirstOrDefault(u => u.UserName == user.UserName).Email == user.Email))
                    {
                        ModelState.AddModelError("Custom", "Invalid username or email");
                    }

                    if (ModelState.IsValid)
                    {
                        TempData["success"] = "Logged in successfully";
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
           
            return View(user);
        }
    }
}
