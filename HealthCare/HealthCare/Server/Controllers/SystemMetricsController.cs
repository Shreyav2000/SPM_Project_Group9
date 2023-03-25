using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Objects;
using Microsoft.AspNetCore.Mvc;


[ApiController]
public class SystemMetricsController : ControllerBase
{
    private readonly ISystemMetricsService _systemMetricsService;

    public SystemMetricsController(ISystemMetricsService systemMetricsService)
    {
        _systemMetricsService = systemMetricsService;
    }

    [HttpGet]
    [Route("api/systemmetrics")]
    public async Task<ActionResult<SystemMetrics>> GetCpuUsage()
    {
        var cpuUsage = await _systemMetricsService.GetCpuUsageAsync();
        return Ok(cpuUsage);
    }

    
}
