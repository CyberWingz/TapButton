using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	[SerializeField] float m_minDuration;
	[SerializeField] Fader m_Fader;

	Fader Fader
	{
		get
		{
			if(m_Fader == null)
				m_Fader = GetComponentInChildren<Fader>();
			
			return m_Fader;
		}
	}

	// Use this for initialization
	void Start () {
	
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Space))
			StartCoroutine(iChangeScene("HelloWorld"));
	}

	public void ChangeScene(string sceneName)
	{
		StartCoroutine(iChangeScene(sceneName));
	}

	IEnumerator iChangeScene(string sceneName)
	{
		print(this);

		yield return StartCoroutine(m_Fader.FadeIn());	
		// Load loading screen
		yield return SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Single);

		// !!! unload old screen (automatic)

		// Fade to loading screen
		yield return StartCoroutine(m_Fader.FadeOut());

		float endTime = Time.time + m_minDuration;

		// Load level async
		yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

		while (Time.time < endTime)
			yield return null;

		// Play music or perform other misc tasks

		// Fade to black
		yield return StartCoroutine(m_Fader.FadeIn());

		// !!! unload loading screen
		SceneManager.UnloadScene("LoadingScreen");

		// Fade to new screen
		yield return StartCoroutine(m_Fader.FadeOut());

	}
}
