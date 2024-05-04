namespace bloggie.web.Repositories;

public class CloudinaryImageRepository : IImageRepository
{
    public Task<string> UploadAsync(IFormFile file)
    {
        throw new NotImplementedException();
    }
}