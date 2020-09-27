using Newtonsoft.Json;

namespace Modelos
{
    public class RespostaAutomaticaErroModel
    {
        [JsonProperty("alerta")]
        public bool Alerta { get; set; }

        [JsonProperty("regraNegocio")]
        public bool RegraNegocio { get; set; }

        [JsonProperty("message")]
        public string DescricaoErro { get; set; }
    }
     
    public class RespostaErroModel
    {
        public RespostaErroModel(string mensagem)
        {
            errors = new RespostaAutomaticaErroModel
            {
                DescricaoErro = mensagem
            };
        }

        public RespostaAutomaticaErroModel errors { get; set; }
    }
}
