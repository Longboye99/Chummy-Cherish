using UnityEngine;

public class PlayCatchQuestStep : QuestStep
{
    private bool ballCollected = false;
    private GameObject player;
    private string questItemId;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onItemEquipped += Equip;
        
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onItemEquipped -= Equip;

    }

    private void Equip()
    {
        player = GameObject.Find("ItemPickupRadius");
        questItemId = player.GetComponent<ItemController>().ItemId;
        Debug.Log("Quest item colected : " + questItemId);

        if(questItemId == "Lucky's Ball")
        {
            ballCollected = true;
            FinishQuestStep();
        }
    }
}
