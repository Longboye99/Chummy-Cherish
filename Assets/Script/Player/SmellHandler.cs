using UnityEngine;
using UnityEngine.UIElements;

public class SmellHandler : MonoBehaviour
{
    [SerializeField] GameObject targetSmell;
    internal Vector2 direction;
    internal float angle;
    [SerializeField] float hideDistance;

    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onSmelling += Smelling;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onSmelling -= Smelling;

    }

    void Update () 
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
    void SetChildActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }

    private void Smelling()
    {

        
        
    }
}
