using System;
using UnityEngine;

[Serializable]
public class UIConstructingCell
{
    public Transform targetCanvas;
    public GameObject[] uiArray;
}

public class SceneSettler : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private UIConstructingCell[] cells;

    private void Awake()
    {
        SoundManager.Instance.ChangeBGM(clip);

        foreach(UIConstructingCell cell in cells)
        {
            foreach(GameObject ui in cell.uiArray)
            {
                Instantiate(ui, cell.targetCanvas);
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.player = null;
        GameManager.Instance.monsters.Clear();
    }
}