using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Parámetros de temblor de cámara
    [SerializeField] float _shakeDuration = 0.5f;
    [SerializeField] float _shakeMagnitude = 0.2f;

    // Referencia a la cámara
    public Camera _mainCamera;

    // Posición original de la cámara antes del temblor
    private Vector3 _originalCameraPosition;

    private void Start()
    {
        // Obtener la cámara principal (Main Camera) del escenario
        _mainCamera = Camera.main;
    }

    // Método que se llama cuando el personaje recibe un golpe
    public void Shake()
    {
        // Iniciar el temblor de la cámara en un hilo separado (corutina)
        StartCoroutine(ShakeCamera());
       // StopCoroutine(ShakeCamera());
    }

    public IEnumerator ShakeCamera()
    {
        // Guardar la posición original de la cámara antes de temblar
        _originalCameraPosition = _mainCamera.transform.localPosition;

        float elapsedTime = 0f;

        // Hacer que la cámara tiemble durante el tiempo especificado (shakeDuration)
        while (elapsedTime < _shakeDuration)
        {
            // Generar una posición aleatoria dentro de un círculo usando ruido Perlin
            float x = Random.Range(-1f, 1f) * _shakeMagnitude;
            float y = Random.Range(-1f, 1f) * _shakeMagnitude;

            // Aplicar el temblor a la posición de la cámara
            _mainCamera.transform.localPosition = _originalCameraPosition + new Vector3(x, y, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Restaurar la posición original de la cámara después del temblor
        _mainCamera.transform.localPosition = _originalCameraPosition;
    }
}
