using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UitlegNextBack : MonoBehaviour
{
    public GameObject veiligCanvas;
    public GameObject dangerCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        veiligCanvas.SetActive(true);
        dangerCanvas.SetActive(false);
    }

    public void Back()
    {
        veiligCanvas.SetActive(false);
        dangerCanvas.SetActive(true);
    }
}
