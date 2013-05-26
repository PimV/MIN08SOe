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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ExcelTest
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {

        Thread loadingThread;
        Storyboard Showboard;
        Storyboard Hideboard;
        private delegate void ShowDelegate(string txt);
        private delegate void HideDelegate();
        ShowDelegate showDelegate;
        HideDelegate hideDelegate;

        Thread isLoading;

        public SplashWindow(Thread t)
        {
            isLoading = t;
            InitializeComponent();

            showDelegate = new ShowDelegate(this.showText);
            hideDelegate = new HideDelegate(this.hideText);


            Showboard = this.Resources["showStoryBoard"] as Storyboard;
            Hideboard = this.Resources["HideStoryBoard"] as Storyboard;

            var da = new DoubleAnimation(360, 0, new Duration(TimeSpan.FromSeconds(5)));
            var rt = new RotateTransform();
            loadingLabel.RenderTransform = rt;
            loadingLabel.RenderTransformOrigin = new Point(0.5, 0.5);
            da.RepeatBehavior = RepeatBehavior.Forever;
            rt.BeginAnimation(RotateTransform.AngleProperty, da);

            loadingThread = new Thread(load);
            loadingThread.Start();
        }

        private void load()
        {

            //Thread.Sleep(1000);
            this.Dispatcher.Invoke(showDelegate, "Importing data...");           
            //Thread.Sleep(2000);
            //do some loading work
            while (isLoading.IsAlive)
            {
                
            }
            this.Dispatcher.Invoke(hideDelegate);

            //close the window
           // Thread.Sleep(2000);
            this.Dispatcher.Invoke(DispatcherPriority.Normal,
        (Action)delegate() { Close(); });
        }

        private void showText(string txt)
        {
            txtLoading.Text = txt;
            BeginStoryboard(Showboard);
        }
        private void hideText()
        {
            BeginStoryboard(Hideboard);
        }
    }
}
