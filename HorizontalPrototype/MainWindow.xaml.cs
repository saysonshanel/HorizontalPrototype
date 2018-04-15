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
        private int activeProfile = 2;
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

            this.LeftButton1.Click += LeftButton1_Click;
            this.RightButton1.Click += RightButton1_Click;
            this.viewMatch.ViewButton.Click += ViewButton_Click;
            this.viewMatch1.ViewButton.Click += ViewButton_Click;
            this.viewMatch2.ViewButton.Click += ViewButton_Click;

            this.LocationButton.Click += LocationButton_Click;
            this.ChatButton.Click += ChatButton_Click;
            this.BackToViewProfile.Click += ViewButton_Click;
            this.ConfirmationButton.Click += ViewButton_Click;

            //menu buttons
            this.homebutton.Click += MainFeedTextbox_Click;
            this.matchesbutton.Click += ViewMatchesTextbox_Click;
            this.profilebutton.Click += EditProfileTextbox_Click;
        }

        private void RightButton1_Click(object sender, RoutedEventArgs e)
        {
            if (activeProfile == 2)
            {
                Storyboard sb = this.FindResource("2to3") as Storyboard;
                sb.Begin();
                activeProfile = 3;
            }
            if (activeProfile == 1)
            {
                Storyboard sb = this.FindResource("1to2") as Storyboard;
                sb.Begin();
                activeProfile = 2;
            }
        }

        private void LeftButton1_Click(object sender, RoutedEventArgs e)
        {
            if(activeProfile == 2)
            {
                Storyboard sb = this.FindResource("2to1") as Storyboard;
                sb.Begin();
                activeProfile = 1;
            }
            if (activeProfile == 3)
            {
                Storyboard sb = this.FindResource("3to2") as Storyboard;
                sb.Begin();
                activeProfile = 2;
            }
        }

        private void HideKeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            this.KeyboardView.Visibility = Visibility.Hidden;
            this.HamburgerBack.Visibility = Visibility.Hidden;
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
            this.VerifyCheckErrorCanvas.Visibility = Visibility.Hidden;
            //show signup screen
            this.SignupScreenCanvas.Visibility = Visibility.Visible;

            this.SignupPasswordTextbox.PreviewMouseDown += PreviewKeyboard;
            this.SignupPwRepeatTextbox.PreviewMouseDown += PreviewKeyboard;
            this.SignupUsernameTextbox.PreviewMouseDown += PreviewKeyboard;
            this.SignupFullnameTextbox.PreviewMouseDown += PreviewKeyboard;
            this.SignupEmailTextbox.PreviewMouseDown += PreviewKeyboard;


            //when signup button is clicked, direct to edit profile
            //this.RegisterSignupButton.Click += ToLoginScreen;
            this.RegisterSignupButton.PreviewMouseDown += CheckSignupFieldsOnClicked;
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
                this.FeedbackMessage.Visibility = Visibility.Visible;


            }
            else if ((this.SignupUsernameTextbox.Text == string.Empty) && (this.SignupPasswordTextbox.Password == string.Empty) && (this.SignupPwRepeatTextbox.Password == string.Empty))
            {
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Visible;
                this.SignupUsernameErrorCanvas.Visibility = Visibility.Visible;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Visible;
                this.FeedbackMessage.Visibility = Visibility.Visible;

            }
            else if ((this.SignupUsernameTextbox.Text == string.Empty) && (this.SignupPasswordTextbox.Password == string.Empty))
            {
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Visible;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Hidden;
                this.SignupUsernameErrorCanvas.Visibility = Visibility.Visible;
                this.FeedbackMessage.Visibility = Visibility.Visible;

            }
            else if((this.SignupPasswordTextbox.Password == string.Empty) && (this.SignupPwRepeatTextbox.Password == string.Empty))
            {
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Visible;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Visible;
                this.SignupUsernameErrorCanvas.Visibility = Visibility.Hidden;
                this.FeedbackMessage.Visibility = Visibility.Visible;



            }
            else if ((this.SignupUsernameTextbox.Text == string.Empty) && (this.SignupPwRepeatTextbox.Password == string.Empty))
            {
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Hidden;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Visible;
                this.SignupUsernameErrorCanvas.Visibility = Visibility.Visible;
                this.FeedbackMessage.Visibility = Visibility.Visible;

            }
            else if(this.SignupUsernameTextbox.Text == string.Empty)
            {
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Hidden;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Hidden;
                this.SignupUsernameErrorCanvas.Visibility = Visibility.Visible;
                this.FeedbackMessage.Visibility = Visibility.Visible;

            }
            else if(this.SignupPasswordTextbox.Password == string.Empty)
            {
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Visible;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Hidden;
                this.SignupUsernameErrorCanvas.Visibility = Visibility.Hidden;
                this.FeedbackMessage.Visibility = Visibility.Visible;


            }
            else if(this.SignupPwRepeatTextbox.Password == string.Empty)
            {
                this.SignupPasswordErrorCanvas.Visibility = Visibility.Hidden;
                this.SignupRepeatErrorCanvas.Visibility = Visibility.Visible;
                this.SignupUsernameErrorCanvas.Visibility = Visibility.Hidden;
                this.FeedbackMessage.Visibility = Visibility.Visible;


            }else if (this.VerifyCheckbox.IsChecked == false)
            {
                this.VerifyCheckErrorCanvas.Visibility = Visibility.Visible;
                this.FeedbackMessage.Visibility = Visibility.Visible;

            }
            else
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
            this.mainfeedpicture2.Visibility = Visibility.Hidden;
            this.LoginFeedbackMessage.Visibility = Visibility.Hidden;
            this.MainScreenCanvas.Visibility = Visibility.Hidden;
            this.SignupScreenCanvas.Visibility = Visibility.Hidden;
            this.LoginUsernameErrorCanvas.Visibility = Visibility.Hidden;
            this.LoginPasswordErrorCanvas.Visibility = Visibility.Hidden;
            //show the login screen
            this.LoginScreenCanvas.Visibility = Visibility.Visible;

            this.UsernameTextbox.PreviewMouseDown += PreviewKeyboard;
            this.PasswordBox.PreviewMouseDown += PreviewKeyboard;

            //when login button is clicked, move to the edit profile screen
            //  this.LoginScreenLoginButton.Click += ToEditProfileScreen;
            this.LoginScreenLoginButton.PreviewMouseDown += CheckLoginScreenFieldsOnClicked;

            //otherwise, if back button is clicked, go back to main screen
            this.BackButton.Click += OnBackButtonClicked;

        }

        private void PreviewKeyboard(object sender, MouseButtonEventArgs e)
        {
            this.KeyboardView.Visibility = Visibility.Visible;
            this.HamburgerBack.Visibility = Visibility.Visible;
            this.HamburgerBack.Click += HideKeyboardButton_Click;

        }

        private void CheckLoginScreenFieldsOnClicked(object sender, RoutedEventArgs e)
        {
  
            
            if ((this.UsernameTextbox.Text == string.Empty) && (this.PasswordBox.Password == string.Empty))
            {
                this.LoginUsernameErrorCanvas.Visibility = Visibility.Visible;
                this.LoginPasswordErrorCanvas.Visibility = Visibility.Visible;
                this.LoginFeedbackMessage.Visibility = Visibility.Visible;

            }
            else if (this.UsernameTextbox.Text == string.Empty)
            {
                this.LoginPasswordErrorCanvas.Visibility = Visibility.Hidden;

                this.LoginUsernameErrorCanvas.Visibility = Visibility.Visible;
                this.LoginFeedbackMessage.Visibility = Visibility.Visible;

            }
            else if (this.PasswordBox.Password == string.Empty)
            {
                this.LoginUsernameErrorCanvas.Visibility = Visibility.Hidden;
                this.LoginPasswordErrorCanvas.Visibility = Visibility.Visible;
                this.LoginFeedbackMessage.Visibility = Visibility.Visible;

            }
            else
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

            this.NameTextbox.PreviewMouseDown += PreviewKeyboard;
            this.UsernameTextbox1.PreviewMouseDown += PreviewKeyboard;
            this.InterestTextbox.PreviewMouseDown += PreviewKeyboard;
            this.DescriptionTextbox.PreviewMouseDown += PreviewKeyboard;
            this.BioTextbox.PreviewMouseDown += PreviewKeyboard;
            this.EmailTextbox.PreviewMouseDown += PreviewKeyboard;


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

        private void MainFeedSwipeUp(object sender, RoutedEventArgs e)
        {
            Storyboard story = this.FindResource("MainFeedScrollUp") as Storyboard;
            story.Begin();

            Storyboard appear = this.TryFindResource("profileappear") as Storyboard;
            appear.Begin();

            //hide santiagos
            this.profilepicture.Visibility = Visibility.Hidden;
            this.ProfileImage.Visibility = Visibility.Hidden;
            this.Gallery.Visibility = Visibility.Hidden;


            this.buttonbackmain.Click += Buttonbackmain_Click;

            if(this.mainfeedpicture.Visibility == Visibility.Visible)
            {   
                //show selena
                this.profilepicture3.Visibility = Visibility.Visible;
                this.profilepicture1.Visibility = Visibility.Hidden;
                this.Gallery2.Visibility = Visibility.Visible;
                this.Gallery3.Visibility = Visibility.Hidden;
            }
            if(this.mainfeedpicture2.Visibility == Visibility.Visible)
            {
                //show other
                this.profilepicture1.Visibility = Visibility.Visible;
                this.profilepicture3.Visibility = Visibility.Hidden;
                this.Gallery3.Visibility = Visibility.Visible;
                this.Gallery2.Visibility = Visibility.Hidden;

            }

            this.editprofilebutton.Visibility = Visibility.Hidden;
            this.questionnairebutton.Visibility = Visibility.Hidden;
            this.buttonbackmain.Visibility = Visibility.Visible;
            this.BottomMenu.Visibility = Visibility.Hidden;
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.ProfileScreenCanvas.Visibility = Visibility.Visible;

        }

        private void Buttonbackmain_Click(object sender, RoutedEventArgs e)
        {
          //  Storyboard disappear = this.TryFindResource("profiledisappear") as Storyboard;
         //   disappear.Begin();

            Storyboard appear = this.TryFindResource("ResetMainFeed") as Storyboard;
            appear.Begin();
            HideAll();

            this.editprofilebutton.Visibility = Visibility.Visible;
            this.questionnairebutton.Visibility = Visibility.Visible;
            this.MainFeedCanvas.Visibility = Visibility.Visible;
            this.BottomMenu.Visibility = Visibility.Visible;

        }

        private void MainFeedScrollUp_Completed(object sender, EventArgs e)
        {
            this.MainFeedCanvas.Visibility = Visibility.Hidden;
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
            this.BackButton1_Copy1.Click += ShowOwnProfile; ;

            this.Quiz1Button.Click += QuizbuttonOnClick;
            this.Quiz2Button.Click += QuizbuttonOnClick;
            this.Quiz3Button.Click += QuizbuttonOnClick;
            this.Quiz4Button.Click += QuizbuttonOnClick;
        }

        private void ShowOwnProfile(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.BottomMenu.Visibility = Visibility.Visible;
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.ProfileScreenCanvas.Visibility = Visibility.Visible;
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
            this.buttonbackmain.Visibility = Visibility.Hidden;
            this.ProfileImage.Visibility = Visibility.Hidden;
            this.profilepicture3.Visibility = Visibility.Hidden;
            this.profilepicture.Visibility = Visibility.Visible;
            this.profilepicture1.Visibility = Visibility.Hidden;

            this.Gallery.Visibility = Visibility.Visible;
            this.Gallery2.Visibility = Visibility.Hidden;
            this.Gallery3.Visibility = Visibility.Hidden;

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
            this.profilepicture3.Visibility = Visibility.Hidden;
            this.MatchInitScreen.Visibility = Visibility.Visible;
            this.BottomMenu.Visibility = Visibility.Visible;

            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
        }

        private void MainFeedTextbox_Click(object sender, RoutedEventArgs e)
        {

            HideAll();
            //    this.mainfeedpicture.Visibility = Visibility.Hidden;
            this.mainfeedpicture2.Visibility = Visibility.Visible;
            this.MainFeedCanvas.Visibility = Visibility.Visible;
            //this.MainFeedCanvas.Margin = new Thickness { Top = 0 };

            this.SignifierUp.Click += MainFeedSwipeUp;

            Storyboard animation = this.FindResource("tutorialanimation") as Storyboard;
            animation.Begin();


            this.BottomMenu.Visibility = Visibility.Visible;
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.SignifierDown.Click += SignifierDown_Click;
            this.SwipeUpButton.Click += MainFeedSwipeUp;
           // this.SwipeLeft.Click += SwipeLeft_Click;
            this.SwipeRight.Click += ViewMatchesTextbox_Click;

            if(this.mainfeedpicture2.Visibility == Visibility.Visible)
            {
                this.SwipeLeft.Click += SwipeLeft_Click; 
            }
            else
            {
                this.SwipeLeft.Click += SwipeLeft_Click1;
            }
        }

        private void SwipeLeft_Click1(object sender, RoutedEventArgs e)
        {
            Storyboard sb1 = this.FindResource("SwipeLeft") as Storyboard;
            sb1.Begin();
            this.mainfeedpicture.Visibility = Visibility.Hidden;
            this.mainfeedpicture2.Visibility = Visibility.Visible;
        }

        private void SwipeLeft_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb1 = this.FindResource("SwipeLeft") as Storyboard;
            sb1.Begin();

            this.mainfeedpicture2.Visibility = Visibility.Hidden;
            this.mainfeedpicture.Visibility = Visibility.Visible;
        }

        private void InterestedClicked(object sender, RoutedEventArgs e)
        {
            HideAll();

            this.MatchInitScreen.Visibility = Visibility.Visible;
            this.BottomMenu.Visibility = Visibility.Visible;
        }

      /*  private void SwipeLeft_Click(object sender, RoutedEventArgs e)
        {

            if (this.mainfeedpicture.Visibility == Visibility.Visible)
            {
                this.mainfeedpicture.Visibility = Visibility.Hidden;
                this.mainfeedpicture2.Visibility = Visibility.Visible;

            }
            else {//(this.mainfeedpicture2.Visibility == Visibility.Visible) { 
                this.mainfeedpicture.Visibility = Visibility.Visible;
                this.mainfeedpicture2.Visibility = Visibility.Hidden;

            }

            Storyboard sb1 = this.FindResource("SwipeLeft") as Storyboard;
            sb1.Begin();

            this.SwipeUpButton.Click += SwipeUpButton_Click;
            
        }*/

        private void SwipeUpButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard story = this.FindResource("MainFeedScrollUp") as Storyboard;
            story.Begin();

            Storyboard appear = this.TryFindResource("profileappear") as Storyboard;
            appear.Begin();

            this.buttonbackmain.Click += Buttonbackmain_Click;

            this.editprofilebutton.Visibility = Visibility.Hidden;
            this.questionnairebutton.Visibility = Visibility.Hidden;
            this.buttonbackmain.Visibility = Visibility.Visible;
            this.BottomMenu.Visibility = Visibility.Hidden;
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
            this.ProfileScreenCanvas.Visibility = Visibility.Visible;

            if(this.mainfeedpicture.Visibility == Visibility.Visible)
            {
                this.profilepicture3.Visibility = Visibility.Visible;
                this.profilepicture1.Visibility = Visibility.Hidden;
                this.Gallery3.Visibility = Visibility.Hidden;
                this.Gallery2.Visibility = Visibility.Visible;
            }
        }

        private void SignifierDown_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            Storyboard flash = this.TryFindResource("Flash") as Storyboard;
            flash.Begin();
            this.BottomMenu.Visibility = Visibility.Visible;
            this.MainFeedCanvas.Visibility = Visibility.Visible;
            this.UndoFeedback.Visibility = Visibility.Visible;
            //this.HamburgerMenuButton.Visibility = Visibility.Visible;
            //this.UndoButton_Copy.Click += MainFeedTextbox_Click;
            this.UndoButton_Copy.Click += UndoButton_Copy_Click;
            this.Continue_Button.Click += Continue_Button_Click;
           

            if (this.mainfeedpicture.Visibility == Visibility.Visible)
            {
                this.Continue_Button.Click += Continue_Button_Click1;

            }
            if (this.mainfeedpicture2.Visibility == Visibility.Visible)
            {
                this.Continue_Button.Click += Continue_Button_Click;

            }

        }

        private void UndoButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            this.BottomMenu.Visibility = Visibility.Visible;
            this.MainFeedCanvas.Visibility = Visibility.Visible;
        }

        private void Continue_Button_Click1(object sender, RoutedEventArgs e)
        {
            this.mainfeedpicture.Visibility = Visibility.Hidden;


            Storyboard disappear = this.FindResource("MainFeedScrollDown") as Storyboard;
            disappear.Begin();
            this.UndoFeedback.Visibility = Visibility.Hidden;
            this.profilepicture1.Visibility = Visibility.Visible;
            this.profilepicture3.Visibility = Visibility.Hidden;
            this.mainfeedpicture2.Visibility = Visibility.Visible;


        }

        private void Continue_Button_Click(object sender, RoutedEventArgs e)
        {
            this.mainfeedpicture2.Visibility = Visibility.Hidden;

            Storyboard disappear = this.FindResource("MainFeedScrollDown") as Storyboard;
            disappear.Begin();
            this.UndoFeedback.Visibility = Visibility.Hidden;

            this.mainfeedpicture.Visibility = Visibility.Visible;


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
            this.ViewMatch.Visibility = Visibility.Hidden;
            this.MessagingScreen.Visibility = Visibility.Hidden;
            this.HamburgerMenu.Visibility = Visibility.Hidden;
            this.HamburgerMenuButton.Visibility = Visibility.Hidden;
            this.MatchInitScreen.Visibility = Visibility.Hidden;
            this.SwipeDown.Visibility = Visibility.Hidden;
            this.MeetingScreen.Visibility = Visibility.Hidden;
            this.MainFeedCanvas.Visibility = Visibility.Hidden;
            this.HamburgerBack.Visibility = Visibility.Hidden;
            this.BottomMenu.Visibility = Visibility.Hidden;
            this.KeyboardView.Visibility = Visibility.Hidden;
            this.UndoFeedback.Visibility = Visibility.Hidden;

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
