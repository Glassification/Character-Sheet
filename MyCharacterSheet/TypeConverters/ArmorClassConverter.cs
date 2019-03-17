using MyCharacterSheet.Characters;
using System;
using System.ComponentModel;
using System.Globalization;

namespace MyCharacterSheet.TypeConverters
{
    class ArmorClassConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type t)
        {
            if (t == typeof(string))
            {
                return true;
            }

            return base.CanConvertFrom(context, t);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo info, object value)
        {
            string worn, aType, stealth, sType;
            int aAC, sAC, miscAC, magicAC, strength;//, aWgt, sWgt;
            string[] tokens;

            if (value is string)
            {
                try
                {
                    string str = value as string;

                    tokens = str.Split(',');
                    worn = tokens[0];
                    aType = tokens[1];
                    aAC = int.Parse(tokens[2]);
                    stealth = tokens[3];
                    sType = tokens[4];
                    sAC = int.Parse(tokens[5]);
                    miscAC = int.Parse(tokens[6]);
                    magicAC = int.Parse(tokens[7]);
                    strength = int.Parse(tokens[8]);

                    return null;// new ArmorClass(worn, aType, aAC, stealth, sType, sAC, miscAC, magicAC, strength);
                }
                catch { }
                throw new ArgumentException("Can not convert '" + (string)value + "' to type Person");

            }

            return base.ConvertFrom(context, info, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            if (destType == typeof(string) && value is ArmorClass)
            {
                ArmorClass a = (ArmorClass)value;

                return a.ArmorWorn + "," + a.ArmorType + "," + a.ArmorAC + "," + a.ArmorStealth + "," + a.ShieldType + "," + a.ShieldAC + "," + a.MiscAC + "," + a.MagicAC;
            }

            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
