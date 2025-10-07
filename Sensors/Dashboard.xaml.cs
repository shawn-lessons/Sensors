// Dashboard.xaml.cs
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace Sensors
{
    public partial class Dashboard : Window
    {
        private CancellationTokenSource _cts = new();

        public Dashboard()
        {
            InitializeComponent();
            Loaded += Dashboard_Loaded;
            Closed += Dashboard_Closed;
        }

        private async void Dashboard_Loaded(object sender, RoutedEventArgs e)
        {
            await StartPollingAsync(_cts.Token);
        }

        private void Dashboard_Closed(object sender, EventArgs e)
        {
            _cts.Cancel();
        }

        private static async Task<Reading?> GetLatestAsync(CancellationToken ct)
        {
            await using var ctx = new SensorContext();
            return await ctx.Readings
                .AsNoTracking()
                .OrderByDescending(r => r.DateTime)
                .ThenByDescending(r => r.ReadingId)
                .FirstOrDefaultAsync(ct);
        }

        private async Task StartPollingAsync(CancellationToken ct)
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(2));
            try
            {
                while (await timer.WaitForNextTickAsync(ct))
                {
                    var r = await GetLatestAsync(ct);
                    if (r is null)
                    {
                        SensorText.Text = "No data";
                        ValueText.Text = "";
                        TimeText.Text = "";
                        continue;
                    }

                    SensorText.Text = $"Sensor: {r.SensorName}";
                    ValueText.Text = $"Value:  {r.Value:F1}";
                    TimeText.Text = $"Time:   {r.DateTime:yyyy-MM-dd HH:mm:ss}";
                }
            }
            catch (OperationCanceledException) { }
            finally
            {
                timer.Dispose();
            }
        }
    }
}
