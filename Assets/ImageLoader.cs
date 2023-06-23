using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
	public static Sprite choosedSprite = null;

    private Image image;
	private RectTransform rectTransform;
	private bool isLoaded = false;

	public int pictureNumber;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
		rectTransform = GetComponent<RectTransform>();
	}

	private void Update()
	{
		if (IsVisibleFrom(rectTransform, Camera.main) && !isLoaded)
		{
			StartCoroutine(LoadTextureFromServer());
			isLoaded = true;
		}
	}

	IEnumerator LoadTextureFromServer()
	{

		UnityWebRequest request = UnityWebRequestTexture.GetTexture("http://data.ikppbb.com/test-task-unity-data/pics/" + pictureNumber + ".jpg");

		yield return request.SendWebRequest();

		Texture2D texture = DownloadHandlerTexture.GetContent(request);

		image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());

		request.Dispose();
	}


	private  int CountCornersVisibleFrom(RectTransform rectTransform, Camera camera)
	{
		Rect screenBounds = new Rect(0f, 0f, Screen.width, Screen.height);
		Vector3[] objectCorners = new Vector3[4];
		rectTransform.GetWorldCorners(objectCorners);


		int visibleCorners = 0;
		Vector3 tempScreenSpaceCorner;
		for (var i = 0; i < objectCorners.Length; i++)
		{
			tempScreenSpaceCorner = camera.WorldToScreenPoint(objectCorners[i]);
			if (screenBounds.Contains(tempScreenSpaceCorner))
			{
				visibleCorners++;
			}
		}
		return visibleCorners;
	}

	public bool IsFullyVisibleFrom(RectTransform rectTransform, Camera camera)
	{
		return CountCornersVisibleFrom(rectTransform, camera) == 4;
	}

	public bool IsVisibleFrom(RectTransform rectTransform, Camera camera)
	{
		return CountCornersVisibleFrom(rectTransform, camera) > 0;
	}

	public void OnImageClick()
	{
		if (isLoaded)
		{
			choosedSprite = image.sprite;
			SceneManager.LoadScene(3, LoadSceneMode.Additive);
		}
	}
}
