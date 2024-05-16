using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tirachinas : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public Transform puntoDeLanzamiento;
    public float fuerzaDeLanzamiento = 10f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }
    }

    void Disparar()
    {
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDeLanzamiento.position, puntoDeLanzamiento.rotation);
        Rigidbody rb = proyectil.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.AddForce(puntoDeLanzamiento.forward * fuerzaDeLanzamiento, ForceMode.Impulse);
            }

        Collider proyectilCollider = proyectil.GetComponent<Collider>();
            if (proyectilCollider != null)
            {
                proyectilCollider.enabled = true;
            }
    }
}
