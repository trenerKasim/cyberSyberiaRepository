using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuManager : MonoBehaviour
{
	[SerializeField]
	private GameObject mainCanvas;
	[SerializeField]
	private GameObject creditsCanvas;
	public void playButton()
	{
		StartCoroutine(loading());
	}

	[SerializeField]
	private GameObject loadingImage;

	IEnumerator loading()
	{
		loadingImage.SetActive(true);
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene(1);
	}

	public void creditsButton()
	{
		mainCanvas.SetActive(false);
		creditsCanvas.SetActive(true);
	}

    public void exitButton()
	{
		Application.Quit();
	}
}
