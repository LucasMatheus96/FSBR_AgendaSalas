
using System.Text.Json.Serialization;

namespace FSBR_AgendaSalas.Domain.Shared
{
    public class Resultado
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }

        public static Resultado Ok(string mensagem = "") =>
            new() { Sucesso = true, Mensagem = mensagem };

        public static Resultado Falha(string mensagem) =>
            new() { Sucesso = false, Mensagem = mensagem };
    }

    public class Resultado<T> : Resultado
    {
        public T Dados { get; set; }

        public static Resultado<T> Ok(T dados, string mensagem = "") =>
            new() { Sucesso = true, Dados = dados, Mensagem = mensagem };

        public new static Resultado<T> Falha(string mensagem) =>
            new() { Sucesso = false, Mensagem = mensagem };
    }

    public class ResultadoModal
    {
        public string Erro { get; set; }

        public string Mensagem { get; set; }
    
    }


    public class MensagemResponse
    {   

        public string Mensagem { get; set; }

        [JsonPropertyName("erro")]
        public string Erro { get; set; }

    }
}
