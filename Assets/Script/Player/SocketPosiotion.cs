using UnityEngine;

public class SocketPosiotion : MonoBehaviour
{
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
            this.transform.localPosition = new Vector2(0.1f, 0);
        }
        if (moveDir.x < 0)
        {
            this.transform.localPosition = new Vector2(-0.1f, 0);
        }
        if (moveDir.y > 0 && moveDir.x == 0)
        {
            this.transform.localPosition = new Vector2(0, 0.1f);
        }
        if (moveDir.y < 0 && moveDir.x == 0)
        {
            this.transform.localPosition = new Vector2(0, -0.1f);

        }
    }
}
