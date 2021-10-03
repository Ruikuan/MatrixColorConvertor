using System.Drawing;
using System.Drawing.Imaging;

if (args.Length < 2)
{
    Console.WriteLine("Please input file name: ");
    Console.WriteLine("MatrixConvertor {source} {dest}");
    return;
}
string originImage = args[0];
string destImage = args[1];

ConvertImage(originImage, destImage);

unsafe static void ConvertImage(string originImage, string destImage)
{
    using Bitmap bmp = new(originImage);
    Rectangle rect = new(0, 0, bmp.Width, bmp.Height);

    var bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);

    IntPtr ptr = bmpData.Scan0;
    int byteCount = Math.Abs(bmpData.Stride) * bmp.Height;

    Span<byte> rgbValues = new Span<byte>(ptr.ToPointer(), byteCount);
    for (int i = 0; i < byteCount; i += 3)
    {
        rgbValues[i] = Matrixify(rgbValues[i], 1.5);
        rgbValues[i + 1] = Matrixify(rgbValues[i + 1], 0.8);
        rgbValues[i + 2] = Matrixify(rgbValues[i + 2], 1.5);
    }

    bmp.UnlockBits(bmpData);
    bmp.Save(destImage, ImageFormat.Jpeg);
}


static byte Matrixify(byte value, double pow)
{
    const float F255 = 255f;
    float fValue = value / F255;
    var dResult = Math.Pow(fValue, pow);
    return (byte)(dResult * F255);
}
