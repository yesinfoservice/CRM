using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.BAL.Interface
{
    public interface IItem
    {
        Task<BaseModel> AddItem(ItemModel model);
        Task<BaseModel> UpdateItem(ItemModel model);
        Task<BaseModel> RemoveItem(ItemModel model);
        Task<ItemModel> GetItem(int id);
        Task<List<ItemModel>> GetItems();
    }
}
