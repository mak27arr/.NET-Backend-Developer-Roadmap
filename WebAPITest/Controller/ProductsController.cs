using Database.Entities;
using Database.Interface;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _db;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _db = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return new ActionResult<IEnumerable<Product>>(await _db.Products.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var user = await _db.Products.GetAsync(id);

            return user == null ? NotFound() : new ObjectResult(user);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            if (product == null)
                return BadRequest();

            await _db.Products.AddAsync(product);
            await _db.SaveAsync();

            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<Product>> Put(Product product)
        {
            if (product == null)
                return BadRequest();
            
            if (await _db.Products.GetAsync(product.Id) == null)
                return NotFound();

            await _db.Products.UpdateAsync(product);
            await _db.SaveAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var user = _db.Products.GetAsync(id);
            
            if (user == null)
                return NotFound();

            await _db.Products.DeleteAsync(id);
            await _db.SaveAsync();
            return Ok(user);
        }
    }
}
