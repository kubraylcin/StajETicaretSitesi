using ETicaret.BusinessLayer.Abstract;
using ETicaret.DataAccessLayer.Abstract;
using ETicaret.DtoLayer.ProductDetailDto;
using ETicaretEntityLayer.Entities;
using System.Collections.Generic;

namespace ETicaret.BusinessLayer.Concrete
{
    public class ProductDetailManager : IProductDetailService
    {
        private readonly IProductDetailDal _productDetailDal;

        public ProductDetailManager(IProductDetailDal productDetailDal)
        {
            _productDetailDal = productDetailDal;
        }

        public async Task<ProductDetailGetDto> GetByProductIdProductDetailAsync(int id)
        {
            var productDetail = await _productDetailDal.GetByProductIdAsync(id);

            if (productDetail != null)
            {
                return new ProductDetailGetDto
                {
                    ProductDetailID = productDetail.ProductDetailID,
                    ProductDescription = productDetail.ProductDescription,
                    ProductInfo = productDetail.ProductInfo,
                    ProductID = productDetail.ProductID
                };
            }
            return null;
        }

        public void TAdd(ProductDetail entity) => _productDetailDal.Add(entity);

        public void TDelete(ProductDetail entity) => _productDetailDal.Delete(entity);

        public ProductDetail TGetById(int id) => _productDetailDal.GetById(id);

        public List<ProductDetail> TGetListAll() => _productDetailDal.GetListAll();

        public void TUpdate(ProductDetail entity) => _productDetailDal.Update(entity);
    }
}
