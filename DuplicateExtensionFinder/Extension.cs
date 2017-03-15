namespace DuplicateExtensionFinder
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Xml;
  using System.Xml.Serialization;

  public class Extension
  {
    public string Id
    {
      get; set;
    }

    public string Name
    {
      get; set;
    }

    public string Path
    {
      get; set;
    }

    public Version Version
    {
      get; set;
    }

    public DateTime CreationTime
    {
      get; set;
    }
  }
}
