﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс, отвечающий за вращение игровых очков
public class Rotator : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
