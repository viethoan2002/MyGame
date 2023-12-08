using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPosition : MonoBehaviour
{
    private Rigidbody rb;
    private Renderer rendererObj;

    public GameObject outline;
    public bool isPlayerHere;
    public GameObject Player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rendererObj = GetComponent<Renderer>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            isPlayerHere = true;
            Player = other.gameObject;
            SetRender(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            isPlayerHere = false;
            Player.transform.GetComponent<PlayerInventory>().carController = null;
            Player = null;
            InputHandle.Instance.use = false;
            SetRender(false);
        }
    }

    public void SetRender(bool isEn)
    {
        outline.SetActive(isEn);
    }
}
