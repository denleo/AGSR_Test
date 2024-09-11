using AGSR.Domain.Entities;
using AGSR.Domain.Exceptions;
using AGSR.Domain.Repositories;
using AGSR.Domain.Repositories.Base;
using AGSR.Services.Contracts;
using AGSR.Services.Dto;
using Mapster;
using MapsterMapper;

namespace AGSR.Services;

internal class PatientsService : IPatientsService
{
    private readonly IMapper _mapper;
    private readonly IPatientsRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public PatientsService(IPatientsRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PatientDto> CreateAsync(CreatePatientDto patientDto, CancellationToken token = default)
    {
        var patient = _mapper.Map<Patient>(patientDto);

        _repository.Create(patient);

        await _unitOfWork.SaveChangesAsync(token);

        return _mapper.Map<PatientDto>(patient);
    }

    public async Task<PatientDto> UpdateAsync(PatientDto patientDto, CancellationToken token = default)
    {
        var patient = await _repository.GetByIdAsync(patientDto.Id, token)
                      ?? throw new PatientNotFoundException("Patient was not found");

        patientDto.Adapt(patient);

        await _unitOfWork.SaveChangesAsync(token);

        return _mapper.Map<PatientDto>(patient);
    }

    public async Task DeleteAsync(Guid patientId, CancellationToken token = default)
    {
        var patient = await _repository.GetByIdAsync(patientId, token)
                      ?? throw new PatientNotFoundException("Patient was not found");

        _repository.Delete(patient);

        await _unitOfWork.SaveChangesAsync(token);
    }

    public async Task<PatientDto?> GetByIdAsync(Guid patientId, CancellationToken token = default)
    {
        var patient = await _repository.GetByIdAsync(patientId, token);

        return patient is null ? null : _mapper.Map<PatientDto>(patient);
    }

    public async Task<List<PatientDto>> GetAllAsync(string[] dateFilters, CancellationToken token = default)
    {
        var patients = await _repository.GetAllAsync(dateFilters, token);
        return _mapper.Map<List<PatientDto>>(patients);
    }
}