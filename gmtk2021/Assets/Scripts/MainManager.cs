using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    // Variables
    public Animator Transition;
    private float TransitionTime = 0.66f;
    private int ClickCount = 0;
    public GameObject ScoreWindows;

    // Function called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ClickCount++;
        }
    }

    // Function called when Play button is pressed
    public void PlayNextLevel()
    {
        StartCoroutine(LoadLevel());
    }
    
    // Function to display score popup
    public void DisplayScore()
    {
        ScoreWindows.SetActive(true);
        ScoreWindows.GetComponent<Score>().DisplayScore(ClickCount);
    }

    // Function called to change level
    IEnumerator LoadLevel()
    {
        Transition.SetTrigger("StartFade"); // Activate the transition
        yield return new WaitForSeconds(TransitionTime); // Wait a few time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Change scene to the next level
        ClickCount = 0;
    }
}
