using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour, IPooledObject
{
    [SerializeField] string effectTag;
    private Vector3 mousePos;
    private Rigidbody2D rb;
    ObjectPooler objectPooler;

    public void OnObjectSpawn()
    {
        objectPooler = ObjectPooler.Instance;
        rb = GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * 5;
        StartCoroutine(FlaskCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            objectPooler.SpawnFromPool(effectTag, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    IEnumerator FlaskCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        objectPooler.SpawnFromPool(effectTag, transform.position, Quaternion.identity);
        gameObject.SetActive(false); ;
    }
}
