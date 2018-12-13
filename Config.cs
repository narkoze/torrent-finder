using System.Collections.Generic;
using System.Deployment.Application;
using System.Xml;

namespace TM2
{
    class Config
    {
        //private string xmlUrl = ApplicationDeployment.CurrentDeployment.DataDirectory + @"\Config.xml";
        private string xmlUrl = "Config.xml";

        public void Write(string Tag, string Value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);

            switch (Tag)
            {
                case "autologin":
                    doc.SelectSingleNode("Config/Options/autologin").InnerText = Value;
                    break;
                case "autohide":
                    doc.SelectSingleNode("Config/Options/autohide").InnerText = Value;
                    break;
                case "downloadspath":
                    doc.SelectSingleNode("Config/Options/downloadspath").InnerText = Value;
                    break;
                case "fanologin":
                    doc.SelectSingleNode("Config/Accounts/fanologin").InnerText = Value;
                    break;
                case "fanopassword":
                    doc.SelectSingleNode("Config/Accounts/fanopassword").InnerText = Value;
                    break;
                case "kinozallogin":
                    doc.SelectSingleNode("Config/Accounts/kinozallogin").InnerText = Value;
                    break;
                case "kinozalpassword":
                    doc.SelectSingleNode("Config/Accounts/kinozalpassword").InnerText = Value;
                    break;
                case "filebaselogin":
                    doc.SelectSingleNode("Config/Accounts/filebaselogin").InnerText = Value;
                    break;
                case "filebasepassword":
                    doc.SelectSingleNode("Config/Accounts/filebasepassword").InnerText = Value;
                    break;
            }                        
            doc.Save(xmlUrl);
        }
        public string Read(string Type, string Tag)
        {
            string innerText = string.Empty;

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);
            switch (Type)
            {
                case "options":
                    innerText = doc.SelectSingleNode("Config/Options/" + Tag).InnerText;
                    break;
                case "account":
                    innerText = doc.SelectSingleNode("Config/Accounts/" + Tag).InnerText;
                    break;
            }
            return innerText;
        }

        public bool CheckIfAccountsExist()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);

            if (doc.SelectSingleNode("Config/Accounts/fanologin").InnerText != string.Empty)
                return true;
            else if (doc.SelectSingleNode("Config/Accounts/kinozallogin").InnerText != string.Empty)
                return true;
            else if (doc.SelectSingleNode("Config/Accounts/filebaselogin").InnerText != string.Empty)
                return true;

            return false;
        }
        public bool FanoAccountExist()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);

            if (doc.SelectSingleNode("Config/Accounts/fanologin").InnerText != string.Empty && doc.SelectSingleNode("Config/Accounts/fanopassword").InnerText != string.Empty)
                return true;
            return false;
        }
        public bool KinozalAccountExist()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);

            if (doc.SelectSingleNode("Config/Accounts/kinozallogin").InnerText != string.Empty && doc.SelectSingleNode("Config/Accounts/kinozalpassword").InnerText != string.Empty)
                return true;
            return false;
        }
        public bool FilebaseAccountExist()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);

            if (doc.SelectSingleNode("Config/Accounts/filebaselogin").InnerText != string.Empty && doc.SelectSingleNode("Config/Accounts/filebasepassword").InnerText != string.Empty)
                return true;
            return false;
        }
        public void DeleteAccounts()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);
            doc.SelectSingleNode("Config/Accounts/fanologin").InnerText = string.Empty;
            doc.SelectSingleNode("Config/Accounts/fanopassword").InnerText = string.Empty;
            doc.SelectSingleNode("Config/Accounts/kinozallogin").InnerText = string.Empty;
            doc.SelectSingleNode("Config/Accounts/kinozalpassword").InnerText = string.Empty;
            doc.SelectSingleNode("Config/Accounts/filebaselogin").InnerText = string.Empty;
            doc.SelectSingleNode("Config/Accounts/filebasepassword").InnerText = string.Empty;
            doc.Save(xmlUrl);
        }

        public void AddHistory(string text)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);
            XmlNode newElem = doc.CreateNode("element", "history", "");
            newElem.InnerText = text;
            XmlElement root = doc.DocumentElement;
            root.AppendChild(newElem);
            doc.Save(xmlUrl);
        }
        public bool CheckIfHistoryExist()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);
            XmlNode history = doc.SelectSingleNode("Config/history");
            if(history != null)
                return true;
            return false;
        }
        public void DeleteHistory()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);
            XmlNodeList nodes = doc.SelectNodes("Config/history");
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                nodes[i].ParentNode.RemoveChild(nodes[i]);
            }
            doc.Save(xmlUrl);
        }
        public List<string> GetHistory()
        {
            List<string> list = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);
            XmlNodeList history = doc.SelectNodes("Config/history");

            if (history.Count > 0)
            {
                for (int i = (history.Count - 1); i >= 0; i--)
                {
                    list.Add(history[i].InnerText);
                }
            }
            return list;
        }
        public void DeleteHistoryElementIfExist(string Text)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);
            XmlNodeList history = doc.SelectNodes("Config/history");

            foreach (XmlNode i in history)
            {
                if (i.InnerText == Text)
                {
                    i.ParentNode.RemoveChild(i);
                }
            }

            doc.Save(xmlUrl);
        }
    }
}
