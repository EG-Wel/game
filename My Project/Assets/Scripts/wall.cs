using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public GameObject edgeColl;

    public void DestroyWall() => Destroy(edgeColl);
}
