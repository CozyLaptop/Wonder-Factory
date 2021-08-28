using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class BottomShadowPhysics : MonoBehaviour
{
    private float intensity;
    private SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        //intensi
        //sr.color = new Color(1f, 1f, 1f, 1f);// is a normal sprite
       //sr.color = new Color(1f, 1f, 1f, .5f);// is about 50 % transparent
       // sr.color = new Color(1f, 1f, 1f, 0f);// is about 100 % transparent(Cant be seen at all, but still active)
    }
}