using AutoMapper;
using MaterialRequisition.Application.DTOs.Response;
using MaterialRequisition.DAL.Entities;

namespace MaterialRequisition.Business.Automapper
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<InventoryCategoryResponse, InventoryCategory>().ReverseMap();
            CreateMap<RoleResponse, Role>().ReverseMap();
            CreateMap<RequisitionItemResponse, RequisitionItem>().ReverseMap();
            CreateMap<StockResponse, ItemStock>().ReverseMap();
            CreateMap<UserResponse, User>().ReverseMap();
        }
    }
}
