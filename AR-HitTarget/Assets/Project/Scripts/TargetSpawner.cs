using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{

    // Prefab do alvo
    public GameObject targetPrefab;
    
	void Start ()
    {
        // Fica instanciando alvos a cada 7 segundos
        InvokeRepeating("IntantiateTarget", 7f, 7f);
	}

    void IntantiateTarget()
    {
        // Instancia até 3 alvos
        if (transform.childCount < 3)
        {
            // Instancia um alvo na posição do objeto vazio Targets
            GameObject targetInstance = Instantiate(targetPrefab, transform.position, Quaternion.identity) as GameObject;
            // Deixa o gato olhando para a câmera
            targetInstance.transform.Rotate(0, 180, 0);
            // Deixa o gato (alvo) como filho do objeto vazio Targets
            targetInstance.transform.parent = transform;
        }
    }
}
