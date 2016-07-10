using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HDC1000;
using Newtonsoft.Json;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace Remote_Temp_client
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public global::HDC1000.HDC1000 hdc1000 { get; set; } = new global::HDC1000.HDC1000();
        public HDC1000Data Hdc1000Data { get; set; }

        //Sense Measureament Interval is 5 Min
        public DispatcherTimer Timer { get; set; } = new DispatcherTimer() {Interval = TimeSpan.FromSeconds(30)};

        public MainPage()
        {
            this.InitializeComponent();
            Timer.Start();
            hdc1000.InitHdc1000();
            Timer.Tick += async (sender, o) =>
            {
                var result = await hdc1000.Measurement();
                Debug.Write(JsonConvert.SerializeObject(result));
                PushMilkcocoa(result);
            };

        }

        private void PushMilkcocoa(HDC1000Data message)
        {
            var milkcocoa = new Milkcocoa.Milkcocoa("leadinu36n1u.mlkcca.com");
            var store = milkcocoa.dataStore("HDC1000");
            store.push(message);
        }
    }
}
