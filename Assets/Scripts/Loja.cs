using UnityEngine;
using System.Collections;

public class Loja : MonoBehaviour {
    public GameObject item;
    public float value;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public GameObject InstantiateItem()
    {
        GameObject obj = Instantiate(item);

        obj.transform.position = new Vector3(100, 100, 100);

        return obj;
    }

    public float getValue()
    {
        return this.value;
    }
}
