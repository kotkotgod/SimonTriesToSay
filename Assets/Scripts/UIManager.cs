using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int currentStreak = 0;
    int maxStreak = 0;

    public Text maxText;
    public Text currText;
    public GameObject startButton;

    public void Current(int i)
    {
        currentStreak = i;
        UpdateLabels();
    }


    void UpdateLabels()
    {
        if (currentStreak > maxStreak)
        {
            maxStreak = currentStreak;
            maxText.text = maxStreak.ToString();
        }
        currText.text = currentStreak.ToString();

    }

    public void hideStartButton()
    {
        startButton.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif

    }



}
