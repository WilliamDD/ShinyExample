using Shiny;
using Shiny.Beacons;
using Shiny.Locations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShinyExample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var ranging = ShinyHost.Resolve<IBeaconRangingManager>();
            ranging
                .WhenBeaconRanged(new BeaconRegion("RangingId", Guid.Parse("00000000-0000-0000-0000-000000000000")))
                .Subscribe(beacon =>
                {
                    Debug.WriteLine($"Ranged beacon {beacon.Uuid} {beacon.Major} {beacon.Minor}");
                });
        }

        ~MainPage()
        {
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var monitoring = ShinyHost.Resolve<IBeaconMonitoringManager>();
            var alreadyMonitored = await monitoring.GetMonitoredRegions();
            if (!alreadyMonitored.Any(xx => xx.Identifier == "monitoringId"))
            {
               await monitoring.StartMonitoring(new BeaconRegion("monitoringId", Guid.Parse("00000000-0000-0000-0000-000000000000")));
            }

            var manager = ShinyHost.Resolve<IGeofenceManager>();
            var result = await manager.RequestAccess();
            if (result == AccessState.Available)
            {
                await manager.StartMonitoring(new GeofenceRegion("YourIdentifier", new Position(51.587761, 5.076640), Distance.FromMeters(50))
                {
                    NotifyOnEntry = true,
                    NotifyOnExit = true,
                    SingleUse = false
                });
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            //var manager = ShinyHost.Resolve<IGeofenceManager>();
            //var result = await manager.RequestAccess();
            //if (result == AccessState.Available)
            //{
            //   await manager.StopMonitoring("YourIdentifier");
            //}
        }
    }
}
