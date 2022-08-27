using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using tiktokBot.util;

namespace tiktokBot.web
{
    class rssSerializer : sourceGetter
    {
        private static string formatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                return xml;
            }
        }

        public static Dictionary<string, string> getSubreddit(string rssUrl)
        {
            Dictionary<string, string> returnVals = new Dictionary<string, string>();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(formatXml(sourceGetter.dowloadSource(rssUrl)));
            XmlNode root = doc.FirstChild;

            List<string> titles = new List<string>();
            List<string> links = new List<string>();

            for (int i = 0; i < root.ChildNodes.Count; i++)
            {
                if (root.ChildNodes.Item(i).Name != "entry")
                {
                    root.ChildNodes.Item(i).ParentNode.RemoveChild(root.ChildNodes.Item(i));
                }
                else
                {
                    XmlNode entry = root.ChildNodes.Item(i);

                    for (int j = 0; j < entry.ChildNodes.Count; j++)
                    {
                        if (entry.ChildNodes.Item(j).Name == "title")
                        {
                            Dictionary<string, string> curse = curseWordsGetter.getCurseWords();
                            string addable = HttpUtility.HtmlDecode(entry.ChildNodes.Item(j).InnerText);

                            foreach (var word in curse)
                            {
                                addable = addable.Replace(word.Key, word.Value);
                            }

                            titles.Add(addable);
                        }
                        else if (entry.ChildNodes.Item(j).Name == "link")
                        {
                            string link = entry.ChildNodes.Item(j).Attributes["href"].Value;
                            links.Add(link.Substring(0, link.Length - 1) + "?sort=top&depth=1");
                        }
                    }
                }
            }

            for (int i = 0; i < titles.Count; i++)
            {
                returnVals.Add(titles[i], links[i]);
            }

            return returnVals;
        }

        // deprecated due to the latency between the image and rss getters producing non-matching sound-image combination
        public static List<string> getComments(string postRssUrl, int videoQuantity)
        {
            List<string> comments = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(formatXml(sourceGetter.dowloadSource(postRssUrl)));
            XmlNode root = doc.FirstChild;

            if (videoQuantity < Math.Floor((decimal) root.ChildNodes.Count / 5))
            {
                for (int i = 0; i < videoQuantity * 5; i++)
                {
                    if (root.ChildNodes.Item(i).Name != "entry")
                    {
                        root.ChildNodes.Item(i).ParentNode.RemoveChild(root.ChildNodes.Item(i));
                        i--;
                    }
                    else
                    {
                        XmlNode entry = root.ChildNodes.Item(i);

                        for (int j = 0; j < entry.ChildNodes.Count; j++)
                        {
                            if (entry.ChildNodes.Item(j).Name == "content")
                            {
                                string comment = stringUtil.betweenParagraph(entry.ChildNodes.Item(j).InnerText);
                                if (comment == invalidCommentUtil.getDeletedOrNullConst())
                                {
                                    root.ChildNodes.Item(i).ParentNode.RemoveChild(root.ChildNodes.Item(i));
                                    i--;
                                }
                                else
                                {
                                    comments.Add(HttpUtility.HtmlDecode(comment));
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Post doesn't have that many comments! (max. {})", root.ChildNodes.Count);
            }
            return comments;
        }

        public static int getCommentCount(string postRssUrl)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(formatXml(sourceGetter.dowloadSource(postRssUrl)));
            XmlNode root = doc.FirstChild;

            return root.ChildNodes.Count;
        }
    }
}
