using AGSR.Domain.Exceptions;
using AGSR.Services.Contracts;
using AGSR.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AGSR.Api.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly IPatientsService _patientsService;

    public PatientsController(IPatientsService patientsService)
    {
        _patientsService = patientsService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get patients with birthDate filtering")]
    [SwaggerResponse(200, "Patients list", typeof(List<PatientDto>), "application/json")]
    public async Task<List<PatientDto>> GetPatients(
        [FromQuery(Name = "date")] string[] filters,
        CancellationToken token)
    {
        return await _patientsService.GetAllAsync(filters, token);
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(Summary = "Get patient by id")]
    [SwaggerResponse(200, "Patient data", typeof(PatientDto), "application/json")]
    [SwaggerResponse(404, "Patient was not found")]
    public async Task<ActionResult<PatientDto?>> GetPatientById(Guid id, CancellationToken token)
    {
        var patient = await _patientsService.GetByIdAsync(id, token);

        if (patient is null) return NotFound();

        return patient;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a new patient")]
    [SwaggerResponse(200, "Patient created successfully", typeof(PatientDto), "application/json")]
    public async Task<ActionResult<PatientDto>> CreatePatient(CreatePatientDto model, CancellationToken token)
    {
        return await _patientsService.CreateAsync(model, token);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update patient data")]
    [SwaggerResponse(200, "Patient updated successfully", typeof(PatientDto), "application/json")]
    [SwaggerResponse(404, "Patient was not found")]
    public async Task<ActionResult<PatientDto>> UpdatePatient(PatientDto model, CancellationToken token)
    {
        try
        {
            return await _patientsService.UpdateAsync(model, token);
        }
        catch (PatientNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(Summary = "Delete patient by id")]
    [SwaggerResponse(200, "Patient deleted successfully")]
    [SwaggerResponse(404, "Patient was not found")]
    public async Task<IActionResult> DeletePatient(Guid id, CancellationToken token)
    {
        try
        {
            await _patientsService.DeleteAsync(id, token);
            return Ok();
        }
        catch (PatientNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}