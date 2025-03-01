using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GoOutsideStep : QuestStep
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Reached outside");
            FinishQuestStep();
        }
    }
}
