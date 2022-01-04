using UnityEngine;
using System.Collections;

public class FireplaceController : MonoBehaviour
{

    [SerializeField] LocationPlate locationPlate;
    [SerializeField] ParticleSystem particles;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (locationPlate.getState())
        {
            var emmision = particles.emission;
            emmision.enabled = true;
            audioSource.enabled = true;
        }
        else
        {
            var emmision = particles.emission;
            emmision.enabled = false;
            audioSource.enabled = false;
        }
    }
}
