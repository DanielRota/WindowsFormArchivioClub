using System;

namespace FORMClubRota
{
    public struct Socio
    {
        public string tessera;
        public string nome;
        public string cognome;
        public string luogo;
        public string indirizzo;
        public DateTime nascita;
        public DateTime iscrizione;
        public DateTime ver;
        public decimal quota;
    }

    internal class Class1
    {
        public static bool CancellaID(Socio[] eleS, ref int num, string tesseraCanc)
        {
            int x = 0;

            while (x < num)
            {
                if (String.Compare(eleS[x].tessera, tesseraCanc) == 0)
                {
                    eleS[x] = eleS[num - 1];
                    num = num - 1;
                    return true;
                }
                x = x + 1;
            }
            return false;
        }

        public static void SelectionSortAlfabetico(Socio[] eleS, int num)
        {
            Socio tmp;
            int x = default;
            int y = default;

            while (x < num)
            {
                y = x + 1;
                while (y < num)
                {
                    if (string.Compare(eleS[x].cognome, eleS[y].cognome) > 0)
                    {
                        tmp = eleS[x];
                        eleS[x] = eleS[y];
                        eleS[y] = tmp;
                    }
                    y = y + 1;
                }
                x = x + 1;
            }
        }

        public static decimal MediaQuote(Socio[] eleS, int num)
        {
            int x = 0;
            decimal media = default;

            while (x < num)
            {
                if (eleS[x].quota > -1)
                {
                    media += eleS[x].quota;
                }
                x++;
            }
            media /= x;

            return media;
        }

        public static int Cerca(Socio[] eleS, int num, string dato)
        {
            int x = 0;

            while (x < num)
            {
                if (eleS[x].tessera == dato)
                    return x;   // Trovato
                x = x + 1;
            }
            return -1;          // Non Trovato
        }
    }
}