using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrosManager : MonoBehaviour
{
    public Vector2 kk;
    public Texture2D cursor;

    private void Start()
    {
        Cursor.SetCursor(cursor, kk, CursorMode.Auto);
    }
}
