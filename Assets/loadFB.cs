//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Facebook.Unity;
//using UnityEngine.UI;
//using Photon.Pun;
//using Photon.Realtime;


//public class loadFB : MonoBehaviourPunCallbacks
//{
//    public Text FB_userName;
//    public Image FB_userDp;
//    public GameObject canvasMenu;
//    public GameObject canvasLogin;

//    // Start is called before the first frame update
//    //void Start()
//    //{

//    //}

//    //// Update is called once per frame
//    //void Update()
//    //{

//    //}

//    private void Awake()
//    {
//        if (!FB.IsInitialized)
//        {
//            FB.Init(InitCallback);
//        }
//        else
//        {
//            FBLogin();
//        }
//    }
//    void InitCallback()
//    {
//        if (FB.IsInitialized)
//        {
//            FBLogin();
//        }
//        else
//        {
//            Debug.Log("Failed to initialize the FB SDK");
//        }
//    }

//    void DealWithFbMenus(bool isLoggedIn)
//    {
//        if (isLoggedIn)
//        {
//            canvasMenu.SetActive(true);
//            canvasLogin.SetActive(false);
//            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
//            FB.API("/me/picture?type=square&height=1092&width=1092", HttpMethod.GET, DisplayProfilePic);
//        }
//        else
//        {

//        }
//    }

//    void DisplayUsername(IResult result)
//    {
//        if (result.Error == null)
//        {
//            string name = "" + result.ResultDictionary["first_name"];
//            FB_userName.text = name;
//            Debug.Log("" + name);
//        }
//        else
//        {
//            Debug.Log(result.Error);
//        }
//    }

//    //Display Profile Pic
//    void DisplayProfilePic(IGraphResult result)
//    {
//        if (result.Texture != null)
//        {
//            Debug.Log(" Profile Pic");
//            FB_userDp.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 600, 600), new Vector2());
//        }
//        else
//        {
//            Debug.Log(result.Error);
//        }
//    }

//    //FB Login
//    public void FBLogin()
//    {
//        if (FB.IsLoggedIn)
//        {
//            OnFacebookLoggedIn();
//        }
//        else
//        {
//            List<string> permissions = new List<string>();
//            permissions.Add("public_profile");
//            FB.LogInWithReadPermissions(permissions, AuthCallBack);
//        }
//    }

//    void AuthCallBack(IResult result)
//    {
//        if (result.Error != null)
//        {
//            Debug.Log(result.Error);
//        }
//        else
//        {
//            if (FB.IsLoggedIn)
//            {
//                OnFacebookLoggedIn();
//                Debug.Log("facebook is login");
//            }
//            else
//            {
//                Debug.Log("facebook is not login {0}");
//            }
//            DealWithFbMenus(FB.IsLoggedIn);
//        }
//    }

//    private void OnFacebookLoggedIn()
//    {
//        // AccessToken class will have session details
//        string aToken = AccessToken.CurrentAccessToken.TokenString;
//        string facebookId = AccessToken.CurrentAccessToken.UserId;
//        PhotonNetwork.AuthValues = new AuthenticationValues();
//        PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Facebook;
//        PhotonNetwork.AuthValues.UserId = facebookId; // alternatively set by server
//        PhotonNetwork.AuthValues.AddAuthParameter("token", aToken);
//        PhotonNetwork.ConnectUsingSettings();
//    }

//    public override void OnConnectedToMaster()
//    {
//        Debug.Log("Successfully connected to Photon!");
//    }

//    // something went wrong
//    public override void OnCustomAuthenticationFailed(string debugMessage)
//    {
//        Debug.LogErrorFormat("Error authenticating to Photon using Facebook: {0}", debugMessage);
//    }

//    //FB LogOut 
//    public void CallLogout()
//    {
//        StartCoroutine("FBLogout");
//    }

//    public void QUIT()
//    {
//        Application.Quit();
//    }

//    IEnumerator FBLogout()
//    {
//        FB.LogOut();
//        while (FB.IsLoggedIn)
//        {
//            print(" Logging Out");
//            yield return null;
//        }
//        canvasMenu.SetActive(false);
//        canvasLogin.SetActive(true);
//        print(" Logout Successful");
//        FB_userDp.sprite = null;
//        FB_userName.text = "" ;
//    }
//}
