using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;


namespace DeltaArm_UI
{
    public partial class MainWindow : Window
    {
        private SerialPort port = new SerialPort();

        public bool running = false;

        public bool recording = false;
        static public int save_length = 1000;
        public string[] recordSaved = new string[save_length];

        public int color = 0;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void inkCanvas1_Gesture(object sender, InkCanvasGestureEventArgs e)
        {
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.inkCanvas1.Strokes.Clear();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            port.Close();
            this.Close();
        }

        private void OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.inkCanvas1.Strokes.Clear();
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            running = false;
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            port = new SerialPort();
            port.PortName = "COM3";
            port.BaudRate = 115200;
            running = true;
            Tracker();
            try
            {
                port.Open();
            }
            catch (Exception e1)
            {
                System.Windows.MessageBox.Show(e1.Message);
            }
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            if (recording)
            {
                recording = false;
            }
            else if (!recording)
            {
                recordSaved = new string[save_length];
                recording = true;
            }
            if (color % 2 == 0)
            {
                button5.Background = button5.Background == Brushes.Red ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDDDDDD")) : Brushes.Green;
            }
            else
            {
                button5.Background = button5.Background == Brushes.Red ? (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDDDDDD")) : Brushes.LightGray;
            }
            color++;
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            PlayRecording();
        }
        
        private string ReSize(int x)
        {
            string val = x.ToString();

            if (x.ToString().Length == 1)
            {
                val = "00" + x.ToString();
            }
            else if (x.ToString().Length == 2)
            {
                val = "0" + x.ToString();
            }
            return val;
        }

        public async void Tracker()
        {

            while (running)
            {
                bool mouseIsDown = System.Windows.Input.Mouse.LeftButton == MouseButtonState.Pressed;

                int x = Convert.ToInt32(Mouse.GetPosition(inkCanvas1).X);
                int y = 600 - Convert.ToInt32(Mouse.GetPosition(inkCanvas1).Y);

                int z = 178;
                string x1, y1, z1;

                if ((x >= 0) && (x <= 800) && (y >= 0) && (y <= 600))
                {
                    if (mouseIsDown)
                    {
                        z = 192;
                    }
                    x1 = ReSize(x);
                    y1 = ReSize(y);
                    z1 = ReSize(z);

                    Trace.WriteLine(x1 + "," + y1 + "," + z1);
                    if (port.IsOpen && !recording)
                    {
                        port.WriteLine(x1 + y1 + z1);
                    }
                    else if (recording)
                    {
                        string newPos = x1 + y1 + z1;
                        List<string> list = new List<string>(recordSaved.ToList());
                        list.Add(newPos);
                        recordSaved = list.ToArray();
                    }
                }
                await Task.Delay(12);
            }
        }

        public async void PlayRecording()
        {
            for (int i = 0; i < recordSaved.Length; i++)
            {
                if (port.IsOpen && (recordSaved[i] != null))
                {
                    port.WriteLine(recordSaved[i]);
                    await Task.Delay(13);
                }
            }
        }
    }
}
