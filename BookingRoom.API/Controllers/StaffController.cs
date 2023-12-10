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
    /// CRUD контроллер по работе с сотрудниками
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Staff")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService staffService;
        private readonly IMapper mapper;

        public StaffController(IStaffService staffService, IMapper mapper)
        {
            this.staffService = staffService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список сотрудников
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<StaffResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await staffService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<StaffResponse>(x)));
        }

        /// <summary>
        /// Получить сотрудника по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(StaffResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await staffService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<StaffResponse>(item));
        }

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(StaffResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateStaffRequest model, CancellationToken cancellationToken)
        {
            var staffModel = mapper.Map<StaffModel>(model);
            var result = await staffService.AddAsync(staffModel, cancellationToken);
            return Ok(mapper.Map<StaffResponse>(result));
        }

        /// <summary>
        /// Изменить cотрудника по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(StaffResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(StaffRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<StaffModel>(request);
            var result = await staffService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<StaffResponse>(result));
        }

        /// <summary>
        /// Удалить сотрудника по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await staffService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
