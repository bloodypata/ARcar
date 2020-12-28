using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public Joystick joystick;
    public float rotateCar;
    public float moveCar;

    void Start()
    {
        joystick = GameObject.Find("Canvas/Fixed Joystick").GetComponent<Joystick>();
    }

    void Update()
    {

        rotateCar = joystick.Horizontal * 2f;
        moveCar = joystick.Vertical * Time.deltaTime * 2f;
        transform.Rotate(0, rotateCar, 0);
        transform.Translate(0, 0, moveCar);

    }

}

