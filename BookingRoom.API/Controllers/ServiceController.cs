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
    /// CRUD контроллер по работе с услугами
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    [ApiExplorerSettings(GroupName = "Service")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService serviceService;
        private readonly IMapper mapper;

        public ServiceController(IServiceService serviceService, IMapper mapper)
        {
            this.serviceService = serviceService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получить список услуг
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<ServiceResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await serviceService.GetAllAsync(cancellationToken);
            return Ok(result.Select(x => mapper.Map<ServiceResponse>(x)));
        }

        /// <summary>
        /// Получить услугу по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> GetById([Required] Guid id, CancellationToken cancellationToken)
        {
            var item = await serviceService.GetByIdAsync(id, cancellationToken);
            return Ok(mapper.Map<ServiceResponse>(item));
        }

        /// <summary>
        /// Добавить услугу
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(CreateServiceRequest model, CancellationToken cancellationToken)
        {
            var serviceModel = mapper.Map<ServiceModel>(model);
            var result = await serviceService.AddAsync(serviceModel, cancellationToken);
            return Ok(mapper.Map<ServiceResponse>(result));
        }

        /// <summary>
        /// Изменить услугу по Id
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIValidationExceptionDetail), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Edit(ServiceRequest request, CancellationToken cancellationToken)
        {
            var model = mapper.Map<ServiceModel>(request);
            var result = await serviceService.EditAsync(model, cancellationToken);
            return Ok(mapper.Map<ServiceResponse>(result));
        }

        /// <summary>
        /// Удалить фильм по Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIExceptionDetail), StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> Delete([Required] Guid id, CancellationToken cancellationToken)
        {
            await serviceService.DeleteAsync(id, cancellationToken);
            return Ok();
        }
    }
}
