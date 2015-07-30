using UnityEngine;
using System.Collections;

public class Dinheiro : MonoBehaviour {
    public float vel = 1f;
    public float dissapear = 0.01f;
    public float lifeTime = 100;

    private float time = 0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + new Vector3(0,1,0) * vel * 0.01666666f;
        time++;

        if (time == lifeTime)
        {
            //Destroy(gameObject);
            GetComponent<Animator>().SetBool("isDying", true);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
