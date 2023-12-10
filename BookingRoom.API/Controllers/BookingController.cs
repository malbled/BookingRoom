using AutoMapper;
using BookingRoom.API.Exceptions;
using BookingRoom.API.Models.CreateRequest;
using BookingRoom.API.Models.Request;
using BookingRoom.API.Models.Response;
using BookingRoom.Services.Contracts.ModelsRequest;
using BookingRoom.Services.Contracts.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookingRoom.API.Controllers
{
    /// <summary>
    /// CRUD контроллер по работе с бронями
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Booking")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService bookingService;
        private readonly IMapper mapper;

        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            this.bookingService = bookingService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список броней
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<BookingResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await bookingService.GetAllAsync(cancellationToken);
            var result2 = result.Select(x => mapper.Map<BookingResponse>(x));
            return Ok(result2);
        }

        /// <summary>
        /// Получить бронь по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await bookingService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<BookingResponse>(item));
        }

        /// <summary>
        /// Добавить бронь
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateBookingRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<BookingRequestModel>(request);
            var result = await bookingService.AddAsync(model, cancellationToken);
            return Ok(mapper.Map<BookingResponse>(result));
        }

        /// <summary>
        /// Изменить бронь по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(BookingRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<BookingRequestModel>(request);
            var result = await bookingService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<BookingResponse>(result));
        }

        /// <summary>
        /// Удалить бронь по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await bookingService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
