
namespace Api.Features.InspectionOrders;

public partial class InspectionOrderController : ApiControllerBase
{
    [HttpGet("/api/dashboard/get-ecosystem")]
    public async Task<ActionResult<EcosystemDto>> GetEcosystem()
    {
        var result = await Mediator.Send(new GetEcosystemQuery());

        return Ok(result);
    }
}

public record GetEcosystemQuery : IRequest<EcosystemDto>
{
}

internal sealed class GetAllHandler : IRequestHandler<GetEcosystemQuery, EcosystemDto>
{
    private readonly IMongoDBManger _mongoDbManager;
    private readonly IMapper _mapper;

    public GetAllHandler(IMongoDBManger mongoDbManager, IMapper mapper)
    {
        _mongoDbManager = mongoDbManager;
        _mapper = mapper;
    }

    public async Task<EcosystemDto> Handle(GetEcosystemQuery request, CancellationToken cancellationToken)
    {
        //Read user id from token

        //Load json from s3 bucket

        //dummy logic
        var data = new Ecosystem();
        _mongoDbManager.Repository<Ecosystem>().Add(data);
        await _mongoDbManager.CommitAsync();
        //dummy logic

        return new EcosystemDto();
    }
}
public record EcosystemDto : IMapFrom<Ecosystem>
{
    public Guid Id { get; set; }
    void IMapFrom<Ecosystem>.Mapping(Profile profile)
    {
        profile.CreateMap<Ecosystem, EcosystemDto>();
    }
}
