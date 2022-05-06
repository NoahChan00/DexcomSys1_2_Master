using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PentagonHMI.UserControls
{
    /// <summary>
    /// Interaction logic for UcData.xaml
    /// </summary>
    public partial class UcData : UserControl
    {
        public UcData()
        {
            InitializeComponent();
        }

        public string PassFail
        {
            get { return TBox.Text; }
            set { SetValue2(value); }
        }

        string PreValue = "NA";
        bool Grayed = false;
        void SetValue2(string str)
        {
            try
            {
                if (str != PreValue)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                    {
                        PreValue = str;
                        if (string.IsNullOrWhiteSpace(str) || !str.Contains(','))
                        {
                            TBox.Text = "N/A";
                            Round.Fill = Brushes.Wheat;
                        }
                        else
                        {
                            bool isSingleZone = false;
                            string[] strs = str.Split(',');
                            double P1 = Convert.ToInt32(strs[0]);
                            double F1 = Convert.ToInt32(strs[1]);
                            double P2 = 0;
                            double F2 = 0;
                            if (strs[2] != "-" && strs[3] != "-")
                            {
                                P2 = Convert.ToInt32(strs[2]);
                                F2 = Convert.ToInt32(strs[3]);
                            }
                            else
                                isSingleZone = true;



                            double P = P1 + P2;
                            double F = F1 + F2;

                            TPass.Text = P.ToString();
                            TFail.Text = F.ToString();
                            T1Pass.Text = P1.ToString();
                            T1Fail.Text = F1.ToString();

                            if (isSingleZone)
                            {
                                Z2PieContainer.Visibility = Visibility.Collapsed;
                                sp_Zone2.Visibility = Visibility.Collapsed;
                                lblZone2.Visibility = Visibility.Collapsed;
                                z2Line1.Visibility = Visibility.Collapsed;
                                z2Line2.Visibility = Visibility.Collapsed;
                                zone2Pie.Visibility = Visibility.Collapsed;
                            }
                            
                            T2Pass.Text = P2.ToString();
                            T2Fail.Text = F2.ToString();

                            double PF = (F / (P + F)) * 100;
                            double PF1 = (F1 / (P1 + F1)) * 100;
                            double PF2 = (F2 / (P2 + F2)) * 100;


                            if (Double.IsNaN(PF))
                                PF = 100;

                            if (Double.IsNaN(PF1))
                                PF1 = 100;

                            if (Double.IsNaN(PF2))
                                PF2 = 100;

                            TBox.Text = (P == 0 && F == 0 ? 0 : (Math.Floor((100 - PF) * 10)) / 10) + "%";

                            if (Grayed)
                            {
                                Round.Fill = Brushes.DeepSkyBlue;
                                Round1.Fill = Brushes.DodgerBlue;
                                Round2.Fill = Brushes.SteelBlue;
                                Grayed = false;
                            }

                            double degreePF = PF * 360 / 100;
                            double degreePF1 = PF1 * 360 / 100;
                            double degreePF2 = PF2 * 360 / 100;

                            PieContainer.Children.Clear();
                            PieSlice PS = new PieSlice();
                            PS.Fill = P == 0 && F == 0 ? Brushes.LightGray : Brushes.White;
                            PS.Stroke = Brushes.White;
                            PS.Width = 161;
                            PS.Height = 161;
                            PS.StartAngle = 0;
                            PS.EndAngle = degreePF;
                            PS.HorizontalAlignment = HorizontalAlignment.Center;
                            PieContainer.Children.Add(PS);

                            Z1PieContainer.Children.Clear();
                            PieSlice PS2 = new PieSlice();
                            PS2.Fill = P1 == 0 && F1 == 0 ? Brushes.Gray : Brushes.White;
                            PS2.Stroke = Brushes.White;
                            PS2.Width = 181;
                            PS2.Height = 181;
                            PS2.StartAngle = 0;
                            PS2.EndAngle = degreePF1;
                            PS2.HorizontalAlignment = HorizontalAlignment.Center;
                            Z1PieContainer.Children.Add(PS2);

                            Z2PieContainer.Children.Clear();
                            PieSlice PS3 = new PieSlice();
                            PS3.Fill = P2 == 0 && F2 == 0 ? Brushes.DimGray : Brushes.White;
                            PS3.Stroke = Brushes.White;
                            PS3.Width = 201;
                            PS3.Height = 201;
                            PS3.StartAngle = 0;
                            PS3.EndAngle = degreePF2;
                            PS3.HorizontalAlignment = HorizontalAlignment.Center;
                            Z2PieContainer.Children.Add(PS3);
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                {
                    Round.Fill = Brushes.Gray;
                    Round1.Fill = Brushes.Gray;
                    Round2.Fill = Brushes.Gray;
                    TBox.Text = "NA";
                    Grayed = true;
                    MessageBox.Show("ucData: " + ex.Message);
                }));
            }
        }


        void SetValue(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || !str.Contains(','))
            {
                TBox.Text = "N/A";
                Round.Fill = Brushes.Wheat;
            }
            else
            {
                string[] strs = str.Split(',');
                double P = Convert.ToInt32(strs[0]);
                double F = Convert.ToInt32(strs[1]);

                double PF = (F / (P + F)) * 100;
                TBox.Text = (P == 0 && F == 0 ? 0 : Math.Round(100 - PF)) + "%";

                if (PF < 20)
                {
                    Round.Fill = Brushes.DeepSkyBlue;
                }
                else if (PF < 60)
                {
                    Round.Fill = Brushes.Orange;
                }
                else if (PF <= 100)
                {
                    Round.Fill = Brushes.Red;
                }
                else
                {
                    PF = 0;
                    Round.Fill = Brushes.Wheat;
                }

                double degreePF = PF * 360 / 100;

                //if(Pie.StartAngle != 0 || Pie.EndAngle != degreePF)
                //{
                PieContainer.Children.Clear();
                PieSlice PS = new PieSlice();
                PS.Fill = Brushes.White;
                PS.Stroke = Brushes.White;
                PS.Width = 160;
                PS.Height = 160;
                PS.Margin = new Thickness(5);
                PS.StartAngle = 0;
                PS.EndAngle = degreePF;
                //PS.Name = "Pie";
                PS.HorizontalAlignment = HorizontalAlignment.Center;
                PieContainer.Children.Add(PS);
                //}

            }

        }
    }




    public class PolarPoint
    {
        // Angle expressed in degrees
        public PolarPoint(double radius, double angleDeg)
        {
            if (radius < 0.0)
                throw new ArgumentException("Radius must be non-negative");
            if ((angleDeg < 0) || (angleDeg >= 360.0))
                throw new ArgumentException("Angle must be in range [0,360)");

            Radius = radius;
            AngleDeg = angleDeg;
        }

        // Polar coordinates
        public double Radius { get; set; }
        public double AngleDeg { get; set; }

        // Cartesian coordinates
        public double X
        {
            get { return Radius * Math.Cos(AngleDeg * Math.PI / 180.0); }
        }

        public double Y
        {
            get { return Radius * Math.Sin(AngleDeg * Math.PI / 180.0); }
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }
    }

    public class PieSlice : Shape
    {
        // Angle that arc starts at
        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        // DependencyProperty - StartAngle
        private static PropertyMetadata startAngleMetadata =
                new PropertyMetadata(
                    0.0,     // Default value
                    null,    // Property changed callback
                    new CoerceValueCallback(CoerceAngle));   // Coerce value callback

        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(PieSlice), startAngleMetadata);

        // Angle that arc ends at
        public double EndAngle
        {
            get { return (double)GetValue(EndAngleProperty); }
            set { SetValue(EndAngleProperty, value); }
        }

        // DependencyProperty - EndAngle
        private static PropertyMetadata endAngleMetadata =
                new PropertyMetadata(
                    90.0,     // Default value
                    null,    // Property changed callback
                    new CoerceValueCallback(CoerceAngle));   // Coerce value callback

        public static readonly DependencyProperty EndAngleProperty =
            DependencyProperty.Register("EndAngle", typeof(double), typeof(PieSlice), endAngleMetadata);

        private static object CoerceAngle(DependencyObject depObj, object baseVal)
        {
            double angle = (double)baseVal;
            angle = Math.Min(angle, 359.9);
            angle = Math.Max(angle, 0.0);
            return angle;
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                double maxWidth = Math.Max(0.0, RenderSize.Width - StrokeThickness);
                double maxHeight = Math.Max(0.0, RenderSize.Height - StrokeThickness);
                //Console.WriteLine(string.Format("* maxWidth={0}, maxHeight={1}", maxWidth, maxHeight));

                double xStart = maxWidth / 2.0 * Math.Cos(StartAngle * Math.PI / 180.0);
                double yStart = maxHeight / 2.0 * Math.Sin(StartAngle * Math.PI / 180.0);

                double xEnd = maxWidth / 2.0 * Math.Cos(EndAngle * Math.PI / 180.0);
                double yEnd = maxHeight / 2.0 * Math.Sin(EndAngle * Math.PI / 180.0);

                StreamGeometry geom = new StreamGeometry();
                using (StreamGeometryContext ctx = geom.Open())
                {
                    ctx.BeginFigure(
                        new Point((RenderSize.Width / 2.0) + xStart,
                                  (RenderSize.Height / 2.0) - yStart),
                        true,   // Filled
                        true);  // Closed
                    ctx.ArcTo(
                        new Point((RenderSize.Width / 2.0) + xEnd,
                                  (RenderSize.Height / 2.0) - yEnd),
                        new Size(maxWidth / 2.0, maxHeight / 2),
                        0.0,     // rotationAngle
                        (EndAngle - StartAngle) > 180,   // greater than 180 deg?
                        SweepDirection.Counterclockwise,
                        true,    // isStroked
                        false);
                    ctx.LineTo(new Point((RenderSize.Width / 2.0), (RenderSize.Height / 2.0)), true, false);
                }

                return geom;
            }
        }
    }

}
