using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Variables
    public Text ClicCountText;

    // Function to display score to the player
    public void DisplayScore(int ClickCount)
    {
        ClicCountText.text = ClickCount.ToString();
    }
}
