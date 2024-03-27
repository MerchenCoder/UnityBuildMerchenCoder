using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultCanvasManager : MonoBehaviour
{

    public Transform resultCanvas_parent;

    private void Start()
    {
        // SetResultCanvas();
    }
    public void SetResultCanvas()
    {
        if (GameManager.Instance.missionData.missionCode == "")
        {
            print("mission code 없음");
            return;
        }

        RemoveExistingResultCanvas();

        string resultCanvasName = "Canvas_Result_" + GameManager.Instance.missionData.missionCode;
        GameObject resultCanvas = Instantiate(Resources.Load<GameObject>("Prefabs/Panels/Canvas_Result_Panels/" + resultCanvasName));
        // Instantiate(resultCanvas);
        Camera uiCamera = GameObject.Find("UI_Camera").GetComponent<Camera>();
        resultCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        resultCanvas.GetComponent<Canvas>().worldCamera = uiCamera;
        resultCanvas.transform.SetParent(resultCanvas_parent, false);
        resultCanvas.transform.SetAsFirstSibling(); //부모의 제일 첫번째 자식이 되도록한다.

        NodeManager.Instance.resultCanvas = resultCanvas;
        resultCanvas.SetActive(false);

    }

    public void RemoveExistingResultCanvas()
    {
        foreach (Transform child in resultCanvas_parent)
        {
            if (child != null)
            {
                if (child.CompareTag("ResultCanvas"))
                {
                    Destroy(child.gameObject);
                    print("존재하는 결과 캔버스 삭제");
                }
            }
        }

    }
}
