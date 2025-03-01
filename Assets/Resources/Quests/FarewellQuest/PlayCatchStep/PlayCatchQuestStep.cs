using UnityEngine;

public class PlayCatchQuestStep : QuestStep
{
    public GameObject luckyBallPrefab;
    private string questItemId;

    private void OnEnable()
    {
        GameEventsManager.instance.itemEvents.onEquipItem += EquipItem;    
        Instantiate(luckyBallPrefab, this.transform.position, Quaternion.identity);
        Debug.Log("Spawned quest item: " + luckyBallPrefab.name);
        
    }

    private void OnDisable()
    {
        GameEventsManager.instance.itemEvents.onEquipItem -= EquipItem;

    }

    private void EquipItem(ItemDefinitionSO itemInfo)
    {       
        if(itemInfo.id == "Lucky's Ball")
        {
            Debug.Log("Quest item colected : " + questItemId);
            FinishQuestStep();
        }
    }
}
