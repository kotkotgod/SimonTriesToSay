using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Manager : MonoBehaviour
{
    public static float activeTime = 0.35f;
    
    Button[] buttons; //цветные кнопочки для нажатия

    Dictionary<string, int> buttonDict = new Dictionary<string, int>(); //словарь для записи нажатий(событий) в мелодию
    Melody tune = new Melody(3);
    int i = 0;

    bool nextRound = true;


    private void Start()
    {

        buttons = this.transform.GetComponentsInChildren<Button>();

        foreach(Button button in buttons)
        {
            button.OnButtonPush += ListenerOnButtonPush; //подписываемся на события нажатия
            buttonDict.Add(button.ToString(), i++);
        }
        
    }
       

    void Update()
    {
        if (nextRound)
        {

            nextRound = false;
            tune.AddNote();
            new WaitForSeconds(1);
            StartCoroutine(PlayMelody(tune));
        }
    }


    void ListenerOnButtonPush(object sender, EventArgs e)
    {
        int res = tune.CheckNote(buttonDict[sender.ToString()]);
        if ( res == -1)
        {
            nextRound = true;
            Debug.Log("Поражение");
        } else if( res == 0)
        {
            Debug.Log("ПОБИДА");
            nextRound = true;
        }
        //Debug.Log("Вы нажали по кнопке номер " + buttonDict[sender.ToString()]);
    }

    IEnumerator PlayMelody(Melody m)
    {
        foreach(Button button in buttons)
        {
            button.SwitchCollider(false);
        }

        //m.AddNote();
        int n = m.NextNote();
        while(n >= 0)
        {
            buttons[n].PushTheButton();
            yield return new WaitForSeconds(activeTime + 0.1f);
            n = m.NextNote();
            
        }

        foreach (Button button in buttons)
        {
            button.SwitchCollider(true);
        }
    }
    
}
