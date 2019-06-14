using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ReferenceReplaceTool
{
    public class HintReplacer
    {
        private string _xmlPath;

        public HintReplacer(string xmlPath)
        {
            this._xmlPath = xmlPath;
        }

        public void ReplaceHintPath(Dictionary<string, string> refDic)
        {
            if (refDic == null)
            {
                return;
            }

            if (!string.IsNullOrEmpty(_xmlPath) && File.Exists(_xmlPath))
            {
                XmlDocument doc = new XmlDocument();

                doc.LoadXml(File.ReadAllText(_xmlPath));

                XmlNode rootNode = doc["Project"];

                if (rootNode != null)
                {
                    XmlNode itemGroupNode = rootNode["ItemGroup"];

                    if (itemGroupNode != null)
                    {
                        string refStr = "Reference";

                        for (XmlNode childNode = itemGroupNode.FirstChild; childNode != null; childNode = childNode?.NextSibling)
                        {
                            if (childNode.Name == refStr)
                            {
                                XmlAttributeCollection attrs = childNode.Attributes;

                                XmlAttribute includeAttr = attrs["Include"];

                                if (includeAttr != null)
                                {
                                    string attrVal = includeAttr.Value;

                                    string destHint = string.Empty;

                                    if (refDic.TryGetValue(attrVal, out destHint))
                                    {
                                        XmlNode hintNode = childNode["HintPath"];

                                        if (hintNode != null)
                                        {
                                            hintNode.InnerText = ConvertRelativePath(_xmlPath, destHint);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    doc.Save(_xmlPath);
                }
            }
        }

        private string ConvertRelativePath(string src, string dst)
        {
            Uri srcUri = new Uri(src);

            Uri dstUri = new Uri(dst);

            Uri relativeUri = srcUri.MakeRelativeUri(dstUri);

            string res = Uri.UnescapeDataString(relativeUri.ToString());

            return res;
        }
    }
}