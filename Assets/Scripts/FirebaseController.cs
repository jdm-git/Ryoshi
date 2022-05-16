using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseController : MonoBehaviour
{
    public GameObject loginPanel, signUpPanel, profilePanel, forgetPasswdPanel, notificationPanel;
    public InputField loginMail, loginPasswd, signUpMail, signUpPasswd, signUpCPasswd, signUpUsername, forgetPasswdMail;
    public Text notification_Title_Text, notification_Message_Text, profileUserMail_Text, profileUsername_Text;
    public Toggle rememberMe;

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
    }
    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(signUpMail.text) && string.IsNullOrEmpty(signUpPasswd.text) && string.IsNullOrEmpty(signUpCPasswd.text)) 
        {
            showNotificationMessage("Error", "Fields empty! \b Please enter all informations.");
            return;
        }
    }

    public void ForgetPasswd()
    {
        if (string.IsNullOrEmpty(forgetPasswdMail.text)) 
        {
            showNotificationMessage("Error", "Forget Mail Empty");
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
        profilePanel.SetActive(false);
        profileUserMail_Text.text = "";
        profileUsername_Text.text = "";
        OpenLogPanel();
    }
}