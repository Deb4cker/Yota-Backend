namespace Yota_backend.Commons;

public static class Constants
{
    private const string PersistedFilesPath = "PersistedFiles/";
    private const string Covers = "Covers/";
    private const string CoversPath = PersistedFilesPath + Covers;
    
    public const string MusicFilePath = PersistedFilesPath + "Musics/";
    public const string AlbumCoverPath = CoversPath + "Album" + Covers;
    public const string PlaylistCoverPath = CoversPath + "Playlist" + Covers;

    public const string DefaultAlbumCoverImage = AlbumCoverPath + "DefaultAlbumCover.png";
    public const string DefaultPlaylistCoverImage = PlaylistCoverPath + "DefaultPlaylistCover.png";
}