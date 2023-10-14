using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Par�metros de temblor de c�mara
    [SerializeField] float _shakeDuration = 0.5f;
    [SerializeField] float _shakeMagnitude = 0.2f;

    // Referencia a la c�mara
    public Camera _mainCamera;

    // Posici�n original de la c�mara antes del temblor
    private Vector3 _originalCameraPosition;

    private void Start()
    {
        // Obtener la c�mara principal (Main Camera) del escenario
        _mainCamera = Camera.main;
    }

    // M�todo que se llama cuando el personaje recibe un golpe
    public void Shake()
    {
        // Iniciar el temblor de la c�mara en un hilo separado (corutina)
        StartCoroutine(ShakeCamera());
       // StopCoroutine(ShakeCamera());
    }

    public IEnumerator ShakeCamera()
    {
        // Guardar la posici�n original de la c�mara antes de temblar
        _originalCameraPosition = _mainCamera.transform.localPosition;

        float elapsedTime = 0f;

        // Hacer que la c�mara tiemble durante el tiempo especificado (shakeDuration)
        while (elapsedTime < _shakeDuration)
        {
            // Generar una posici�n aleatoria dentro de un c�rculo usando ruido Perlin
            float x = Random.Range(-1f, 1f) * _shakeMagnitude;
            float y = Random.Range(-1f, 1f) * _shakeMagnitude;

            // Aplicar el temblor a la posici�n de la c�mara
            _mainCamera.transform.localPosition = _originalCameraPosition + new Vector3(x, y, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Restaurar la posici�n original de la c�mara despu�s del temblor
        _mainCamera.transform.localPosition = _originalCameraPosition;
    }
}
