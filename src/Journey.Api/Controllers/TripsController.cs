using Journey.Application.UseCases.Activities.Complete;
using Journey.Application.UseCases.Activities.Delete;
using Journey.Application.UseCases.Activities.Register;
using Journey.Application.UseCases.Trips.Delete;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status500InternalServerError)]
    public IActionResult Register([FromBody] RequestRegisterTripJson request)
    {
        var userCase = new RegisterTripUseCase();

        var response = userCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        var useCase = new GetAllTripsUseCase();

        var result = useCase.Execute();

        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status500InternalServerError)]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var useCase = new GetTripByIdUseCase();

        var response = useCase.Execute(id);

        return Ok(response);        
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status500InternalServerError)]
    public IActionResult Delete([FromRoute] Guid id)
    {
        var useCase = new DeleteTripByIdUseCase();

        useCase.Execute(id);

        return NoContent();
    }

    [HttpPost]
    [Route("{tripId}/activity")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status500InternalServerError)]
    public IActionResult RegisterActivity(
        [FromRoute] Guid tripId, 
        [FromBody] RequestRegisterActivityJson request)
    {
        var userCase = new RegisterActivityForTripUseCase();

        var response = userCase.Execute(tripId, request);

        return Created(string.Empty, response);
    }

    [HttpPut]
    [Route("{tripId}/activity/{activityId}/complete")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status500InternalServerError)]
    public IActionResult CompleteActivity(
        [FromRoute] Guid tripId,
        [FromRoute] Guid activityId)
    {
        var userCase = new CompleteActivityForTripUseCase();

        userCase.Execute(tripId, activityId);

        return NoContent();
    }

    [HttpDelete]
    [Route("{tripId}/activity/{activityId}")]
    [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteActivity(
        [FromRoute] Guid tripId,
        [FromRoute] Guid activityId)
    {
        var userCase = new DeleteActivityForTripUseCase();

        userCase.Execute(tripId, activityId);

        return NoContent();
    }

}
