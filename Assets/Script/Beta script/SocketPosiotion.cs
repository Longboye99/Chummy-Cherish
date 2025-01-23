using UnityEngine;

public class SocketPosiotion : MonoBehaviour
{

    public void Update()
    {
        if (Inputmg.Movement.x > 0)
        {
            this.transform.localPosition = new Vector2(1,0);
        }
        if (Inputmg.Movement.x < 0)
        {
            this.transform.localPosition = new Vector2(-1, 0);

        }
    }
}
