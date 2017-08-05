using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CodeDisplayer;

namespace MaterialDesignDemo.Helper {
    public class SourceRouter {
        private string _typeName;
        private const bool GetFromLocalSource = true;
        public SourceRouter(string typeName) {
            _typeName = typeName;
        }

        public XmlDocument GetSource() {
            if (GetFromLocalSource)
                return Local();
            else
                return Remote();
        }
        public XmlDocument Local() {
            var xmlDoc = new XmlDocument();
            string source = File.ReadAllText($@"..\..\{_typeName}.xaml");
            xmlDoc.LoadXml(source);
            return xmlDoc;
        }

        public XmlDocument Remote() {
            return new MaterialDesignInXamlToolkitGitHubFile(_typeName).GetXmlDocument();

        }
    }
}
