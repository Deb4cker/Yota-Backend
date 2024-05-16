using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yota_backend.Model;

namespace Yota_backend.Model.Configuration;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.ToTable("Tracks");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.Bytes)
            .IsRequired();

        builder.Property(t => t.Composer)
            .HasMaxLength(100);

        builder.Property(t => t.Milliseconds)
            .IsRequired();

        builder.Property(t => t.FilePath)
            .IsRequired();

        builder.Property(t => t.ArtistId)
            .IsRequired();

        builder.Property(t => t.GenreId)
            .IsRequired();

        builder.Property(t => t.AlbumId)
            .IsRequired();
    }
}
