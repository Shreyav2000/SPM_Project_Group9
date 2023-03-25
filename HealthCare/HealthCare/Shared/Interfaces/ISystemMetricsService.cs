using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCare.Shared.Interfaces
{
    public interface ISystemMetricsService
    {
        public Task<double> GetCpuUsageAsync();
        public Task<double> GetMemoryUsageAsync();
    }

    public class SystemMetricsService : ISystemMetricsService
    {
        public async Task<double> GetCpuUsageAsync()
        {
            var process = Process.GetCurrentProcess();
            var startTime = DateTime.UtcNow - process.TotalProcessorTime;
            await Task.Delay(500); // Wait for half a second to get a more accurate reading
            var endTime = DateTime.UtcNow;
            var cpuTime = (process.TotalProcessorTime.TotalMilliseconds / (endTime - startTime).TotalMilliseconds) * 100;
            return Math.Round(cpuTime, 2);
        }

        public Task<double> GetMemoryUsageAsync()
        {
            var process = Process.GetCurrentProcess();
            var memoryUsage = process.PrivateMemorySize64 / 1024.0; // Convert bytes to megabytes
            return Task.FromResult(Math.Round(memoryUsage, 2));
        }
    }
}
