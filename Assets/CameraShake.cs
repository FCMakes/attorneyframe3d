using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	public float shakeDuration = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	public float maxShakeDuration;
	public bool isShaking;
	public Vector3 originalPos;
	public bool isStatic;
	public void Start()
	{
		originalPos = base.gameObject.transform.localPosition;
	}
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
		}
		
	}
	
	
		
	

	void Update()
	{
		

		if (shakeDuration > 0)
		{

			if (shakeDuration == maxShakeDuration)
			{
			
				isShaking = true;
			}
			camTransform.localPosition += Random.insideUnitSphere * shakeAmount;
			
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else
		{

			shakeDuration = 0f;
			if (isShaking)
			{
				camTransform.localPosition = originalPos;
				isShaking = false;
				
			}

			if (isStatic)
			{
				camTransform.localPosition = originalPos;
			}
			
		}
	}
}
