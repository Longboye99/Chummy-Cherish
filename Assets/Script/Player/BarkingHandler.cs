using System.Collections.Generic;
using UnityEngine;

public class BarkingHandler : MonoBehaviour
{
    public List<Bird> inRange = new List<Bird>();

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onBarking += Barking;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onBarking -= Barking;

    }

    private void Barking()
    {
        foreach (Bird bird in inRange)
        {
            bird.Flyaway();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bird bird = collision.GetComponent<Bird>();
        if (bird != null) { inRange.Add(bird); }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Bird bird = collision.GetComponent<Bird>();
        if (bird != null) { inRange.Remove(bird); }
    }
}
