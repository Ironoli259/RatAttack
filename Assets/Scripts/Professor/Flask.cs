using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flask : MonoBehaviour
{
    [SerializeField] GameObject effectPrefab;
    private Vector3 mousePos;
    private Rigidbody2D rb;    
    void Start()
    {
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
            Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    IEnumerator FlaskCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
