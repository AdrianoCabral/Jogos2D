using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    public Color startColor = Color.white;
    public Color endColor = new(46,62,101,0);
    [Range(0,10)]
    public float speed = 1.0f;

    Image imgComp;
    // Start is called before the first frame update
    void Awake()
    {
        imgComp = GetComponent<Image>();        
    }

    // Update is called once per frame
    void Update()
    {
        imgComp.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed,1));
    }
}
