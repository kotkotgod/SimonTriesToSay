using System;
using UnityEngine;

public class Button : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    BoxCollider2D boxCollider;

    Color lightOnColor, lightOffColor;
    
    public float ActiveTime { get; set; } //button lights up for that long, getting from Manager


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


    //
    void OnMouseDown()
    {
        PushTheButton();
        OnButtonPush?.Invoke(this, EventArgs.Empty);
    }

    public void PushTheButton()
    {
        LightOn();
        //call LightOff() in ActiveTime seconds
        Invoke("LightOff", ActiveTime);
    }

    void LightOn()
    {
        spriteRenderer.color = lightOnColor;
        audioSource.Play();
    }

    void LightOff()
    {
        spriteRenderer.color = lightOffColor;
    }

    //using this to block buttons in manager when current sequence is playing 
    public void SwitchCollider(bool b)
    {
        boxCollider.enabled = b;
    }

}
