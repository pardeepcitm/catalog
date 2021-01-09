using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{

    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {

            new Item { Id = Guid.NewGuid(), Name = "Harimirch", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Test", Price = 10, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "App", Price = 11, CreatedDate = DateTimeOffset.UtcNow }
        };

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items);
        }
        public async Task<Item> GetItemAsync(Guid Id)
        {
            var item= items.Where(item => item.Id == Id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            items.Add(item);
             await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = items.FindIndex(exitingItem => exitingItem.Id == item.Id);
            items[index] = item;
            await Task.CompletedTask;
        }

        public async Task DeleteItemAsync(Guid Id)
        {
            var index = items.FindIndex(exitingItem => exitingItem.Id == Id);
            items.RemoveAt(index);
            await Task.CompletedTask;
            
        }
    }
}