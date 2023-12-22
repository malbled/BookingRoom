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
    /// CRUD контроллер по работе с постояльцами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Guest")]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService guestService;
        private readonly IMapper mapper;

        public GuestController(IGuestService guestService, IMapper mapper)
        {
            this.guestService = guestService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список постояльцев
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<GuestResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await guestService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<GuestResponse>(x)));
        }

        /// <summary>
        /// Получить постояльца по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(GuestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await guestService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<GuestResponse>(item));
        }

        /// <summary>
        /// Добавить постояльца
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(GuestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateGuestRequest model, CancellationToken cancellationToken)
        {
            var guestModel = mapper.Map<GuestModel>(model);
            var result = await guestService.AddAsync(guestModel, cancellationToken);
            return Ok(mapper.Map<GuestResponse>(result));
        }

        /// <summary>
        /// Изменить постояльца по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(GuestResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(GuestRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<GuestModel>(request);
            var result = await guestService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<GuestResponse>(result));
        }

        /// <summary>
        /// Удалить постояльца по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await guestService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
