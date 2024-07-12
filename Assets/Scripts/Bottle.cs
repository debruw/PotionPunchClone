using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bottle : MonoBehaviour
{
    public Renderer rend;
    public Transform platePos;
    public Renderer FillObject;

    private Animator _animator;

    public Color COLOR
    {
        get => _color;
        set
        {
            _color = value;
            rend.material.color = _color;
            FillObject.material.color = _color;
        }
    }

    private Color _color;
    private static readonly int Filling = Animator.StringToHash("_filling");

    private void Start()
    {
        rend.material.SetFloat("_Random", Random.Range(0, 10));
        _animator = GetComponent<Animator>();
    }

    public void StartFill()
    {
        FillObject.enabled = true;
        // open tap
        _animator.SetBool(Filling, true);
    }
    
    public void StopFill()
    {
        FillObject.enabled = false;
        // close tap
        _animator.SetBool(Filling, false);
    }
}