using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KelliPokerPlanning
{
    public class JoinCodeGenerator
    {

        public string Generate()
        {
            var rnd = new Random();
            var words = GetWords();
            var index = rnd.Next(0, words.Length);
            return words[index];
        }

        private string[] GetWords()
        {
            var data = GetResourceData();
            return data.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        private string GetResourceData()
        {
            var asm = Assembly.GetExecutingAssembly();
            using (var strm = asm.GetManifestResourceStream("KelliPokerPlanning.Dictionary.txt"))
            using (var rdr = new StreamReader(strm))
            {
                return rdr.ReadToEnd();
            }
        }
    }
}
