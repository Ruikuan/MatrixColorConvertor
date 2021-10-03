using System.Drawing;

const float F255 = 255f;
if (args.Length < 2)
{
    Console.WriteLine("Please input file name: ");
    Console.WriteLine("MatrixConvertor {source} {dest}");
    return;
}
string originImage = args[0];
using Bitmap image = new Bitmap(originImage);

for (int x = 0; x < image.Width; x++)
{
    for (int y = 0; y < image.Height; y++)
    {
        var color = image.GetPixel(x, y);
        var newColor = Color.FromArgb(M(color.R, 1.5), M(color.G, 0.8), M(color.B, 1.5));
        image.SetPixel(x, y, newColor);
    }
}

image.Save(args[1], System.Drawing.Imaging.ImageFormat.Jpeg);

static int M(int value, double pow)
{
    float fValue = value / F255;
    var dResult = Math.Pow(fValue, pow);
    return (int)(dResult * F255);
}
