using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
    private bool isFirst = false;
    private bool alreadyOrdered = true;
    private bool canDie = false;
    private bool isAngry = false;
    private float dist = 0f;
    private int linePosition = 0;
    private Rigidbody2D rb2D;
    private GameObject target;
    private GameObject gm;
    private GameManager.Pedido pedido;
    private PolygonCollider2D poly;

    public float time = 0.01666666f;
    public float vel = 0.5f;
    public Vector3 dialogOffset;
    public GameObject[] falas;

    [Range((int)0f, (int)1800f)]
    public int patience = 1800;

    public enum Falas
    {
        Chingar
    }

	// Use this for initialization
	void Start () {
        poly = GetComponent<PolygonCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager");
        transform.Rotate(new Vector3(0,1,0), 180);
	}
	
	// Update is called once per frame
	void Update () {
        Move();

        transform.position.Set(transform.position.x, transform.position.y, -transform.position.y);

        if (patience > 0)
        {
            patience--;
        }
        else
        {
            if (!isAngry)
            {
                timeToDie();
            }
        }
	}

    void timeToDie()
    {
        target.GetComponent<Fila>().removeEnemyWithTimer(linePosition);
        poly.enabled = false;
        canDie = true;
        
        GameObject order = (GameObject)Instantiate(falas[(int)new Falas()],
                                        transform.position + dialogOffset,
                                                        Quaternion.identity);
        order.transform.SetParent(transform);

        transform.Rotate(new Vector3(0, 1, 0), 180);
    }

    void Move()
    {
        Vector2 vec = rb2D.position;
        Vector2 targetPos;

        targetPos = new Vector2(target.transform.position.x, target.transform.position.y);

        vec = targetPos - vec;

        if (dist < vec.magnitude)
        {
            vec.Normalize();
            rb2D.MovePosition(rb2D.position + (vec * time * vel * 0.8f));
        }
        else if (canDie)
        {
            Destroy(gameObject);
        }
        else if (isFirst && alreadyOrdered)
        {
            alreadyOrdered = false;

            GameObject order = (GameObject)Instantiate(gm.GetComponent<GameManager>().pedidos[(int)pedido],
                transform.position + dialogOffset,
                Quaternion.identity);

            order.transform.SetParent(transform);
        }
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
    }

    public void setDist(float dist)
    {
        this.dist = dist;
    }

    public void setDist(float dist, int pos)
    {
        this.dist = dist;
        this.linePosition = pos;
    }

    public void setPedido(GameManager.Pedido pedido)
    {
        this.pedido = pedido;
        Debug.Log("NPC " + pedido);
    }

    public void setIsFirst(bool isFirst)
    {
        this.isFirst = isFirst;
    }

    public void setLinePosition(int pos)
    {
        this.linePosition = pos;
    }

    public void setAngriness(bool angry)
    {
        this.isAngry = angry;
    }

    public bool checkPedido(GameObject order)
    {
        if (order.tag == pedido.ToString())
        {
            return true;
        }
        return false;
    }
}
