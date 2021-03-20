using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt1
{
    //zajęcia 28.02.2021
    [Serializable]
    public class Osoba : IAssert
    {
        public const string UNDEFINED_STRING = "[Niezdefiniowano]";

        private int m_iWiek;
        private string m_sImie;
        private string m_sNazwisko;
        private string m_sPlec;

        private Adres _adres;

        public int Wiek
        {
            get => m_iWiek;
            set
            {
                if (value < 0 || value > 150)
                    throw new Exception("Pole <Wiek> musi zawierac sie w zakresie od 0 do 150");

                m_iWiek = value;
            }
        }

        public string Imie
        {
            get => m_sImie;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Pole <Imie> nie moze byc puste!");

                m_sImie = value;
            }
        }
        public string Nazwisko
        {
            get => m_sNazwisko;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Pole <Nazwisko> nie moze byc puste!");

                m_sNazwisko = value;
            }
        }

        public string Plec
        {
            get => m_sPlec;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Pole <Plec> nie moze byc puste!");
                else if (value.ToUpper() != "M" && value.ToUpper() != "K")
                    throw new Exception("Niepoprawna wartosc");

                m_sPlec = value;
            }
        }

        public Adres Adres
        {
            get => _adres;
            set
            {
                _adres = value;
            }
        }
        public Osoba()
        {
            m_iWiek = -1;
            m_sImie = UNDEFINED_STRING;
            m_sNazwisko = UNDEFINED_STRING;
            m_sPlec = UNDEFINED_STRING;
            _adres = Adres;
        }
        public Osoba(int Wiek, string Imie, string Nazwisko, string Plec, string KodPocztowy, string Miasto, string Ulica, int NrDomu, int NrMieszkania) : this()
        {
            this.Wiek = Wiek;
            this.Imie = Imie;
            this.Nazwisko = Nazwisko;
            this.Plec = Plec;
            this.Adres = new Adres(KodPocztowy, Miasto, Ulica, NrDomu, NrMieszkania);
        }
        public Osoba(int Wiek, string Imie, string Nazwisko, string Plec, string KodPocztowy, string Miasto, string Ulica, int NrDomu) : this()
        {
            this.Wiek = Wiek;
            this.Imie = Imie;
            this.Nazwisko = Nazwisko;
            this.Plec = Plec;
            this.Adres = new Adres(KodPocztowy, Miasto, Ulica, NrDomu);
        }
        public override string ToString()
        {
            return $"Imie:{Imie} Nazwisko:{Nazwisko} Plec:{Plec} Wiek:{Wiek} {Adres}";
        }

        public void AssertObject()
        {
            if (Wiek == -1)
                throw new Exception("Nie zainicjalizowano pola <Wiek>!");

            if (Imie == UNDEFINED_STRING)
                throw new Exception("Nie zainicjalizowano pola <Imie>!");

            if (Nazwisko == UNDEFINED_STRING)
                throw new Exception("Nie zainicjalizowano pola <Nazwisko>!");

            if (Plec == UNDEFINED_STRING)
                throw new Exception("Nie zainicjalizowano pola <Plec>!");
        }
    }
}
