﻿namespace DuplicateExtensionFinder
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Xml;
  using System.Xml.Serialization;

  static class Program
  {
    static readonly string[] DefaultPathsVs14 =
      {
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    @"Microsoft\VisualStudio\14.0\Extensions"),
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    @"Microsoft\VisualStudio\15.0_53c35e82\Extensions"), //C:\Users\proficoncept\AppData\Local\Microsoft\VisualStudio\15.0_53c35e82
        Path.Combine(Environment.GetFolderPath(Environment.Is64BitOperatingSystem ? Environment.SpecialFolder.ProgramFilesX86 : Environment.SpecialFolder.ProgramFiles),
                    @"Microsoft Visual Studio 14.0\Common7\IDE\Extensions"),
        @"Y:\Programme\VS2017 Ent RC\Common7\IDE\Extensions"
      };

    static void Main(string[] args)
    {
      var paths = new List<string>();
      var pathArgs = args.Where(arg => !arg.StartsWith("-")).ToArray();
      paths.AddRange(pathArgs.Length == 0 ? DefaultPathsVs14 : pathArgs);

      var onlyDupes = args.Any(x => string.Equals(x, "-dupes", StringComparison.OrdinalIgnoreCase));
      var doDelete = args.Any(x => string.Equals(x, "-delete", StringComparison.OrdinalIgnoreCase));

      var extensions = new List<Extension>();
      var manifestNames = new List<string>() { "extension.vsixmanifest", "extension.vsixmanifest.deleteme" };

      foreach (var path in paths)
      {
        if (!Directory.Exists(path))
        {
          Console.Error.WriteLine($"Directory does not exist: {path}");
          continue;
        }

        Console.WriteLine($"Searching in {path}");
        var extensionDir = new DirectoryInfo(path);

        var vsixSerializer = new XmlSerializer(typeof(Vsix));
        var packageSerializer = new XmlSerializer(typeof(PackageManifest));

        var extDirs = extensionDir.GetDirectories("*.*", SearchOption.AllDirectories);

        foreach (var dir in extDirs)
        {
          // skip symlinks and junctions
          if (dir.Attributes.HasFlag(FileAttributes.ReparsePoint))
          {
            continue;
          }

          foreach (var name in manifestNames)
          {
            var manifest = Path.Combine(dir.FullName, name);

            if (File.Exists(manifest))
            {
              using (var rdr = XmlReader.Create(manifest, new XmlReaderSettings { IgnoreComments = true }))
              {
                try
                {
                  if (vsixSerializer.CanDeserialize(rdr))
                  {
                    var vsix = (Vsix)vsixSerializer.Deserialize(rdr);
                    extensions.Add(new Extension()
                    {
                      Id = vsix.Identifier.Id,
                      Name = vsix.Identifier.Name,
                      Version = new Version(vsix.Identifier.Version),
                      Path = dir.FullName,
                      CreationTime = dir.CreationTime
                    });
                  }
                  else if (packageSerializer.CanDeserialize(rdr))
                  {
                    var package = (PackageManifest)packageSerializer.Deserialize(rdr);
                    extensions.Add(new Extension()
                    {
                      Id = package.Metadata.Identity.Id,
                      Name = package.Metadata.DisplayName,
                      Version = new Version(package.Metadata.Identity.Version),
                      Path = dir.FullName,
                      CreationTime = dir.CreationTime
                    });
                  }
                }
                catch (XmlException)
                {
                  // invalid manifest, ignore...
                }
              }
            }
          }
        }
      }

      var grouped = extensions.OrderBy(x => x.Name).GroupBy(x => x.Id).ToList();
      var toDelete = grouped.Where(x => x.Count() > 1).SelectMany(@group => @group.OrderByDescending(x => x.Version).ThenByDescending(x => x.CreationTime).Skip(1)).ToList();

      foreach (var group in grouped.Where(x => x.Count() > (onlyDupes ? 1 : 0)))
      {
        Console.WriteLine("{0}", @group.First().Name);
        foreach (var vsix in group.OrderBy(x => x.Version).ThenBy(x => x.CreationTime))
        {
          var isDuplicate = toDelete.Contains(vsix);
          Console.WriteLine(" - {0} [{2}] ({1})", vsix.Version, vsix.Path, isDuplicate ? "DELETE" : "KEEP");

          if (isDuplicate && doDelete)
          {
            try
            {
              Directory.Delete(vsix.Path, true);
            }
            catch (System.UnauthorizedAccessException)
            {
              Console.WriteLine();
              Console.WriteLine("You must start as administrator to delete global extensions.");
              Console.WriteLine("Press any key to continue...");
              Console.ReadKey();
              return;
            }
          }
        }

        Console.WriteLine();
      }

      if (toDelete.Count == 0)
      {
        Console.WriteLine("No duplicates found.");
      }

      if (!doDelete)
      {
        Console.WriteLine("Specify '-delete' to delete old extensions from disk.");
      }

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }
  }
}
