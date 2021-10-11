using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework3._1_CSharp
{
    public class CDay
    {
        public String giorno { get; set; }
        public int ricoverati_con_sintomi { get; set; }
        public int terapia_intensiva { get; set; }
        public int totale_ospedalizzati { get; set; }
        public int isolamento_domiciliare { get; set; }
        public int totale_positivi { get; set; }
        public int nuovi_positivi { get; set; }
        public int dimessi_guariti { get; set; }
        public int deceduti { get; set; }

        public CDay(String giorno, int ricoveratiConSintomi, int terapiaIntensiva, int totaleOspedalizzati,
            int isolamentoDomiciliare, int totalePositivi, int nuoviPositivi, int dimessiGuariti, int deceduti)
        {
            this.giorno = giorno;
            this.ricoverati_con_sintomi = ricoveratiConSintomi;
            this.terapia_intensiva = terapiaIntensiva;
            this.totale_ospedalizzati = totaleOspedalizzati;
            this.isolamento_domiciliare = isolamentoDomiciliare;
            this.totale_positivi = totalePositivi;
            this.nuovi_positivi = nuoviPositivi;
            this.dimessi_guariti = dimessiGuariti;
            this.deceduti = deceduti;
        }
    }
}
