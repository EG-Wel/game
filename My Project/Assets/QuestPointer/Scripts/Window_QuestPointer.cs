using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Window_QuestPointer : MonoBehaviour {

    [SerializeField] private Camera uiCamera;

    public Vector3 targetPosition;
    public RectTransform pointerRectTransform;
    private Image pointerImage;



    public Vector3 targetPositionScreenPoint;
    public bool isOffScreen;
    public Vector3 cappedTargetScreenPosition;
    public Vector3 pointerWorldPosition;





    private void Awake() {
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
        pointerImage = transform.Find("Pointer").GetComponent<Image>();

    }

    private void Update() {
        float borderSize = 250f;
        targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        if (isOffScreen)
        {            
            RotatePointerTowardsTargetPosition();

            pointerImage.enabled = true;
            cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;

            pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRectTransform.position = pointerWorldPosition;
            //print(new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f));
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
    }
        else
        {
            pointerImage.enabled = false;
            pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            pointerRectTransform.position = pointerWorldPosition;
            //print(new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f));
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

            pointerRectTransform.localEulerAngles = Vector3.zero;
        }
    }

    private void RotatePointerTowardsTargetPosition() {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    public void Show(Vector3 targetPosition)
    {
        //this.gameObject.SetActive(true);
        this.targetPosition = targetPosition;
        //print("dsaf");
    }
}