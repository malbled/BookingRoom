using BookingRoom.API.AutoMappers;
using BookingRoom.Common.Entity;
using BookingRoom.Common.Entity.InterfaceDB;
using BookingRoom.Context;
using BookingRoom.Repositories;
using BookingRoom.Services;
using BookingRoom.Services.AutoMappers;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

namespace BookingRoom.API.Extensions
{
    /// <summary>
    /// Расширения для <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Регистрирует все сервисы, репозитории и все что нужно для контекста
        /// </summary>
        public static void RegistrationSRC(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IDbWriterContext, DBWriterContext>();
            services.RegistrationService();
            services.RegistrationRepository();
            services.RegistrationContext();
            services.AddAutoMapper(typeof(APIMappers), typeof(ServiceMapper));
        }

        /// <summary>
        /// Включает фильтры и ставит шрифт на перечесления
        /// </summary>
        /// <param name="services"></param>
        public static void RegistrationControllers(this IServiceCollection services)
        {
            services.AddControllers(x =>
            {
                x.Filters.Add<BookingRoomExceptionFilter>();
            })
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.Converters.Add(new StringEnumConverter
                    {
                        CamelCaseText = false
                    });
                });
        }

        /// <summary>
        /// Настройки свагера
        /// </summary>
        public static void RegistrationSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Hotel", new OpenApiInfo { Title = "Отели", Version = "v1" });
                c.SwaggerDoc("Guest", new OpenApiInfo { Title = "Постояльцы", Version = "v1" });
                c.SwaggerDoc("Service", new OpenApiInfo { Title = "Услуги", Version = "v1" });
                c.SwaggerDoc("Room", new OpenApiInfo { Title = "Номера", Version = "v1" });
                c.SwaggerDoc("Staff", new OpenApiInfo { Title = "Сотрудники", Version = "v1" });
                c.SwaggerDoc("Booking", new OpenApiInfo { Title = "Брони", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "BookingRoom.API.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        /// <summary>
        /// Настройки свагера
        /// </summary>
        public static void CustomizeSwaggerUI(this WebApplication web)
        {
            web.UseSwagger();
            web.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("Hotel/swagger.json", "Отели");
                x.SwaggerEndpoint("Guest/swagger.json", "Постояльцы");
                x.SwaggerEndpoint("Service/swagger.json", "Услуги");
                x.SwaggerEndpoint("Room/swagger.json", "Номера");
                x.SwaggerEndpoint("Staff/swagger.json", "Сотрудники");
                x.SwaggerEndpoint("Booking/swagger.json", "Брони");
            });
        }
    }
}
