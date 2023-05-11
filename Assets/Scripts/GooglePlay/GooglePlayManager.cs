using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class GooglePlayManager : MonoBehaviour
{
    public bool m_connectedToGooglePlay;
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
            m_connectedToGooglePlay = true;
        }
        else
        {
            Debug.Log("Sing In Failed");
            m_connectedToGooglePlay = false;
        }
    }
    public void AchievementsTest()
    {
        Social.ReportProgress(GPGSIds.achievement_tester,100f,(bool callback) => { });
    }
    public void ShowAchievements()
    {
        Social.ShowAchievementsUI();
    }    

    public void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }
    public void UpdateLeaderBoardCultScore(long score)
    {
        if (m_connectedToGooglePlay)
            Social.ReportScore(score, GPGSIds.leaderboard_cultofthemerger_cultvalue, ValidateLeaderboard);        
    }
    private void ValidateLeaderboard(bool success)
    {
        if(success)
        {
            Debug.Log("leaderboard Updated");
        }
    }
}
