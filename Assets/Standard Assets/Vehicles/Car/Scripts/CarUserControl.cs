using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        public float Accel;
        public float Steer;
        public float Brake;
        public void AccelInput(float input) {  Accel = input; }
        public void BrakeInput(float input) {  Brake = input; }
        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
           // float h = CrossPlatformInputManager.GetAxis("Horizontal");
          // Accel = CrossPlatformInputManager.GetAxis("Vertical");
//#if !MOBILE_INPUT
          // Brake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(Steer, Accel, Accel, Brake);
//#else
      //      m_Car.Move(h, v, v, 0f);
//#endif
        }
    }
}
