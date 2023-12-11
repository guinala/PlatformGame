using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int healthRestoration = 1;
    public GameObject lightingParticles;
    public GameObject burstParticles;

    private Collider2D _collider;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //Cure Player
            collision.SendMessageUpwards("AddHealth", healthRestoration);

            //Disable Collider
            _renderer.enabled = false;

            //Visual stuff
            _renderer.enabled = false;
            lightingParticles.SetActive(false);
            burstParticles.SetActive(false);

            Destroy(gameObject, 2f);
        }
    }

}
