using UnityEngine;
using System.Collections;

public class FixZ : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3 novo;
        Debug.Log(transform.name);
        Debug.Log(transform.position);
        novo = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        transform.position = novo;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
