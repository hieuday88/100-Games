using System;
using UnityEngine;

public class InfoFruit : MonoBehaviour
{
    private int level;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    private bool isCollider;
    public bool IsCollider
    {
        get { return isCollider; }
    }

    private Action<InfoFruit, InfoFruit, int> onMerge;
    private Rigidbody2D rb;
    private Collider2D collider2D;

    public void Init(int level, Action<InfoFruit, InfoFruit, int> onMerge, bool isFall = false)
    {
        this.level = level;
        this.onMerge = onMerge;
        this.isCollider = false;
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (collider2D == null)
            collider2D = GetComponent<Collider2D>();
        if (isFall)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            collider2D.isTrigger = false;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            collider2D.isTrigger = true;
        }
    }

    public void OnFall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        collider2D.isTrigger = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out InfoFruit infoFruit))
        {
            if (level + 1 >= 11) return;
            if (infoFruit.level == level)
            {
                onMerge?.Invoke(this, infoFruit, level + 1);
                isCollider = true;
                infoFruit.isCollider = true;
            }
        }
    }

}
