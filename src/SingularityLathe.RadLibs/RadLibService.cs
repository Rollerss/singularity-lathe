﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SingularityLathe.RadLibs
{
    public class RadLibService : BaseRadLibService
    {
        private readonly Random rnd;
        private readonly string TagOpener;
        private readonly string TagCloser;
        public List<RadLibTagDictionary> RadLibTagDictionaries { get; set; } = new List<RadLibTagDictionary>();

        public string ProcessMadLib(string template)
        {
            var regex = new Regex(@"\[\[.*?\]\]");

            var matches = regex.Matches(template);

            foreach (Match m in matches)
            {
                var replacer = new Regex(m.Value);

                string replace = "";

                var madlib = RadLibTagDictionaries.FirstOrDefault(x => TagOpener + x.Tag + TagCloser == m.Value);

                if (madlib == null)
                {
                    continue;
                }

                replace = madlib.Values[rnd.Next(madlib.Values.Count)];

                template = regex.Replace(template, replace, 1);
            }

            return template;
        }

        public RadLibService(RadLibConfiguration config) : base(config)
        {
            this.rnd = config.RandomSeed == 0 ? new Random() : new Random(config.RandomSeed);
            this.TagCloser = config.TagCloser;
            this.TagOpener = config.TagOpener;
        }
    }
}
