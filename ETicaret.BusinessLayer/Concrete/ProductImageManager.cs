using AutoMapper;
using ETicaret.BusinessLayer.Abstract;
using ETicaret.DataAccessLayer.Abstract;
using ETicaret.DtoLayer.ProductImageDto;
using ETicaretEntityLayer.Entities;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Concrete
{
    public class ProductImageManager : IProductImageService
    {
        private readonly IProductImageDal _productImageDal;
        private readonly IMapper _mapper;
        public ProductImageManager(IProductImageDal productImageDal, IMapper mapper)
        {
            _productImageDal = productImageDal;
            _mapper = mapper;
        }

        public async Task<ProductImageGetDto> GetByProductIdProductImageAsync(int id)
        {
            var images = await _productImageDal.GetByProductIdProductImageAsync(id);
            return _mapper.Map<ProductImageGetDto>(images);
        }
   

        public void TAdd(ProductImage entity) => _productImageDal.Add(entity);

        public void TDelete(ProductImage entity) => _productImageDal.Delete(entity);

        public ProductImage TGetById(int id) => _productImageDal.GetById(id);

        public List<ProductImage> TGetListAll() => _productImageDal.GetListAll();

        public void TUpdate(ProductImage entity) => _productImageDal.Update(entity);
    }
}
