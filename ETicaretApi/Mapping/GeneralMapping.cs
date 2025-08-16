using AutoMapper;
using ETicaret.DtoLayer.AboutDto;
using ETicaret.DtoLayer.AddressDto;
using ETicaret.DtoLayer.CargoCompany;
using ETicaret.DtoLayer.CargoCustomerDto;
using ETicaret.DtoLayer.CargoDetailDto;
using ETicaret.DtoLayer.CargoOperationDto;
using ETicaret.DtoLayer.CategoryDto;
using ETicaret.DtoLayer.ContactDto;
using ETicaret.DtoLayer.CouponDto;
using ETicaret.DtoLayer.FeatureDto;
using ETicaret.DtoLayer.FeatureSliderDto;
using ETicaret.DtoLayer.OrderDetailDto;
using ETicaret.DtoLayer.OrderingDto;
using ETicaret.DtoLayer.ProductDetailDto;
using ETicaret.DtoLayer.ProductDto;
using ETicaret.DtoLayer.ProductImageDto;
using ETicaret.DtoLayer.SpecialOfferDto;
using ETicaret.DtoLayer.UserCommentDto;
using ETicaret.DtoLayer.VendorDto;
using ETicaretEntityLayer.Entities;

namespace ETicaretApi.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Address, AddressCreateDto>().ReverseMap();
            CreateMap<Address, AddressUpdateDto>().ReverseMap();
            CreateMap<Address, AddressResultDto>().ReverseMap();
            CreateMap<Address, AddressGetDto>().ReverseMap();

            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryResultDto>()
            .ForMember(dest => dest.ProductCount, opt => opt.MapFrom(src => src.Products.Count));
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryGetDto>().ReverseMap();

            CreateMap<Coupon, CouponCreateDto>().ReverseMap();
            CreateMap<Coupon, CouponUpdateDto>().ReverseMap();
            CreateMap<Coupon, CouponResultDto>().ReverseMap();
            CreateMap<Coupon, CouponGetDto>().ReverseMap();

            CreateMap<OrderDetail, OrderDetailCreateDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailUpdateDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailResultDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailGetDto>().ReverseMap();

            // Product
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductResultDto>().ReverseMap();
            CreateMap<Product, ProductGetDto>().ReverseMap();
            CreateMap<Product, ResultProductWithCategoryDto>().ReverseMap();

            // ProductDetail
            CreateMap<ProductDetail, ProductDetailCreateDto>().ReverseMap();
            CreateMap<ProductDetail, ProductDetailUpdateDto>().ReverseMap();
            CreateMap<ProductDetail, ProductDetailResultDto>().ReverseMap();
            CreateMap<ProductDetail, ProductDetailGetDto>().ReverseMap();

            // ProductImage
            CreateMap<ProductImage, ProductImageCreateDto>().ReverseMap();
            CreateMap<ProductImage, ProductImageUpdateDto>().ReverseMap();
            CreateMap<ProductImage, ProductImageResultDto>().ReverseMap();
            CreateMap<ProductImage, ProductImageGetDto>().ReverseMap();

            CreateMap<CargoCompany, CargoCompanyCreateDto>().ReverseMap();
            CreateMap<CargoCompany, CargoCompanyUpdateDto>().ReverseMap();
            CreateMap<CargoCompany, CargoCompanyResultDto>().ReverseMap();

            CreateMap<CargoCustomer, CreateCargoCustomerDto>().ReverseMap();
            CreateMap<CargoCustomer, UpdateCargoCustomerDto>().ReverseMap();
            CreateMap<CargoCustomer, ResultCargoCustomer>().ReverseMap();

            // CargoDetail Mapping
            CreateMap<CargoDetail, CreateCargoDetailDto>().ReverseMap();
            CreateMap<CargoDetail, UpdateCargoDetailDto>().ReverseMap();
            CreateMap<CargoDetail, ResultCargoDetailDto>().ReverseMap();

            // CargoOperation Mapping
            CreateMap<CargoOperation, CreateCargoOperationDto>().ReverseMap();
            CreateMap<CargoOperation, UpdateCargoOperationDto>().ReverseMap();
            CreateMap<CargoOperation, ResultCargoOperationDto>().ReverseMap();

            CreateMap<FeatureSlider, CreateFeatureSliderDto>().ReverseMap();
            CreateMap<FeatureSlider, UpdateFeatureSliderDto>().ReverseMap();
            CreateMap<FeatureSlider, ResultFeatureSliderDto>().ReverseMap();

            CreateMap<SpecialOffer, CreateSpecialOfferDto>().ReverseMap();
            CreateMap<SpecialOffer, UpdateSpecialOfferDto>().ReverseMap();
            CreateMap<SpecialOffer, ResultSpecialOfferDto>().ReverseMap();
            CreateMap<SpecialOffer, GetByIdSpecialOfferDto>().ReverseMap();

            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
            CreateMap<Feature, ResultFeatureDto>().ReverseMap();

            CreateMap<Vendor, VendorCreateDto>().ReverseMap();   // ImageFile elle handle edilecek
            CreateMap<Vendor, VendorUpdateDto>().ReverseMap();   // ImageFile yine manuel işlenir
            CreateMap<Vendor, VendorResultDto>().ReverseMap();

            CreateMap<About, CreateAboutDto>().ReverseMap();
            CreateMap<About, UpdateAboutDto>().ReverseMap();
            CreateMap<About, ResultAboutDto>().ReverseMap();

            CreateMap<UserComment, UserCommentCreateDto>().ReverseMap();
            CreateMap<UserComment, UserCommentUpdateDto>().ReverseMap();
            CreateMap<UserComment, UserCommentResultDto>().ReverseMap();

            CreateMap<Contact, CreateContactDto>().ReverseMap();
            CreateMap<Contact, UpdateContactDto>().ReverseMap();
            CreateMap<Contact, ResultContactDto>().ReverseMap();

            CreateMap<Ordering, OrderingResultDto>().ReverseMap();
            CreateMap<Ordering, OrderingCreateDto>().ReverseMap();
            CreateMap<Ordering, OrderingUpdateDto>().ReverseMap();

            CreateMap<Address, AddressGetDto>().ReverseMap();
            CreateMap<Address, AddressCreateDto>().ReverseMap();
            CreateMap<Address, AddressUpdateDto>().ReverseMap();

            CreateMap<OrderDetail, OrderDetailGetDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailCreateDto>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailUpdateDto>().ReverseMap();
        }
    }
}
