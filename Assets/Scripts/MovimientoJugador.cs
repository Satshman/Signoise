using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] private float velocidad = 3f;

    private Rigidbody2D jugadorRb;
    private Vector2 entradasMovimiento;

    void Start()
    {
        jugadorRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Inputs
        float moverX = Input.GetAxisRaw("Horizontal");
        float moverY = Input.GetAxisRaw("Vertical");
        entradasMovimiento = new Vector2(moverX, moverY).normalized;
    }

    private void FixedUpdate()
    {
        //Fisicas
        jugadorRb.MovePosition(jugadorRb.position + entradasMovimiento * velocidad * Time.fixedDeltaTime);
    }
}
