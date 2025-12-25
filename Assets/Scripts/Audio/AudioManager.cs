using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSFX;

    public void PlayButtonClickSFX() => audioSource.PlayOneShot(buttonClickSFX);
}
