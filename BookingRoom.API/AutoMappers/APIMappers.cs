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
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateServiceRequest, ServiceModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateRoomRequest, RoomModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateGuestRequest, GuestModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<CreateStaffRequest, StaffModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

            CreateMap<HotelRequest, HotelModel>(MemberList.Destination).ReverseMap();
            CreateMap<ServiceRequest, ServiceModel>(MemberList.Destination).ReverseMap();
            CreateMap<RoomRequest, RoomModel>(MemberList.Destination).ReverseMap();
            CreateMap<GuestRequest, GuestModel>(MemberList.Destination).ReverseMap();
            CreateMap<StaffRequest, StaffModel>(MemberList.Destination).ReverseMap();
            CreateMap<BookingRequest, BookingModel>(MemberList.Destination)

                .ForMember(x => x.Hotel, opt => opt.Ignore())
                .ForMember(x => x.Guest, opt => opt.Ignore())
                .ForMember(x => x.Room, opt => opt.Ignore())
                .ForMember(x => x.Staff, opt => opt.Ignore())
                .ForMember(x => x.Service, opt => opt.Ignore()).ReverseMap();

            CreateMap<BookingRequest, BookingRequestModel>(MemberList.Destination).ReverseMap();
            CreateMap<CreateBookingRequest, BookingRequestModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore()).ReverseMap();

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
