using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO.Ports;
using System.Diagnostics;

/// <summary>
/// Created by: Spenser Scruggs
/// Date: 5/2/2022
/// 
/// This Project was created as a graphical user interface for the delta arm project
/// It does this by taking mouse positions and sending them via serial communication to an arduino
/// thoes positions are then converted to servo positions using inverse kinematics
/// </summary>

namespace DeltaArm_UI
{
    public partial class MainWindow : Window
    {
        // initiates serial port
        private SerialPort port = new SerialPort();

        // sets the bool for the tracker
        public bool running = false;

        // sets the bool for the recorde feature
        public bool recording = false;
        static public int save_length = 1000;
        public string[] recordSaved = new string[save_length];

        // color for the record button
        public int color = 0;
        
        // initialize application
        public MainWindow()
        {
            InitializeComponent();
        }

        // initializes the ink canvas
        private void inkCanvas1_Gesture(object sender, InkCanvasGestureEventArgs e)
        {
        }

        // button that clears the canvas
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.inkCanvas1.Strokes.Clear();
        }

        // button that closes the program
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            port.Close();
            this.Close();
        }

        // makes enter a shortcut for clearing the canvas
        private void OnKeyDownHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.inkCanvas1.Strokes.Clear();
            }
        }

        // pause button that stops the program (not a good idea to click)
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            running = false;
        }

        // starts the program (only press once)
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

        // saves the recording to a new string array
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

        // plays the recording
        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            PlayRecording();
        }
        
        // maps the integer inputs to their correct string length
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

        // function that tracks the mouse while on the canvas
        public async void Tracker()
        {

            while (running)
            {
                bool mouseIsDown = System.Windows.Input.Mouse.LeftButton == MouseButtonState.Pressed;

                int x = Convert.ToInt32(Mouse.GetPosition(inkCanvas1).X);
                int y = 600 - Convert.ToInt32(Mouse.GetPosition(inkCanvas1).Y);

                int z = 160;
                string x1, y1, z1;

                if ((x >= 0) && (x <= 800) && (y >= 0) && (y <= 600))
                {
                    if (mouseIsDown)
                    {
                        z = 182;
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

        // plays the recorded string with a given delay
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
