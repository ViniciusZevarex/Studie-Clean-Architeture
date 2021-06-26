using CleanArch.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Category> GetById(int? id);

        Task<Category> Create(Product category);
        Task<Category> Update(Product category);
        Task<Category> Remove(Product category);
    }
}