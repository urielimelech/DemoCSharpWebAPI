using DemoWebAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("EnableCORSPolicy")]
    [ApiController]
    [Authorize]
    public class StoreController : Controller
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public StoreController(DataContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        [HttpPost("GetItems")]
        public ActionResult<List<Item>> GetItems()
        {
            return Ok(_context.ItemsStore.ToList());
        }

        [HttpPost("SearchItemByName")]
        public ActionResult<List<Item>> SearchItemByName(ItemName request)
        {
            if (!request.Name.IsNullOrEmpty())
            {
                return Ok(_context.ItemsStore.Where(item => item.Name.StartsWith(request.Name)).ToList());
            }
            return BadRequest("search parameter is not valid");
        }

        [HttpPost("CreateItem")]
        public ActionResult<ItemObj> CreateItem(ItemObj item)
        {
            string userName = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            Item i = new Item(item.Name, item.Description, item.Price, item.Count, userName);
            _context.ItemsStore.Add(i);
            _context.SaveChanges();
            return Ok(i);
        }

        [HttpPost("UpdateItem")]
        public ActionResult<ItemObj> UpdateItem(ItemObj item)
        {
            try
            {
                Item storedItem = _context.ItemsStore.Where(i => item.Id == i.Id).First();
                storedItem.Description = item.Description;
                storedItem.Name = item.Name;
                storedItem.Price = item.Price;
                storedItem.Count = item.Count;
                storedItem.LastUpdated = DateTime.Now;
                _context.ItemsStore.Update(storedItem);
                _context.SaveChanges();
                return Ok(item);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
