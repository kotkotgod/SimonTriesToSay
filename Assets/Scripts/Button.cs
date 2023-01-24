using System;
using UnityEngine;

public class Button : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    BoxCollider2D boxCollider;

    Color lightOnColor, lightOffColor;
    float activeTime; //button lights up for that long, getting from Manager
    public float ActiveTime
    {
        get 
        {
            return activeTime;
        }
        set
        {
            activeTime = value;
        }
    }  


    public event EventHandler OnButtonPush;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();


        lightOnColor = spriteRenderer.color;
        lightOffColor = spriteRenderer.color;
        lightOffColor.a = 0.6f;
        spriteRenderer.color = lightOffColor;

    }

    void OnMouseDown()
    {
        PushTheButton();
        OnButtonPush?.Invoke(this, EventArgs.Empty);
    }

    public void PushTheButton()
    {
        LightOn();
        //call LightOff() in activeTime seconds
        Invoke("LightOff", activeTime);
    }

    void LightOn()
    {
        spriteRenderer.color = lightOnColor;
        audioSource.Play();
        //blocking player input for a moment
        SwitchCollider(false);
    }

    void LightOff()
    {
        spriteRenderer.color = lightOffColor;
        SwitchCollider(true);
    }

    //collider works with OnMouseButton so turning it on/off helps blocking extra clicks
    public void SwitchCollider(bool b)
    {
        boxCollider.enabled = b;
    }

}
