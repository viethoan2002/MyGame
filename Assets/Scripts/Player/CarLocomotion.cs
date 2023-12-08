using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Axel
{
    Front,
    Back
}

[Serializable]
public class Wheel
{
    public GameObject wheelMesh;
    public WheelCollider wheelcollider;
    public Axel axel;
}

public class CarLocomotion : MonoBehaviour
{
    public float maxSpace;
    public float localVelocityZ;
    public CarStats carStats;

    public List<Wheel> wheels = new List<Wheel>();

    public float maxAcceleration = 30.0f;
    public float brakeAccleration = 50.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass;

    public Rigidbody carRb;


    private void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carStats = GetComponentInChildren<CarStats>();
        carRb.centerOfMass = _centerOfMass;
    }

    private void Update()
    {
        WheelsRotate();
        localVelocityZ = transform.InverseTransformDirection(carRb.velocity).z;
    }

    private void LateUpdate()
    {
        HandleSteer();
        if (InputHandle.Instance.vertical > 0 && carStats.currentPetrol > 0)
        {
            GoForward();
            carStats.isMove = true;
        }
        if (InputHandle.Instance.vertical < 0 && carStats.currentPetrol > 0)
        {
            GoBack();
            carStats.isMove = true;
        }
        if (InputHandle.Instance.vertical == 0 || carStats.currentPetrol <= 0)
        {
            ThrottleOff();
            carStats.isMove = false;
        }

        DisplayCar.Instance.setCarSpeedUI(Mathf.Abs(localVelocityZ) / maxSpace);
    }

    private void GoForward()
    {
        if (localVelocityZ < -0.1f)
        {
            Brakes();
        }
        else
        {
            if (Mathf.Abs(localVelocityZ) < maxSpace)
            {
                foreach (var wheel in wheels)
                {
                    wheel.wheelcollider.brakeTorque = 0;
                    wheel.wheelcollider.motorTorque = 10 * maxAcceleration;
                }
            }
            else
            {
                foreach (var wheel in wheels)
                {
                    wheel.wheelcollider.motorTorque = 0;
                }
            }
        }
    }

    private void GoBack()
    {
        if (localVelocityZ > 0.1f)
        {
            Brakes();
        }
        else
        {
            if (Mathf.Abs(localVelocityZ) < maxSpace)
            {
                foreach (var wheel in wheels)
                {
                    wheel.wheelcollider.brakeTorque = 0;
                    wheel.wheelcollider.motorTorque = -10 * maxAcceleration;
                }
            }
            else
            {
                foreach (var wheel in wheels)
                {
                    wheel.wheelcollider.motorTorque = 0;
                }
            }
        }           
    }

    private void HandleSteer()
    {
        foreach(var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = InputHandle.Instance.horizontal * turnSensitivity * maxSteerAngle;
                wheel.wheelcollider.steerAngle = Mathf.Lerp(wheel.wheelcollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    private void HandleMovement()
    {
        if (Mathf.Abs(localVelocityZ) > 0)
        {
            var throttleAxis = localVelocityZ > 0 ? 1 : -1;
            foreach (var wheel in wheels)
            {
                wheel.wheelcollider.brakeTorque = 0;
                wheel.wheelcollider.motorTorque = throttleAxis * 100 * maxAcceleration;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelcollider.motorTorque = 0;
            }
        }
 
    }

    private void WheelsRotate()
    {
        foreach(var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelcollider.GetWorldPose(out pos, out rot);
            wheel.wheelMesh.transform.position = pos;
            wheel.wheelMesh.transform.rotation = rot;
        }
    }

    private void HandleBreak()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            foreach(var wheel in wheels)
            {
                wheel.wheelcollider.brakeTorque = 0;
                wheel.wheelcollider.brakeTorque = 1000 * brakeAccleration;
            }
        }
        else
        {
            foreach(var wheel in wheels)
            {
                wheel.wheelcollider.brakeTorque = 0;
            }
        }
    }

    public void Brakes()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelcollider.brakeTorque = 500;
        }
    }

    public void ThrottleOff()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelcollider.motorTorque = 0;
            wheel.wheelcollider.brakeTorque = 200;
        }
    }
}
