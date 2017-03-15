namespace DuplicateExtensionFinder
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Xml;
  using System.Xml.Serialization;

  [XmlRoot(ElementName = "Identity", Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2011")]
  public class Identity
  {
    [XmlAttribute(AttributeName = "Id")]
    public string Id
    {
      get; set;
    }

    [XmlAttribute(AttributeName = "Version")]
    public string Version
    {
      get; set;
    }
  }
}
