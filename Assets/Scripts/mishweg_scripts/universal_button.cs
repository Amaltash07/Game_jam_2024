using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class universal_button : MonoBehaviour
{
    public void Changescene(int buildIndex)
    {
        level_manager.Instance.LoadScene(buildIndex);
    }
}
