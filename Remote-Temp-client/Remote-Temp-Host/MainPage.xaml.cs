using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using HDC1000;
using Milkcocoa;
using Newtonsoft.Json;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace Remote_Temp_Host
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public DataStore DataStore { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
             
            DataStore = new Milkcocoa.Milkcocoa("leadinu36n1u.mlkcca.com").dataStore("HDC1000");
           
        }

        private async void RefreshValue_Click(object sender, RoutedEventArgs e)
        {
            var result = await DataStore.history().limit(1).run();
            HDC1000Data resultobject=  JsonConvert.DeserializeObject<HDC1000Data>(result[0].ToString());
            Temp_Value.Text = $"Temp: {(int)resultobject.Temp}°";
            Hum_Value.Text = $"Hum: {(int) resultobject.Hum}%";
        }
    }
}
