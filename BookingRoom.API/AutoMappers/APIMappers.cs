using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using BookingRoom.API.Enums;
using BookingRoom.API.Models.CreateRequest;
using BookingRoom.API.Models.Request;
using BookingRoom.API.Models.Response;
using BookingRoom.Services.Contracts.Enums;
using BookingRoom.Services.Contracts.Models;
using BookingRoom.Services.Contracts.ModelsRequest;

namespace BookingRoom.API.AutoMappers
{
    /// <summary>
    /// Маппер
    /// </summary>
    public class APIMappers : Profile
    {
        public APIMappers()
        {
            CreateMap<PostModel, PostResponse>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<CreateHotelRequest, HotelModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CreateServiceRequest, ServiceModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CreateRoomRequest, RoomModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CreateGuestRequest, GuestModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<CreateStaffRequest, StaffModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<HotelRequest, HotelModel>(MemberList.Destination);
            CreateMap<ServiceRequest, ServiceModel>(MemberList.Destination);
            CreateMap<RoomRequest, RoomModel>(MemberList.Destination);
            CreateMap<GuestRequest, GuestModel>(MemberList.Destination);
            CreateMap<StaffRequest, StaffModel>(MemberList.Destination);
            CreateMap<BookingRequest, BookingModel>(MemberList.Destination)
                .ForMember(x => x.Room, opt => opt.Ignore())
                .ForMember(x => x.Hotel, opt => opt.Ignore())
                .ForMember(x => x.Guest, opt => opt.Ignore())
                .ForMember(x => x.Service, opt => opt.Ignore())
                .ForMember(x => x.Staff, opt => opt.Ignore());

            CreateMap<BookingRequest, BookingRequestModel>(MemberList.Destination);
            CreateMap<CreateBookingRequest, BookingRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<ServiceModel, ServiceResponse>(MemberList.Destination);
            CreateMap<RoomModel, RoomResponse>(MemberList.Destination);
            CreateMap<HotelModel, HotelResponse>(MemberList.Destination);
            CreateMap<BookingModel, BookingResponse>(MemberList.Destination);
            CreateMap<StaffModel, StaffResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.MiddleName}"));

            CreateMap<GuestModel, GuestResponse>(MemberList.Destination)
                .ForMember(x => x.Name, opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.MiddleName}"));
        }
    }
}
