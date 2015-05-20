using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {
    private int time = 0;
    
    public int lifeTime;
	
	void Update () {
        time++;

        if (time == lifeTime)
        {
            GetComponent<Animator>().SetBool("isDying", true);
        }
	}

    public void Die()
    {
        Destroy(gameObject);
    }
}
