using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnterLostAreaStep : QuestStep
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FinishQuestStep();
        }
    }
}
