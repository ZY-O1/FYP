using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class FadeInBounce : MonoBehaviour
{
	public Vector2 targetPositionA; // First target position (x, y)
	public Vector2 targetPositionB; // Second target position (x, y)
	public float delayBeforeStart = 1f; // Delay before starting the fall
	public float fallDuration = 0.5f; // Duration to fall to the first target
	public float bounceDuration = 1f; // Duration of the bounce to the second target
	public float parabolaHeight = 50f; // Height of the parabolic arc

	private RectTransform rectTransform;
	private CanvasGroup canvasGroup;

	void Start()
	{
		rectTransform = GetComponent<RectTransform>();
		canvasGroup = GetComponent<CanvasGroup>();

		StartCoroutine(AnimateUI());
	}

	IEnumerator AnimateUI()
	{
		canvasGroup.alpha = 0f; // Start with full transparency

		// Wait for the initial delay
		yield return new WaitForSeconds(delayBeforeStart);

		// Fall down to target position A
		yield return StartCoroutine(FallDown());

		// Parabolic movement to the final target position B
		yield return StartCoroutine(BounceToTarget());

		// Ensure final position and alpha
		rectTransform.anchoredPosition = targetPositionB;
		canvasGroup.alpha = 1f;
	}

	IEnumerator FallDown()
	{
		float elapsedTime = 0f;
		Vector2 startPosition = rectTransform.anchoredPosition;

		while (elapsedTime < fallDuration)
		{
			elapsedTime += Time.unscaledDeltaTime;
			float t = elapsedTime / fallDuration;

			// Linear interpolation for position and alpha
			rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPositionA, t);
			canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

			yield return null;
		}

		// Ensure the final position and alpha
		rectTransform.anchoredPosition = targetPositionA;
		canvasGroup.alpha = 1f;
	}

	IEnumerator BounceToTarget()
	{
		float elapsedTime = 0f;
		Vector2 startPosition = rectTransform.anchoredPosition;
		float distance = Vector2.Distance(targetPositionA, targetPositionB);
		float parabolaPeak = Mathf.Min(distance / 2, parabolaHeight);

		while (elapsedTime < bounceDuration)
		{
			elapsedTime += Time.unscaledDeltaTime;
			float t = elapsedTime / bounceDuration;

			// Parabolic movement
			float parabola = 4 * parabolaPeak * t * (1 - t); // Parabola equation for the bounce effect
			rectTransform.anchoredPosition = Vector2.Lerp(targetPositionA, targetPositionB, t) + new Vector2(0, parabola);

			yield return null;
		}

		// Ensure the final position
		rectTransform.anchoredPosition = targetPositionB;
	}
}

