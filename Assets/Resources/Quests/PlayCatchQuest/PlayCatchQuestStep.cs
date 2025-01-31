using UnityEngine;

public class PlayCatchQuestStep : QuestStep
{
    private bool ballCollected = false;
    private GameObject itemCollecter;
    private SmellHandler targetSmellIndicator;
    private GameObject questItemSmell;
    private string questItemId;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onItemEquipped += Equip;
        targetSmellIndicator = GameObject.Find("SmellIndicator").GetComponent<SmellHandler>();
        questItemSmell = GameObject.FindGameObjectWithTag("Red");
        Debug.Log("Found quest item: " + questItemSmell.name);
        targetSmellIndicator.SetTargetSmell(questItemSmell);
        
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onItemEquipped -= Equip;

    }

    private void Equip()
    {
        itemCollecter = GameObject.Find("ItemPickupRadius");
        questItemId = itemCollecter.GetComponent<ItemController>().ItemId;
        Debug.Log("Quest item colected : " + questItemId);

        if(questItemId == "Lucky's Ball")
        {
            ballCollected = true;
            FinishQuestStep();
        }
    }
}
