using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    // Variables
    public Animator Transition;
    private float TransitionTime = 0.66f;
    public int ClickCount = 0;
    public GameObject ScoreWindows;

    public UnityEngine.UI.Image FadeImage;
    private bool fadeDisabled = false;

    // Function called once per frame
    void Update()
    {
        if(FadeImage.color.a == 0 && !fadeDisabled)
        {
            FadeImage.gameObject.SetActive(false);
            fadeDisabled = true;
        }
    }

    // Function called when Play button is pressed
    public void PlayNextLevel()
    {
        FadeImage.gameObject.SetActive(true);
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Restart()
    {
        FadeImage.gameObject.SetActive(true);
        StartCoroutine(RestartLevel());
    }

    public void MainMenu()
    {
        FadeImage.gameObject.SetActive(true);
        StartCoroutine(RestartGame());
    }
    
    // Function to display score popup
    public void DisplayScore()
    {
        Instantiate(ScoreWindows);
        ScoreWindows.GetComponent<Score>().DisplayScore(ClickCount);
    }

    // Function called to change level
    IEnumerator LoadLevel(int buildIndex)
    {
        Transition.SetTrigger("StartFade"); // Activate the transition
        yield return new WaitForSeconds(TransitionTime); // Wait a few time
        SceneManager.LoadScene(buildIndex); // Change scene to the next level
        ClickCount = 0;
    }

    IEnumerator RestartLevel()
    {
        Transition.SetTrigger("StartFade"); // Activate the transition
        yield return new WaitForSeconds(TransitionTime); // Wait a few time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Change scene to the next level
        ClickCount = 0;
    }

    IEnumerator RestartGame()
    {
        Transition.SetTrigger("StartFade"); // Activate the transition
        yield return new WaitForSeconds(TransitionTime); // Wait a few time
        SceneManager.LoadScene(0); // Change scene to the next level
        ClickCount = 0;
    }
}
