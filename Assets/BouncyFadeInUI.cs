using System.Collections;
using UnityEngine;

public class BouncyFadeInUI : MonoBehaviour
{
	public float moveDuration = 1.0f;        // Duration of the movement from left to right
	public float bounceDuration = 0.5f;      // Duration of the bounce effect
	public float bounceFrequency = 3f;       // Frequency of the bounces
	public float bounceIntensity = 0.2f;     // Intensity of the bounce effect
	public float startOffsetX = -500f;       // Initial horizontal offset for the fly-in effect

	private RectTransform rectTransform;
	private CanvasGroup canvasGroup;
	private Vector3 targetPosition;

	void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		canvasGroup = GetComponent<CanvasGroup>();
		targetPosition = rectTransform.anchoredPosition;

		StartCoroutine(FlyInAndBounce());
	}

	IEnumerator FlyInAndBounce()
	{
		float elapsedTime = 0f;
		canvasGroup.alpha = 0f; // Start with full transparency

		// Start position for the fly-in effect
		Vector3 startPosition = targetPosition + new Vector3(startOffsetX, 0, 0);
		rectTransform.anchoredPosition = startPosition;

		// Fly-in effect from left to right
		while (elapsedTime < moveDuration)
		{
			elapsedTime += Time.unscaledDeltaTime;
			float t = elapsedTime / moveDuration;

			// Linear interpolation for position and alpha
			rectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
			canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

			yield return null;
		}

		// Ensure the final position and alpha
		rectTransform.anchoredPosition = targetPosition;
		canvasGroup.alpha = 1f;

		// Start the bounce effect
		StartCoroutine(BounceEffect());
	}

	IEnumerator BounceEffect()
	{
		float elapsedTime = 0f;

		while (elapsedTime < bounceDuration)
		{
			elapsedTime += Time.unscaledDeltaTime;
			float t = elapsedTime / bounceDuration;

			// Bounce effect for scale
			float bounce = Mathf.Sin(t * Mathf.PI * bounceFrequency) * Mathf.Exp(-t * bounceFrequency) * bounceIntensity;

			// Wiggle effect
			float wiggle = Mathf.Sin(t * Mathf.PI * bounceFrequency * 2) * bounceIntensity;

			// Apply the bounce and wiggle effect to the scale and position
			rectTransform.localScale = new Vector3(2f + bounce, 2f - bounce, 1f);
			rectTransform.anchoredPosition += new Vector2(wiggle, 0);

			yield return null;
		}

		// Ensure final scale and position
		rectTransform.localScale = new Vector3(2f, 2f, 1f);
		rectTransform.anchoredPosition = targetPosition;
	}
}
