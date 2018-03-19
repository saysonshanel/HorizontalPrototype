﻿using System;
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

            //When the login button is pressed on the main screen, direct to login screen
            Storyboard lginsb = this.FindResource("SplashDisappear") as Storyboard;
            lginsb.Completed += LoginButtonComplete;


            //when the signup button is clicked, direct to registration screen
            Storyboard sb1 = this.FindResource("SplashDisappear1") as Storyboard;
            sb1.Completed += SignupButtonComplete;

            //hide other canvas's
            this.LoginScreenCanvas.Visibility = Visibility.Hidden;
            this.LoginScreenCanvas_Username.Visibility = Visibility.Hidden;
            this.SignupScreenCanvas.Visibility = Visibility.Hidden;
            this.EditScreenViewer.Visibility = Visibility.Hidden;

            //when facebook or google connection is clicked,  move to login
            this.FacebookButton.Click += LoginButtonComplete;
            this.GoogleButton.Click += LoginButtonComplete;

        }

        //sign up screen
        private void SignupButtonComplete(object sender, EventArgs e)
        {
            //hide other screens
            this.LoginScreenCanvas.Visibility = Visibility.Hidden;
            this.MainScreenCanvas.Visibility = Visibility.Hidden;
            //show signup screen
            this.SignupScreenCanvas.Visibility = Visibility.Visible;

            //when signup button is clicked, direct to edit profile
            this.RegisterSignupButton.Click += ToEditProfileScreen;
            //otherwise back button is clicked, direct to main screen
            this.BackButton1.Click += OnBackButtonClicked;

        }

        //move back to main screen
        private void OnBackButtonClicked(object sender, RoutedEventArgs e)
        {
            //hide the other screens
            this.LoginScreenCanvas.Visibility = Visibility.Hidden;
            this.SignupScreenCanvas.Visibility = Visibility.Hidden;

            //show the main screen
            Storyboard sb = this.FindResource("SplashAppear") as Storyboard;
            this.MainScreenCanvas.Visibility = Visibility.Visible;
            sb.Begin();

        }

        //loginscreen
        private void LoginButtonComplete(object sender, EventArgs e)
        {
            //hide the main screen
            this.MainScreenCanvas.Visibility = Visibility.Hidden;
            //show the login screen
            this.LoginScreenCanvas.Visibility = Visibility.Visible;

            //when login button is clicked, check for required fields
            //this.LoginScreenLoginButton.Click += OnLoginScreenLoginButtonClicked;
            this.LoginScreenLoginButton.Click += ToEditProfileScreen;

            //otherwise, if back button is clicked, go back to main screen
            this.BackButton.Click += OnBackButtonClicked;


        }

      /*  private void OnLoginScreenLoginButtonClicked(object sender, RoutedEventArgs e)
        {

            if (UsernameTextbox.Text == string.Empty)
            {
                this.LoginScreenCanvas_Username.Visibility = Visibility.Visible;
                MessageBox.Show("Please enter in a Username");
            }
            else
            {
                this.LoginScreenLoginButton.Click += ToEditProfileScreen;
                this.LoginScreenLoginButton1.Click += ToEditProfileScreen;
            }
            
               
            
        }
        */

        //edit profile screen
        private void ToEditProfileScreen(object sender, RoutedEventArgs e)
        {

            this.LoginScreenCanvas_Username.Visibility = Visibility.Hidden;
            this.LoginScreenCanvas.Visibility = Visibility.Hidden;
            this.SignupScreenCanvas.Visibility = Visibility.Hidden;
            this.EditScreenViewer.Visibility = Visibility.Visible;
        }
    }

}
