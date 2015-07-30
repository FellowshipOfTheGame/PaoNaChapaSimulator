using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {
    private bool isFirst = false;
    private bool alreadyOrdered = false;
    private bool canDie = false;
    private bool isHappy = false;
    private bool isLeaving = false;
    private bool canTalk = true;

    private float dist = 0f;
    private int linePosition = 0;
    private Rigidbody2D rb2D;
    private GameObject target;
    private GameObject gm;
    private GameManager.Pedido pedido;
    private PolygonCollider2D poly;
    private Fila line;
    private Animator anim;

    public float time = 0.01666666f;
    public float vel = 0.5f;
    public Vector3 dialogOffset;
    public GameObject[] falas;

    [Range((int)0f, (int)1800f)]
    public int patience = 1800;

    public enum Falas
    {
        Chingar,
        Feliz
    }

	void Start () {
        poly = GetComponent<PolygonCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager");
        transform.Rotate(new Vector3(0,1,0), 180);
        line = target.GetComponent<Fila>();
        anim = GetComponent<Animator>();
	}
	
	void Update () {
        Move();

        transform.position.Set(transform.position.x, transform.position.y, -transform.position.y);

        if (patience > 0)
        {
            patience--;
        }
        else if (!isLeaving)
        {
            timeToDie();
        }

        if (isLeaving && isHappy)
        {
            timeToLeave();
        }
	}

    private void timeToDie()
    {
        line.removeEnemyWithTimer(linePosition);
        poly.enabled = false;
        canDie = true;
        isLeaving = true;

        Speak(Falas.Chingar);

        transform.Rotate(new Vector3(0, 1, 0), 180);
    }

    private void timeToLeave()
    {
        poly.enabled = false;
        canDie = true;
        isLeaving = false;

        Speak(Falas.Feliz);

        transform.Rotate(new Vector3(0, 1, 0), 180);
    }

    void Move()
    {
        Vector2 vec = rb2D.position;
        Vector2 targetPos;

        targetPos = new Vector2(target.transform.position.x, target.transform.position.y);

        vec = targetPos - vec;

        if (vec.magnitude < 1)
        {
            if (isFirst && !alreadyOrdered && canTalk)
            {
                gm.GetComponent<GameManager>().falaPlayer = true;
                canTalk = false;
            }
        }

        if (dist < vec.magnitude)
        {
            anim.SetBool("isWalking", true);
            vec.Normalize();
            rb2D.MovePosition(rb2D.position + (vec * time * vel * 0.8f));
        }
        else if (canDie)
        {
            anim.SetBool("isWalking", false);
            Destroy(gameObject);
        }
        else if (isFirst && !alreadyOrdered)
        {
            anim.SetBool("isWalking", false);
            alreadyOrdered = true;

            //FAZER PEDIDO
            Speak();
        }
        else if (!poly.enabled)
        {
            anim.SetBool("isWalking", false);
            poly.enabled = true;
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    private void Speak()
    {
        GameObject message;
        
        message = (GameObject)Instantiate(gm.GetComponent<GameManager>().pedidos[(int)pedido],
                                                            transform.position + dialogOffset,
                                                                         Quaternion.identity);

        message.transform.SetParent(transform);
    }

    private void Speak(Falas fala)
    {
        GameObject message;

        message = (GameObject)Instantiate(falas[(int) fala],
                                transform.position + dialogOffset,
                                             Quaternion.identity);

        message.transform.SetParent(transform);
    }

    public bool checkPedido(GameObject order)
    {
        if (order.tag == pedido.ToString())
        {
            return true;
        }
        return false;
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
    }

    public void setIsFirst(bool isFirst)
    {
        this.isFirst = isFirst;
    }

    public void setLinePosition(int pos)
    {
        this.linePosition = pos;
    }

    public void setHappiness(bool happy)
    {
        this.isHappy = happy;
    }

    public void setCollision (bool arrived)
    {
        poly.enabled = arrived;
    }

    public void setIsLeaving(bool leaving)
    {
        this.isLeaving = leaving;
    }

    public bool getIsLeaving()
    {
        return this.isLeaving;
    }
}
