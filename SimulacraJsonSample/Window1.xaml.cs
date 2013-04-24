using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SimulacraJson;
using System.Diagnostics;

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
            Json.SerializationWithoutEscape = !(bool)CharEscapeCheck.IsChecked;

            int trycount = 1;

            try
            {
                trycount = int.Parse(NumberOfRuns.Text);
            }
            catch { }

            var sw = new Stopwatch();

            sw.Start();
            try
            {
                for (var i = 0; i < trycount; i++)
                {
                    var json = JsonValue.Parse(InputText.Text);

                    if (json != null)
                    {
                        OutputText.Text = json.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            sw.Stop();

            MessageBox.Show(trycount.ToString() + " times - " + sw.Elapsed.ToString());
        }

        private void TestCreateJsonValueButton_Click(object sender, RoutedEventArgs e)
        {
            Json.SerializationWithoutEscape = !(bool)CharEscapeCheck.IsChecked;
            
            var root = new JsonObject {{"Key1", "Value1"}, {"Key2", 100000}, {"Key3", "漢字"}};

            var array = new JsonArray {100.5, 200, 300, true, false, null};
            root.Add("Key4", array);
            root.Add("Key5", new JsonArray() { "test", 123.5, });
            root.Add("Key6", JsonValue.Parse(@"[10,20,35.6,""Test""]"));

            var a = root["Key1"];
            int b = root["Key2"];
            var f = (int)root["Key5"][1];

            try
            {
                var c = int.Parse(root["Key1"]);
            }
            catch { //(Exception ex) {
                //MessageBox.Show(ex.Message);
            }

            var d = root["Key2"].ToString();

            JsonValue value;
            if (root.TryGetValue("Key1", out value))
            {
                MessageBox.Show("TryGetValue: OK Result = " + value);
            }
            else
            {
                MessageBox.Show("TryGetValue: NG");
            }

            if (root.TryGetValue("NoKey", out value))
            {
                MessageBox.Show("TryGetValue(Key not found): NG");
            }
            else
            {
                MessageBox.Show("TryGetValue(Key not found): OK");
            }

            // Key not found error
            try
            {
                var x = root["xxx"];
            }
            catch (Exception eknf)
            {
                MessageBox.Show("Key not found error:" + eknf.Message);
            }

            // Out of range error
            try
            {
                var x = root[1];
            }
            catch (Exception eoor)
            {
                MessageBox.Show("Out of range:" + eoor.Message);
            }

            OutputText.Text = root.ToString();
        }
    }
}
