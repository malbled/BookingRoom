using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using BookingRoom.Context.Contracts.Enums;
using BookingRoom.Context.Contracts.Models;
using BookingRoom.Services.Contracts.Models;
using BookingRoom.Services.Contracts.ModelsRequest;
using BookingRoom.Services.Contracts.Enums;

namespace BookingRoom.Services.AutoMappers
{
    /// <summary>
    /// Маппер
    /// </summary>
    public class ServiceMapper : Profile
    {
        public ServiceMapper()
        {
            CreateMap<Post, PostModel>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<Room, RoomModel>(MemberList.Destination).ReverseMap();
            CreateMap<Service, ServiceModel>(MemberList.Destination).ReverseMap();
            CreateMap<Guest, GuestModel>(MemberList.Destination).ReverseMap();
            CreateMap<Hotel, HotelModel>(MemberList.Destination).ReverseMap();
            CreateMap<Staff, StaffModel>(MemberList.Destination).ReverseMap();
            CreateMap<Booking, BookingModel>(MemberList.Destination)
                .ForMember(x => x.Room, opt => opt.Ignore())
                .ForMember(x => x.Hotel, opt => opt.Ignore())
                .ForMember(x => x.Guest, opt => opt.Ignore())
                .ForMember(x => x.Service, opt => opt.Ignore())
                .ForMember(x => x.Staff, opt => opt.Ignore()).ReverseMap();

            CreateMap<BookingRequestModel, Booking>(MemberList.Destination)
                .ForMember(x => x.Room, opt => opt.Ignore())
                .ForMember(x => x.Hotel, opt => opt.Ignore())
                .ForMember(x => x.Guest, opt => opt.Ignore())
                .ForMember(x => x.Service, opt => opt.Ignore())
                .ForMember(x => x.Staff, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.DeletedAt, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedBy, opt => opt.Ignore());
        }
    }
}
