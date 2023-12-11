using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int suma = 1;
    

    private Collider2D _collider;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Cure Player
            collision.SendMessageUpwards("AddCoin", suma);

            //Disable Collider
            _renderer.enabled = false;

            //Visual stuff
            _renderer.enabled = false;
         

            Destroy(gameObject, 2f);
        }
    }

}
