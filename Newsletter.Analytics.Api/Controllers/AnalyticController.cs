using Microsoft.AspNetCore.Mvc;
using Newsletter.Analytics.Api.Models;
using Newsletter.Analytics.Api.Repositories;

namespace Newsletter.Reporting.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AnalyticController : ControllerBase
{
    private readonly AnalyticRepository _analyticRepository;

    public AnalyticController(AnalyticRepository analyticRepository)
    {
        _analyticRepository = analyticRepository;
    }

    [HttpGet("{articleId}/overview")]
    public async Task<ActionResult<Overview>> Get(int articleId)
    {
        return await _analyticRepository.Get(articleId);
    }
}
