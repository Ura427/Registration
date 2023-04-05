using Login_in.Data;
using Login_in.DTOs;
using Login_in.Models;
using Microsoft.AspNetCore.Mvc;

namespace Login_in.Controllers
{
    public class RegistrationController : Controller
    {
        public static bool userIsLoggedIn { get; set; }//Check if user is logged in
        public static User currentUser { get; set; }//Receive an user instance, to reach its properties, such as UserName and Email
        public static bool admin { get; set; }

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
        public IActionResult SignUp(SighUpDto user)
        {
            //Check if inputed email matches any from database
            if (_usersDbContext.Users.Any(u => u.Email == user.Email)) { ModelState.AddModelError("Email", "This email is already used"); }

            //Check if input username matches any from database
            if (_usersDbContext.Users.Any(u => u.UserName == user.UserName)) { ModelState.AddModelError("UserName", "This username is already taken"); }

            //Check if passwords match
            if(user.Password != user.PasswordConfirm) { ModelState.AddModelError("PasswordConfirm", "Passwords don't match"); }

            //If there is no errors till this moment, go to the next validations 
            if (ModelState.IsValid)
            {

                User localUser = new User
                {
                    //Id = _usersDbContext.Users.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1,
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                };
                _usersDbContext.Users.Add(localUser);//Add user to database
                _usersDbContext.SaveChanges();//Save changes
                TempData["success"] = "Sighned up successfully";//Adding a message to be displayed in toast notification
                return RedirectToAction("Index", "Home");//return to home page on success
            }

            return View(user);//returns the same page on failure
        }








        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(LogInDto user)
        {
            //if (!_usersDbContext.Users.Any(u => u.Email == user.Email)) { ModelState.AddModelError("Email", "This email is not registered"); }

            if (!_usersDbContext.Users.Any(u => u.UserName == user.UserName)) { ModelState.AddModelError("UserName", "This username doesn't exist"); }

            if (ModelState.IsValid)
            {
                User localUser = _usersDbContext.Users.FirstOrDefault(u => u.UserName == user.UserName);//Creating instance of user to reduce amount of code
                if (!(localUser.Password == user.Password))
                    ModelState.AddModelError("Custom", "Invalid username or password"); 

                if (ModelState.IsValid)
                {
                    TempData["success"] = "Logged in successfully";
                    currentUser = localUser;//Get insatnce of user
                    userIsLoggedIn = true;//Changing userIsLoggedIn value
                    admin = localUser.isAdmin;
                    return RedirectToAction("Index", "Home");
                }
            }


            return View(user);
        }





        public IActionResult LogOut()
        {
            userIsLoggedIn = false;
            admin = false;
            currentUser = null;
            TempData["success"] = "Logged out successfully";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null) return NotFound();

            var user = _usersDbContext.Users.Find(id);

            if (user == null) return NotFound();

            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            if(ModelState.IsValid)
            {
                _usersDbContext.Users.Update(user);
                _usersDbContext.SaveChanges();
                TempData["success"] = "User data was successfully updated";
                return RedirectToAction("Index", "Home");
            }
            return View(user); 
        }



        public IActionResult Delete(int? id) 
        {
            if(id == null) return NotFound();
          
            var user = _usersDbContext.Users.Find(id);

            if (user == null) return NotFound();
            

            if (ModelState.IsValid)
            {
                _usersDbContext.Users.Remove(user);
                _usersDbContext.SaveChanges();
                TempData["success"] = "User was successfully deleted";
            }
            return RedirectToAction("Index", "Home");
        }


    }
}
