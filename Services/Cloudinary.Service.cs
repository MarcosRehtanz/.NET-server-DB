using CloudinaryDotNet.Actions;

namespace CloudinaryDotNet.Services;
public class InputCloudinary
{
    public required string Image { get; set; }
}
class Configuration
{
    public static Cloudinary Cloudinary()
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<Program>();
        var configuration = builder.Build();
        string secretValue = configuration["CLOUDINARY_KEY"];
        Cloudinary cloudinary = new(secretValue);
        cloudinary.Api.Secure = true;
        return cloudinary;
    }
}
public class CloudinaryServices
{
    private static Cloudinary cloudinary { get; } = Configuration.Cloudinary();
    public static async Task<ImageUploadResult> Upload(string img)
    {
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(img),
            Folder = "images"
        };
        return await cloudinary.UploadAsync(uploadParams);
    }
    public static async Task<DelResResult> Delete(string publicIds)
    {
        var deletionParams = new DelResParams()
        {
            PublicIds = [publicIds]
        };
        var result = await cloudinary.DeleteResourcesAsync(deletionParams);
        return result;
    }
}