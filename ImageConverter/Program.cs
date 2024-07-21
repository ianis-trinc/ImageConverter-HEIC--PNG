using ImageMagick;
using System.Diagnostics;

Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();

var inputPath = @"C:\Users\instr\Pictures\Absolvire";
var outputPath = @"C:\Users\instr\Pictures\AbsolvirePNG";

string[] allFiles = Directory.GetFiles(inputPath, "*.heic", searchOption: SearchOption.AllDirectories);

var tasks = new List<Task>();

foreach (string fileName in allFiles)
{
    tasks.Add(Task.Run(() =>
    {
        FileInfo info = new FileInfo(fileName);
        using (MagickImage image = new MagickImage(info.FullName))
        {
            Console.WriteLine($"Processing: {info.FullName}");
            image.Format = MagickFormat.Png;

            string newFileName = info.Name.Replace(".HEIC", "") + ".png";
            string newFilePath = Path.Combine(outputPath, newFileName);

            image.Write(Path.Join(newFilePath));
            Console.WriteLine($"Processed: {newFilePath}");
        }
    }));
}

Task.WaitAll(tasks.ToArray());

stopwatch.Stop();

Console.WriteLine("=== Done ===");

Console.WriteLine($"Processing time: {stopwatch.ElapsedMilliseconds}");