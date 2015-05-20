using UnityEngine;
using System.Collections;

public class Loja : MonoBehaviour {
    public GameObject item;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public GameObject InstantiateItem()
    {
        return Instantiate(item);
    }
}
