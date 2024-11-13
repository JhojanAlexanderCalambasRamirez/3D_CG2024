using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Asigna el componente VideoPlayer en el inspector

    private void Start()
    {
        // Asegúrate de que el VideoPlayer esté configurado para reproducir al iniciar la escena
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Registrar el evento para cuando el video termine
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Cambia a la escena "Ciudad" cuando el video termine
        SceneManager.LoadScene("Ciudad");
    }
}
