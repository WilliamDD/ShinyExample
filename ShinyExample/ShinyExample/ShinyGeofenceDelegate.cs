using Shiny.Locations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ShinyExample
{
    public class ShinyGeofenceDelegate : IGeofenceDelegate
    {
        public async Task OnStatusChanged(GeofenceState newStatus, GeofenceRegion region)
        {
            Debug.WriteLine($"{region.Identifier} {newStatus}" );
        }
    }
}
