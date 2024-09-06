using BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IProductService
    {
        public Task<ResponseDto<Guid>> AddProductAsync(ProductDTO productDto);
        public Task<ResponseDto<object>> GetProductByIdAsync(Guid id);
        public Task<ResponseDto<ICollection<ProductDTO>>> GetAllProductsAsync();
        public Task<ResponseDto<bool>> EditProductAsync(Guid id,ProductDTO productDTO);
        public Task<ResponseDto<bool>> DeleteProductAsync(Guid id);
    }
}
