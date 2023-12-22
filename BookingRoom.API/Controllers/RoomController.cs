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
    /// CRUD контроллер по работе с номерами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Room")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService roomService;
        private readonly IMapper mapper;

        public RoomController(IRoomService roomService, IMapper mapper)
        {
            this.roomService = roomService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список номеров
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<RoomResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await roomService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<RoomResponse>(x)));
        }

        /// <summary>
        /// Получить номер по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await roomService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<RoomResponse>(item));
        }

        /// <summary>
        /// Добавить номер
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateRoomRequest model, CancellationToken cancellationToken)
        {
            var roomModel = mapper.Map<RoomModel>(model);
            var result = await roomService.AddAsync(roomModel, cancellationToken);
            return Ok(mapper.Map<RoomResponse>(result));
        }

        /// <summary>
        /// Изменить номер по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(RoomResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(RoomRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<RoomModel>(request);
            var result = await roomService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<RoomResponse>(result));
        }

        /// <summary>
        /// Удалить номер по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await roomService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
