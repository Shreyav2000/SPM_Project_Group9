using HealthCare.Shared.Interfaces;
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
    [Route("api/systemmetrics/cpuusage")]
    public async Task<ActionResult<double>> GetCpuUsage()
    {
        var cpuUsage = await _systemMetricsService.GetCpuUsageAsync();
        return Ok(cpuUsage);
    }

    [HttpGet]
    [Route("api/systemmetrics/memoryusage")]
    public async Task<ActionResult<double>> GetMemoryUsage()
    {
        var memoryUsage = await _systemMetricsService.GetMemoryUsageAsync();
        return Ok(memoryUsage);
    }
}
