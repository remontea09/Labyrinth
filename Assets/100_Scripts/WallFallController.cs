using System;
using UnityEngine;

public class WallFallController : MonoBehaviour
{

    public Action FallWall;

    private void OnTriggerEnter(Collider other)
    {
        FallWall?.Invoke();
    }
}
