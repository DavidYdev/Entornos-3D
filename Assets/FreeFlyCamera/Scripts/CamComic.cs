using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamComic : MonoBehaviour
{
    // Arreglo de Transform que contiene los puntos de vista de la cámara
    public Transform[] views;

    // Velocidad de transición entre los puntos de vista
    public float transitionSpeed;

    // Punto de vista actual
    private Transform currentView;

    // Índice del punto de vista actual en el arreglo views
    private int currentIndex = 0;

    // Variable para controlar si las vistas ya han cambiado una vez
    private bool viewsChanged = false;

    void Start()
    {
        // Establece el punto de vista actual al primer punto de vista en el arreglo views
        currentView = views[0];

        // Coloca la cámara en la posición del punto de vista actual
        transform.position = currentView.position;

        // Inicia la corutina para cambiar automáticamente los puntos de vista
        StartCoroutine(ChangeViewAutomatically());
    }

    // Corutina para cambiar automáticamente los puntos de vista
    IEnumerator ChangeViewAutomatically()
    {
        // Mientras las vistas no hayan cambiado y no se haya alcanzado el último punto de vista
        while (!viewsChanged && currentIndex < views.Length - 1)
        {
            // Espera 3 segundos antes de cambiar al siguiente punto de vista
            yield return new WaitForSeconds(3f);

            // Incrementa el índice para cambiar al siguiente punto de vista
            currentIndex++;

            // Actualiza el punto de vista actual
            currentView = views[currentIndex];
        }
    }

    // Método LateUpdate se ejecuta después de que todos los objetos hayan sido procesados en la actualización de fotograma
    private void LateUpdate()
    {
        // Actualiza gradualmente la posición de la cámara hacia la posición del punto de vista actual
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);
    }
}
