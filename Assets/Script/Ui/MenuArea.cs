using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class MenuArea : MonoBehaviour
{
    private bool isNear = false;
    public SpriteRenderer spriteRenderer;
    private GameObject player;
    private Animator animator;
    public GameObject light;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onInteract += Interact;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = player.GetComponent<Animator>();
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onInteract -= Interact;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNear = true;
            spriteRenderer.color = new Color(1f, 1f, 0.4f, 1f);
            light.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNear = false;
            spriteRenderer.color = Color.white;
            light.gameObject.SetActive(false);

        }
    }

    private void Interact(InputEventContext inputEventContext)
    {
        if(isNear)
        {
            animator.SetBool("IsSleeping", true);
            GameEventsManager.instance.playerEvents.DisablePlayerMovement();
            Invoke("LoadGame", 4);
        }
    }
    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
