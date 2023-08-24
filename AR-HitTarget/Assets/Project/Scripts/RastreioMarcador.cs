using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class RastreioMarcador : MonoBehaviour, ITrackableEventHandler {

    // Variável para pegar o estado do marcador
    private TrackableBehaviour mTrackableBehaviour;

    void Start ()
    {
        // Carrega o componente que verifica o estado do marcador
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            // Permite alterar a conduta de cada estado do marcador
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
	}

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            // Se o marcador está visível, chama o método OnTrackingFound
            OnTrackingFound();
        }
        else
        {
            // Se o marcador não está visivel, chama o método OnTrackingLost
            OnTrackingLost();
        }
    }

    private void OnTrackingFound()
    {
        // Pegando os componentes de renderização e colisão de todos os "filhos"
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
        Collider[] colliderComponents = GetComponentsInChildren<Collider>();

        // Percorre todos os "filhos" e os ativa
        for (int i = 0; i < this.transform.GetChildCount(); ++i)
        {
            Debug.Log("Ativando os filhos");
            this.transform.GetChild(i).gameObject.SetActive(true);
        }

        // Ativa os renderizadores de todos os "filhos"
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Ativa os colisores de todos os "filhos"
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }

        Debug.Log("Rastreamento de " + mTrackableBehaviour.TrackableName + " encontrado");
    }

    private void OnTrackingLost()
    {
        // Pegando os componentes de renderização e colisão de todos os "filhos"
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
        Collider[] colliderComponents = GetComponentsInChildren<Collider>();

        // Percorre todos os "filhos" e os desativa
        for (int i = 0; i < this.transform.GetChildCount(); ++i)
        {
            Debug.Log("Ativando os filhos");
            this.transform.GetChild(i).gameObject.SetActive(false);
        }

        // Desativa os renderizadores de todos os "filhos"
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Desativa os colisores de todos os "filhos"
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

        Debug.Log("Rastreamento de " + mTrackableBehaviour.TrackableName + " perdido");
    }
}
