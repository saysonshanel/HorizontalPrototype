using Microsoft.Win32;
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
    /// 

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
            this.MainScreenCanvas.Visibility = Visibility.Visible;

            //when facebook or google connection is clicked,  move to login
            this.FacebookButton.Click += ToEditProfileScreen;
            this.GoogleButton.Click += ToEditProfileScreen;
            
            /*for(int i = 0; i < 5; i++)
            {
                ViewMatch person = new ViewMatch();
                this.AddMatches.Children.Add(person);
                person.ViewButton.Click += ViewButton_Click;
            }
            this.AddMatches.Width = 5 * 160;*/


            ViewMatch[] persons = new ViewMatch[5];// people in carousel
            double center = this.Canvas.Width / 2;
            double[] location = {center-75, center-(center/4+20), center+ (center / 4 + 20), center-(center+50), center+(center+50)};// location of people in carousel
            double[] size = { 1, .65, .65, .25, .25 };// size of people in carousel
            for(int i = 0; i < 5; i++)// add and scale people in carousel
            {
                persons[i] = new ViewMatch{Width = 150, Height = 200};
                this.Carousel.Children.Add(persons[i]);
                persons[i].ViewButton.Click += ViewButton_Click;
                ScalePerson(persons[i], size[i]);
            }
            persons[0].HorizontalAlignment = HorizontalAlignment.Center;// using alignment to move poeple because I can't modify margins without hardcoding
            persons[1].HorizontalAlignment = HorizontalAlignment.Left;
            persons[2].HorizontalAlignment = HorizontalAlignment.Right;
            persons[3].Visibility = Visibility.Hidden;
            persons[4].Visibility = Visibility.Hidden;

            this.LocationButton.Click += LocationButton_Click;
            this.ChatButton.Click += ChatButton_Click;
            this.BackToViewProfile.Click += ViewButton_Click;
            this.ConfirmationButton.Click += ViewButton_Click;

            //menu buttons
            this.homebutton.Click += MainFeedTextbox_Click;
            this.matchesbutton.Click += ViewMatchesTextbox_Click;
            this.profilebutton.Click += EditProfileTextbox_Click;
        }

        private void ScalePerson(ViewMatch person, double scale)
        {
            person.Width *= scale;
            person.Height = (person.Height - 50) * scale + 50;
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.BackButton1_Copy3.Click += ViewButton_Click;
            this.MessagingScreen.Visibility = Visibility.Visible;
        }

        private void LocationButton_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.BackButton1_Copy4.Click += ViewButton_Click;
            this.MeetingScreen.Visibility = Visibility.Visible;
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.BottomMenu.Visibility = Visibility.Visible;
            //  this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.BackButton1_Copy2.Click += ViewMatchesTextbox_Click;

            this.ViewMatch.Visibility = Visibility.Visible;
        }


        //sign up screen
        private void SignupButtonComplete(object sender, EventArgs e)
        {
            //hide other screens
            this.FeedbackMessage.Visibility = Visibility.Hidden;
            this.LoginScreenCanvas.Visibility = Visibility.Hidden;
            this.MainScreenCanvas.Visibility = Visibility.Hidden;
            this.SignupUsernameErrorCanvas.Visibility = Visibility.Hidden;
            this.SignupPasswordErrorCanvas.Visibility = Visibility.Hidden;
            this.SignupRepeatErrorCanvas.Visibility = Visibility.Hidden;
            //show signup screen
            this.SignupScreenCanvas.Visibility = Visibility.Visible;

            //when signup button is clicked, direct to edit profile
            //this.RegisterSignupButton.Click += ToLoginScreen;
            this.RegisterSignupButton.PreviewMouseDown += CheckSignupFieldsOnClicked;
            //otherwise back button is clicked, direct to main screen
            this.BackButton1.Click += OnBackButtonClicked;

        }

        private void CheckSignupFieldsOnClicked(object sender, RoutedEventArgs e)
        {
            this.FeedbackMessage.Visibility = Visibility.Visible;
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
            HideAll();
            this.LoginFeedbackMessage.Visibility = Visibility.Hidden;
            this.MainScreenCanvas.Visibility = Visibility.Hidden;
            this.SignupScreenCanvas.Visibility = Visibility.Hidden;
            this.LoginUsernameErrorCanvas.Visibility = Visibility.Hidden;
            this.LoginPasswordErrorCanvas.Visibility = Visibility.Hidden;
            //show the login screen
            this.LoginScreenCanvas.Visibility = Visibility.Visible;

            //when login button is clicked, move to the edit profile screen
            //  this.LoginScreenLoginButton.Click += ToEditProfileScreen;
            this.LoginScreenLoginButton.PreviewMouseDown += CheckLoginScreenFieldsOnClicked;

            //otherwise, if back button is clicked, go back to main screen
            this.BackButton.Click += OnBackButtonClicked;

        }

        private void CheckLoginScreenFieldsOnClicked(object sender, RoutedEventArgs e)
        {
            this.LoginFeedbackMessage.Visibility = Visibility.Visible;
            if ((this.UsernameTextbox.Text == string.Empty) && (this.PasswordBox.Password == string.Empty))
            {
                this.LoginUsernameErrorCanvas.Visibility = Visibility.Visible;
                this.LoginPasswordErrorCanvas.Visibility = Visibility.Visible;

            }
            else if (this.UsernameTextbox.Text == string.Empty)
            {
                this.LoginPasswordErrorCanvas.Visibility = Visibility.Hidden;

                this.LoginUsernameErrorCanvas.Visibility = Visibility.Visible;
            } else if (this.PasswordBox.Password == string.Empty)
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
            HideAll();
            this.EditScreenViewer.Visibility = Visibility.Visible;
            this.BottomMenu.Visibility = Visibility.Visible;
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
           // this.HamburgerMenuButton.Click += HamburgerMenuButton_Click1;

            this.SaveButton.Click += EditProfileTextbox_Click;
            this.LogoutButton.Click += ToLoginScreen;
            this.ChangePhotoButton.Click += ChangePhotoButton_Click;
        }

        private void ChangePhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "JPG(*.JPG) | *.jpg";
            if (open.ShowDialog() == true)
            {
                this.pictureBox.Source = new BitmapImage(new Uri(open.FileName));
            }

        }

        private void HamburgerMenuButton_Click1(object sender, RoutedEventArgs e)
        {
            this.HamburgerMenu.Visibility = Visibility.Visible;
            this.HamburgerBack.Visibility = Visibility.Visible;
            this.MainFeedTextbox.Click += MainFeedTextbox_Click;
            this.ViewMatchesTextbox.Click += ViewMatchesTextbox_Click;
            this.EditProfileTextbox.Click += EditProfileTextbox_Click;
            this.QuestionnaireTextbox.Click += QuestionnaireTextbox_Click;
            this.LogoutTextbox.Click += LogoutTextbox_Click;
            this.BackTextbox.Click += BackTextbox_Click;
            this.HamburgerBack.Click += BackTextbox_Click;
            this.View_Profile.Click += View_Profile_Click;
        }

        private void View_Profile_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.BottomMenu.Visibility = Visibility.Visible;
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.ProfileScreenCanvas.Visibility = Visibility.Visible;
        }

        private void BackTextbox_Click(object sender, RoutedEventArgs e)
        {
            this.HamburgerMenu.Visibility = Visibility.Hidden;
            this.HamburgerBack.Visibility = Visibility.Hidden;
        }

        private void LogoutTextbox_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.LoginUsernameErrorCanvas.Visibility = Visibility.Hidden;
            this.LoginPasswordErrorCanvas.Visibility = Visibility.Hidden;
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
            this.BottomMenu.Visibility = Visibility.Visible;
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.QuizMainScreenCanvas.Visibility = Visibility.Visible;
            this.BackButton1_Copy1.Click += View_Profile_Click;

            this.Quiz1Button.Click += QuizbuttonOnClick;
            this.Quiz2Button.Click += QuizbuttonOnClick;
            this.Quiz3Button.Click += QuizbuttonOnClick;
            this.Quiz4Button.Click += QuizbuttonOnClick;
        }



        private void QuizbuttonOnClick(object sender, RoutedEventArgs e)
        {
            HideAll();

            Storyboard quizsignifiersb = this.FindResource("animation1") as Storyboard;
            quizsignifiersb.Begin();
            this.QuizQuestionScreen.Visibility = Visibility.Visible;
            this.QuizSaveButton.Click += QuestionnaireTextbox_Click;
            this.BackButton1_Copy.Click += QuestionnaireTextbox_Click;
        }

        private void EditProfileTextbox_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.BottomMenu.Visibility = Visibility.Visible;
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.ProfileScreenCanvas.Visibility = Visibility.Visible;
            this.editprofilebutton.Click += Editprofilebutton_Click;
            this.questionnairebutton.Click += QuestionnaireTextbox_Click;
        }

        private void Editprofilebutton_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.EditScreenViewer.Visibility = Visibility.Visible;
            this.BottomMenu.Visibility = Visibility.Visible;
        }

        private void ViewMatchesTextbox_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.MatchInitScreen.Visibility = Visibility.Visible;
            this.BottomMenu.Visibility = Visibility.Visible;
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
        }

        private void MainFeedTextbox_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
          //  Storyboard bouncesb = this.FindResource("bounce") as Storyboard;
           // bouncesb.Begin();

            Storyboard animation = this.FindResource("tutorialanimation") as Storyboard;
            animation.Begin();

            this.BottomMenu.Visibility = Visibility.Visible;
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.MainFeedCanvas.Visibility = Visibility.Visible;
            this.SignifierDown.Click += SignifierDown_Click;
            this.SwipeUpButton.Click += View_Profile_Click;
        }

        private void SignifierDown_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.BottomMenu.Visibility = Visibility.Visible;
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.SwipeDown.Visibility = Visibility.Visible;
            this.UndoButton.Click += MainFeedTextbox_Click;
        }


        public void HideAll()
        {
            this.MainScreenCanvas.Visibility = Visibility.Hidden;
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
            this.BottomMenu.Visibility = Visibility.Hidden;
        }

        //profile screen
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.BottomMenu.Visibility = Visibility.Visible;
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.ProfileScreenCanvas.Visibility = Visibility.Visible;

        }
    }

}
