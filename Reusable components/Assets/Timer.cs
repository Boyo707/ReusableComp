using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float seconds;
    [SerializeField] private bool startOnAwake; 
    [SerializeField] private bool canLoop;
    [SerializeField] private UnityEvent onTimerEnd;

    private float currentTime;
    private bool canPlayTimer;

    private bool isOn;

    private void Awake()
    {
        if (startOnAwake)
        {
            canPlayTimer = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(seconds == 0)
        {
            Debug.LogWarning("TIMER HAS NO TIME");
        }
        else
        {
            currentTime = seconds;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0 && canPlayTimer)
        {
            currentTime -= Time.deltaTime;
        }
        else if(currentTime <= 0)
        {
            onTimerEnd.Invoke();
            if (canLoop)
            {
                currentTime = seconds;
            }
            else
            {
                canPlayTimer = false;
            }
        }
    }


    public void StartTimer()
    {
        canPlayTimer = true;
    }

    public void StopTimer()
    {
        canPlayTimer = false;
        GetComponent<Image>().enabled = false;
    }

    public void GameObjectOnOff()
    {
        isOn = !isOn;

        GetComponent<Image>().enabled = isOn;
    }
}
