using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class make_open : MonoBehaviour
{
    public GameObject coc;
    int a = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && a== 0)
        {
            coc.SetActive(true);
            a++;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            coc.SetActive(false);
        }
    }

}
