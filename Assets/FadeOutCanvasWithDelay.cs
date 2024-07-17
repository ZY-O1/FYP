using System.Collections;
using UnityEngine;

public class FadeOutCanvasWithDelay : MonoBehaviour
{
	public float fadeDuration = 1f; // Duration of the fade-out effect
	public float delayBeforeFade = 1f; // Delay before starting the fade-out

	[SerializeField]
	private CanvasGroup canvasGroup;
	private UIManagerStart uiStart;

	void Start()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		uiStart = FindObjectOfType<UIManagerStart>();
		if (canvasGroup == null)
		{
			Debug.LogError("No CanvasGroup component found on the GameObject.");
			return;
		}

		if (uiStart == null)
		{
			Debug.LogError("No UIManager component found on the GameObject.");
			return;
		}

		StartCoroutine(FadeOutWithDelay());
	}

	IEnumerator FadeOutWithDelay()
	{
		yield return new WaitForSecondsRealtime(delayBeforeFade); // Wait for the delay time

		float startAlpha = canvasGroup.alpha;
		float elapsedTime = 0f;

		while (elapsedTime < fadeDuration)
		{
			elapsedTime += Time.unscaledDeltaTime; // Use unscaled time for fading regardless of time scale
			canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeDuration);
			yield return null;
		}
		uiStart.runLoading();
		canvasGroup.alpha = 0f; // Ensure alpha is set to 0 at the end
		
	}
}
