using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PentagonHMI.ChildControls
{
    public partial class ucIOLocation : UserControl, IDisposable
    {
        private LogicClasses.Main _Main;
        private bool isFullscreen = false;
        private bool isZoomed = false;

        #region Constructor

        public ucIOLocation(ref LogicClasses.Main Main)
        {
            _Main = Main;
            InitializeComponent();

            if(!Directory.Exists(@"D:\IOLocations"))
                Directory.CreateDirectory(@"D:\IOLocations");

            DirectoryInfo DI = new DirectoryInfo(@"D:\IOLocations");
            List<IOLocationImg> ioLocations = new List<IOLocationImg>();

            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
            {
                try
                {
                    DI.GetFiles().ToList().ForEach(x =>
                    {
                        // check if image
                        ioLocations.Add(new IOLocationImg
                        {
                            Img = new BitmapImage(new Uri(x.FullName)),
                            Name = System.IO.Path.ChangeExtension(x.Name, null),
                        });
                        Fullscreen.Source = new BitmapImage(new Uri(x.FullName));
                    });
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                itemControls.ItemsSource = ioLocations;
            }));
        }

        #endregion Constructor

        #region Destructor

        ~ucIOLocation()
        {
            Dispose();
        }

        public void Dispose()
        {
            _Main.IOLocPageON = false;
        }

        #endregion Destructor

        private void StackPanel_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(isFullscreen)
            {
                Fullscreen.RenderTransform = null;
                var position = e.MouseDevice.GetPosition(Fullscreen);
                if(isZoomed)
                {
                    Fullscreen.RenderTransform = null;
                    isZoomed = !isZoomed;
                }
                else
                {
                    Fullscreen.RenderTransform = new ScaleTransform(1.5, 1.5, position.X, position.Y);
                    isZoomed = !isZoomed;
                }
            }
            else
            {
                StackPanel SP = (StackPanel)sender;
                Fullscreen.RenderTransform = null;
                Fullscreen.Source = ((IOLocationImg)SP.Tag).Img;
                Fullscreen.Visibility = Visibility.Visible;
                btnBack.Visibility = Visibility.Visible;
                ioListBorder.Visibility = Visibility.Collapsed;
                isFullscreen = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Fullscreen.Visibility = Visibility.Collapsed;
            btnBack.Visibility = Visibility.Collapsed;
            ioListBorder.Visibility = Visibility.Visible;
            isFullscreen = false;
        }
    }

    public class IOLocationImg
    {
        public BitmapImage Img { get; set; }
        public string Name { get; set; }
    }
}