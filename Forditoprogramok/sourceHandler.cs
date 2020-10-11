using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Forditoprogramok
{
    class sourceHandler
    {
        private string path1, path2 = "";
        private string content = "";

        public string Path1
        {
            get { return this.path1; }
            set { this.path1 = value; }
        }

        public string Path2
        {
            get { return this.path2; }
            set { this.path2 = value; }
        }

        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }

        public sourceHandler(string path1, string path2)
        {
            this.path1 = path1;
            this.path2 = path2;
        }

        public void openFileToRead()
        {
            try
            {
                StreamReader SR = new StreamReader(File.OpenRead(this.path1));
                content = SR.ReadToEnd();

                //while (SR.Peek()>-1)
                //{
                //    s = SR.ReadLine();
                //}

                SR.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //replaceText("  "," ");
        public void replaceText(string from, string to) {
            while (content.Contains(from)) {
                content = content.Replace(from, to);
            }
        }

        public void replaceFirst(){

            Regex rgx = new Regex(@"^[a-zA-Z0-9]\d{2}[a-zA-Z0-9](-\d{3}){2}[A-Za-z0-9]$");

            Dictionary<string,string> fromTo=new Dictionary<string,string>();
            /*List<string> from = new List<string>();
            List<string> to = new List<string>();*/


            content = Regex.Replace(content,@"//(.*?)\r?\n","");
            content = Regex.Replace(content, @"/\*(.*?)\*/","");
            content = Regex.Replace(content, @"/\*[\w\W]*\*/","");

            fromTo.Add("  "," ");
            fromTo.Add("\r\n"," ");
            fromTo.Add("    ", " ");    //Tab to 1 space
            fromTo.Add(" {", "{");
            fromTo.Add(" }", "}");
            fromTo.Add("{ ", "{");
            fromTo.Add("} ", "}");
            fromTo.Add(" (", "(");
            fromTo.Add(" )", ")");
            fromTo.Add("( ", "(");
            fromTo.Add(") ", ")");
            fromTo.Add(" ;", ";");
            fromTo.Add("; ", ";");
            fromTo.Add(" =", "=");
            fromTo.Add("= ", "=");
            #region
            /*
            from.Add("  ");
            to.Add(" ");
            from.Add("\br");
            to.Add(" ");

            for (int i = 0; i < from.Count; i++)
            {
                replaceText(from[i], to[i]);
            }*/
            #endregion

            foreach (KeyValuePair<string,string> kvp in fromTo)
            {
                replaceText(kvp.Key, kvp.Value);
            }

            content = Regex.Replace(content, @"int [a-zA-Z0-9_-]*", "VARIABLE");
            content = Regex.Replace(content, @"^[0-9]*", "CONST");

            fromTo.Add("if", " 10 ");
            fromTo.Add("for", " 20 ");
            fromTo.Add("while", " 30 ");
            fromTo.Add("(", " 40 ");
            fromTo.Add(")", " 50 ");
            fromTo.Add("==", " 60 ");
            fromTo.Add("=", " 61 ");
            fromTo.Add("{", " 70 ");
            fromTo.Add("}", " 80 ");

            foreach (KeyValuePair<string, string> kvp in fromTo)
            {
                replaceText(kvp.Key, kvp.Value);
            }

            
            if (rgx.IsMatch("[0-9]*"))
            {
                MatchCollection mc = Regex.Matches(content, "[0-9]");
                for (int i = 0; i < mc.Count; i++)
                {
                    replaceText(content, "CONST");
                }
            }

        }


        public void replaceContent(string s)
        {
            try
            {
                this.content += s;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void openFileToWrite()
        {
            try
            {
                StreamWriter SW = new StreamWriter(File.Open(this.path2, FileMode.Create));
                SW.WriteLine(content);
                SW.Flush();
                SW.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

}
