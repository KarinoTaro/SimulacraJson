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
    }
}
