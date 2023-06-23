using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ImageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject imagePrefab;
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(CheckAndSpawnImages());
    }

	IEnumerator CheckAndSpawnImages()
	{
		int countOfImages = 1;
		while (true)
		{
			UnityWebRequest request = UnityWebRequestTexture.GetTexture("http://data.ikppbb.com/test-task-unity-data/pics/" + countOfImages + ".jpg");

			yield return request.SendWebRequest();

			if(request.isHttpError || request.isNetworkError) 
			{
				request.Dispose();
				break;
			}

			GameObject go = Instantiate(imagePrefab, transform);
			go.GetComponent<ImageLoader>().pictureNumber = countOfImages;

			countOfImages++;

			Debug.Log(countOfImages);

			request.Dispose();
		}
	}
}
