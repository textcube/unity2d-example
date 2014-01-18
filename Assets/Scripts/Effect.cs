using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {
    void OnEnd()
    {
        Destroy(gameObject);
    }
}
