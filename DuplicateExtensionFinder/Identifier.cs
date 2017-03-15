namespace DuplicateExtensionFinder
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Xml;
  using System.Xml.Serialization;

  [XmlRoot(ElementName = "Identifier", Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
  public class Identifier
  {
    [XmlElement(ElementName = "Name", Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    public string Name
    {
      get; set;
    }

    [XmlElement(ElementName = "Version", Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    public string Version
    {
      get; set;
    }

    [XmlAttribute(AttributeName = "Id")]
    public string Id
    {
      get; set;
    }
  }
}
