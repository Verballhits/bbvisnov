using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BBVisNov
{
    [XmlRoot("Dialog")]
    public class DialogNodeList
    {
        [XmlArray("Triggers")]
        [XmlArrayItem("Trigger")]
        public List<DialogTrigger> Triggers { get; set; }

        [XmlArray("DialogNodes")]
        [XmlArrayItem("DialogNode")]
        public List<DialogNode> Nodes { get; set; }

        [XmlArray("CharacterImages")]
        [XmlArrayItem("CharacterImage")]
        public List<DialogCharacter> Characters { get; set; }

        [XmlElement("PostDialogHideCharacter")]
        public string PostDialogHideCharacter { get; set; }

        public DialogNodeList()
        {
            Nodes = new List<DialogNode>();
            Characters = new List<DialogCharacter>();
        }
    }

    public class DialogNode
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("ShowSceneCharacter")]
        public string ShowSceneCharacter { get; set; }

        [XmlElement("HideSceneCharacter")]
        public string HideSceneCharacter { get; set; }

        [XmlElement("CharacterImage")]
        public string CharacterImage { get; set; }

        [XmlArray("Text")]
        [XmlArrayItem("Line")]
        public string[] Text { get; set; }

        [XmlElement("SoundEffect")]
        public string SoundEffect { get; set; }

        [XmlArray("Responses")]
        [XmlArrayItem("Response")]
        public List<ReponseNode> Responses { get; set; }
    }

    public class ReponseNode
    {
        [XmlAttribute("nextnode")]
        public int NextNode { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    public class DialogCharacter
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlText]
        public string Image { get; set; }
    }

    public class DialogTrigger
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
