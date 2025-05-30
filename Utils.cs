using SixLabors.ImageSharp.Formats.Jpeg;
using System.Numerics;

namespace EsportApp
{
    public static class Utils
    {

        public static async Task<byte[]> FileToBytes(Microsoft.AspNetCore.Http.IFormFile file)
        {
            using var ms = new System.IO.MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }

        public static string getAvatarBase64Photo(byte[] Photo)
        {
            if (Photo != null && Photo.Length > 0)
                return Convert.ToBase64String(Photo);
            else
                return Convert.ToBase64String(System.IO.File.ReadAllBytes("wwwroot/images/player_silhouette.jpg"));
        }


        public static string getTeamLogoBase64Photo(byte[] Photo)
        {
            if (Photo != null && Photo.Length > 0)
                return Convert.ToBase64String(Photo);
            else
                return Convert.ToBase64String(System.IO.File.ReadAllBytes("wwwroot/images/team_logo.jpg"));
        }

        public static string getGameBase64Photo(byte[] Photo)
        {
            if (Photo != null && Photo.Length > 0)
                return Convert.ToBase64String(Photo);
            else
                return Convert.ToBase64String(System.IO.File.ReadAllBytes("wwwroot/images/game_logo.jpg"));
        }

        public static string ValidateImage(IFormFile Photo,int MaxW = 512, int MaxH = 512, int MaxFileSize = (2*1024*1024))
        {
            if (Photo != null && Photo.Length > 0)
            {

                if (Photo.Length > MaxFileSize)
                    return "Файл слишком большой (макс. 2 МБ)";


                if (!Photo.ContentType.StartsWith("image/"))
                    return "Только изображения разрешены!";

                try
                {
                    using var img = SixLabors.ImageSharp.Image.Load(Photo.OpenReadStream());
                    if (img.Width > MaxW || img.Height > MaxH)
                        return "Максимальный размер изображения 2000x2000 пикселей!";

                    return "";

                }
                catch
                {
                    return "Файл не является изображением!";
                }

            }
            return "";

        }
    }
}
