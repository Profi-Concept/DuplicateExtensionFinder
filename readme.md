# Duplicate Extension Finder for VS2015

[![Build status](https://ci.appveyor.com/api/projects/status/cfgak6mw0v8fg9d2?svg=true)](https://ci.appveyor.com/project/remcoros/duplicateextensionfinder)
[Appveyor artifacts (latest .exe here)](https://ci.appveyor.com/project/remcoros/duplicateextensionfinder/build/artifacts)

Simple console tool to find duplicate extension folders / manifests for Visual Studio 2015 extensions. Used by me to cleanup and fix duplicate extension loading errors.

Extensions that mysteriously disable themselves can be an indicator of duplicate extensions.

Searches for extensions (by default) in

 * `%APP_DATA%\Microsoft\VisualStudio\14.0\Extensions`
 * `C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\Extensions`

You can override this and specify one or multiple paths on the commandline.

It scans extension folders (containing an 'extension.vsixmanifest' or 'extension.vsixmanifest.deleteme' file). Specify -delete to actually remove duplicate extensions, leaving the latest version.

## Command line parameters

 * _none_: shows all extensions installed (marking each line as "KEEP" or "DELETE")
 * `-dupes`: only show duplicate extensions\*
 * `-delete`: deletes the extensions listed as "DELETE" from the file system
 * Any argument not starting with a "-" is used as a root path to find extension folders. You can specify multiple paths.

 > ALL duplicate extensions from all specified paths are removed, leaving the latest version. 
 > If you want to remove duplicates per directory, run the tool one time per different directory.

## How to run

1. Clone/download and build (or download latest binary from Appveyor, see link on top)
2. Run the .exe via a command prompt (CMD, Powershell) e.g.
      
         > DuplicateExtensionFinder.exe -dupes

    or

         > DuplicateExtensionFinder.exe -delete
  
    or run in Visual Studio by editing the "Command line arguments" found in _Project Properties => Debug => Start Options => Command line arguments_ then press `F5`

### \*Y u no spell english?

Sorry.

https://translate.google.com/?hl=nl#en/nl/duped
https://translate.google.com/?hl=nl#en/nl/duplicate