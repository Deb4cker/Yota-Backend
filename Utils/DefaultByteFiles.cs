using Yota_backend.Commons;

namespace Yota_backend.Utils;

public static class DefaultByteFiles
{
    private static readonly byte[] DefaultAlbumCoverBytes = File.ReadAllBytesAsync(Constants.AlbumCoverPath).Result;
    private static readonly byte[] DefaultPlaylistCoverBytes = File.ReadAllBytesAsync(Constants.PlaylistCoverPath).Result;

    public static byte[] GetDefaultAlbumCoverBytes() => DefaultAlbumCoverBytes; 
    public static byte[] GetDefaultPlaylistCoverBytes() => DefaultPlaylistCoverBytes;
}