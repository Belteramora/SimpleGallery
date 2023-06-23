using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void OnBackButtonPressed()
    {
        SceneManager.UnloadSceneAsync(3);
    }

	private void Update()
	{
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnBackButtonPressed();
        }
    }
}
