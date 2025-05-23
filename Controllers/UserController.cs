using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // GET: User
    public ActionResult Index(string searchString)
    {
        var users = userlist.AsEnumerable();
        if (!string.IsNullOrEmpty(searchString))
        {
            searchString = searchString.ToLower();
            users = users.Where(u =>
                (!string.IsNullOrEmpty(u.Name) && u.Name.ToLower().Contains(searchString)) ||
                (!string.IsNullOrEmpty(u.Email) && u.Email.ToLower().Contains(searchString)) ||
                (!string.IsNullOrEmpty(u.Phone) && u.Phone.ToLower().Contains(searchString))
            );
        }
        return View(users);
    }

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // GET: User/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: User/Create
    [HttpPost]
    public ActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            user.Id = userlist.Count > 0 ? userlist.Max(u => u.Id) + 1 : 1; // Assign a new ID
            userlist.Add(user); // Add the new user to the list
            return RedirectToAction(nameof(Index)); // Redirect to the Index action
        }
        return View(user); // Return the view with the user model if validation fails
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        var existingUser = userlist.FirstOrDefault(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound(); // Return a 404 if the user is not found
        }

        if (ModelState.IsValid)
        {
            // Update the user's properties
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Phone = user.Phone;

            return RedirectToAction(nameof(Index)); // Redirect to the Index action
        }

        return View(user); // Return the Edit view with validation errors if the model state is invalid
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Return a 404 if the user is not found
        }
        return View(user); // Pass the user to the Delete view for confirmation
    }

    // POST: User/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound(); // Return a 404 if the user is not found
        }

        userlist.Remove(user); // Remove the user from the list
        return RedirectToAction(nameof(Index)); // Redirect to the Index action
    }
}
