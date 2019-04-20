using MyCharacterSheet.Lists;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MyCharacterSheet.Utility
{
    public static class SpellParser
    {

        #region Constants

        private const string CSV_FILE_NAME = "C:\\Users\\TomBe\\Documents\\5E Spells.csv";
        private const string XML_FILE_NAME = "C:\\Users\\TomBe\\Documents\\SpellList.xml";

        #endregion

        #region Methods

        /// =========================================
        /// ParseSpellCSV()
        /// =========================================
        public static void ParseSpellCSV()
        {
            List<Spell> list = new List<Spell>();
            Spell spell;
            string[] lines = File.ReadAllLines(CSV_FILE_NAME);
            string[] tokens;

            foreach (string line in lines)
            {
                spell = new Spell();
                tokens = line.Split(',');
                ReplaceComma(tokens);

                if (!tokens[0].Equals(""))
                {
                    spell.Name = tokens[0];
                    spell.Level = tokens[1].Equals("0") ? "Cantrip" : tokens[1];
                    spell.School = tokens[2];
                    spell.Ritual = tokens[3].Equals("Ritual") ? "Yes" : "No";
                    //tokens[4] = casting time
                    spell.Range = tokens[5];
                    spell.Area = tokens[6];
                    spell.Components = ParseComponents(tokens[7], tokens[8], tokens[9], tokens[10], tokens[11]);
                    spell.Concentration = tokens[12].Equals("Concentration") ? "Yes" : "No";
                    spell.Duration = tokens[13];
                    spell.Save = tokens[14];
                    spell.Damage = ParseDamageHeal(tokens[15], tokens[16]);
                    //tokens[17] = Sourcebook
                    spell.Page = tokens[18];
                    spell.Description = tokens[19];
                    spell.Prepared = "No";

                    list.Add(spell);
                    Console.WriteLine(tokens[0] + "  " + tokens.Length);
                }
            }

            CreateSpellXML(list);
        }

        /// =========================================
        /// ReplaceComma()
        /// =========================================
        private static void ReplaceComma(string[] tokens)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Replace('|', ',');
            }
        }

        /// =========================================
        /// ParseDamageHeal()
        /// =========================================
        private static string ParseDamageHeal(string type, string dmgHeal)
        {
            StringBuilder damageHeal = new StringBuilder();
            const string KEYWORD_DAM = "DAM:";
            const string KEYWORD_HEAL = "HEAL:";

            if (dmgHeal.Contains(KEYWORD_DAM))
            {
                dmgHeal = dmgHeal.Replace(KEYWORD_DAM, "").Trim();
                damageHeal.Append(dmgHeal);
                damageHeal.Append(" ");
                damageHeal.Append(type);
            }
            else if (dmgHeal.Contains(KEYWORD_HEAL))
            {
                dmgHeal = dmgHeal.Replace(KEYWORD_HEAL, "").Trim();
                damageHeal.Append(dmgHeal);
                damageHeal.Append(" Healing");
            }

            return damageHeal.ToString();
        }

        /// =========================================
        /// ParseComponents()
        /// =========================================
        private static string ParseComponents(string V, string S, string M, string comp, string cost)
        {
            StringBuilder components = new StringBuilder();
            List<string> list = new List<string>();
            bool first = true;

            if (!V.Equals(""))
                list.Add(V);
            if (!S.Equals(""))
                list.Add(S);
            if (!M.Equals(""))
                list.Add(M);

            foreach (string item in list)
            {
                if (!first)
                    components.Append(", ");
                components.Append(item);
                first = false;
            }

            if (!comp.Equals("") || !cost.Equals(""))
            {
                components.Append(" (");
                components.Append(comp.ToLower());
                components.Append(cost.Equals("") ? "" : " ");
                components.Append(cost);
                components.Append(")");
            }

            return components.ToString();
        }

        /// =========================================
        /// CreateSpellXML()
        /// =========================================
        private static void CreateSpellXML(List<Spell> list)
        {
            XDocument xml = new XDocument(
                new XElement("Spells",
                    from spell in list
                    select
                        new XElement("Spell",
                            new XAttribute("name", spell.Name),
                            new XAttribute("level", spell.Level),
                            new XAttribute("page", spell.Page),
                            new XAttribute("school", spell.School),
                            new XAttribute("ritual", spell.Ritual),
                            new XAttribute("comp", spell.Components),
                            new XAttribute("concen", spell.Concentration),
                            new XAttribute("range", spell.Range),
                            new XAttribute("duration", spell.Duration),
                            new XAttribute("area", spell.Area),
                            new XAttribute("save", spell.Save),
                            new XAttribute("damage", spell.Damage),
                            new XAttribute("description", spell.Description),
                            new XAttribute("prepared", spell.Prepared)
                            )
                        )
                    );

            xml.Save(XML_FILE_NAME);
        }

        #endregion

    }
}
