using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

    // Variável para manter um ponteiro ao componente de animação
    private Animator catAnimator;
    // Movimento do gato
    private float move = 0f;
    // Estado do gato (pode ser parado, rotacionando ou em movimento)
    private int catState = 0;

	// Use this for initialization
	void Start ()
    {
        // Buscando o ponteiro do componente de animação
        catAnimator = GetComponent<Animator>();
        // Faz uma decisão a cada 3 segundos
        InvokeRepeating("MakeDecision", 2f, 3f);
    }

    /// <summary>
    /// Random decision
    /// </summary>
    void MakeDecision()
    {
        // Se o gato está tombado, levantá-lo
        if (transform.rotation.x != 0 || transform.rotation.z != 0)
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        // Decisão sobre o que o gato vai fazer
        catState = Random.Range(0, 6);
        switch (catState)
        {
            case 0: // parar
                ChangeMovement(0);
                break;
            case 1: // rotacionar
                Rotating(-90);
                break;
            case 2: // rotacionar
                Rotating(90);
                break;
            case 3: // andar
                ChangeMovement(1);
                break;
            case 4: // andar
                ChangeMovement(1);
                break;
            case 5: // andar
                ChangeMovement(1);
                break;
        }
    }

    private void Update()
    {
        // Move o gato para frente se a variável move != 0
        transform.position += transform.forward * move * Time.deltaTime;
        // Se o gato cair, destrui-lo
        if (transform.position.y < -1)
            Destroy(gameObject);
    }

    void Rotating(float angle)
    {
        // Rotaciona o gato
        catAnimator.SetFloat("Speed", 0);
        transform.Rotate(transform.up * angle);
        move = 0f;
    }

    void ChangeMovement(int moveSpeed)
    {
        // Altera movimento do gato
        catAnimator.SetFloat("Speed", moveSpeed);
        move = moveSpeed * 0.04f;
    }
}
