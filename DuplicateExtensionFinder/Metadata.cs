namespace DuplicateExtensionFinder
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Xml;
  using System.Xml.Serialization;

  [XmlRoot(ElementName = "Metadata", Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2011")]
  public class Metadata
  {
    [XmlElement(ElementName = "Identity", Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2011")]
    public Identity Identity
    {
      get; set;
    }

    [XmlElement(ElementName = "DisplayName", Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2011")]
    public string DisplayName
    {
      get; set;
    }
  }
}
