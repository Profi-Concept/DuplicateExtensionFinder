namespace DuplicateExtensionFinder
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Xml;
  using System.Xml.Serialization;

  [XmlRoot(ElementName = "Vsix", Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
  public class Vsix
  {
    [XmlElement(ElementName = "Identifier", Namespace = "http://schemas.microsoft.com/developer/vsx-schema/2010")]
    public Identifier Identifier
    {
      get; set;
    }
  }
}
