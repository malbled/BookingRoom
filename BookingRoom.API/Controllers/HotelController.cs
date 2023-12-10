using AutoMapper;
using BookingRoom.API.Exceptions;
using BookingRoom.API.Models.CreateRequest;
using BookingRoom.API.Models.Request;
using BookingRoom.API.Models.Response;
using BookingRoom.Services.Contracts.Models;
using BookingRoom.Services.Contracts.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookingRoom.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с отелями
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Hotel")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService hotelService;
        private readonly IMapper mapper;

        public HotelController(IHotelService hotelService, IMapper mapper)
        {
            this.hotelService = hotelService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список отелей
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HotelResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await hotelService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<HotelResponse>(x)));
        }

        /// <summary>
        /// Получить ОТЕЛЬ по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(HotelResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await hotelService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<HotelResponse>(item));
        }

        /// <summary>
        /// Добавить ОТЕЛЬ
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(HotelResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] CreateHotelRequest model, CancellationToken cancellationToken)
        {
            var hotelModel = mapper.Map<HotelModel>(model);
            var result = await hotelService.AddAsync(hotelModel, cancellationToken);
            return Ok(mapper.Map<HotelResponse>(result));
        }

        /// <summary>
        /// Изменить ОТЕЛЬ
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(HotelResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(HotelRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<HotelModel>(request);
            var result = await hotelService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<HotelResponse>(result));
        }

        /// <summary>
        /// Удалить ОТЕЛЬ по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await hotelService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
