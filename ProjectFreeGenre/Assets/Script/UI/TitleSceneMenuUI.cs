using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject ButtonCollection;
    [SerializeField] private GameObject StageSelection;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        if (clip != null)
        {
            GameObject soundObj = SoundManager.Instance.PlayClip(clip);
            soundObj.transform.position = Vector3.zero;
        }

        ButtonCollection.SetActive(true);
        StageSelection.SetActive(false);
    }

    public void OnStart()
    {
        if (clip != null)
        {
            GameObject soundObj = SoundManager.Instance.PlayClip(clip);
            soundObj.transform.position = Vector3.zero;
        }

        ButtonCollection.SetActive(false);
        StageSelection.SetActive(true);
    }

    public void OnCancel()
    {
        if (clip != null)
        {
            GameObject soundObj = SoundManager.Instance.PlayClip(clip);
            soundObj.transform.position = Vector3.zero;
        }

        ButtonCollection.SetActive(true);
        StageSelection.SetActive(false);
    }

    public void OnStage1Select()
    {
        StartCoroutine(StageSelect("Stage1"));
    }

    public void OnStage2Select()
    {
        StartCoroutine(StageSelect("Stage2"));
    }

    private IEnumerator StageSelect(string stageName)
    {
        if (clip != null)
        {
            GameObject soundObj = SoundManager.Instance.PlayClip(clip);
            soundObj.transform.position = Vector3.zero;

            yield return new WaitForSeconds(clip.length);
        }
        else
            yield return null;

        SceneManager.LoadScene(stageName);
    }

    public void OnExit()
    {
        StartCoroutine(ExitGame());
    }

    private IEnumerator ExitGame()
    {
        if (clip != null)
        {
            GameObject soundObj = SoundManager.Instance.PlayClip(clip);
            soundObj.transform.position = Vector3.zero;

            yield return new WaitForSeconds(clip.length);
        }
        else
            yield return null;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
