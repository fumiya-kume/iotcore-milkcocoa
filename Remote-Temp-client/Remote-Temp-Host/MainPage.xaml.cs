using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
            HDC1000Data resultobject = JsonConvert.DeserializeObject<HDC1000Data>(result[0].ToString());
            Temp_Value.Text = $"Temp: {(int)resultobject.Temp}°";
            Hum_Value.Text = $"Hum: {(int)resultobject.Hum}%";
        }

        private void singleViewbutton_Click(object sender, RoutedEventArgs e)
        {
            this.Temp_Value.Visibility = Visibility.Visible;
            this.Hum_Value.Visibility = Visibility.Visible;
            this.Humbagerview.IsPaneOpen = false;
        }

        private void GraphViewbutton_Click(object sender, RoutedEventArgs e)
        {
            this.Temp_Value.Visibility = Visibility.Collapsed;
            this.Hum_Value.Visibility = Visibility.Collapsed;
            this.Humbagerview.IsPaneOpen = false;
        }
    }
}
