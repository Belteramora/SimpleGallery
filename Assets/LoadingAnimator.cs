using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingAnimator : MonoBehaviour
{
	[SerializeField]
	private Slider slider;

	[SerializeField]
	private TextMeshProUGUI progressText;

	[SerializeField]
	private float loadingTime;

	private float currentTime;
	private float progress;

	private void Update()
	{

		if(currentTime <= loadingTime)
		{
			currentTime += Time.deltaTime;
			progress = Mathf.Lerp(0, 1, currentTime / loadingTime);
			slider.value = progress;
			progressText.text = Mathf.RoundToInt(progress * 100) + "%";
		}
		else
		{
			SceneManager.LoadScene(2);
		}
	}


}
