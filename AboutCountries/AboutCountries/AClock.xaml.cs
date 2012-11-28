using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AboutCountries
{
    public partial class AClock : UserControl
    {
        private DateTime _time = new DateTime(2000, 1, 1, 18, 15, 50);
        private double hour;
        private double minute;
        private double second;

        public double handThickness;
        public double minuteThickness;
        public double secondThickness;

        double ClockWidth;

        public static readonly DependencyProperty HoursProperty =
        DependencyProperty.Register("Hours", typeof(double), typeof(AClock), new PropertyMetadata(null));
        
        public static readonly DependencyProperty MinutesProperty =
        DependencyProperty.Register("Minutes", typeof(double), typeof(AClock), new PropertyMetadata(null));
        
        public static readonly DependencyProperty LongitudeProperty =
        DependencyProperty.Register("Longitude", typeof(double), typeof(AClock), new PropertyMetadata(null));

        public static readonly DependencyProperty LatitudeProperty =
        DependencyProperty.Register("Latitude", typeof(double), typeof(AClock), new PropertyMetadata(null));
            
        public static readonly DependencyProperty LocationProperty =
        DependencyProperty.Register("Location", typeof(String), typeof(AClock), new PropertyMetadata(null));

        public double Hours
        {
            set { SetValue(HoursProperty, value); }
            get { return (double)GetValue(HoursProperty); }
        }

        public double Minutes
        {
            set { SetValue(MinutesProperty, value); }
            get { return (double)GetValue(MinutesProperty); }
        }

        public double Longitude
        {
            set { SetValue(LongitudeProperty, value); }
            get { return (double)GetValue(LongitudeProperty); }
        }
        public double Latitude
        {
            set { SetValue(LatitudeProperty, value); }
            get { return (double)GetValue(LatitudeProperty); }
        }
        public String Location
        {
            set { SetValue(LocationProperty, value); }
            get { return (string)GetValue(LocationProperty); }
        }
        
        //ConfigClass cc = new ConfigClass();
        public AClock()
        {
           // Hours = 0;
            
            handThickness = 4;
            minuteThickness = handThickness - 1;
            secondThickness = minuteThickness - 1;

            InitializeComponent();

            ClockWidth = Width;
            Loaded += new RoutedEventHandler(onLoad);
        }

        public void onLoad(object o, RoutedEventArgs e)
        {
            DispatcherTimer tmr = new DispatcherTimer();
            tmr.Interval = TimeSpan.FromSeconds(1);
            tmr.Tick += Draw;
            tmr.Start();
        }
       /* private void Draw2(object sender, object e)
        {
            SetTime(DateTime.Now);
            ClockLayout.Children.Clear();

            var step = 360 / 60;
            var innerRadiusX = (ClockWidth * 0.8) / 2;
            var innerRadiusY = (Height * 0.8) / 2;

            var outerRadiusX = (ClockWidth * 0.9) / 2;
            var outerRadiusY = (Height * 0.9) / 2;

            for (var i = 0; i < 60; i++)
            {
                Line line = new Line();
                if (i % 5 == 0)
                {
                    line.Stroke = new SolidColorBrush(Colors.White);
                    line.X1 = (ClockWidth / 2) + Math.Sin((step * i) * (Math.PI / 180)) * innerRadiusX;
                    line.Y1 = (Height / 2) + Math.Cos((step * i) * (Math.PI / 180)) * innerRadiusY;
                    line.X2 = (ClockWidth / 2) + Math.Sin((step * i) * (Math.PI / 180)) * outerRadiusX;
                    line.Y2 = (Height / 2) + Math.Cos((step * i) * (Math.PI / 180)) * outerRadiusY;
                    line.StrokeThickness = 6;
                }
                else
                {
                    innerRadiusX = (ClockWidth * 0.85) / 2;
                    innerRadiusY = (Height * 0.85) / 2;
                    line.Stroke = new SolidColorBrush(Colors.White);
                    line.X1 = (ClockWidth / 2) + Math.Sin((step * i) * (Math.PI / 180)) * innerRadiusX;
                    line.Y1 = (Height / 2) + Math.Cos((step * i) * (Math.PI / 180)) * innerRadiusY;
                    line.X2 = (ClockWidth / 2) + Math.Sin((step * i) * (Math.PI / 180)) * outerRadiusX;
                    line.Y2 = (Height / 2) + Math.Cos((step * i) * (Math.PI / 180)) * outerRadiusY;
                    line.StrokeThickness = 1;
                }

                ClockLayout.Children.Add(line);
            }
            DrawHourHand(Colors.White);
            DrawMinuteHand(Colors.White);
            DrawSecondHand(Colors.Red);
        }
        */
        void Draw(object sender, EventArgs args)
        {

            SetTime(DateTime.UtcNow);
            textBlock2.Text = _time.ToLongTimeString();
            ClockLayout.Children.Clear();

            double ClockWidth = ClockLayout.Width;
            double ClockHeight = ClockLayout.Height;

            var step = 360 / 60;
            var innerRadiusX = (ClockWidth * 0.8) / 2;
            var innerRadiusY = (ClockHeight * 0.8) / 2;

            var outerRadiusX = (ClockWidth * 0.9) / 2;
            var outerRadiusY = (ClockHeight * 0.9) / 2;

            var textRadiusX = (ClockWidth * 0.7) / 2;
            var textRadiusY = (ClockHeight * 0.7) / 2;

            outerCasing.Visibility = Visibility.Visible;

            ClockLayout.Children.Add(outerCasing);


            ///
            // Determine the visibility of the dark background.
            Visibility darkBackgroundVisibility =
                (Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"];
            Color lineStroke;
            // Write the theme background value.
            if (darkBackgroundVisibility == Visibility.Visible)
            {
                lineStroke = Colors.Black;
            }
            else
            {
                lineStroke = Colors.White;
            }

            ///
            for (var i = 0; i < 60; i++)
            {
                Line line = new Line();
                if (i % 5 == 0)
                {
                    line.Stroke = new SolidColorBrush(lineStroke);
                    line.X1 = (ClockWidth / 2) + Math.Sin((step * i) * (Math.PI / 180)) * innerRadiusX;
                    line.Y1 = (ClockHeight / 2) + Math.Cos((step * i) * (Math.PI / 180)) * innerRadiusY;
                    line.X2 = (ClockWidth / 2) + Math.Sin((step * i) * (Math.PI / 180)) * outerRadiusX;
                    line.Y2 = (ClockHeight / 2) + Math.Cos((step * i) * (Math.PI / 180)) * outerRadiusY;

                    var textblock = new TextBlock();
                    textblock.FontFamily = new FontFamily("verdana");
                    textblock.FontSize = 14;
                    SolidColorBrush mytextBrush = new SolidColorBrush(lineStroke);

                    textblock.Foreground = mytextBrush;
                    if (i % 15 == 0)
                        textblock.Text = i == 0 ? "12" : ((double)(i / 60D) * 12D).ToString();
                    else
                        textblock.Text = "";

                    var textX = (ClockWidth / 2) + Math.Sin(-((step * i + 180) % 360) * (Math.PI / 180)) * textRadiusX;
                    var textY = (ClockHeight / 2) + Math.Cos(-((step * i + 180) % 360) * (Math.PI / 180)) * textRadiusY;

                    textblock.SetValue(Canvas.LeftProperty, textX - textblock.ActualWidth / 2);
                    textblock.SetValue(Canvas.TopProperty, textY - textblock.ActualHeight / 2);

                    ClockLayout.Children.Add(textblock);
                }
                else

                    line.StrokeThickness = 4;
                ClockLayout.Children.Add(line);
            }

            DrawHourHand(lineStroke);
            DrawMinuteHand(lineStroke);
            DrawSecondHand();
        }

        private void DrawHourHand(Color clr)
        {
            //Change hour value to percentage for use with 360
            double hourPercentage = (hour + (minute / 60D)) / 12D;

            //Get the Hour degree value
            double hourDegree = 360 * hourPercentage;
            double ClockWidth = ClockLayout.Width;
            double ClockHeight = ClockLayout.Height;
            //DrawHand(ClockWidth / 5.5D, Height / 5.5D, -hourDegree, Colors.Black, handThickness);
            DrawHand2(ClockWidth / 4.5D, ClockHeight / 4.5D, -hourDegree, clr, handThickness);
        }

        private void DrawMinuteHand(Color clr)
        {
            //Change minute value to percentage for use with 360
            double minutePercentage = (minute + (second / 60D)) / 60;
            //Get the minute percentage
            double minuteDegree = 360 * minutePercentage;
            double ClockWidth = ClockLayout.Width;
            double ClockHeight = ClockLayout.Height;
            // DrawHand(ClockWidth / 4.5D, Height / 4.5D, -minuteDegree, Colors.Blue, minuteThickness);
            // DrawHand2(ClockWidth / 3.5D, Height / 3.5D, -minuteDegree, Colors.Blue, minuteThickness);
            DrawHand2(ClockWidth / 2.8D, ClockHeight / 2.8D, -minuteDegree, clr, minuteThickness);
        }

        private void DrawSecondHand()
        {
            double secondPercentage = second / 60D;
            //Get the minute percentage
            double secondDegree = 360 * secondPercentage;
            double ClockWidth = ClockLayout.Width;
            double ClockHeight = ClockLayout.Height;
            //DrawHand2(ClockWidth / 3.5D, Height / 3.5D, -secondDegree, Colors.Red, secondThickness);
            DrawHand2(ClockWidth / 2.7D, ClockHeight / 2.7D, -secondDegree, Colors.Red, secondThickness);
        }

        private void DrawHand(double radiusX, double radiusY, double angle, Color color, double thickness)
        {
            double ClockWidth = ClockLayout.Width;
            double ClockHeight = ClockLayout.Height;

            var hand = new Line
            {
                Stroke = new SolidColorBrush(color),
                X1 = ClockWidth / 2,
                Y1 = ClockHeight / 2,
                X2 = (ClockWidth / 2) + -Math.Sin(angle * (Math.PI / 180)) * radiusX,
                Y2 = (ClockHeight / 2) + -Math.Cos(angle * (Math.PI / 180)) * radiusY
            };
            hand.StrokeThickness = thickness;

            ClockLayout.Children.Add(hand);
        }
        private void DrawHand2(double radiusX, double radiusY, double angle, Color color, double thickness)
        {
            double ClockWidth = ClockLayout.Width;
            double ClockHeight = ClockLayout.Height;

            var hand = new Line
            {
                Stroke = new SolidColorBrush(color),
                X1 = ClockWidth / 2 + Math.Sin(angle * (Math.PI / 180)) * 12,
                Y1 = ClockHeight / 2 + Math.Cos(angle * (Math.PI / 180)) * 12,
                X2 = (ClockWidth / 2) + -Math.Sin(angle * (Math.PI / 180)) * radiusX,
                Y2 = (ClockHeight / 2) + -Math.Cos(angle * (Math.PI / 180)) * radiusY
            };
            hand.StrokeThickness = thickness;

            ClockLayout.Children.Add(hand);
        }

     /*   private void DrawHourHand(Color cc)
        {
            //Change hour value to percentage for use with 360
            double hourPercentage = (hour + (minute / 60D)) / 12D;

            //Get the Hour degree value
            double hourDegree = 360 * hourPercentage;
            //double ClockWidth = ClockLayout.Width;
            //DrawHand(ClockWidth / 5.5D, Height / 5.5D, -hourDegree, Colors.Black, handThickness);
            DrawHand3(ClockWidth / 4.5D, Height / 4.5D, -hourDegree, cc, handThickness);
        }

        private void DrawMinuteHand(Color cc)
        {
            //Change minute value to percentage for use with 360
            double minutePercentage = (minute + (second / 60D)) / 60;
            //Get the minute percentage
            double minuteDegree = 360 * minutePercentage;
            // double ClockWidth = ClockLayout.Width;
            // DrawHand(ClockWidth / 4.5D, Height / 4.5D, -minuteDegree, Colors.Blue, minuteThickness);
            // DrawHand2(ClockWidth / 3.5D, Height / 3.5D, -minuteDegree, Colors.Blue, minuteThickness);
            DrawHand2(ClockWidth / 2.8D, Height / 2.8D, -minuteDegree, cc, minuteThickness);
        }

        private void DrawSecondHand(Color cc)
        {
            double secondPercentage = second / 60D;
            //Get the minute percentage
            double secondDegree = 360 * secondPercentage;
            //  double ClockWidth = ClockLayout.Width;

            DrawHand(ClockWidth / 2.7D, Height / 2.7D, -secondDegree, cc, secondThickness);
        }

        private void DrawHand(double radiusX, double radiusY, double angle, Color color, double thickness)
        {
            // double ClockWidth = ClockLayout.Width;

            var hand = new Line
            {
                Stroke = new SolidColorBrush(color),
                X1 = ClockWidth / 2,
                Y1 = Height / 2,
                X2 = (ClockWidth / 2) + -Math.Sin(angle * (Math.PI / 180)) * radiusX,
                Y2 = (Height / 2) + -Math.Cos(angle * (Math.PI / 180)) * radiusY
            };
            hand.StrokeThickness = thickness;

            ClockLayout.Children.Add(hand);

        }
        private void DrawHand2(double radiusX, double radiusY, double angle, Color color, double thickness)
        {
            //double ClockWidth = ClockLayout.Width;

            var hand = new Line
            {
                Stroke = new SolidColorBrush(color),
                //X1 = ClockWidth / 2 + Math.Sin(angle * (Math.PI / 180)) * 12,
                //Y1 = Height / 2 + Math.Cos(angle * (Math.PI / 180)) * 12,
                X1 = ClockWidth / 2 + Math.Sin(angle * (Math.PI / 180)) * 1,
                Y1 = Height / 2 + Math.Cos(angle * (Math.PI / 180)) * 1,
                X2 = (ClockWidth / 2) + -Math.Sin(angle * (Math.PI / 180)) * radiusX,
                Y2 = (Height / 2) + -Math.Cos(angle * (Math.PI / 180)) * radiusY
            };
            // hand.StrokeThickness = thickness;

            var hand2 = new Line
            {
                Stroke = new SolidColorBrush(color),
                X1 = ClockWidth / 2,
                Y1 = Height / 2,
                X2 = (ClockWidth / 2) + -Math.Sin((angle - 5) * (Math.PI / 180)) * radiusX / 2,
                Y2 = (Height / 2) + -Math.Cos((angle - 5) * (Math.PI / 180)) * radiusY / 2
            };
            var hand3 = new Line
            {
                Stroke = new SolidColorBrush(color),
                X1 = ClockWidth / 2,
                Y1 = Height / 2,
                X2 = (ClockWidth / 2) + -Math.Sin((angle + 5) * (Math.PI / 180)) * radiusX / 2,
                Y2 = (Height / 2) + -Math.Cos((angle + 5) * (Math.PI / 180)) * radiusY / 2
            };
            var hand4 = new Line
            {
                Stroke = new SolidColorBrush(color),
                X1 = hand.X2,
                Y1 = hand.Y2,
                X2 = (ClockWidth / 2) + -Math.Sin((angle + 5) * (Math.PI / 180)) * radiusX / 2,
                Y2 = (Height / 2) + -Math.Cos((angle + 5) * (Math.PI / 180)) * radiusY / 2
            };
            var hand5 = new Line
            {
                Stroke = new SolidColorBrush(color),
                X1 = hand.X2,
                Y1 = hand.Y2,
                X2 = (ClockWidth / 2) + -Math.Sin((angle - 5) * (Math.PI / 180)) * radiusX / 2,
                Y2 = (Height / 2) + -Math.Cos((angle - 5) * (Math.PI / 180)) * radiusY / 2
            };
            ClockLayout.Children.Add(hand);
            ClockLayout.Children.Add(hand2);

            ClockLayout.Children.Add(hand4); ClockLayout.Children.Add(hand3);
            ClockLayout.Children.Add(hand5);
        }

        private void DrawHand3(double radiusX, double radiusY, double angle, Color color, double thickness)
        {
            int handWidth = 11;

            var hand = new Line
            {
                Stroke = new SolidColorBrush(color),
                //X1 = ClockWidth / 2 + Math.Sin(angle * (Math.PI / 180)) * 12,
                //Y1 = Height / 2 + Math.Cos(angle * (Math.PI / 180)) * 12,
                X1 = ClockWidth / 2 + Math.Sin(angle * (Math.PI / 180)) * 1,
                Y1 = Height / 2 + Math.Cos(angle * (Math.PI / 180)) * 1,
                X2 = (ClockWidth / 2) + -Math.Sin(angle * (Math.PI / 180)) * radiusX,
                Y2 = (Height / 2) + -Math.Cos(angle * (Math.PI / 180)) * radiusY
            };
            // hand.StrokeThickness = thickness;

            var hand2 = new Line
            {
                Stroke = new SolidColorBrush(color),
                X1 = ClockWidth / 2,
                Y1 = Height / 2,
                X2 = (ClockWidth / 2) + -Math.Sin((angle - handWidth) * (Math.PI / 180)) * radiusX / 2,
                Y2 = (Height / 2) + -Math.Cos((angle - handWidth) * (Math.PI / 180)) * radiusY / 2
            };
            var hand3 = new Line
            {
                Stroke = new SolidColorBrush(color),
                X1 = ClockWidth / 2,
                Y1 = Height / 2,
                X2 = (ClockWidth / 2) + -Math.Sin((angle + handWidth) * (Math.PI / 180)) * radiusX / 2,
                Y2 = (Height / 2) + -Math.Cos((angle + handWidth) * (Math.PI / 180)) * radiusY / 2
            };
            var hand4 = new Line
            {
                Stroke = new SolidColorBrush(color),
                X1 = hand.X2,
                Y1 = hand.Y2,
                X2 = (ClockWidth / 2) + -Math.Sin((angle + handWidth) * (Math.PI / 180)) * radiusX / 2,
                Y2 = (Height / 2) + -Math.Cos((angle + handWidth) * (Math.PI / 180)) * radiusY / 2
            };
            var hand5 = new Line
            {
                Stroke = new SolidColorBrush(color),
                X1 = hand.X2,
                Y1 = hand.Y2,
                X2 = (ClockWidth / 2) + -Math.Sin((angle - handWidth) * (Math.PI / 180)) * radiusX / 2,
                Y2 = (Height / 2) + -Math.Cos((angle - handWidth) * (Math.PI / 180)) * radiusY / 2
            };
            ClockLayout.Children.Add(hand);
            ClockLayout.Children.Add(hand2);

            ClockLayout.Children.Add(hand4); ClockLayout.Children.Add(hand3);
            ClockLayout.Children.Add(hand5);
        }*/
        #region IClockView Members

        public void SetTime(DateTime time)
        {
            _time = time.AddHours(Hours).AddMinutes(Minutes);
            
            //_time;
            hour = _time.Hour;
            minute = _time.Minute;
            second = _time.Second;

            // Draw;
        }

        #endregion
    }
}
