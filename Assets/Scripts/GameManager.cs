using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject enemy;
    public Player player;
    public GameObject[] target;
    public GameObject[] pedidos;
    public Pedido pedido;

    public float maxItems = 3;
    public float xMin = -0.1f;
    public float xMax = 0.1f;
    public float yMin = -0.1f;
    public float yMax = 0.1f;

    public bool falaPlayer = false;

    public enum Pedido
    {
        Bolo,
        PaoNaChapa,
        PaoDeQueijo,
        Cafe
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Spawn"))
        {
            SpawnEnemy();
        }
        else if (falaPlayer)
        {
            player.GetComponent<Player>().Speak();
            falaPlayer = false;
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
        thing = (GameObject) Instantiate(enemy, rndPosWithin, transform.rotation);

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
