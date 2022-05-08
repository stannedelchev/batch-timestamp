# batch-timestamp

Tool for adjusting timestamps for `Date Taken` EXIF fields on images.

# Usage

## Example 
`./BatchTimestamp.exe -f c:\MyFolderWithImages -o "01:00"`
Adds one hour to all images' Date Taken EXIF field.

## Options

```
  -o, --offset     Required. TimeSpan offset to add/subtract from each photo's Date Taken. Values should be parsable by TimeSpan.Parse. See https://docs.microsoft.com/en-us/dotnet/api/system.timespan.parse?view=net-6.0 for more information.

  -f, --folder     Required. Absolute path to the folder where images reside.

  -d, --dry-run    Do a dry run and don't update the images on the file system.

  --help           Display this help screen.

  --version        Display version information.
```
