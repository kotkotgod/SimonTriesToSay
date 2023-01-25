using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Manager : MonoBehaviour
{
    public float activeTime = 0.35f; //how long the button lights up when pressed, used in button
    public float pauseTime = 0.1f; //pause between notes during playing sequence
    Button[] buttons; //game buttons
    Dictionary<string, int> buttonDict = new Dictionary<string, int>(); //mapping ints to buttons as "notes"
    List<int> melody;
    List<int> allButtons = new List<int>();
    int numberOfButtons;
    int currentNote;
    int currentStreak;


    private void Start()
    {
       //get all buttons
        buttons = this.transform.GetComponentsInChildren<Button>();
        int i = 0;
        foreach (Button button in buttons)
        {
            //sub to push event
            button.OnButtonPush += ListenerOnButtonPush;
            //map buttons to int, ints are later used to access them through .GetChild
            buttonDict.Add(button.ToString(), i++);
            button.ActiveTime = activeTime;
        }
        numberOfButtons = buttonDict.Count;
        for (int j =0; j < numberOfButtons;j++)
        {
            allButtons.Add(j);
        }
    }

    public void StartGame()
    {
        Reset();
        StartRound();
    }

    //round starts with adding an extra note and playing the whole sequence
    void StartRound()
    {
        melody.Add(UnityEngine.Random.Range(0, numberOfButtons));
        StartCoroutine(PlayMelody(melody, activeTime + pauseTime, 1f));
    }

    
    void Reset()
    {
        melody = new List<int>();
        currentNote = 0;
        currentStreak = 0;
    }

    bool CheckNote(int n)
    {
        return melody[currentNote] == n;
    }

    //checking mapped ints from buttons with current sequence "note" 
    void ListenerOnButtonPush(object sender, EventArgs e)
    {
        // if wrong note
        if (!CheckNote(buttonDict[sender.ToString()]))
        {
            Mistake();
            Reset();
            StartRound();
        } else if(++currentNote == melody.Count) //if the sequence is done
        {
            currentStreak++;
            currentNote = 0;
            StartRound();
        }
    }
    
    void Mistake()
    {
        StartCoroutine(PlayMelody(allButtons, 0f, 0f));
    }

    IEnumerator PlayMelody(List<int> mel, float timeBetweenNotes, float pause)
    {
        //disabling player input on game buttons while sequence plays
        foreach(Button button in buttons) 
            button.SwitchCollider(false);
        
        //small pause before sequence starts
        yield return new WaitForSeconds(pause);
        
        //activating buttons in the saved sequence with a small pause between them
        foreach (int i in mel)
        {
            buttons[i].PushTheButton();
            yield return new WaitForSeconds(timeBetweenNotes);
        }  
        
        //reactivating game buttons
        foreach (Button button in buttons)
            button.SwitchCollider(true);
    }
    
}
