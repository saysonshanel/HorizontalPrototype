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


            //When the login button is pressed on the main screen, direct to login screen
            Storyboard lginsb = this.FindResource("SplashDisappear") as Storyboard;
            lginsb.Completed += ToLoginScreen;


            //when the signup button is clicked, direct to registration screen
            Storyboard sb1 = this.FindResource("SplashDisappear1") as Storyboard;
            sb1.Completed += SignupButtonComplete;


            //hide other canvas's
            HideAll();

            //when facebook or google connection is clicked,  move to login
            this.FacebookButton.Click += ToLoginScreen;
            this.GoogleButton.Click += ToLoginScreen;


            //When toggle menu button is checked
           // this.MenuButton.Click += UponToggleMenuButtonChecked;

            //When toggle menu is collapsed
           // Storyboard sb2 = this.FindResource("MenuCollapse") as Storyboard;
           // sb2.Completed += DropDownMenuCollapseComplete;

            //When save button is clicked, direct to profile screen
            //this.SaveButton.Click += UponEditProfileButtonClicked;

            //Quiz button clicked, start quiz
           // this.Quiz1Button.Click += UponQuizButtonClicked;
       

        }


        /*  private void UponQuizButtonClicked(object sender, RoutedEventArgs e)
          {
              QuizMainScreenCanvas.Visibility = Visibility.Hidden;
              QuizQuestionScreen.Visibility = Visibility.Visible;
          }

          private void UponEditProfileButtonClicked(object sender, RoutedEventArgs e)
          {
              this.EditScreenCanvas.Visibility = Visibility.Hidden;
          }

          private void DropDownMenuCollapseComplete(object sender, EventArgs e)
          {
              this.dropDownMenuControl.Visibility = Visibility.Hidden;
          }

          private void UponToggleMenuButtonChecked(object sender, RoutedEventArgs e)
          {
              this.dropDownMenuControl.Visibility = Visibility.Visible;
          }*/

        //sign up screen
        private void SignupButtonComplete(object sender, EventArgs e)
        {
            //hide other screens
            this.LoginScreenCanvas.Visibility = Visibility.Hidden;
            this.MainScreenCanvas.Visibility = Visibility.Hidden;
            //show signup screen
            this.SignupScreenCanvas.Visibility = Visibility.Visible;

            //when signup button is clicked, direct to edit profile
            this.RegisterSignupButton.Click += ToLoginScreen;

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
        private void ToLoginScreen(object sender, EventArgs e)
        {
            //hide the main screen
            this.MainScreenCanvas.Visibility = Visibility.Hidden;
            this.SignupScreenCanvas.Visibility = Visibility.Hidden;
            //show the login screen
            this.LoginScreenCanvas.Visibility = Visibility.Visible;

            //when login button is clicked, move to the edit profile screen
            this.LoginScreenLoginButton.Click += ToEditProfileScreen;

            //otherwise, if back button is clicked, go back to main screen
            this.BackButton.Click += OnBackButtonClicked;

        }

        //edit profile screen
        private void ToEditProfileScreen(object sender, RoutedEventArgs e)
        {
            this.LoginScreenCanvas.Visibility = Visibility.Hidden;
            this.SignupScreenCanvas.Visibility = Visibility.Hidden;
            this.EditScreenViewer.Visibility = Visibility.Visible;
            this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.HamburgerMenuButton.Click += HamburgerMenuButton_Click1;

            this.SaveButton.Click += SaveButton_Click;
        }

        private void HamburgerMenuButton_Click1(object sender, RoutedEventArgs e)
        {
            this.HamburgerMenu.Visibility = Visibility.Visible;
            this.MainFeedTextbox.Click += MainFeedTextbox_Click;
            this.ViewMatchesTextbox.Click += ViewMatchesTextbox_Click;
            this.EditProfileTextbox.Click += EditProfileTextbox_Click;
            this.QuestionnaireTextbox.Click += QuestionnaireTextbox_Click;
            this.LogoutTextbox.Click += LogoutTextbox_Click;
            this.BackTextbox.Click += BackTextbox_Click;
        }

        private void BackTextbox_Click(object sender, RoutedEventArgs e)
        {
            this.HamburgerMenu.Visibility = Visibility.Hidden;
        }

        private void LogoutTextbox_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.MainScreenCanvas.Visibility = Visibility.Hidden;
            this.SignupScreenCanvas.Visibility = Visibility.Hidden;
            //show the login screen
            this.LoginScreenCanvas.Visibility = Visibility.Visible;

            //when login button is clicked, move to the edit profile screen
            this.LoginScreenLoginButton.Click += ToEditProfileScreen;

            //otherwise, if back button is clicked, go back to main screen
            this.BackButton.Click += OnBackButtonClicked;
        }

        private void QuestionnaireTextbox_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditProfileTextbox_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ViewMatchesTextbox_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.MatchInitScreen.Visibility = Visibility.Visible;
            this.HamburgerMenuButton.Visibility = Visibility.Visible;
        }

        private void MainFeedTextbox_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.MainFeedCanvas.Visibility = Visibility.Visible;
        }

        public void HideAll()
        {
            this.LoginScreenCanvas.Visibility = Visibility.Hidden;
            this.SignupScreenCanvas.Visibility = Visibility.Hidden;
            this.EditScreenViewer.Visibility = Visibility.Hidden;
            this.dropDownMenuControl.Visibility = Visibility.Hidden;
            this.ProfileScreenCanvas.Visibility = Visibility.Hidden;
            this.QuizMainScreenCanvas.Visibility = Visibility.Hidden;
            this.QuizQuestionScreen.Visibility = Visibility.Hidden;
            this.ViewAllMatches.Visibility = Visibility.Hidden;
            this.ViewMatch.Visibility = Visibility.Hidden;
            this.MessagingScreen.Visibility = Visibility.Hidden;
            this.dropDownMenuControl.Visibility = Visibility.Hidden;
            this.MenuButton.Visibility = Visibility.Hidden;
            this.HamburgerMenu.Visibility = Visibility.Hidden;
            this.HamburgerMenuButton.Visibility = Visibility.Hidden;
            this.MatchInitScreen.Visibility = Visibility.Hidden;
            this.SwipeUp1.Visibility = Visibility.Hidden;
            this.SwipeDown.Visibility = Visibility.Hidden;
            this.MeetingScreen.Visibility = Visibility.Hidden;
            this.MainFeedCanvas.Visibility = Visibility.Hidden;

        }

        //profile screen
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.ProfileScreenCanvas.Visibility = Visibility.Visible;

        }
    }

}
