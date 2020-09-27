using System;
using System.Collections.Generic;
using System.Text;

namespace Infra
{
    public static partial class Funcoes
    {
        #region Data

        /// <summary>
        /// Retorna uma data de início(01/XX/XXXX) ou final do mês(31/XX/XXXX)
        /// </summary>
        /// <param name="inicioFinalMes">Valores 1: Início do Mês, 2: Final do Mês</param>
        /// <returns>Data Desejada</returns>
        public static DateTime RetornaData(int inicioFinalMes, DateTime? dataDigitada = null)
        {
            // Buscando a Data atual do sistema           
            DateTime dataAtual = dataDigitada ?? DateTime.Now;

            // Montando a data do inicio do mes
            DateTime dataRetorno = new DateTime(Convert.ToInt32(dataAtual.Year), Convert.ToInt32(dataAtual.Month), 1);

            // Montando a data do final do mes
            if (inicioFinalMes == 2)
            {
                int ultimaDia = DateTime.DaysInMonth(Convert.ToInt32(dataAtual.Year), Convert.ToInt32(dataAtual.Month));
                dataRetorno = new DateTime(Convert.ToInt32(dataAtual.Year), Convert.ToInt32(dataAtual.Month), ultimaDia);
            }

            return dataRetorno;
        }

        /// <summary>
        /// Monta uma data qualquer
        /// </summary>
        /// <param name="ano">Ano desejado</param>
        /// <param name="mes">Mês desejado</param>
        /// <param name="dia">Dia Desejado</param>
        /// <param name="ultimaDia">Se deseja retornar o último dia do mês</param>
        /// <returns>Data desejada</returns>
        public static DateTime RetornaData(int ano, int mes, int dia, bool ultimaDia = false)
        {
            try
            {
                // Buscando a Data atual do sistema           
                DateTime data = new DateTime(ano, mes, dia);

                // Montando a data do final do mes
                if (ultimaDia)
                {
                    int strUltimaDia = DateTime.DaysInMonth(Convert.ToInt32(data.Year), Convert.ToInt32(data.Month));
                    data = new DateTime(Convert.ToInt32(data.Year), Convert.ToInt32(data.Month), strUltimaDia);
                }

                return data;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public static DateTime? RetornaData(this string data)
        {
            if (string.IsNullOrEmpty(data) == false && ValidaData(data))
            {
                return Convert.ToDateTime(data);                
            }

            return null;
        }

        /// <summary>
        /// Verificando se uma data é valida
        /// </summary>
        /// <param name="data">Data a ser verificada</param>
        /// <returns>verdadeiro ou falso</returns>
        public static bool ValidaData(string data)
        {
            DateTime novaData = DateTime.Now;
            return DateTime.TryParse(data, out novaData);
        }

        public static DateTime DataMinima()
        {
            DateTime _minDate = DateTime.ParseExact("01/01/1753", "d", System.Globalization.CultureInfo.InvariantCulture);
            return _minDate;
        }

        #endregion
    }
}
