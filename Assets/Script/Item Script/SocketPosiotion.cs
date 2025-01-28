using UnityEngine;

public class SocketPosiotion : MonoBehaviour
{

    public void Update()
    {
        if (Inputmg.Movement.x > 0)
        {
            this.transform.localPosition = new Vector2(0.1f,0);
        }
        if (Inputmg.Movement.x < 0)
        {
            this.transform.localPosition = new Vector2(-0.1f, 0);
        }
        if (Inputmg.Movement.y > 0 && Inputmg.Movement.x == 0)
        {
            this.transform.localPosition = new Vector2(0, 0.1f);
        }
        if (Inputmg.Movement.y < 0 && Inputmg.Movement.x == 0)
        {
            this.transform.localPosition = new Vector2(0, -0.1f);

        }
    }
}
