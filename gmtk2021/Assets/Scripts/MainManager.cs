using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    // Variables
    public Animator Transition;
    private float TransitionTime = 0.66f;

    // Function called when Play button is pressed
    public void PlayNextLevel()
    {
        StartCoroutine(LoadLevel());
    }    

    // Function called to change level
    IEnumerator LoadLevel()
    {
        Transition.SetTrigger("StartFade"); // Activate the transition
        yield return new WaitForSeconds(TransitionTime); // Wait a few time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Change scene to the next level
    }
}
