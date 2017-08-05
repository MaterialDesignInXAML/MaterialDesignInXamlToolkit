using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            string ownerName = "wongjiahau";
            string branchName = "New-Demo-2";
            string sourceCode = DownloadFile(
                @"https://raw.githubusercontent.com/" +
                    $"{ownerName}/" +
                    "MaterialDesignInXamlToolkit/" +
                    $"{branchName}/"+
                    "MainDemo.Wpf/" +
                    $"{_typeName}.xaml"
                );
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sourceCode);
            return xmlDoc;
        }

        private static string DownloadFile(string sourceUrl) 
        //Refered from : https://gist.github.com/nboubakr/7812375
        {
            
            long existLen = 0;
            var httpReq = (HttpWebRequest)WebRequest.Create(sourceUrl);
            httpReq.AddRange((int)existLen);
            var httpRes = (HttpWebResponse)httpReq.GetResponse();
            var responseStream = httpRes.GetResponseStream();
            if (responseStream == null) return "Fail to fetch file";
            var streamReader = new StreamReader(responseStream);
            return streamReader.ReadToEnd();
        }
 
    }
}
