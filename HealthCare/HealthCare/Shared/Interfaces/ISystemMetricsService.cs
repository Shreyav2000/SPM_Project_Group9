using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HealthCare.Shared.Models;
using HealthCare.Shared.Objects;

namespace HealthCare.Shared.Interfaces
{
    /// <summary>
    /// Represents a service for obtaining system metrics.
    /// </summary>
    public interface ISystemMetricsService
    {
        /// <summary>
        /// Gets the current CPU usage as a percentage.
        /// </summary>
        /// <returns>The current CPU usage as a <see cref="Task{TResult}"/> of type <see cref="SystemMetrics"/>.</returns>
        Task<SystemMetrics> GetCpuUsageAsync();
        
    }

    /// <summary>
    /// Implements the <see cref="ISystemMetricsService"/> interface for obtaining system metrics.
    /// </summary>
    public class SystemMetricsService : ISystemMetricsService
    {
        private readonly Process m_process;

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemMetricsService"/> class.
        /// </summary>
        public SystemMetricsService()
        {
            m_process = Process.GetCurrentProcess();
        }

        /// <summary>
        /// Gets the current CPU usage as a percentage.
        /// </summary>
        /// <returns>The current CPU usage as a <see cref="Task{TResult}"/> of type <see cref="SystemMetrics"/>.</returns>
        public async Task<SystemMetrics> GetCpuUsageAsync()
        {
            var startTime = DateTime.UtcNow - m_process.TotalProcessorTime;
            await Task.Delay(500); // Wait for half a second to get a more accurate reading
            m_process.Refresh();
            var endTime = DateTime.UtcNow;
            var cpuTime = (m_process.TotalProcessorTime.TotalMilliseconds / (endTime - startTime).TotalMilliseconds) * 100;
         
            var driveInfo = new DriveInfo(Environment.GetFolderPath(Environment.SpecialFolder.System));
            var totalSpace = driveInfo.TotalSize / 1024.0 ; // Convert bytes to megabytes
            var freeSpace = driveInfo.AvailableFreeSpace / 1024.0 ; // Convert bytes to megabytes
            var usedSpace = totalSpace - freeSpace;
            return new SystemMetrics
            {
                cpuUsage = Math.Round(cpuTime, 2),
                
            };
        }

    }
}
