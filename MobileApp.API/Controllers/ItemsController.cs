using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MobileApp.BAL.Interface;
using MobileApp.Models;

namespace MobileApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private IItem item;
        private readonly ILogger<HomeController> logger;
        public ItemsController(IItem item, ILogger<HomeController> logger)
        {
            this.item = item;
            this.logger = logger;
        }

        /// <summary>
        /// Add item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("additem")]
        public async Task<BaseModel> AddItem([FromBody] ItemModel model)
        {
            return await item.AddItem(model);
        }

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("updateitem")]
        public async Task<BaseModel> UpdateItem([FromBody] ItemModel model)
        {
            return await item.UpdateItem(model);
        }

        /// <summary>
        /// remove item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("removeitem")]
        public async Task<BaseModel> RemoveItem([FromBody] ItemModel model)
        {
            return await item.RemoveItem(model);
        }

        /// <summary>
        /// get item by item id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getitem/{id}")]
        public async Task<ItemModel> GetItem(int id)
        {
            return await item.GetItem(id);
        }

        /// <summary>
        /// get all items
        /// </summary>
        /// <returns></returns>
        [HttpGet("getitems")]
        public async Task<List<ItemModel>> GetItems()
        {
            return await item.GetItems();
        }
    }
}
