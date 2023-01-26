using DemoWebAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DemoWebApi.Controllers
{
    [Route("api/[controller]")]
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
        public ActionResult<List<Item>> GetItemes()
        {
            return Ok(_context.ItemsStore.ToList());
        }

        [HttpPost("GetItemsName")]
        public ActionResult<List<Item>> GetItemsName(string name)
        {
            return Ok(_context.ItemsStore.Where(item=>item.Name.StartsWith(name)).ToList());
        }

        [HttpPost("CreateItem")]
        public ActionResult<ItemObj> CreateItem(ItemObj item)
        {
            string userName = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            Item i = new Item(item.Name, item.Description, item.Price, item.Count, userName);
            _context.ItemsStore.Add(i);
            _context.SaveChanges();
            return Ok(item);
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

        [HttpPost("AddToCart")]
        public ActionResult<Item> AddToCart(int id)
        {
            Item item = _context.ItemsStore.Where(i => i.Id == id).First();
            /* add cart store */
            return Ok(item);
        }
    }
}
