using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIConstructingCell
{
    public string CanvasName;
    public GameObject[] uiArray;
}

[Serializable]
public class ObjectConstructingCell
{
    public GameObject targetObject;
    public Vector3 position;
}


public class SceneSettler : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private UIConstructingCell[] cells;
    [SerializeField] private ObjectConstructingCell[] objects;

    private void Awake()
    {
        if(clip != null) 
            SoundManager.Instance.ChangeBGM(clip);

        foreach (UIConstructingCell cell in cells)
        {
            GameObject curObj = new GameObject(cell.CanvasName);

            Canvas curCanvas = curObj.AddComponent<Canvas>();
            curCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler scaler = curObj.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Shrink;

            GraphicRaycaster graphicRaycaster = curObj.AddComponent<GraphicRaycaster>();
            graphicRaycaster.ignoreReversedGraphics = true;

            foreach (GameObject ui in cell.uiArray)
            {
                Instantiate(ui, curObj.transform);
            }
        }

        foreach(ObjectConstructingCell cell in objects)
        {
            Instantiate(cell.targetObject, position: cell.position, Quaternion.identity);
        }
    }
}