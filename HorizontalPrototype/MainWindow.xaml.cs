using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HorizontalPrototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Storyboard lginsb = this.FindResource("SplashDisappear") as Storyboard;
            lginsb.Completed += OnSplashDisappearComplete;

            Storyboard sb1 = this.FindResource("SplashDisappear1") as Storyboard;
            sb1.Completed += Sb1_Completed;

            this.LoginScreenCanvas.Visibility = Visibility.Hidden;
            this.SignupScreenCanvas.Visibility = Visibility.Hidden;
            this.EditScreenViewer.Visibility = Visibility.Hidden;
        }

        private void Sb1_Completed(object sender, EventArgs e)
        {
            this.LoginScreenCanvas.Visibility = Visibility.Hidden;
            this.MainScreenCanvas.Visibility = Visibility.Hidden;
            this.SignupScreenCanvas.Visibility = Visibility.Visible;

            this.SignupButton1.Click += SaveButton_Click;

            this.BackButton1.Click += OnBackButtonClicked;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.SignupScreenCanvas.Visibility = Visibility.Hidden;
            this.EditScreenViewer.Visibility = Visibility.Visible;
        }

        private void OnBackButtonClicked(object sender, RoutedEventArgs e)
        {
            this.LoginScreenCanvas.Visibility = Visibility.Hidden;
            this.SignupScreenCanvas.Visibility = Visibility.Hidden;
            Storyboard sb = this.FindResource("SplashAppear") as Storyboard;
            this.MainScreenCanvas.Visibility = Visibility.Visible;
            sb.Begin();

        }

        private void OnSplashDisappearComplete(object sender, EventArgs e)
        {
            this.MainScreenCanvas.Visibility = Visibility.Hidden;
            this.LoginScreenCanvas.Visibility = Visibility.Visible;

            this.BackButton.Click += OnBackButtonClicked;

        }


    }

}
