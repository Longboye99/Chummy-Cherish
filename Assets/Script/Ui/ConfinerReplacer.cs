using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class ConfinerReplacer : MonoBehaviour
{
    public GameObject levelGO;
    public GameObject bossGO;

    public bool isBoss;

    public CinemachineConfiner2D confiner;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isBoss)
        {
            replaceConfinerShape2D(levelGO, bossGO);

        }
        else
        {
            replaceConfinerShape2D(bossGO, levelGO);

        }
    }

    public void replaceConfinerShape2D(GameObject oldGO, GameObject newGO)
    {

        oldGO.SetActive(false);
        newGO.SetActive(true);
        confiner.BoundingShape2D = newGO.GetComponent<CompositeCollider2D>();
        confiner.InvalidateBoundingShapeCache();

    }
}