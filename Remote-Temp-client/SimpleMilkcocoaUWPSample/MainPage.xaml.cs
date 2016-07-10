using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace SimpleMilkcocoaUWPSample
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PushMilkcocoa("Hello World");
        }

        private void PushMilkcocoa(string message)
        {
            var milkcocoa = new Milkcocoa.Milkcocoa("leadinu36n1u.mlkcca.com");
            var store = milkcocoa.dataStore("HDC1000");
            store.push(new { content = message });
        }
    }
}
