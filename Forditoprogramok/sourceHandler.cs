using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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
                SW.WriteLine("some text");
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
