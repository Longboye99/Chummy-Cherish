using UnityEngine;

public class TrashArea : MonoBehaviour
{
    [SerializeField] private GameObject trashOverlay;
    [SerializeField] private GameObject trashProps;

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onFinishQuest += FinishShopQuest;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishShopQuest;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trashOverlay.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trashOverlay.SetActive(true);
        }
    }

    private  void FinishShopQuest(string id)
    {
        if(id == "GotLostStep")
        {
            Destroy(trashOverlay.gameObject);
            Destroy(trashProps.gameObject);
        }
    }
}
