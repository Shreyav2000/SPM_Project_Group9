using HealthCare.Shared.Interfaces;
using HealthCare.Shared.Objects;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller to handle system metrics requests.
/// </summary>
[ApiController]
public class SystemMetricsController : ControllerBase
{
    private readonly ISystemMetricsService _systemMetricsService;
    /// <summary>
    /// Constructor for the SystemMetricsController.
    /// </summary>
    /// <param name="systemMetricsService">The service used to retrieve system metrics.</param>
    public SystemMetricsController(ISystemMetricsService systemMetricsService)
    {
        _systemMetricsService = systemMetricsService;
    }
    /// <summary>
    /// Retrieves the CPU usage of the system.
    /// </summary>
    /// <returns>An HTTP action result containing the system metrics.</returns>
    [HttpGet]
    [Route("api/systemmetrics")]
    public async Task<ActionResult<SystemMetrics>> GetCpuUsage()
    {
        var cpuUsage = await _systemMetricsService.GetCpuUsageAsync();
        return Ok(cpuUsage);
    }

    
}
