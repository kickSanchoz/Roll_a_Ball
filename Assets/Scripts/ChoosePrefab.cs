using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePrefab : MonoBehaviour
{

    public void GetIndex(int index)
    {
        PlayerPrefs.SetInt("PrefBall", index);
    }
}
