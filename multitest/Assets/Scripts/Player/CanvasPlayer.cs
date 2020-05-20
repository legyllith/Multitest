using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPlayer : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
    }

   
}
