using System;
using System.Threading.Tasks; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;


public class FirebaseController : MonoBehaviour
{
    public GameObject loginPanel, signUpPanel, profilePanel, forgetPasswdPanel, notificationPanel;
    public InputField loginMail, loginPasswd, signUpMail, signUpPasswd, signUpCPasswd, signUpUsername, forgetPasswdMail;
    public Text notification_Title_Text, notification_Message_Text, profileUserMail_Text, profileUsername_Text;
    public Toggle rememberMe;

    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    bool isSignIn = false;

    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                InitializeFirebase();

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            } else {
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }
    public void OpenLogPanel()
    {
        loginPanel.SetActive(true);
        signUpPanel.SetActive(false);
        profilePanel.SetActive(false);
        forgetPasswdPanel.SetActive(false);
    }
    
    public void OpenSignUpPanel()
    {
        loginPanel.SetActive(false);
        signUpPanel.SetActive(true);
        profilePanel.SetActive(false);
        forgetPasswdPanel.SetActive(false);
    }
    public void OpenProfilePanel()
    {
        loginPanel.SetActive(false);
        signUpPanel.SetActive(false);
        profilePanel.SetActive(true);
        forgetPasswdPanel.SetActive(false);
    }

    public void OpensForgetPasswdPanel()
    {
        loginPanel.SetActive(false);
        signUpPanel.SetActive(false);
        profilePanel.SetActive(false);
        forgetPasswdPanel.SetActive(true);
    }

    public void LoginUser()
    {
        if(string.IsNullOrEmpty(loginMail.text) && string.IsNullOrEmpty(loginPasswd.text))
        {
            showNotificationMessage("Error", "Fields empty! \b Please enter all informations.");
            return;
        }

        SignInUser(loginMail.text, loginPasswd.text);
    }
    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(signUpMail.text) && string.IsNullOrEmpty(signUpPasswd.text) && string.IsNullOrEmpty(signUpCPasswd.text)) 
        {
            showNotificationMessage("Error", "Fields empty! \b Please enter all informations.");
            return;
        }

        CreateUser(signUpMail.text, signUpPasswd.text, signUpUsername.text);
    }

    public void ForgetPasswd()
    {
        if (string.IsNullOrEmpty(forgetPasswdMail.text)) 
        {
            showNotificationMessage("Error", "Empty field, please input your mail.");
            return;
        }
    }

    private void showNotificationMessage(string title, string message)
    {
        notification_Title_Text.text = "" + title;
        notification_Message_Text.text = "" + message;

        notificationPanel.SetActive(true);
    }

    public void CloseNotif_Panel()
    {
        notification_Message_Text.text = "";
        notification_Title_Text.text = "";

        notificationPanel.SetActive(false);
    }

    public void LogOut()
    {
        profileUserMail_Text.text = "";
        profileUsername_Text.text = "";
        OpenLogPanel();
    }

    public void CreateUser(string email, string password, string Username) 
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });

        UpadteUserProfile(Username);
    }

    public void SignInUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled) {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted) {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            profileUsername_Text.text = "" + newUser.DisplayName;
            profileUserMail_Text.text = "" + newUser.Email;
            OpenProfilePanel();
        }); 
    }

    void InitializeFirebase() {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs) {
        if (auth.CurrentUser != user) {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null) 
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn) 
            {
                Debug.Log("Signed in " + user.UserId);
                // displayName = user.DisplayName ?? "";
                // emailAddress = user.Email ?? "";
                // photoUrl = user.PhotoUrl ?? "";
                isSignIn = true;
            }
        }
    }

    void OnDestroy() 
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    void UpadteUserProfile(string UserName)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null) {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile {
                DisplayName = UserName,
                PhotoUrl = new System.Uri("https://via.placeholder.com/150"),
            };
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled) {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted) {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }
                Debug.Log("User profile updated successfully.");

                showNotificationMessage("Alert", "Successfully created!");
            });
        }
    }

    bool isSigned = false;
    void Update()
    {
        if(isSignIn)
        {
            if(isSigned)
            {
                isSigned = true;
                profileUsername_Text.text = "" + user.DisplayName;
                profileUserMail_Text.text = "" + user.Email;
                OpenProfilePanel();
            }
        }
    }
}