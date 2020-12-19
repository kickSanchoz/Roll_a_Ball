using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Material[] BallsMaterials;
    public CameraController Operator;

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        int index = PlayerPrefs.GetInt("PrefBall");
        Player.GetComponent<MeshRenderer>().material = BallsMaterials[index];
        Operator.player = Player;
    }

}
