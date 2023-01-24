using System;
using UnityEngine;

public class Button : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    BoxCollider2D boxCollider;
    float lightOnTime; //время подсвета кнопки и блокировки повторного нажатия
    Color lightOnColor, lightOffColor;

    public event EventHandler OnButtonPush;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();


        lightOnTime = Manager.activeTime;

        lightOnColor = spriteRenderer.color;
        lightOffColor = spriteRenderer.color;
        lightOffColor.a = 0.6f;
        spriteRenderer.color = lightOffColor;
        
    }

    void OnMouseDown()
    {
        //если не нажимали последние activeTime секунд
        PushTheButton();
        OnButtonPush?.Invoke(this,EventArgs.Empty);
    }

    public void PushTheButton()
    {
        LightOn();
        //вызвать LightOff через activeTime секунд
        Invoke("LightOff", lightOnTime);
    }

    void LightOn()
    {
        spriteRenderer.color = lightOnColor;
        audioSource.Play();
        SwitchCollider(false);
    }

    void LightOff()
    {
        spriteRenderer.color = lightOffColor;
        SwitchCollider(true);
    }

    public void SwitchCollider(bool b)
    {
        boxCollider.enabled = b;
    }

}
