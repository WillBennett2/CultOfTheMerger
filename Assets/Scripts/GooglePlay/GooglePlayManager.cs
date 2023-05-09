using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class GooglePlayManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(SignInResults);
    }

    void SignInResults(SignInStatus status)
    {
        if(status == SignInStatus.Success)
        {
            Debug.Log("Sign In Success");
        }
        else
        {
            Debug.Log("Sing In Failed");
        }
    }
    public void AchievementsTest()
    {
        Social.ReportProgress(GPGSIds.achievement_tester,100f,(bool callback) => { });
    }
}
