using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
	[SerializeField] float _interactDistance=0.4f;
	[SerializeField] private Image _killBar;
	[SerializeField] private GameObject _interactionImage, _nonInteractiveImage;
	[SerializeField] private float _speedToFill=1;
	[SerializeField] Vector3 _boxSize= Vector3.one;

	public float currentKillFill;


    private void Start()
    {
		currentKillFill = 0;
    }
    private void Update()
	{
		_killBar.fillAmount = currentKillFill / 100;
		

		RaycastHit hit;

		//Physics.Raycast(transform.position, transform.forward, out hit, _interactDistance);
		//Physics.BoxCast(transform.position, BoxSize, transform.forward, out hit, transform.rotation, _interactDistance);

		Vector3 boxCastStartPosition = transform.position + transform.forward * _interactDistance;
		Quaternion boxCastRotation = transform.rotation;

		Physics.BoxCast(boxCastStartPosition, _boxSize / 2f, transform.forward, out hit, boxCastRotation, _interactDistance);
		
			if (hit.collider != null && hit.collider.CompareTag("Interactive") )
			{
			 _interactionImage.SetActive(true);
            if ( Input.GetKeyDown(KeyCode.E))
            {
				hit.transform.GetComponent<ObjectInteraction>().Use();
			}

            }
        else
        {
			_interactionImage.SetActive(false);
        }
		if (hit.collider != null && hit.collider.CompareTag("NonInteractive"))
		{
			_nonInteractiveImage.SetActive(true);
        }
        else
        {
			_nonInteractiveImage.SetActive(false);
        }
			if (hit.collider != null && hit.collider.CompareTag("Enemy")&& Input.GetMouseButton(1))
        {
			currentKillFill += _speedToFill * Time.deltaTime;
			currentKillFill = Mathf.Clamp(currentKillFill, 0f, 100f);
		}
        else
        {
			currentKillFill -= _speedToFill * Time.deltaTime;
			currentKillFill = Mathf.Clamp(currentKillFill, 0f, 100f);
		}

		/*if (currentKillFill >= 100) currentKillFill = 100;
		if (currentKillFill <= 0) currentKillFill = 0;*/
      

	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.cyan;

		Vector3 boxCastStartPosition = transform.position + transform.forward * _interactDistance;
		Quaternion boxCastRotation = transform.rotation;

		Gizmos.DrawWireCube(boxCastStartPosition + transform.forward * (_interactDistance / 2f), _boxSize);
		Gizmos.DrawLine(boxCastStartPosition, boxCastStartPosition + transform.forward * _interactDistance);
	}
}
