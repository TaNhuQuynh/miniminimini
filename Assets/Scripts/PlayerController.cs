using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Mover
{
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //Debug.Log(x);
        //Debug.Log(y);
        UpdateMotor(new Vector3(x, y, 0));
    }

}
   
