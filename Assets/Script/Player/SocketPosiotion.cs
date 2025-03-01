using UnityEngine;

public class SocketPosiotion : MonoBehaviour
{
    [SerializeField] private Vector2 positionUp;
    [SerializeField] private Vector2 positionLeft = new Vector2(0.1f, 0);
    [SerializeField] private Vector2 positionRight = new Vector2(0.1f, 0);
    [SerializeField] private Vector2 positionDown;
    private void Start()
    {
        GameEventsManager.instance.inputEvents.onMovePressed += MovePressed;
    }

    private void OnDestroy()
    {
        GameEventsManager.instance.inputEvents.onMovePressed -= MovePressed;
    }

    public void Update()
    {
        
    }

    private void MovePressed(Vector2 moveDir)
    {
        if (moveDir.x > 0)
        {
            this.transform.localPosition = positionRight;
        }
        if (moveDir.x < 0)
        {
            this.transform.localPosition = positionLeft;
        }
        if (moveDir.y > 0 && moveDir.x == 0)
        {
            this.transform.localPosition = new Vector2(0, -1000f);
        }
        if (moveDir.y < 0 && moveDir.x == 0)
        {
            this.transform.localPosition = positionDown;

        }
    }
}
