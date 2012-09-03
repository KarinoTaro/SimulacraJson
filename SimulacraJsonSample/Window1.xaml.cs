using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimulacraJson;

namespace SimulacraJsonSample
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void ParseAndSerializeButton_Click(object sender, RoutedEventArgs e)
        {
            SimulacraJson.Json.SerializationWithoutEscape = !(bool)CharEscapeCheck.IsChecked;

            var json = JsonValue.Parse(InputText.Text);

            if (json != null)
            {
                OutputText.Text = json.ToString();
            }
        }

        private void TestCreateJsonValueButton_Click(object sender, RoutedEventArgs e)
        {
            SimulacraJson.Json.SerializationWithoutEscape = !(bool)CharEscapeCheck.IsChecked;
            
            var root = new JsonObject();

            root.Add("Key1", "Value1");
            root.Add("Key2", 100000);
            root.Add("Key3", "漢字");
            var array = new JsonArray();
            array.Add(100.5);
            array.Add(200);
            array.Add(300);
            array.Add(true);
            array.Add(false);
            array.Add(null);
            root.Add("Key4", array);
            root.Add("Key5", new JsonArray() { "test", 123.5, });
            root.Add("Key6", JsonValue.Parse(@"[10,20,35.6,""Test""]"));

            string a = root["Key1"];
            int b = root["Key2"];
            try
            {
                int c = int.Parse(root["Key1"]);
            }
            catch { //(Exception ex) {
                //MessageBox.Show(ex.Message);
            }

            string d = root["Key2"].ToString();

            OutputText.Text = root.ToString();
        }
    }
}
