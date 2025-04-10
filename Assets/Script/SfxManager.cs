using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance;
    [SerializeField] private AudioSource sfxPrefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySfxClip(AudioClip audioClip, Transform spawnTransform, float volumn)
    {
        AudioSource audioSource = Instantiate(sfxPrefab, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volumn;
        audioSource.Play();
        float clipLenght = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLenght );
    }

    public void PlayRandomSfxClip(AudioClip[] audioClip, Transform spawnTransform, float volumn)
    {
        int rand = Random.Range( 0, audioClip.Length );
        AudioSource audioSource = Instantiate(sfxPrefab, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip[rand];
        audioSource.volume = volumn;
        audioSource.Play();
        float clipLenght = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLenght);
    }
}
