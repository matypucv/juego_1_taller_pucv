using UnityEngine;


public class BallImpactParticles : MonoBehaviour
{
    public float minSpeedToEmit = 5f; 
    public ParticleSystem impactParticles;
    [SerializeField] AudioSource audio;
    [SerializeField] float pitchRange;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float speed = rb.linearVelocity.magnitude;

        if (speed >= minSpeedToEmit)
        {
            if (impactParticles != null)
            {
                ContactPoint2D contact = collision.contacts[0];
                impactParticles.transform.position = contact.point;

                impactParticles.Play();
                PlayAudio();
            }
        }
    }

    void PlayAudio()
    {
        audio.pitch = Random.value * 2 + 1;
        audio.Play();
    }
}