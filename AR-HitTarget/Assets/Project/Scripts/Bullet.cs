using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Variável com ponteiro do script que administra a pontuação
    private ScoreManager scoreManager;

    void Start()
    {
        // Buscando o script que administra a pontuação
        scoreManager = GameObject.Find("Canvas/ScoreUI").GetComponent<ScoreManager>();
    }

    void OnCollisionEnter(Collision col)
    {
        // Se colidir com o alvo
        if (col.gameObject.name == "Target(Clone)")
        {
            // Ganha ponto e destroi ambos os objetos
            scoreManager.AddScore();
            Destroy(gameObject);
            Destroy(col.gameObject, 0.5f);
        }
    }
}
