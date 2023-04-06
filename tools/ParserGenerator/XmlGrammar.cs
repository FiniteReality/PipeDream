using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PipeDream.Tools.ParserGenerator;

[XmlType("Grammar")]
public sealed record XmlGrammar(
    [property: XmlElement(typeof(XmlKindDefinition))]
    [property: XmlElement(typeof(XmlAbstractGrammarItem))]
    [property: XmlElement(typeof(XmlSealedGrammarItem))]
    [property: XmlElement(typeof(XmlRootGrammarItem))]
    XmlGrammarElement[] Elements)
{
    private XmlGrammar()
        : this(Array.Empty<XmlGrammarElement>())
    { }
}

public abstract record XmlGrammarElement();

[XmlType("KindDef")]
public sealed record XmlKindDefinition(
    [property: XmlAnyElement("Comment")]
    XElement? Comment,
    [property: XmlAttribute("Group")]
    string Group,
    [property: XmlAttribute("Name")]
    string Kind)
    : XmlGrammarElement()
{
    private XmlKindDefinition()
        : this(
            Comment: null,
            Group: null!,
            Kind: null!)
    { }
}

public abstract record XmlGrammarItem(
    [property: XmlAttribute("Base")]
    string? Base,
    [property: XmlAnyElement("Comment")]
    XElement? Comment,
    [property: XmlElement("Kind")]
    XmlKind[] Kinds,
    [property: XmlElement("Member")]
    XmlMember[] Members,
    [property: XmlAttribute("Type")]
    string Type)
    : XmlGrammarElement();

[XmlType("Kind")]
public sealed record XmlKind(
    [property: XmlAttribute("Name")]
    string Kind)
{
    private XmlKind()
        : this(default(string)!)
    { }
}

[XmlType("RootNode")]
public sealed record XmlRootGrammarItem(
    XElement? Comment,
    XmlMember[] Members,
    string Type)
    : XmlGrammarItem(
        Base: null,
        Comment: Comment,
        Kinds: Array.Empty<XmlKind>(),
        Members: Members,
        Type: Type)
{
    private XmlRootGrammarItem()
        : this(
            Comment: null,
            Members: Array.Empty<XmlMember>(),
            Type: null!)
    { }
}

[XmlType("AbstractNode")]
public sealed record XmlAbstractGrammarItem(
    string Base,
    XElement? Comment,
    XmlKind[] Kinds,
    XmlMember[] Members,
    string Type)
    : XmlGrammarItem(
        Base: Base,
        Comment: Comment,
        Kinds: Kinds,
        Members: Members,
        Type: Type)
{
    private XmlAbstractGrammarItem()
        : this(
            Base: null!,
            Comment: null,
            Kinds: Array.Empty<XmlKind>(),
            Members: Array.Empty<XmlMember>(),
            Type: null!)
    { }
}

[XmlType("Node")]
public sealed record XmlSealedGrammarItem(
    string Base,
    XElement? Comment,
    XmlKind[] Kinds,
    XmlMember[] Members,
    string Type)
    : XmlGrammarItem(
        Base: Base,
        Comment: Comment,
        Kinds: Kinds,
        Members: Members,
        Type: Type)
{
    private XmlSealedGrammarItem()
        : this(
            Base: null!,
            Comment: null,
            Kinds: Array.Empty<XmlKind>(),
            Members: Array.Empty<XmlMember>(),
            Type: null!)
    { }
}

[XmlType("Member")]
public sealed record XmlMember(
    [property: XmlAnyElement("Comment")]
    XElement? Comment,
    [property: XmlElement("Kind")]
    XmlKind[] Kinds,
    [property: XmlAttribute("Name")]
    string Name,
    [property: XmlAttribute("Type")]
    string Type)
{
    private XmlMember()
        : this(
            Comment: null,
            Kinds: Array.Empty<XmlKind>(),
            Name: null!,
            Type: null!)
    { }
}