using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SmellHandler : MonoBehaviour
{
    [SerializeField] GameObject targetSmell;
    [SerializeField] GameObject smellPointer;
    [SerializeField] float hideDistance;

    internal Vector2 direction;
    internal float angle;
    private float skillTime = 0;
    private bool usingSkill = false;
    private SpriteRenderer targetSprite;
    private Color oldColor;
    private bool oldSpriteEnable;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onSmelling += Smelling;
        GameEventsManager.instance.playerEvents.onSetTargetSmell += SetTargetSmell;
        smellPointer.SetActive(false);

    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onSmelling -= Smelling;
        GameEventsManager.instance.playerEvents.onSetTargetSmell -= SetTargetSmell;

    }

    void Update () 
    {
        
        if (usingSkill)
        {         
            if (targetSmell != null)
            {
                smellPointer.SetActive(true);
                GetObjectDirection();
                GameEventsManager.instance.playerEvents.DisablePlayerMovement();
                skillTime = skillTime + Time.deltaTime;

                if (skillTime >= 1.5)
                {
                    usingSkill = false;
                    smellPointer.SetActive(false);
                    UnlitTarget();
                    Debug.Log("Stop Smelling");
                    GameEventsManager.instance.playerEvents.EnablePlayerMovement();
                    skillTime = 0;
                }
            }
            else
            {
                usingSkill = false;
                smellPointer.SetActive(false);
                GameEventsManager.instance.playerEvents.EnablePlayerMovement();
            }
        }
    }

    void SetChildActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }

    private void Smelling()
    {
        UpdateTargetSmell();

        if (targetSmell != null)
        {
            
            SetTargetLit();
            usingSkill = true;
        }
        
        Debug.Log("Lucky is smelling");
    }

    private void GetObjectDirection()
    {
            direction = targetSmell.transform.position - transform.position;
            if (direction.magnitude < hideDistance)
            {
                SetChildActive(false);
            }
            else
            {
                SetChildActive(true);

                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, 0, angle);
            }
       
    }

    public void SetTargetSmell(GameObject targetObject)
    {
        targetSmell = targetObject;
    }

    private void UpdateTargetSmell()
    {
        GameObject[] items;
        items = GameObject.FindGameObjectsWithTag("Item");
        GameObject closestItem = null;
        float distance = 10000;
        foreach (GameObject item in items)
        {
            float curDistance = (item.transform.position - this.transform.position).sqrMagnitude;
            if (curDistance < distance)
            {
                closestItem = item;
                distance = curDistance;
            }
        }
        if (closestItem != null)
        {
            GameEventsManager.instance.playerEvents.SetTargetSmell(closestItem);
        }
    }


    public void SetTargetLit()
    {
        targetSprite = targetSmell.gameObject.GetComponent<SpriteRenderer>();
        oldColor = targetSprite.color;
        oldSpriteEnable = targetSprite.enabled;
        Debug.Log(oldColor + " , " + oldSpriteEnable);
        targetSprite.enabled = true;
        targetSprite.color = new Color(1, 1, 1, 0.5f);
        Debug.Log("Lit: " + targetSmell.name);
    }

    public void UnlitTarget()
    {
        targetSprite = targetSmell.gameObject.GetComponent<SpriteRenderer>();

        targetSprite.enabled = oldSpriteEnable;
        targetSprite.color = oldColor;
        targetSprite = null;
        Debug.Log("Unlit: " + targetSmell.name);

    }
}
