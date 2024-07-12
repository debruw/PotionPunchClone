using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bottle : MonoBehaviour
{
    public Renderer rend;
    public Transform platePos;

    private void Start()
    {
        rend.material.SetFloat("_Random", Random.Range(0, 10));
    }

    public void SetColor(Color color)
    {
        rend.material.color = color;
    }
}