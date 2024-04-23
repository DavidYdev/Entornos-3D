using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Asigna tu cámara virtual en el Inspector

    void Start()
    {
        // Asegúrate de que la cámara virtual esté asociada con la pista dolly
        if (virtualCamera != null)
        {
            // Aquí puedes configurar la prioridad de la cámara virtual
            // Por ejemplo, para establecerla a 10:
            virtualCamera.Priority = 10;
        }
    }

    void Update()
    {
        if (Time.time >= 43f)
        {
            // Realizar alguna acción después de 43 segundos
            // Por ejemplo, cambiar la prioridad de la cámara virtual nuevamente
            virtualCamera.Priority = 5; // Cambiar la prioridad a 5 después de 43 segundos
        }
    }
}