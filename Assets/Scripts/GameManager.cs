using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject[] enemy;
    public Player player;
    public GameObject[] target;
    public GameObject[] pedidos;
    public Pedido pedido;
    //public AnimationCurve numsei;

    public float maxItems = 3;
    public float xMin = -0.1f;
    public float xMax = 0.1f;
    public float yMin = -0.1f;
    public float yMax = 0.1f;

    public bool falaPlayer = false;

    public float levelTime = 0f;
    public float spawnDelayMin;
    public float spawnDelayMax;

    private float timer = 0f;

    public enum Pedido
    {
        Bolo,
        PaoNaChapa,
        PaoDeQueijo,
        Cafe
    }

	// Use this for initialization
	void Start () {
        timer = Random.Range(spawnDelayMin, spawnDelayMax);
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetButtonDown("Spawn"))
        //if(Random.Range(0,1) < numsei.Evaluate(Time.timeSinceLevelLoad))
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            SpawnEnemy();
            timer = Random.Range(spawnDelayMin, spawnDelayMax);
        }
        else if (falaPlayer)
        {
            player.GetComponent<Player>().Speak();
            falaPlayer = false;
        }
        if(levelTime < Time.timeSinceLevelLoad)
        {
            Application.LoadLevel("gameover");
        }        
	}

    private void SpawnEnemy()
    {
        Vector3 rndPosWithin;
        GameObject thing;
        NPC npc;
        int rand;
        float randY = Random.Range(yMin, yMax);

        rndPosWithin = new Vector3(Random.Range(xMin, xMax), randY, randY);
        rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
        thing = (GameObject) Instantiate(enemy[Random.Range(0,enemy.Length)], rndPosWithin, transform.rotation);

        rand = Random.Range(0, target.Length);

        npc = thing.GetComponent<NPC>();
        npc.setTarget(target[rand]);

        pedido = (Pedido) Random.Range(0f, maxItems);
        Debug.Log(pedido);
        
        npc.setPedido(pedido);

        target[rand].GetComponent<Fila>().addEnemy(thing);
    }

    public Vector3 getRandomPosition()
    {
        Vector3 rndPosWithin;
        float randY = Random.Range(yMin, yMax);
        
        rndPosWithin = new Vector3(Random.Range(xMin, xMax), randY, randY);
        rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);

        return rndPosWithin;
    }
}
