using AutoMapper;
using MaterialRequisition.Application.DTOs.Response;
using MaterialRequisition.DAL.Entities;

namespace MaterialRequisition.Business.Automapper
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<InventoryCategory, InventoryCategoryResponse>().ReverseMap();

            CreateMap<Role, RoleResponse>().ReverseMap();

            CreateMap<RequisitionItem, RequisitionItemResponse>().ReverseMap();

            CreateMap<ItemStock, StockResponse>().ReverseMap();

            CreateMap<User, UserResponse>().ReverseMap();

            CreateMap<Account, AccountResponse>()
                .ForMember(dest => dest.Manager, src => 
                    src.MapFrom(src => src.ManagerAccount != null ? $"{src.ManagerAccount.FirstName} {src.ManagerAccount.LastName}": ""))
                .ForMember(dest => dest.BusinessUnit, src => 
                    src.MapFrom(src => src.BusinessUnit != null ? src.BusinessUnit.UnitName: ""))
                .ReverseMap();

            CreateMap<ActivityTimeline, ActivityTimelineResponse>()
                .ForMember(dest => dest.AccountName, src =>
                    src.MapFrom(src => src.Account != null ? $"{src.Account.FirstName} {src.Account.LastName}" : ""))
                .ReverseMap();

            CreateMap<BusinessUnit, BusinessUnitResponse>()
                .ForMember(dest => dest.ParentUnit, src => src.MapFrom(src => src.ParentUnit != null ? src.ParentUnit.UnitName : ""))
                .ReverseMap();

            CreateMap<InventoryItem, InventoryItemResponse>()
                .ForMember(dest => dest.Category, src => src.MapFrom(src => src.InventoryCategory != null ? src.InventoryCategory.CategoryName : ""))
                .ReverseMap();

            CreateMap<ItemStock, StockResponse>()
                .ForMember(dest => dest.InventoryItem, src => src.MapFrom(src => src.InventoryItem != null ? src.InventoryItem.ItemName : ""))
                .ReverseMap();

            CreateMap<Permission, PermissionResponse>()
                .ForMember(dest => dest.Role, src => src.MapFrom(src => src.Role != null ? src.Role.RoleName : ""))
                .ReverseMap();
        }
    }
}
