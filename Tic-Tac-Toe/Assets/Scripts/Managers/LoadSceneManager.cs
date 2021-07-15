using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : GenericSingleton<LoadSceneManager>
{
    [SerializeField]
    private GameObject fadeOut;
    private const float fadeOutTimer = 0.45f;

    internal void LoadPreviousScene()
    {
        StartCoroutine(LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex - 1));
    }

    internal void LoadNextScene()
    {
        StartCoroutine(LoadSceneByIndex(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadSceneByIndex(int indx)
    {
        fadeOut.SetActive(true);

        yield return new WaitForSeconds(fadeOutTimer);

        SceneManager.LoadScene(indx);
    }
}