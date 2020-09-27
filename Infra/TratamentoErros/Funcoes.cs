using System;
using System.Collections.Generic;
using System.Text;

namespace Infra
{
    public static partial class Funcoes
    {
        #region Tratamento de erros

        /// <summary>
        /// Método que trata a exceção, de acordo com o tipo de erro
        /// </summary>
        /// <param name="excessao">exceção a ser tratada</param>
        /// <returns>exceção tratada</returns>
        public static string TratarExcessao(this Exception excessao)
        {
            string erro = string.Empty;

            if (excessao != null)
            {
                erro = erro = excessao.Message;

                if (excessao.InnerException != null)
                {
                    erro = $"{ erro }[ Expecífico ]:\n{excessao.InnerException.ToString()}";
                }
            }

            return erro;
        }

        #endregion
    }
}
