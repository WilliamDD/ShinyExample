using Shiny.Beacons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ShinyExample
{
    internal class ShinyBeaconMonitoringDelegate : IBeaconMonitorDelegate
    {
        public Task OnStatusChanged(BeaconRegionState newStatus, BeaconRegion region)
        {
            Debug.WriteLine($"Monitored beacon {region.Identifier} {region.Uuid} {region.Major} {region.Major} {newStatus}");
            return Task.CompletedTask;
        }
    }
}
