using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookItem : MonoBehaviour
{
 

   public void OnLookItemBox(bool isLookat)
    {
        PlayerMove.isStopped = isLookat;
    }
}
