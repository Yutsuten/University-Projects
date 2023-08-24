using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

// Para fazer o som de atirar, é necessário ter o AudioSource
[RequireComponent(typeof(AudioSource))]
public class FireBullet : MonoBehaviour, IVirtualButtonEventHandler
{
    // Prefab da bala
    public GameObject bulletPrefab;
    // Local onde o tiro será instanciado
    private GameObject bulletSpawnPlace;
    // Botão (virtual) que dispara o tiro
    private GameObject gunTrigger;
    // Animação quando é feito um disparo
    private ParticleSystem fireAnimation;
    // UI informando quantas munições ainda tem
    private Text bulletsNumberUI;
    // Fonte de som para executar som do disparo
    private AudioSource fireSound;

    // Variáveis da arma e do tiro
    private int bulletSpeed = 80;
    private int numberBullets = 5;
    private bool reloading = false;
    private bool firing = false;

    // Use this for initialization
    void Start ()
    {
        // Buscando o Game Object do botão virtual que dispara o tiro
        gunTrigger = transform.FindChild("GunTrigger").gameObject;
        // Buscando o Game Object do local a ser instanciado o tiro
        bulletSpawnPlace = transform.FindChild("FrontloaderToon/bulletSpawnPlace").gameObject;
        // Buscando a UI que apresenta o texto de quantas munições a arma tem
        bulletsNumberUI = GameObject.Find("Canvas/BulletsUI").GetComponent<Text>();
        // Buscando o Particle System para animação no tiro
        fireAnimation = transform.FindChild("FrontloaderToon/bulletSpawnPlace").GetComponent<ParticleSystem>();
        // Ponteiro para o componente que irá executar o som do disparo
        fireSound = GetComponent<AudioSource>();

        // Registra o botão virtual
        gunTrigger.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }
    
    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        // Cancela qualquer outra chamada à função que efetua o disparo
        CancelInvoke("Fire");
        // Se for possível atirar, atira
        if (!reloading && !firing && numberBullets >= 1)
        {
            firing = true;
            InvokeRepeating("Fire", 0f, 0.7f);
        }
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        // Não está mais apertando o botão de disparar
        firing = false;
        CancelInvoke("Fire");
        // Se a arma está descarregada, recarrega-a
        if (numberBullets < 1)
        {
            reloading = true;
            Invoke("Reload", 2f);
            UpdateUI(string.Format("Bullets: {0}\nReloading...", numberBullets));
        }
    }

    private void Fire()
    {
        // Verifica se tem munição suficiente para efetuar o disparo
        if (numberBullets < 1)
        {
            CancelInvoke("Fire");
            return;
        }
        // Efetua o disparo
        var bulletInstance = Instantiate(bulletPrefab, bulletSpawnPlace.transform.position, Quaternion.identity) as GameObject;
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletSpawnPlace.transform.forward * bulletSpeed);
        numberBullets--;
        // Ativa som e animação
        fireAnimation.Play();
        fireSound.Play();
        // Atualiza UI com o número de munições disponíveis
        UpdateUI(string.Format("Bullets: {0}", numberBullets));
    }

    private void Reload()
    {
        // Recarrega as munições, atualiza na UI e permite que novos disparos sejam efetuados
        numberBullets = 5;
        UpdateUI(string.Format("Bullets: {0}", numberBullets));
        reloading = false;
    }

    private void UpdateUI(string text)
    {
        // Atualiza o texto com o número de munições
        bulletsNumberUI.text = text;
    }
}
