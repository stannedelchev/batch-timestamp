using CommandLine;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;

Parser.Default.ParseArguments<Options>(args).WithParsed(o =>
{
    var files = new DirectoryInfo(o.Folder).GetFiles();
    Console.WriteLine($"Updating files in {o.Folder}");

    foreach (var (fileInfo, index) in files.Select((f,i) => (f,i)))
    {
        using var img = Image.Load(fileInfo.FullName);
        var exif = img.Metadata.ExifProfile;
        var value = exif.GetValue(ExifTag.DateTimeDigitized).Value;
        var parts = value.Split(" ");
        var changedValue = $"{parts[0].Replace(":", ".")} {parts[1]}";
        var newDateTaken = DateTime.Parse(changedValue).Add(o.Offset);
        var newDateTakenAsString = newDateTaken.ToString("yyyy:MM:dd HH:mm:ss");
        exif.SetValue(ExifTag.DateTimeOriginal, newDateTakenAsString);

        Console.WriteLine($"{index + 1}/{files.Length} {fileInfo.Name}: {value} to {newDateTakenAsString}.");

        if (!o.DryRun)
        {
            img.Save(fileInfo.FullName);
        }
    }
});

public class Options
{
    [Option('o', "offset", Required = true, HelpText = "TimeSpan offset to add/subtract from each photo's Date Taken. Values should be parsable by TimeSpan.Parse. See https://docs.microsoft.com/en-us/dotnet/api/system.timespan.parse?view=net-6.0 for more information.")]
    public TimeSpan Offset { get; set; }

    [Option('f', "folder", Required = true, HelpText = "Absolute path to the folder where images reside.")]
    public string Folder { get; set; }

    [Option('d', "dry-run", Required = false, HelpText = "Do a dry run and don't update the images on the file system.")]
    public bool DryRun { get; set; }
}
