using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Manager : MonoBehaviour
{
    public float activeTime = 0.35f; //how long the button lights up when pressed, used in button
    
    Button[] buttons; //game buttons

    Dictionary<string, int> buttonDict = new Dictionary<string, int>(); //mapping ints to buttons as "notes"
    
    

    int currentNote;
    int currentStreak;
    List<int> melody;
    int numberOfButtons;
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
        Reset();
        StartRound();
    }


    void StartRound()
    {
        melody.Add(UnityEngine.Random.Range(0, numberOfButtons));
        StartCoroutine(PlayMelody());

    }

        private void Reset()
    {
        melody = new List<int>();
        currentNote = 0;
    }
    bool CheckNote(int n)
    {
        return melody[currentNote] == n;
    }


    void ListenerOnButtonPush(object sender, EventArgs e)
    {
        if (!CheckNote(buttonDict[sender.ToString()]))
        {
            Reset();
            StartRound();
        } else if(++currentNote == melody.Count)
        {
            currentNote = 0;
            StartRound();
            
        }
    }

    IEnumerator PlayMelody()
    {
        foreach(Button button in buttons)
        {
            button.SwitchCollider(false);
        }
       
        yield return new WaitForSeconds(activeTime + 1);
        foreach (int i in melody)
        {
            
            buttons[i].PushTheButton();
            yield return new WaitForSeconds(activeTime + 0.1f);
        }  
            
        

        foreach (Button button in buttons)
        {
            button.SwitchCollider(true);
        }
    }
    
}
