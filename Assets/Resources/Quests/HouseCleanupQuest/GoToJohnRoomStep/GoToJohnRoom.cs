using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GoToJohnRoom : QuestStep
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Reached John room");
            FinishQuestStep();
        }
    }
        
}
