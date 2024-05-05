namespace bloggie.web.Repositories;

public interface IImageRepository
{
    Task<string?> UploadAsync(IFormFile file);
}