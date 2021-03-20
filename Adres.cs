using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Projekt1
{
    public class Adres : IAssert
    {
        public const string UNDEFINED_STRING = "[Niezdefiniowano]";

        private string m_sKodPocztowy;
        private string m_sMiasto;
        private string m_sUlica;
        private int m_iNrDomu;
        private int m_iNrMieszkania;

        public string KodPocztowy
        {
            get => m_sKodPocztowy;
            set
            {
                string sWzorRegEx = @"^\d{2}-\d{3}";
                if (!Regex.IsMatch(value, sWzorRegEx))
                    throw new Exception("Niepoprawny format pola <KodPocztowy> lub bledne dane");

                m_sKodPocztowy = value;
            }
        }

        public string Miasto
        {
            get => m_sMiasto;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Pole <Miasto> nie moze byc puste!");

                m_sMiasto = value;
            }
        }
        public string Ulica
        {
            get => m_sUlica;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Pole <Ulica> nie moze byc puste!");

                m_sUlica = value;
            }
        }

        public int NrDomu
        {
            get => m_iNrDomu;
            set
            {
                if (value < 0)
                    throw new Exception("Pole <NrDomu> nie moze byc mniejsze od 0!");

                m_iNrDomu = value;
            }
        }

        public int NrMieszkania
        {
            get => m_iNrMieszkania;
            set
            {
                if (value < 0)
                    throw new Exception("Pole <NrMieszkania> nie moze byc mniejsze od 0!");

                m_iNrMieszkania = value;
            }
        }

        public Adres()
        {
            m_sKodPocztowy = UNDEFINED_STRING;
            m_sMiasto = UNDEFINED_STRING;
            m_sUlica = UNDEFINED_STRING;
            m_iNrDomu = -1;
            m_iNrMieszkania = -1;
        }
        public Adres(string KodPocztowy, string Miasto, string Ulica, int NrDomu, int NrMieszkania) : this()
        {
            this.KodPocztowy = KodPocztowy;
            this.Miasto = Miasto;
            this.Ulica = Ulica;
            this.NrDomu = NrDomu;
            this.NrMieszkania = NrMieszkania;
        }
        public Adres(string KodPocztowy, string Miasto, string Ulica, int NrDomu) : this()
        {
            this.KodPocztowy = KodPocztowy;
            this.Miasto = Miasto;
            this.Ulica = Ulica;
            this.NrDomu = NrDomu;
        }

        public override string ToString()
        {
            if (NrMieszkania > 0)
                return $"KodPocztowy:{KodPocztowy} Miasto:{Miasto} Ulica:{Ulica} NrDomu:{NrDomu} NrMieszkania:{NrMieszkania}";
            else
                return $"KodPocztowy:{KodPocztowy} Miasto:{Miasto} Ulica:{Ulica} NrDomu:{NrDomu}";
        }

        public void AssertObject()
        {
            if (KodPocztowy == UNDEFINED_STRING)
                throw new Exception("Nie zainicjalizowano pola <KodPocztowy>!");

            if (Miasto == UNDEFINED_STRING)
                throw new Exception("Nie zainicjalizowano pola <Miasto>!");

            if (Ulica == UNDEFINED_STRING)
                throw new Exception("Nie zainicjalizowano pola <Ulica>!");

            if (NrDomu == -1)
                throw new Exception("Nie zainicjalizowano pola <NrDomu>!");
        }
    }
}
