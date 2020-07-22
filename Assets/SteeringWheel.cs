using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;
public class SteeringWheel : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    private bool Wheelbeingheld = false;
    public RectTransform Wheel;
    private float WheelAngle = 0f;
    private float LastWheelAngle = 0f;
    private Vector2 center;
    public float MaxSteerAngle = 200f;
    public float ReleaseSpeed = 300f;
    public float OutPut;
    public CarUserControl Car;
    void Update()
    {
        if(!Wheelbeingheld&&WheelAngle != 0f)
        {
            float DeltaAngle = ReleaseSpeed * Time.deltaTime;
            if (Mathf.Abs(DeltaAngle) > Mathf.Abs(WheelAngle))
                WheelAngle = 0f;
            else if (WheelAngle > 0f)
                WheelAngle -= DeltaAngle;
            else
                WheelAngle += DeltaAngle;
        }
        Wheel.localEulerAngles = new Vector3(0, 0, -MaxSteerAngle*OutPut);
            OutPut = WheelAngle / MaxSteerAngle;
        Car.Steer = OutPut;
    }
    public void OnPointerDown(PointerEventData data)
    {
        Wheelbeingheld = true;
        center = RectTransformUtility.WorldToScreenPoint(data.pressEventCamera, Wheel.position);
        LastWheelAngle = Vector2.Angle(Vector2.up, data.position - center);
    }
    public void OnDrag(PointerEventData data)
    {
        float NewAngle = Vector2.Angle(Vector2.up, data.position - center);
        if((data.position-center).sqrMagnitude >= 400)
        {
            if (data.position.x > center.x)
                WheelAngle += NewAngle - LastWheelAngle;
            else
                WheelAngle -= NewAngle - LastWheelAngle;
        }
        WheelAngle = Mathf.Clamp(WheelAngle, -MaxSteerAngle, MaxSteerAngle);
        LastWheelAngle = NewAngle;
    }
    public void OnPointerUp(PointerEventData data)
    {
        OnDrag(data);
        Wheelbeingheld = false;
    }
}
