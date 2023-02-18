using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    void Start()
    {
        transform.position = new Vector3(0, 8, 0);
    }


    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y >= -3.8f)
        {
            Destroy(this.gameObject);
        }

    }
}
