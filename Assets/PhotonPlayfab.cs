using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonPlayfab : MonoBehaviour
{
    public static PhotonPlayfab Instance;
    private string _playFabPlayerIdCache;
    public GameObject ds;
    public GameObject loginScreen;
    public GameObject registerScreen;
    public GameObject mainmenu;
    public static bool logOut = true;
    public static string name;
    public Text showName;
    public GameObject loginF;
    public GameObject registerF;
    //CurrencyEarn ce;

    public InputField username;
    public InputField pass;
    public InputField uLogin;
    public InputField pLogin;
    //Run the entire thing on awake
     void Awake()
    {
        
        showName.text = name;
        //ce = GameObject.Find("Currency").GetComponent<CurrencyEarn>();
        if (!logOut)
        {
            ds.SetActive(false);
            mainmenu.SetActive(true);
            registerScreen.SetActive(false);
            loginScreen.SetActive(false);
        }
        PlayFabSettings.TitleId = "AFD3A";
        Instance = this;
        //AuthenticateWithPlayFab();
    }

    private void AuthenticateWithPlayFab()
    {
        //LogMessage("PlayFab authenticating using Custom ID...");
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        //request.CreateAccount = true;
        //request.CustomId = "CustomID#" + Random.Range(0, 10000);
        PlayFabClientAPI.LoginWithPlayFab(request, RequestPhotonToken, OnPlayFabError);
    }


    private void RequestPhotonToken(LoginResult obj)
    {
        LogMessage("PlayFab authenticated. Requesting photon token...");
        //We can player PlayFabId. This will come in handy during next step
        _playFabPlayerIdCache = obj.PlayFabId;
        GetPhotonAuthenticationTokenRequest request = new GetPhotonAuthenticationTokenRequest();
        request.PhotonApplicationId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime;

        PlayFabClientAPI.GetPhotonAuthenticationToken(request, AuthenticateWithPhoton, OnPlayFabError);
    }

    private void AuthenticateWithPhoton(GetPhotonAuthenticationTokenResult obj)
    {
        LogMessage("Photon token acquired: " + obj.PhotonCustomAuthenticationToken + "  Authentication complete.");

        //We set AuthType to custom, meaning we bring our own, PlayFab authentication procedure.
        var customAuth = new AuthenticationValues { AuthType = CustomAuthenticationType.Custom };
        //We add "username" parameter. Do not let it confuse you: PlayFab is expecting this parameter to contain player PlayFab ID (!) and not username.
        customAuth.AddAuthParameter("username", _playFabPlayerIdCache);    // expected by PlayFab custom auth service

        //We add "token" parameter. PlayFab expects it to contain Photon Authentication Token issues to your during previous step.
        customAuth.AddAuthParameter("token", obj.PhotonCustomAuthenticationToken);

        //We finally tell Photon to use this authentication parameters throughout the entire application.
        PhotonNetwork.AuthValues = customAuth;
    }

    private void OnPlayFabError(PlayFabError obj)
    {
        LogMessage(obj.GenerateErrorReport());
    }


    //PUBLIC
    public void LogMessage(string message)
    {
        Debug.Log("PlayFab + Photon Example: " + message);
    }

    public void LoginBtn()
    {
        ds.SetActive(false);
        loginScreen.SetActive(true);
        logOut = false;
    }


    public void RegisterBtn()
    {
        ds.SetActive(false);
        registerScreen.SetActive(true);
        logOut = false;
    }

    public void confirmLogin()
    {
        var loginRequest = new LoginWithPlayFabRequest { Password = pLogin.text, Username = uLogin.text};
        PlayFabClientAPI.LoginWithPlayFab(loginRequest, loginSuccess, loginFailure);
        name = uLogin.text;
    }

    public void confirmRegister()
    {
        var registerRequest = new RegisterPlayFabUserRequest { Password = pass.text, Username = username.text, RequireBothUsernameAndEmail = false };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, registerSuccess, registerFailure);

    }

    public void loginSuccess(LoginResult result)
    {
        AuthenticateWithPlayFab();
        showName.text = "Welcome Back \n" + name;
        Debug.Log("Login Successfully");
        mainmenu.SetActive(true);
        loginScreen.SetActive(false);
        PhotonNetwork.GameVersion = "1";
        Debug.Log("Connecting");
        loginF.SetActive(false);
        CurrencyEarn.instanceC.showOnce = true;
        //PhotonNetwork.ConnectUsingSettings();
    }

    public void loginFailure(PlayFabError error)
    {
        name = "";
        loginF.SetActive(true);
        Debug.Log("Login Fail");
    }


    public void registerSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Register Successfully");
        registerF.SetActive(false);
        ds.SetActive(true);
        registerScreen.SetActive(false);
    }

    public void registerFailure(PlayFabError error)
    {
        registerF.SetActive(true);
        Debug.Log("Register Failed");
    }

    public void backDS()
    {
        ds.SetActive(true);
        registerScreen.SetActive(false);
        loginScreen.SetActive(false);
    }

    public void logOutBtn()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        name = "";
        showName.text = name;
        logOut = true;
        ds.SetActive(true);
        mainmenu.SetActive(false);
    }

    public void EXIT()
    {
        Application.Quit();
    }
}

