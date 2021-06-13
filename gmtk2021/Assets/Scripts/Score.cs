using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Variables
    public Text ClicCountText;

    public MainManager MainManager { get { return FindObjectOfType <MainManager>();} }
    public Player Player { get { return FindObjectOfType<Player>(); } }

    // Function to display score to the player
    public void DisplayScore(int ClickCount)
    {
        Player.LevelFinished = true;
        ClicCountText.text = ClickCount.ToString();
    }

    public void NextLevel()
    {
        MainManager.PlayNextLevel();
    }
}
