using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class Mask
    {
        public static string RemoveMaskField(Mask.Type typeMask, string mask)
        {
            switch (typeMask)
            {
                case Type.CNPJ:
                    return mask.Replace(".", "").Replace("/", "").Replace("-", "");
                case Type.CPF:
                    return mask.Replace(".", "").Replace("-", "");
                case Type.Phone:
                    return mask.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                case Type.IE:
                    return mask.Replace(".", "").Replace("-", "");
                case Type.RG:
                    return mask.Replace(".", "").Replace("-", "");
                case Type.CEP:
                    return mask.Replace("-", "");
                default:
                    return null;
            }
        }

        public enum Type
        {
            CNPJ = 1,
            CPF = 2,
            Phone = 3,
            IE = 4,
            RG = 5,
            CEP = 6
        }
    }
}
