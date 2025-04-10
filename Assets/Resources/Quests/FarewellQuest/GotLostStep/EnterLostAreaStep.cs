using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnterLostAreaStep : QuestStep
{
    private bool playerIsNear = false;
    private GameObject janiter;
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onBarking += Barking;
        janiter = GameObject.FindGameObjectWithTag("Janiter");
        animator = janiter.GetComponent<Animator>();
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onBarking -= Barking;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

    private void Barking()
    {
        if (playerIsNear)
        {
            animator.SetBool("isSleeping", false);
            FinishQuestStep();
        }
    }
}
