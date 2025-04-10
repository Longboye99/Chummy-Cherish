using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class DreamCutscene : MonoBehaviour
{
    [SerializeField] private VideoClip clip;
    private float clipLength;
    private void Awake()
    {
        clipLength = (float)clip.length;
        StartCoroutine(WaitClipDuration());
    }

    private IEnumerator WaitClipDuration()
    {
        yield return new WaitForSeconds(clipLength);
        SceneManager.LoadScene(0);
    }
}
