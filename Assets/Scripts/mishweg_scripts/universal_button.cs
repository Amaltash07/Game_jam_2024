using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class universal_button : MonoBehaviour
{
    public void Changescene(string sceneName)
    {
        level_manager.Instance.LoadScene(sceneName);
    }
}
