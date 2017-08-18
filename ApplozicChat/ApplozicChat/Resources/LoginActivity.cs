
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Applozic;
using Com.Applozic.Mobicomkit.Api.Account.Register;
using Com.Applozic.Mobicomkit.Api.Account.User;
using Com.Applozic.Mobicommons.People.Channel;

namespace ApplozicChat
{
    [Activity(Label = "Login", MainLauncher = true, Icon = "@mipmap/icon")]
    public class LoginActivity : Activity
    {

        private UserLoginListener loginListener;
        //  AddMemberListner addMemberListner;            // Enable this for addmemberlistner
        //  ApplozicContactService contactService;       // Enable this for ApplozicContactService class
        //  String UserId;                              // Enable this for loggedInUserId
        //  String contactGroupId = "GroupName";       // Enable this and pass your GroupID here  

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            loginListener = new UserLoginListener();
            loginListener.OnRegistrationSucessHandler += OnRegistrationSucessHandler;
            loginListener.OnRegistrationFailedHandler += OnRegistrationFailedHandler;
            // AddMemberHandler();                                                       // Enable this method for its handler when adding members to contact group 

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.login_activity_layout);

            // Get our button from the layout resource,
            // and attach an event to it
            EditText userName = FindViewById<EditText>(Resource.Id.username_input);
            EditText password = FindViewById<EditText>(Resource.Id.password_input);
            Button signIn = FindViewById<Button>(Resource.Id.sign_in_btn);
            ApplozicChatManager chatManager = new ApplozicChatManager(this);


            signIn.Click += delegate
            {
                chatManager.RegisterUser(userName.Text, userName.Text, password.Text, loginListener);
            };

            if (chatManager.ISUserLoggedIn())
            {
                System.Console.WriteLine("Already Registred ::");
                Intent myIntent = new Intent(this, typeof(MainActivity));
                this.StartActivity(myIntent);
                this.Finish();
            }


        }


        void OnRegistrationSucessHandler(RegistrationResponse res, Context context)
        {
            System.Console.WriteLine("Successfully got callback in LoginActivity :" + res.Message);

            // Enable  below commented code for adding member to contact group

    /*      var applozicPref = MobiComUserPreference.GetInstance(context);
            UserId = applozicPref.UserId;
            contactService = new ApplozicContactService(this);        
            applozicPref.ContactsGroupId = contactGroupId;
            contactService.AddMemberToContactGroup(this, contactGroupId, (String)(Channel.GroupType.ContactGroup.Value), UserId, addMemberListner);   */
            
            Intent myIntent = new Intent(this, typeof(MainActivity));
            this.StartActivity(myIntent);
            this.Finish();
        }

        void OnRegistrationFailedHandler(RegistrationResponse res, Java.Lang.Exception exception)
        {
            System.Console.WriteLine("Error while doing registrations:" + exception.Message);

            Toast.MakeText(ApplicationContext, "Login Failed : " + exception.Message, ToastLength.Long).Show();
        }


        void OnAddMemberSucessHandler(bool res, Context context)
        {

            System.Console.WriteLine("Successfully got callback for AddMember in LoginActivity :" + res);
        }

        void OnAddMemberFailedHandler(bool res, Java.Lang.Exception exception, Context context)
        {

            System.Console.WriteLine("Error while Adding Member:" + exception.Message);
        }

        // Enable below commented code for adding contact group handler

        /*    public void AddMemberHandler()
                {
                    addMemberListner = new AddMemberListner();
                    addMemberListner.OnAddMemberSucessHandler += OnAddMemberSucessHandler;
                    addMemberListner.OnAddMemberFailedHandler += OnAddMemberFailedHandler;
                } */
    }

}
