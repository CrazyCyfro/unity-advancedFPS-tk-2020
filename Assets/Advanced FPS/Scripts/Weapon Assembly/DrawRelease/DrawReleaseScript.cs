using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawReleaseScript : MonoBehaviour
{
    public float drawDuration;
    private bool drawn;
    private float drawSpeed;

    private Animator animator;

    void Awake()
    {
        if (drawDuration == 0) Debug.LogError("Draw duration = 0");
        drawSpeed = 1/drawDuration;
        
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        animator.SetFloat("Draw Speed", drawSpeed);
    }

    void OnDisable()
    {
        Release();
    }

    public void Draw()
    {
        animator.SetBool("Draw", true);
        StartCoroutine(Drawing());
    }

    IEnumerator Drawing()
    {
        yield return new WaitForSeconds(drawDuration);
        drawn = true;
    }

    public void Release()
    {
        StopAllCoroutines();
        animator.SetBool("Draw", false);
        drawn = false;
    }

    public bool Drawn()
    {
        return drawn;
    }
}
