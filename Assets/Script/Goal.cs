using System;
using UnityEngine;

public class Goal : MonoBehaviour
{

    public Action OnGoal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnGoal?.Invoke();
        }
    }

}
