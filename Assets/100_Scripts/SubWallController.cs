using System;
using System.Collections;
using UnityEngine;

public class SubWallController : MonoBehaviour
{

    private WallFallController wallFallController;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        wallFallController = GetComponentInChildren<WallFallController>();
        wallFallController.FallWall += OnWallFall;
    }

    private void OnWallFall()
    {
        animator.SetBool("Fall",true);
        StartCoroutine(ThisWallDestroy());
    }

    private IEnumerator ThisWallDestroy()
    {
        yield return new WaitForSeconds(1);
        GameManager.instance.GetDanage();
        Destroy(this.gameObject);
    }
}
