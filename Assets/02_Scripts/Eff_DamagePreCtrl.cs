using UnityEngine;

public class Eff_DamagePreCtrl : MonoBehaviour
{
    public ParticleSystem particle;
    
    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        particle.Play();
        Invoke(nameof(SetDeActive), 0.5f);
    }

    void SetDeActive()
    {
        particle.Stop();
        gameObject.SetActive(false);   
    }
}
