using System;
using System.Linq;

namespace amisa.asserts
{
    public class ScriptAssert
    {
        /// <summary>
        /// Asserts that a script or query or string ....., equal with another.
        /// at first, remove newLines Char and trim it and then lowerCasing them in both excepte and actual parameters
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be examined</param>
        public static void Equal(string expected, string actual)
        {
            Equal(new AmisaScript(expected), new AmisaScript(actual));
        }
        private static void Equal(AmisaScript expect, AmisaScript actual)
        {
            foreach (var expectSubScript in expect.SubScript)
            {
                AmisaScript.FindInActual(actual, expectSubScript.Script!);
            }

            if (actual.SubScript.Length > 0)
            {
                string message = string.Join("", actual.SubScript.Select(i => i.Script).ToArray());
                throw new Exception($"to this section is equal :'{message}' but remind of script is not equal");
            }
        }

        /// <summary>
        /// Asserts that a script or query or string ....., starts with another is same.
        /// at first, remove newLines Char and trim it and then lowerCasing them in both excepte and actual parameters
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be examined</param>
        public static void StartWith(string expected, string actual)
        {
            StartWith(new AmisaScript(expected), new AmisaScript(actual));
        }
        private static void StartWith(AmisaScript expect, AmisaScript actual)
        {
            foreach (var expectSubScript in expect.SubScript)
            {
                AmisaScript.FindInActual(actual, expectSubScript.Script!);
            }
        }
        /// <summary>
        /// Asserts that a script or query or string ....., end with another is same.
        /// at first, remove newLines Char and trim it and then lowerCasing them in both excepte and actual parameters
        /// </summary>
        /// <param name="expected">The expected value</param>
        /// <param name="actual">The value to be examined</param>
        public static void EndWith(string expected, string actual)
        {
            EndWith(new AmisaScript(expected, true), new AmisaScript(actual, true));
        }
        private static void EndWith(AmisaScript expect, AmisaScript actual)
        {
            foreach (var expectSubScript in expect.SubScript)
            {
                AmisaScript.FindInActual(actual, expectSubScript.Script!);
            }
        }

        public class AmisaScript
        {
            public AmisaSubScript[] SubScript { get; set; }
            public AmisaScript(string script, bool reverse = false)
            {
                SubScript = script.Split(' ', '\r', '\n', '\t', ' ').Where(i => !string.IsNullOrEmpty(i)).Select(i => new AmisaSubScript(i)).ToArray();
                if (reverse)
                {
                    SubScript = SubScript.Reverse().ToArray();
                }
            }

            public static void FindInActual(string actual, string expect) => FindInActual(new AmisaScript(actual), expect);
            public static void FindInActual(AmisaScript actual, string expect)
            {
                while (actual.SubScript[0] is AmisaSubScript actualSubScript)
                {
                    string actualScript = actualSubScript.Script!;
                    AmisaSubScript.FindWords(ref expect, ref actualScript);

                    if (string.IsNullOrEmpty(actualScript))
                    {
                        actual.SubScript = actual.SubScript[1..];
                    }
                    else
                    {
                        actual.SubScript[0].Script = actualScript;
                    }

                    if (string.IsNullOrEmpty(expect))
                    {
                        return;
                    }
                }

                if (string.IsNullOrEmpty(expect))
                {
                    throw new Exception($"can not find expect subScript :'{expect}'");
                }
            }
        }

        public class AmisaSubScript
        {
            public AmisaSubScript(string script)
            {
                string scr = script.Trim()
                    .Replace("\r\n", "")
                    .Replace("\r", "")
                    .Replace("\n", "")
                    .Replace("\t", "")
                    .Trim()
                    .ToLower();

                if (!string.IsNullOrEmpty(scr))
                {
                    Script = scr;
                }
                else
                {
                    Script = "";
                }
            }
            public string Script { get; set; }

            public static void FindWords(ref string expect, ref string actual)
            {
                FindWords(expect.Split(' ', '\r', '\n', '\t', ' ').Where(i => i != "").ToArray(), ref expect, ref actual);
            }
            private static void FindWords(string[] expectWords, ref string expect, ref string actual)
            {
                actual = actual.Replace(" ", "");
                while (expectWords.Length > 0)
                {
                    string word = expectWords[0];
                    if (actual.StartsWith(word))
                    {
                        actual = actual[word.Length..];
                        expectWords = expectWords[1..];
                        expect = string.Join("", expectWords);
                    }
                    else
                    {
                        throw new Exception($"can not find the word :'{word}' from expect subScript :'{expect}' in actual subScript :'{actual}'");
                    }
                }
            }
        }
    }
}
