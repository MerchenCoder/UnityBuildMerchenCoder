using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnaSleep : MonoBehaviour
{
    // 자식 객체들을 관리하기 위한 리스트
    private List<Transform> childObjects = new List<Transform>();

    // 이미지가 들어 있는 객체에 대한 참조
    public GameObject imageObject;

    public RectTransform imageRectTransform;

    void Start()
    {
        // 자식 객체들을 리스트에 추가
        foreach (Transform child in transform)
        {
            if (child.gameObject != imageObject)
            {
                childObjects.Add(child);
            }
        }
        if (imageObject != null)
        {
            imageRectTransform = imageObject.GetComponent<RectTransform>();
            //Debug.Log("Make ImageRectTransform Success");
        }
    }

    void Update()
    {
        if (GameManager.Instance.CheckPlayProgress("Chap2Start") || !DataManager.Instance.gameStateData.ch2MissionClear[0])
        {
            //Debug.Log("안나가 쓰러져있는 조건을 충족함");
            FlipImageY(true);
            RotateImage();
            //gameObject.transform.localPosition = new Vector3(12.4144f, -3.564708f, 0);
        }
    }

    bool rotated = false; // Add this line as a class member

    void RotateImage()
    {
        //Debug.Log("This is Function RotateImage");

        if (imageObject != null && !rotated)
        {
            // Rotate the main object
            imageObject.transform.localEulerAngles = new Vector3(0, 0, 270);
            //Debug.Log("Rotated imageObject to: " + imageObject.transform.localEulerAngles);

            // Find the CharCanvas child object
            Transform charCanvas = transform.Find("CharCanvas");

            if (charCanvas != null)
            {
                // Find the quiz_btn child within CharCanvas
                Transform quizBtn = charCanvas.Find("quiz_Btn");

                if (quizBtn != null)
                {
                    // Log current position
                    //Debug.Log("Current position of quiz_Btn: " + quizBtn.localPosition);

                    // Adjust position of the quiz_Btn
                    float newPosX = (float)-77.8;
                    float newPosY = (float)13;
                    quizBtn.localPosition = new Vector3(newPosX, newPosY, quizBtn.localPosition.z);

                    // Log new position
                    //Debug.Log("New position of quiz_Btn: " + quizBtn.localPosition);

                    // Adjust rotation of the quiz_Btn
                    quizBtn.localEulerAngles = new Vector3(180, 0, -270);
                    //Debug.Log("Adjusted rotation for quiz_Btn to: " + quizBtn.localEulerAngles);

                    // Set the flag to true to prevent further adjustments
                    rotated = true;
                }
            }
        }
    }

    void ResetImageRotation()
    {
        // AnnaSleepObject의 회전을 초기화
        if (imageObject != null)
        {
            transform.localEulerAngles = Vector3.zero;

            // 하위 객체들의 회전도 초기화
            foreach (Transform child in childObjects)
            {
                child.localEulerAngles = Vector3.zero;
            }
        }
    }

    void FlipImageY(bool flip)
    {
        //Debug.Log("This is Function FlipImageY");
        float scale = imageRectTransform.eulerAngles.y;
        scale = flip ? 180 : 0;
        //Debug.Log("SCale is " + scale);
        // Get the current rotation and modify the y-axis rotation
        Vector3 currentRotation = imageRectTransform.eulerAngles;
        currentRotation.y = scale;

        // Apply the new rotation
        imageRectTransform.eulerAngles = currentRotation;
    }
}
