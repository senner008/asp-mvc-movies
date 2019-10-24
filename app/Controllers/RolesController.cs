// using System.Threading.Tasks;
// using asp_identity.Data;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;


// namespace TestNet2oAuthBlogy.Controllers
// {
//     [Authorize]
//     [ApiController]
//     [Route("[controller]")]
//     public class RolesController : Controller
//     {
//         private readonly RoleManager<IdentityRole> _rolemanager;
//         private readonly UserManager<IdentityUser> _usermanager;
//         private readonly ApplicationDbContext _appcontext;

//         public RolesController(RoleManager<IdentityRole> rolemanager, UserManager<IdentityUser> usermanager, ApplicationDbContext appcontext)
//         {
//             _rolemanager = rolemanager;
//             _usermanager = usermanager;
//             _appcontext = appcontext;
//         }

//         // GET: Blogs
//         [HttpGet]
//         public async Task<IActionResult> Index()
//         {
//             bool adminExists = await _rolemanager.RoleExistsAsync("Admin");
//             if (!adminExists) {
//                 var role = new IdentityRole();
//                 role.Name = "Admin";
//                 await _rolemanager.CreateAsync(role);
//                 var currentUser = await _usermanager.FindByNameAsync(User.Identity.Name);
//                 var roleresult = await _usermanager.AddToRoleAsync(currentUser, "Admin");
//                 _appcontext.SaveChanges();
//                 return Ok("User set as Admin");
//             }
//             else {
//                 return BadRequest("Admin already exists!"); 
//             }
//         }
//     }
// }