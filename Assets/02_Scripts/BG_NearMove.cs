using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_NearMove : MonoBehaviour
{
    private Transform tr;
    private float speed;
    private float width;
    private BoxCollider2D boxCollider2D;

    IEnumerator Start()
    {
        speed = 25.0f;
        tr = GetComponent<Transform>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        width = boxCollider2D.size.x;
        yield return null;
        StartCoroutine(BackGroundLoop());
    }

    IEnumerator BackGroundLoop()
    {
        while (!GameManager.instance.isGameOver)
        {
            tr.Translate(Vector3.left * speed * Time.deltaTime);
            if (tr.position.x <= -width * 2f)
                RePosition();
            yield return new WaitForSeconds(0.02f);
        }
    }
    void RePosition()
    {
        Vector2 offset = new Vector2(width * 3f, 0.0f);
        tr.position = (Vector2)tr.position + offset;
    }

}
