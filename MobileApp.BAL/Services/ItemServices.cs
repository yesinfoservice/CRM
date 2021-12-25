using Microsoft.EntityFrameworkCore;
using MobileApp.BAL.Interface;
using MobileApp.DAL;
using MobileApp.DAL.Entities;
using MobileApp.Models;
using MobileApp.Utility.Resource;

namespace MobileApp.BAL.Services
{
    public class ItemServices : IItem
    {
        private readonly ApplicationDBContext context;
        public ItemServices(ApplicationDBContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Add Item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseModel> AddItem(ItemModel model)
        {
            BaseModel baseModel = new BaseModel();
            Item entity = new Item()
            {
                ItemName = model.ItemName,
                ItemPrice = model.ItemPrice,
                ExpiryDate = model.ExpiryDate,
                BrandName = model.BrandName,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
            };
            context.Items.Add(entity);
            int intRowsAffected = await context.SaveChangesAsync();
            if (intRowsAffected > 0)
            {
                baseModel.Success = true;
                baseModel.Message = CommonResource.ItemSavedSuccessfully;
            }
            else
            {
                baseModel.Success = false;
                baseModel.Message = CommonResource.SomeErrorOccured;
            }
            return baseModel;
        }
        /// <summary>
        /// Get item details by Item id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ItemModel> GetItem(int id)
        {
            ItemModel itemModels = new ItemModel();
            Item? item = await context.Items.Where(mod => mod.ItemId == id).FirstOrDefaultAsync();
            if (item != null)
            {
                itemModels.ItemId = item.ItemId;
                itemModels.ItemName = item.ItemName;
                itemModels.BrandName = item.BrandName;
                itemModels.ItemPrice = item.ItemPrice;
                itemModels.ExpiryDate = item.ExpiryDate;
                itemModels.IsActive = item.IsActive;
                itemModels.IsDeleted = item.IsDeleted;
            }
            return itemModels;
        }
        /// <summary>
        /// Get all items
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ItemModel>> GetItems()
        {
            List<ItemModel> itemModels = new List<ItemModel>();
            List<Item> items = await context.Items.ToListAsync();
            foreach (Item item in items)
            {
                itemModels.Add(new ItemModel()
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    BrandName = item.BrandName,
                    ItemPrice = item.ItemPrice,
                    ExpiryDate = item.ExpiryDate,
                    IsActive = item.IsActive,
                    IsDeleted = item.IsDeleted,
                });
            }
            return itemModels;
        }
        /// <summary>
        /// Delete Item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<BaseModel> RemoveItem(ItemModel model)
        {
            BaseModel baseModel = new BaseModel();
            Item? entity = await context.Items.Where(mod => mod.ItemId == model.ItemId).FirstOrDefaultAsync();
            if (entity != null)
            {
                context.Items.Remove(entity);
                int intRowsAffected = await context.SaveChangesAsync();
                if (intRowsAffected > 0)
                {
                    baseModel.Success = true;
                    baseModel.Message = CommonResource.ItemDeletedSuccessfully;
                }
                else
                {
                    baseModel.Success = false;
                    baseModel.Message = CommonResource.SomeErrorOccured;
                }
            }
            else
            {
                baseModel.Success = false;
                baseModel.Message = CommonResource.ItemDoesNotExist;
            }
            return baseModel;
        }

        /// <summary>
        /// Update Item
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseModel> UpdateItem(ItemModel model)
        {
            BaseModel baseModel = new BaseModel();

            Item? entity = await context.Items.FirstOrDefaultAsync(mod => mod.ItemId == model.ItemId);
            if (entity != null)
            {
                entity.ItemName = model.ItemName;
                entity.ItemPrice = model.ItemPrice;
                entity.ExpiryDate = model.ExpiryDate;
                entity.BrandName = model.BrandName;
                entity.IsActive = model.IsActive;
                entity.IsDeleted = model.IsDeleted;
                context.Items.Update(entity);
                int intRowsAffected = await context.SaveChangesAsync();
                if (intRowsAffected > 0)
                {
                    baseModel.Success = true;
                    baseModel.Message = CommonResource.ItemUpdatedSuccessfully;
                }
                else
                {
                    baseModel.Success = false;
                    baseModel.Message = CommonResource.SomeErrorOccured;
                }
            }
            else
            {
                baseModel.Success = false;
                baseModel.Message = CommonResource.ItemDoesNotExist;
            }

            return baseModel;
        }
    }
}
