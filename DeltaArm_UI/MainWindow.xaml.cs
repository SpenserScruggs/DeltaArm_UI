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
            port.Close();
            running = false;
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            port = new SerialPort();
            port.PortName = "COM3";
            port.BaudRate = 9600;
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
                int y = Convert.ToInt32(Mouse.GetPosition(inkCanvas1).Y);
                int z = 100;

                string x1, y1, z1;

                if ((x >= 0) && (x <= 800) && (y >= 0) && (y <= 600))
                {
                    if (mouseIsDown)
                    {
                        z = 150;
                    }
                    x1 = ReSize(x);
                    y1 = ReSize(y);
                    z1 = ReSize(z);
                    Trace.WriteLine(x1 + "," + y1 + "," + z1);
                    
                    if (port.IsOpen)
                    {
                        port.WriteLine(x1 + y1 + z1);
                    }
                }
                await Task.Delay(10);
            }
        }
    }
}
