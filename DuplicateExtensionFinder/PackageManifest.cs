namespace DuplicateExtensionFinder
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Xml;
  using System.Xml.Serialization;

  [XmlRoot(ElementName = "PackageManifest", Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2011")]
  public class PackageManifest
  {
    [XmlElement(ElementName = "Metadata", Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2011")]
    public Metadata Metadata
    {
      get; set;
    }
  }
}
