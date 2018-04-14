using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {


	public string sceneName;
	public void NewScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
	}
}