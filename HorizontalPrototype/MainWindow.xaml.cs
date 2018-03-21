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
            
            for(int i = 0; i < 5; i++)
            {
                ViewMatch person = new ViewMatch();
                this.AddMatches.Children.Add(person);
                person.ViewButton.Click += ViewButton_Click;
            }
            this.AddMatches.Width = 5 * 160;
            this.LocationButton.Click += LocationButton_Click;
            this.ChatButton.Click += ChatButton_Click;
            this.BackToViewProfile.Click += ViewButton_Click;
            this.ConfirmationButton.Click += ViewButton_Click;
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.MessagingScreen.Visibility = Visibility.Visible;
        }

        private void LocationButton_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.MeetingScreen.Visibility = Visibility.Visible;
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.ViewMatch.Visibility = Visibility.Visible;
        }


        //sign up screen
        private void SignupButtonComplete(object sender, EventArgs e)
        {
            //hide other screens
            this.LoginScreenCanvas.Visibility = Visibility.Hidden;
            this.MainScreenCanvas.Visibility = Visibility.Hidden;
            this.SignupUsernameErrorCanvas.Visibility = Visibility.Hidden;
            this.SignupPasswordErrorCanvas.Visibility = Visibility.Hidden;
            this.SignupRepeatErrorCanvas.Visibility = Visibility.Hidden;
            //show signup screen
            this.SignupScreenCanvas.Visibility = Visibility.Visible;

            //when signup button is clicked, direct to edit profile
            //this.RegisterSignupButton.Click += ToLoginScreen;
            this.RegisterSignupButton.Click += CheckSignupFieldsOnClicked;
            //otherwise back button is clicked, direct to main screen
            this.BackButton1.Click += OnBackButtonClicked;

        }

        private void CheckSignupFieldsOnClicked(object sender, RoutedEventArgs e)
        {

            if(SignupPasswordTextbox.Password != SignupPwRepeatTextbox.Password)
            {
                MessageBox.Show("Passwords do not match.");
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Visible;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Visible;

            }else if ((this.SignupUsernameTextbox.Text == string.Empty) && (this.SignupPasswordTextbox.Password == string.Empty) && (this.SignupPwRepeatTextbox.Password == string.Empty))
            {
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Visible;
                this.SignupUsernameErrorCanvas.Visibility = Visibility.Visible;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Visible;
            }else if ((this.SignupUsernameTextbox.Text == string.Empty) && (this.SignupPasswordTextbox.Password == string.Empty))
            {
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Visible;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Hidden;
                this.SignupUsernameErrorCanvas.Visibility = Visibility.Visible;
            }else if((this.SignupPasswordTextbox.Password == string.Empty) && (this.SignupPwRepeatTextbox.Password == string.Empty))
            {
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Visible;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Visible;
                this.SignupUsernameErrorCanvas.Visibility = Visibility.Hidden;
            

            }else if ((this.SignupUsernameTextbox.Text == string.Empty) && (this.SignupPwRepeatTextbox.Password == string.Empty))
            {
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Hidden;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Visible;
                this.SignupUsernameErrorCanvas.Visibility = Visibility.Visible;
            }
            else if(this.SignupUsernameTextbox.Text == string.Empty)
            {
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Hidden;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Hidden;
                this.SignupUsernameErrorCanvas.Visibility = Visibility.Visible;
            }else if(this.SignupPasswordTextbox.Password == string.Empty)
            {
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Visible;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Hidden;
                this.SignupUsernameErrorCanvas.Visibility = Visibility.Hidden;

            }else if(this.SignupPwRepeatTextbox.Password == string.Empty)
            {
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Hidden;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Visible;
                this.SignupUsernameErrorCanvas.Visibility = Visibility.Hidden;

            }else
            {
                this.RegisterSignupButton.Click += ToLoginScreen;
            }
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
            this.LoginUsernameErrorCanvas.Visibility = Visibility.Hidden;
            this.LoginPasswordErrorCanvas.Visibility = Visibility.Hidden;
            //show the login screen
            this.LoginScreenCanvas.Visibility = Visibility.Visible;

            //when login button is clicked, move to the edit profile screen
            //  this.LoginScreenLoginButton.Click += ToEditProfileScreen;
            this.LoginScreenLoginButton.Click += CheckLoginScreenFieldsOnClicked;

            //otherwise, if back button is clicked, go back to main screen
            this.BackButton.Click += OnBackButtonClicked;

        }

        private void CheckLoginScreenFieldsOnClicked(object sender, RoutedEventArgs e)
        {
            if((this.UsernameTextbox.Text == string.Empty) && (this.PasswordBox.Password == string.Empty))
            {
                this.LoginUsernameErrorCanvas.Visibility = Visibility.Visible;
                this.LoginPasswordErrorCanvas.Visibility = Visibility.Visible;

            }
            else if(this.UsernameTextbox.Text == string.Empty)
            {
                this.LoginPasswordErrorCanvas.Visibility = Visibility.Hidden;

                this.LoginUsernameErrorCanvas.Visibility = Visibility.Visible;
            }else if(this.PasswordBox.Password == string.Empty)
            {
                this.LoginUsernameErrorCanvas.Visibility = Visibility.Hidden;
                this.LoginPasswordErrorCanvas.Visibility = Visibility.Visible;
            }else
            {
                this.LoginScreenLoginButton.Click += ToEditProfileScreen;
            }
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
         //   this.HamburgerBack.Visibility = Visibility.Visible;
            this.MainFeedTextbox.Click += MainFeedTextbox_Click;
            this.ViewMatchesTextbox.Click += ViewMatchesTextbox_Click;
            this.EditProfileTextbox.Click += EditProfileTextbox_Click;
            this.QuestionnaireTextbox.Click += QuestionnaireTextbox_Click;
            this.LogoutTextbox.Click += LogoutTextbox_Click;
            this.BackTextbox.Click += BackTextbox_Click;
         //   this.HamburgerBack.Click += BackTextbox_Click;
        }

        private void BackTextbox_Click(object sender, RoutedEventArgs e)
        {
            this.HamburgerMenu.Visibility = Visibility.Hidden;
            this.HamburgerBack.Visibility = Visibility.Hidden;
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
            HideAll();
            this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.QuizMainScreenCanvas.Visibility = Visibility.Visible;

            this.Quiz1Button.Click += QuizbuttonOnClick;
            this.Quiz2Button.Click += QuizbuttonOnClick;
            this.Quiz3Button.Click += QuizbuttonOnClick;
            this.Quiz4Button.Click += QuizbuttonOnClick;
        }

        private void QuizbuttonOnClick(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.QuizQuestionScreen.Visibility = Visibility.Visible;
        }

        private void EditProfileTextbox_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.EditScreenViewer.Visibility = Visibility.Visible;
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
            this.SwipeUpControl.Click += SwipeUpControl_Click;
            this.SwipeDownControl.Click += SwipeDownControl_Click;
        }

        private void SwipeDownControl_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.SwipeDown.Visibility = Visibility.Visible;
        }

        private void SwipeUpControl_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.SwipeUp1.Visibility = Visibility.Visible;
        }

        public void HideAll()
        {
            this.LoginScreenCanvas.Visibility = Visibility.Hidden;
            this.SignupScreenCanvas.Visibility = Visibility.Hidden;
            this.EditScreenViewer.Visibility = Visibility.Hidden;
            this.ProfileScreenCanvas.Visibility = Visibility.Hidden;
            this.QuizMainScreenCanvas.Visibility = Visibility.Hidden;
            this.QuizQuestionScreen.Visibility = Visibility.Hidden;
            this.ViewAllMatches.Visibility = Visibility.Hidden;
            this.ViewMatch.Visibility = Visibility.Hidden;
            this.MessagingScreen.Visibility = Visibility.Hidden;
            this.HamburgerMenu.Visibility = Visibility.Hidden;
            this.HamburgerMenuButton.Visibility = Visibility.Hidden;
            this.MatchInitScreen.Visibility = Visibility.Hidden;
            this.SwipeUp1.Visibility = Visibility.Hidden;
            this.SwipeDown.Visibility = Visibility.Hidden;
            this.MeetingScreen.Visibility = Visibility.Hidden;
            this.MainFeedCanvas.Visibility = Visibility.Hidden;
            this.HamburgerBack.Visibility = Visibility.Hidden;
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
