using AutoMapper;
using Innoloft.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Innoloft.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _db;
        private readonly IMapper _mapper;

        public ProductController(ProductContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAllProducts(string productType, int page = 1, int size = 10)
        {
            List<Product> result;
            if (productType != null)
            {
                // Filter products retrieved by category and paginate
                result = await _db.Products.Where(p => p.Category.ToLower() == productType.ToLower())
                        .Skip((page - 1) * size).Take(size)
                        .ToListAsync();
            }
            else
            {
                result = await _db.Products
                        // Paginate
                        .Skip((page - 1) * size).Take(size)
                        .ToListAsync();
            }

            if (result == null)
            {
                return NotFound();
            }
            List<ProductDTO> responseList = new List<ProductDTO>(); ;
            foreach (Product p in result)
            {
                var productDTO = _mapper.Map<ProductDTO>(p);
                productDTO.User = await UserDTO.fetchUserAsync(productDTO.UserId);
                responseList.Add(productDTO);
            }
            return responseList;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {

            var result = await _db.Products.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            // Map to ProductDTO since it hides user's address
            // TODO: include the address if they own the product
            var productDTO = _mapper.Map<ProductDTO>(result);
            productDTO.User = await UserDTO.fetchUserAsync(productDTO.UserId);
            return productDTO;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product prd)
        {
            // In case productId is provided, ensure product does not already exist.
            if (_db.Products.Any(e => e.Id == prd.Id))
            {
                return StatusCode(409);
            }
            _db.Products.Add(prd);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = prd.Id }, prd);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product old)
        {
            // Ensure correct product is updated
            if (id != old.Id)
            {
                return BadRequest();
            }
            // Make sure product exists
            var current = await _db.Products.FindAsync(id);
            if (current == null)
            {
                return NotFound();
            }
            // Manually update any non-nulls
            if (old.UserId != 0)
            {
                current.UserId = old.UserId;
            }
            if (old.Title != null)
            {
                current.Title = old.Title;
            }
            if (old.Category != null)
            {
                current.Category = old.Category;
            }
            if (old.Description != null)
            {
                current.Description = old.Description;
            }

            try
            {
                await _db.SaveChangesAsync();
                return NoContent();

            }
            catch (DbUpdateException e)
            {
                // Handle a failed update
                Console.Error.WriteLine(e);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {

            var result = await _db.Products.FindAsync(id);
            // Ensure product exists
            if (result == null)
            {
                return NotFound();
            }
            _db.Remove(result);
            await _db.SaveChangesAsync();

            return StatusCode(204);
        }
    }
}
