using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            SoldadoIA soldadoIA = other.GetComponent<SoldadoIA>();

            if (soldadoIA != null && soldadoIA.currentState != SoldadoIA.State.Distracted)
            {
                soldadoIA.currentState = SoldadoIA.State.Distracted;
            }
        }
    }
}
