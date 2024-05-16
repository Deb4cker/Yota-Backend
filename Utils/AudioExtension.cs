namespace Yota_backend.Utils;

public class AudioExtension
{
    public static bool IsAudioFormat(string contentType)
    {
        return contentType.Contains(".mp3");
    }

    public static long GetAudioDuration(IFormFile file)
    {
        return 100000;
    }
}
