 using System;
using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using System.Collections.Generic;
using Catalog.Entities;
using System.Linq;
using Catalog.Dtos;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ItemsController: ControllerBase
    {
        
        private readonly IItemsRepository repository;
        public ItemsController(IItemsRepository repository){
            this.repository = repository;
        }   
        //GET /Items   
        [HttpGet]      
        public async  Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await repository.GetItemsAsync()).Select(item => item.AsDTO());
            return items;            
        } 
        // GET /Items/{Id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid Id){
            var item = await repository.GetItemAsync(Id);
            if(item is null)
            {
             return NotFound();
            }
            else 
            return item.AsDTO();
        }
        //Post /Items
        [HttpPost]

        public async Task<ActionResult<ItemDto>> CreateItemAsync( CreateItemDto itemDto)
        {
            Item item = new(){
                Id = Guid.NewGuid(),
                Name= itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await repository.CreateItemAsync(item);
            return CreatedAtAction(nameof(GetItemAsync) ,new {id =item.Id}, item.AsDTO());
        }
        //PUT /Items/
        [HttpPut("{Id}")]
        public async  Task<ActionResult> UpdateItemAsync (Guid Id , UpdateItemDto updateItemDto){
            var existingitem = await repository.GetItemAsync(Id);
             if(existingitem is null)
            {
             return NotFound();
            }

            Item updatedItem = existingitem with {
                Name = updateItemDto.Name,
                Price = updateItemDto.Price
            };

            await repository.UpdateItemAsync(updatedItem);
            return NoContent();
        }
        //Delete /Items/{Id}
        [HttpDelete ("{Id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid Id)
        {
            var existingitem = await repository.GetItemAsync(Id);
         if(existingitem is null)
            {
             return NotFound();
            }
            await repository.DeleteItemAsync(existingitem.Id);
            return NoContent();
        }
        
    }
}
