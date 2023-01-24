using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Melody
{
    int[] melody = new int[200];
    int numberOfButtons;
    int noteToAdd = 0;
    int noteToPlay = 0;
    int noteToCheck = 0;

    public Melody(int n)
    {
        numberOfButtons = n;
    }

    public void AddNote()
    {
        melody[noteToAdd] = UnityEngine.Random.Range(0, numberOfButtons + 1);
        noteToAdd++;
    }

    public int NextNote()
    {
        int n = -1;
        if (noteToPlay < noteToAdd)
        {
            n = melody[noteToPlay];
            noteToPlay++;
        }
        else
        {
            noteToPlay = 0;
        }

        return n;
    }

    public int CheckNote(int i)
    {
        if (melody[noteToCheck] == i)
        {
            if (noteToCheck + 1 == noteToAdd)
            {
                restart();
                return 0;
            } else
            {
                noteToCheck++;
                return 1;
            }
            
        }
        else
        {
            restart();
            return -1; 
        }
    }
    
    void restart()
    {
        noteToAdd = 0;
        noteToPlay = 0;
        noteToCheck = 0;
    }


}