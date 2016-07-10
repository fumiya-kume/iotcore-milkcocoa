using System;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml;
using HDC1000;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace RemoteTempClientConsole
{
    public sealed class StartupTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };
            timer.Tick += async (sender, o) =>
            {
                //var result = await new global::HDC1000.HDC1000().Measurement();
                //PushMilkcocoa(result);
            };
            //timer.Start();
            var result = await new global::HDC1000.HDC1000().Measurement();
            //PushMilkcocoa(result);
        }
        private void PushMilkcocoa(HDC1000Data message)
        {
            var milkcocoa = new Milkcocoa.Milkcocoa("leadinu36n1u.mlkcca.com");
            var store = milkcocoa.dataStore("HDC1000");
            store.push(message);
        }
    }
}
