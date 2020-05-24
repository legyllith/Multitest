using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPlayer : MonoBehaviour
{
    public Text gold;
    // Start is called before the first frame update
    void Start()
    {
        gold.color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
    }
    public void changeGold(double Gld)
    {
        gold.text = ""+Gld+"";
    }
   
}
