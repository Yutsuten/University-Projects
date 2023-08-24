using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    // Componente que escreve a pontuação no UI
    private Text scoreUI;
    // Contador de pontos
    private int score = 0;

    void Start()
    {
        // Referência ao componente que escreve no local da pontuação
        scoreUI = gameObject.GetComponent<Text>();
    }

    public void AddScore()
    {
        // Atualiza os pontos, considerando que conseguiu mais um
        scoreUI.text = string.Format("Score: {0}", ++score);
    }
}
