using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterRender : MonoBehaviour
{
    public RawImage TargetImage;
    public GameObject RootToRender;
    public Transform playerTransform;

    Camera m_Camera;
    RenderTexture m_TargetTexture;

    // Start is called before the first frame update
    void Start()
    {
        m_TargetTexture = new RenderTexture((int)TargetImage.rectTransform.rect.width * 2, (int)TargetImage.rectTransform.rect.height * 2, 16, RenderTextureFormat.ARGB32);

        TargetImage.texture = m_TargetTexture;

        GameObject cameObject = new GameObject();
        m_Camera = cameObject.AddComponent<Camera>();
        m_Camera.enabled = false;

        m_Camera.clearFlags = CameraClearFlags.SolidColor;
        m_Camera.backgroundColor = new Color(0, 0, 0, 0);
        m_Camera.targetTexture = m_TargetTexture;
        m_Camera.cullingMask = (1 << LayerMask.NameToLayer("Character")) | (1 << LayerMask.NameToLayer("CharacterEquipment"));
    }

    // Update is called once per frame
    void Update()
    {
        m_Camera.transform.position = playerTransform.position + playerTransform.forward * 1.7f + Vector3.up * 1.6f;
        m_Camera.transform.LookAt(playerTransform.position + Vector3.up * 1.0f);

        m_Camera.Render();
    }
}
