using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public float angleY;
    public int idWeapon;
    public int idSkin;


    public PlayerData(Vector3 _position, float _angleY, int _idWeapon,int _idSkin)
    {
        position = new float[3];
        position[0] = _position.x;
        position[1] = _position.y;
        position[2] = _position.z;
        angleY=_angleY;
        idWeapon = _idWeapon;
        idSkin = _idSkin;

    }
}
